namespace PricingCalculator.App.Models
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public decimal MaterialCost { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal ProfitMarginPercentage { get; set; }
    }
}