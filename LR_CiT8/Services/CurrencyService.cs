using System.Text.Json;



namespace LR_CiT8.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;

        public CurrencyService() 
        {
            _httpClient = new HttpClient();
        }

        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency, decimal amount)
        {
            string url = $"https://open.er-api.com/v6/latest/{fromCurrency}";

            var responce = await _httpClient.GetAsync(url);
            responce.EnsureSuccessStatusCode();

            string json = await responce.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(json);

            decimal rate = document
                .RootElement
                .GetProperty("rates")
                .GetProperty(toCurrency)
                .GetDecimal();
            
            return rate * amount;
        }
    }
}
