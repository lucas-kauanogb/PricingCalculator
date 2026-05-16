using PricingCalculator.App.Models;
using PricingCalculator.App.Services;

var pricingService = new PricingService();
var exchangeRateService = new ExchangeRateService();

Console.WriteLine("=======================================");
Console.WriteLine(" CALCULADORA DE PRECIFICAÇÃO ARTESANAL ");
Console.WriteLine("=======================================\n");

while (true)
{
    try
    {
        Console.WriteLine("--- NOVO CÁLCULO ---");
        Console.Write("Nome do Produto (ou 'sair' para encerrar): ");
        var name = Console.ReadLine();

        if (name?.ToLower() == "sair")
        {
            Console.WriteLine("Encerrando a calculadora. Bons negócios!");
            break;
        }

        decimal materialCost = ReadDecimal("Custo total dos materiais (R$): ");
        decimal hoursWorked = ReadDecimal("Horas trabalhadas na produção (h): ");
        decimal hourlyRate = ReadDecimal("Valor da sua hora de trabalho (R$): ");
        decimal profitMargin = ReadDecimal("Margem de lucro desejada (%): ");

        var product = new Product
        {
            Name = name ?? "Produto sem nome",
            MaterialCost = materialCost,
            HoursWorked = hoursWorked,
            HourlyRate = hourlyRate,
            ProfitMarginPercentage = profitMargin
        };

        var result = pricingService.CalculatePrice(product);


        decimal usdRate = 0;
        decimal eurRate = 0;

        try
        {
            Console.WriteLine("\n⏳ Buscando cotações de moedas atualizadas na internet...");
            var rates = await exchangeRateService.GetCurrentRatesAsync();
            usdRate = rates.UsdRate;
            eurRate = rates.EurRate;
        }
        catch (Exception)
        {

        }

        Console.WriteLine("\n==============================");
        Console.WriteLine("     RECIBO DE CUSTOS         ");
        Console.WriteLine("==============================");
        Console.WriteLine($"Produto: {product.Name}");
        Console.WriteLine($"Custo de Materiais:   R$ {result.TotalMaterialCost:F2}");
        Console.WriteLine($"Custo de Mão de Obra: R$ {result.LaborCost:F2}");
        Console.WriteLine($"Custo de Produção:    R$ {result.TotalCost:F2}");
        Console.WriteLine($"Valor do Lucro:       R$ {result.ProfitAmount:F2}");
        Console.WriteLine("------------------------------");
        Console.WriteLine($"PREÇO DE VENDA (BRL): R$ {result.FinalPrice:F2}");
        Console.WriteLine("------------------------------");

        if (usdRate > 0 && eurRate > 0)
        {
            Console.WriteLine("🌍 MERCADO INTERNACIONAL:");
            Console.WriteLine($"   Preço em Dólar:    $ {(result.FinalPrice / usdRate):F2} (Cotação: R$ {usdRate:F2})");
            Console.WriteLine($"   Preço em Euro:     € {(result.FinalPrice / eurRate):F2} (Cotação: R$ {eurRate:F2})");
        }
        else
        {
            Console.WriteLine("⚠️ Não foi possível conectar à internet para obter as taxas de câmbio (Modo Offline).");
        }
        Console.WriteLine("==============================\n");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\n[ERRO DE VALIDAÇÃO] {ex.Message}\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[ERRO INESPERADO] {ex.Message}\n");
    }
}

static decimal ReadDecimal(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();

        if (decimal.TryParse(input, out decimal value))
        {
            return value;
        }
        Console.WriteLine("[ERRO] Por favor, digite um número válido.");
    }
}
