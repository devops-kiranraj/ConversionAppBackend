using System;
using System.Collections.Generic;
using System.Text;

namespace ConversionApp.Core.DomainModels
{
    public class ConversionRateHistoryDomainModel : ErrorDomainModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string SourceCurrency { get; set; }
        public List<CurrencyConversionDomainModel> ConversionRate { get; set; }
    }
}
