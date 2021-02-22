using System;
using System.Collections.Generic;
using System.Text;

namespace ConversionApp.Core.DomainModels
{
    public class ConversionRateDomainModel
    {
        public string BaseCurrency { get; set; }
        public string Date { get; set; }
        public int TimeStamp { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
