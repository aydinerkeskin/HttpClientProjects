using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClientWithExtensions
{
    public class OrderServiceConfig
    {
        public string BaseAddress { get; set; }
        public int HandledEventsAllowedBeforeBreaking { get; set; } = 20; // 20 tekil hata.
        public int DurationOfBreak { get; set; } = 30; // 30 sn bekleme süresi.
        public int HttpClientTimeout { get; set; } = 10; // 10 sn işlem sınırı.
        public int HttpHandlerLifeTimeSecond { get; set; } = 120; // 120sn Http Pool Recycle süresi.
    }
}
