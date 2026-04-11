namespace PricingCalculator.App.Models
{
    public class PricingResult
    {
        public decimal TotalMaterialCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal TotalCost { get; set; }
        public decimal ProfitAmount { get; set; }
        public decimal FinalPrice { get; set; }
    }
}