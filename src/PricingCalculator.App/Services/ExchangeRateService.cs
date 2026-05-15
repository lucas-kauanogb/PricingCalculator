using System.Text.Json;
using PricingCalculator.App.Models;

namespace PricingCalculator.App.Services;

public class ExchangeRateService
{
    private readonly HttpClient _httpClient;

    public ExchangeRateService()
    {
        _httpClient = new HttpClient();


        _httpClient.DefaultRequestHeaders.Add("User-Agent", "PricingCalculatorApp/1.0");
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<(decimal UsdRate, decimal EurRate)> GetCurrentRatesAsync()
    {
        try
        {

            var response = await _httpClient.GetStringAsync("https://economia.awesomeapi.com.br/last/USD-BRL,EUR-BRL");


            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<ExchangeRateResponse>(response, options);

            var culture = new System.Globalization.CultureInfo("en-US");
            decimal usdRate = Convert.ToDecimal(data?.UsdBrl?.Bid, culture);
            decimal eurRate = Convert.ToDecimal(data?.EurBrl?.Bid, culture);

            return (usdRate, eurRate);
        }
        catch (Exception ex)
        {

            throw new Exception($"A API rejeitou a conexão ou falhou: {ex.Message}", ex);
        }
    }
}
