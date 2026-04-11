using PricingCalculator.App.Models;

namespace PricingCalculator.App.Services;

public class PricingService
{
    public PricingResult CalculatePrice(Product product)
    {
        // Validação de segurança: Não faz sentido ter valores negativos
        if (product.MaterialCost < 0 || product.HoursWorked < 0 ||
            product.HourlyRate < 0 || product.ProfitMarginPercentage < 0)
        {
            throw new ArgumentException("Os valores de custo, horas e lucro não podem ser negativos.");
        }

        // 1. Calcula o custo da mão de obra (Horas x Valor da Hora)
        decimal laborCost = product.HoursWorked * product.HourlyRate;

        // 2. Calcula o custo total de produção
        decimal totalCost = product.MaterialCost + laborCost;

        // 3. Calcula o valor financeiro do lucro
        decimal profitAmount = totalCost * (product.ProfitMarginPercentage / 100);

        // 4. Calcula o preço final de venda
        decimal finalPrice = totalCost + profitAmount;

        // Retorna o relatório completo
        return new PricingResult
        {
            TotalMaterialCost = product.MaterialCost,
            LaborCost = laborCost,
            TotalCost = totalCost,
            ProfitAmount = profitAmount,
            FinalPrice = finalPrice
        };
    }
}
