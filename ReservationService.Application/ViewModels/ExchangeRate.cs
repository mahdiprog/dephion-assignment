using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CryptoRate.Application.ViewModels
{
    public class ExchangeRate
    {
        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; }

        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
