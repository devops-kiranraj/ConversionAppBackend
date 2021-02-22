using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionApp.WebAPI.Models.Currency
{
    public class CurrencyRateHistoryResponse
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public string Date { get; set; }
        public decimal Rate { get; set; }
    }    
}
