using PricingCalculator.App.Services;

namespace PricingCalculator.Tests;

public class ExchangeRateServiceTests
{
    [Fact]
    public async Task GetCurrentRatesAsync_AoChamarAPIPublica_DeveRetornarCotacoesValidas()
    {

        var service = new ExchangeRateService();


        var (usdRate, eurRate) = await service.GetCurrentRatesAsync();

        Assert.True(usdRate > 0, "A cotação do Dólar (USD) obtida da API deve ser maior que zero.");
        Assert.True(eurRate > 0, "A cotação do Euro (EUR) obtida da API deve ser maior que zero.");
    }
}
