using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoRate.Application.ViewModels
{
    public class CoinMarketCapResponse<T>
        where T : class
    {
        public IEnumerable<T> Data { get; set; }

        public CoinMarketCapResponseStatus Status { get; set; }

        public static async Task<CoinMarketCapResponse<T>> FromJson(Stream jsonStream)
        {
            var element = await JsonSerializer.DeserializeAsync<JsonElement>(jsonStream);
            var response = new CoinMarketCapResponse<T>();
            var dataElements = new List<T>();
            try
            {
                response.Status = JsonSerializer.Deserialize<CoinMarketCapResponseStatus>(element.GetProperty("status")
                    .GetRawText());
            }
            catch (KeyNotFoundException e)
            {

            }

            try
            {
                var data = element.GetProperty("data");
                // iterate over json properties
                var objectEnumerator = data.EnumerateObject();
                while (objectEnumerator.MoveNext())
                {
                    var current = objectEnumerator.Current;
                    // we need child object of each property that is inside the Value
                    dataElements.Add(JsonSerializer.Deserialize<T>(current.Value.GetRawText()));

                }

                response.Data = dataElements;
            }
            catch (KeyNotFoundException e)
            {

            }

            return response;
        }
    }

    public class CoinMarketCapResponseStatus
    {
        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("elapsed")]
        public int Elapsed { get; set; }

        [JsonPropertyName("credit_count")]
        public int CreditCount { get; set; }
    }
}
