namespace ConversionApp.Core.DomainModels
{
    public class CurrencyConversionDomainModel : ErrorDomainModel
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal SourceAmount { get; set; }
        public decimal TargetAmount { get; set; }
        public string Date { get; set; }
    }
}
