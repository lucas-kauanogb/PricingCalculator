using PricingCalculator.App.Models;
using PricingCalculator.App.Services;

namespace PricingCalculator.Tests;

public class PricingServiceTests
{
    private readonly PricingService _pricingService;

    public PricingServiceTests()
    {
        // O Setup do teste: instanciamos o serviço que será testado
        _pricingService = new PricingService();
    }

    [Fact]
    public void CalculatePrice_CaminhoFeliz_DeveRetornarValoresCorretos()
    {
        // Arrange (Preparação)
        var product = new Product
        {
            Name = "Bolo de Cenoura",
            MaterialCost = 10.00m,
            HoursWorked = 1.0m,
            HourlyRate = 20.00m,
            ProfitMarginPercentage = 20.0m // 20% de lucro
        };

        // Act (Ação)
        var result = _pricingService.CalculatePrice(product);

        // Assert (Verificação)
        // Custo total: 10 (material) + 20 (mão de obra) = 30
        // Lucro: 20% de 30 = 6
        // Preço final: 30 + 6 = 36
        Assert.Equal(20.00m, result.LaborCost);
        Assert.Equal(30.00m, result.TotalCost);
        Assert.Equal(6.00m, result.ProfitAmount);
        Assert.Equal(36.00m, result.FinalPrice);
    }

    [Fact]
    public void CalculatePrice_ValoresNegativos_DeveLancarExcecao()
    {
        // Arrange
        var invalidProduct = new Product
        {
            Name = "Produto Inválido",
            MaterialCost = -5.00m, // Custo negativo (Inválido)
            HoursWorked = 2.0m,
            HourlyRate = 15.00m,
            ProfitMarginPercentage = 10.0m
        };

        // Act & Assert
        // Verifica se a função realmente "quebra" e lança o erro de ArgumentException
        var exception = Assert.Throws<ArgumentException>(() => _pricingService.CalculatePrice(invalidProduct));
        Assert.Equal("Os valores de custo, horas e lucro não podem ser negativos.", exception.Message);
    }

    [Fact]
    public void CalculatePrice_SemCustoDeMaterial_ApenasServico_DeveCalcularCorretamente()
    {
        // Arrange (Caso limite: Apenas prestação de serviço, sem material)
        var serviceProduct = new Product
        {
            Name = "Consultoria de Confeitaria",
            MaterialCost = 0.00m,
            HoursWorked = 2.0m,
            HourlyRate = 50.00m,
            ProfitMarginPercentage = 10.0m // 10%
        };

        // Act
        var result = _pricingService.CalculatePrice(serviceProduct);

        // Assert
        // Custo Total: 0 + 100 = 100
        // Lucro: 10% de 100 = 10
        // Preço Final: 110
        Assert.Equal(100.00m, result.LaborCost);
        Assert.Equal(100.00m, result.TotalCost);
        Assert.Equal(10.00m, result.ProfitAmount);
        Assert.Equal(110.00m, result.FinalPrice);
    }
}
