using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace HttpClientWithExtensions
{
    public static class Extension
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services, string key = "OrderService")
        {
            var configuration = services.BuildServiceProvider();
            var logger = configuration.GetService<ILogger>();
            var config = configuration.GetService<IConfiguration>().GetSection(key).Get<OrderServiceConfig>();

            if (config == null)
                throw new Exception($"OrderService in ayarları yüklenemedi, appSettings içerisinde [{key}] bloğu bulunamadı!");

            var policy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<HttpRequestException>()
                .Or<OperationCanceledException>()
                .Or<TimeoutException>()
                .CircuitBreakerAsync(
                    config.HandledEventsAllowedBeforeBreaking,
                    TimeSpan.FromSeconds(config.DurationOfBreak),
                    (_, duration) => logger.LogError($"OrderService circuit tripped. Circuit is open and request won't be allowed through for duration={duration}"),
                    () => logger.LogWarning("OrderService circuit closed. Requests are now allowed through."),
                    () => logger.LogWarning("OrderService circuit is now half-opened and will test the service with the next request")
                );

            var serviceExtension = services.AddHttpClient<IOrderApiClient, OrderApiClient>(c =>
            {
                c.BaseAddress = new Uri(config.BaseAddress);
                c.Timeout = TimeSpan.FromSeconds(config.HttpClientTimeout);
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            })
            .SetHandlerLifetime(TimeSpan.FromSeconds(config.HttpHandlerLifeTimeSecond))
            .AddPolicyHandler(policy);

            return services;
        }
    }
}
