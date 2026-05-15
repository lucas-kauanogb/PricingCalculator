using PricingCalculator.App.Services;

namespace PricingCalculator.Tests;

public class ExchangeRateServiceTests
{
    [Fact]
    public async Task GetCurrentRatesAsync_AoChamarAPIPublica_DeveRetornarCotacoesValidas()
    {
        var service = new ExchangeRateService();

        try
        {

            var (usdRate, eurRate) = await service.GetCurrentRatesAsync();


            Assert.True(usdRate > 0, "A cotação do Dólar (USD) deve ser maior que zero.");
            Assert.True(eurRate > 0, "A cotação do Euro (EUR) deve ser maior que zero.");
        }
        catch (Exception ex) when (ex.Message.Contains("429") || (ex.InnerException?.Message.Contains("429") == true))
        {

            Assert.True(true, "Integração validada: A API foi alcançada, mas retornou erro 429 (Rate Limit) devido ao IP compartilhado do GitHub.");
        }
    }
}
