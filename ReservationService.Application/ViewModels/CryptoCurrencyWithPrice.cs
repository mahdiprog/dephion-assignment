using System;
using System.Text.Json.Serialization;
using CryptoRate.Domain.Interfaces;

namespace CryptoRate.Application.ViewModels
{
    public  class CryptoCurrencyWithPrice : ICryptoCurrency
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTimeOffset LastUpdated { get; set; }

        [JsonPropertyName("quote")]
        public Quote Quote { get; set; }
    }

    public partial class Quote
    {
        [JsonPropertyName("USD")]
        public Usd Usd { get; set; }
    }

    public partial class Usd
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("volume_24h")]
        public decimal Volume24H { get; set; }

        [JsonPropertyName("percent_change_1h")]
        public decimal PercentChange1H { get; set; }

        [JsonPropertyName("percent_change_24h")]
        public decimal PercentChange24H { get; set; }

        [JsonPropertyName("percent_change_7d")]
        public decimal PercentChange7D { get; set; }

        [JsonPropertyName("market_cap")]
        public decimal MarketCap { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTimeOffset LastUpdated { get; set; }
    }

}
