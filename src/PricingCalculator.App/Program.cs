using PricingCalculator.App.Models;
using PricingCalculator.App.Services;

var pricingService = new PricingService();

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

        // Coleta os dados de forma segura (impedindo letras em campos numéricos)
        decimal materialCost = ReadDecimal("Custo total dos materiais (R$): ");
        decimal hoursWorked = ReadDecimal("Horas trabalhadas na produção (h): ");
        decimal hourlyRate = ReadDecimal("Valor da sua hora de trabalho (R$): ");
        decimal profitMargin = ReadDecimal("Margem de lucro desejada (%): ");

        // Monta o objeto Produto
        var product = new Product
        {
            Name = name ?? "Produto sem nome",
            MaterialCost = materialCost,
            HoursWorked = hoursWorked,
            HourlyRate = hourlyRate,
            ProfitMarginPercentage = profitMargin
        };

        // Chama a Regra de Negócio que nós já testamos!
        var result = pricingService.CalculatePrice(product);

        // Imprime o resultado formatado
        Console.WriteLine("\n==============================");
        Console.WriteLine("     RECIBO DE CUSTOS         ");
        Console.WriteLine("==============================");
        Console.WriteLine($"Produto: {product.Name}");
        Console.WriteLine($"Custo de Materiais:   R$ {result.TotalMaterialCost:F2}");
        Console.WriteLine($"Custo de Mão de Obra: R$ {result.LaborCost:F2}");
        Console.WriteLine($"Custo de Produção:    R$ {result.TotalCost:F2}");
        Console.WriteLine($"Valor do Lucro:       R$ {result.ProfitAmount:F2}");
        Console.WriteLine("------------------------------");
        Console.WriteLine($"PREÇO DE VENDA:       R$ {result.FinalPrice:F2}");
        Console.WriteLine("==============================\n");
    }
    catch (ArgumentException ex)
    {
        // Captura aquele erro de valores negativos que fizemos no Teste!
        Console.WriteLine($"\n[ERRO DE VALIDAÇÃO] {ex.Message}\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[ERRO INESPERADO] {ex.Message}\n");
    }
}

// Função auxiliar para garantir que o usuário digite um número válido
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
