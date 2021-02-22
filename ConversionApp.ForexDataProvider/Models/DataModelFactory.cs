using ConversionApp.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConversionApp.ForexDataProvider.Models
{
    public static class DataModelFactory
    {
        public static ConversionRateDomainModel Create(ConversionResponse conversionResponse)
        {
            if (conversionResponse != null && conversionResponse.Rates != null)
            {
                //var rates = conversionResponse.Rates.GetType()
                //        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                //        .ToDictionary(prop => prop.Name, prop => Convert.ToDecimal(prop.GetValue(conversionResponse.Rates, null)));
                if (conversionResponse.Rates.Any())
                {
                    return new ConversionRateDomainModel()
                    {
                        Rates = conversionResponse.Rates,
                        BaseCurrency = conversionResponse.BaseCurrency,
                        Date = conversionResponse.Date,
                        TimeStamp = conversionResponse.Timestamp
                    };
                }
            }
            return default;
        }
    }
}
