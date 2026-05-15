using System.Text.Json.Serialization;

namespace PricingCalculator.App.Models;


public class ExchangeRateResponse
{
    [JsonPropertyName("USDBRL")]
    public CurrencyData? UsdBrl { get; set; }

    [JsonPropertyName("EURBRL")]
    public CurrencyData? EurBrl { get; set; }
}


public class CurrencyData
{
    [JsonPropertyName("bid")]
    public string Bid { get; set; } = string.Empty;
}
