using System;
using System.Collections.Generic;
using System.Text;

namespace ConversionApp.Core.DomainModels
{
    public class CurrencyDomainModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
