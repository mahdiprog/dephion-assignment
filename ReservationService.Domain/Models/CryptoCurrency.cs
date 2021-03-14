using CryptoRate.Domain.Interfaces;

namespace CryptoRate.Domain.Models
{
    public class CryptoCurrency:ICryptoCurrency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
    }
}