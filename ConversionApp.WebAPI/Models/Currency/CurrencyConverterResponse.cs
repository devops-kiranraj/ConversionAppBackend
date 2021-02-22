namespace ConversionApp.WebAPI.Models.Currency
{
    public class CurrencyConverterResponse
    {
        public string SourceCurrency { get; set; }
        public decimal SourceAmount { get; set; }
        public string TargetCurrency { get; set; }
        public decimal TargetAmount { get; set; }
    }
}
