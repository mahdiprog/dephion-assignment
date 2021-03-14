namespace CryptoRate.Domain.Interfaces
{
    public interface ICryptoCurrency
    {
        int Id { get; set; }
        string Name { get; set; }
        string Symbol { get; set; }
        string Slug { get; set; }
    }
}