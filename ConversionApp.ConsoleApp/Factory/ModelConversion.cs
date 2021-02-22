using ConversionApp.ConsoleApp.Models.Currency;
using ConversionApp.Core.DomainModels;
using System;

namespace ConversionApp.ConsoleApp.Factory
{
    public static class ModelConversion
    {
        public static CurrencyConversionDomainModel Create(CurrencyConverterRequest model)
        {
            return new CurrencyConversionDomainModel()
            {
                SourceAmount = Convert.ToDecimal(model.Amount),
                SourceCurrency = model.SourceCurrency,
                TargetCurrency = model.TargetCurrency,
                Date = model.Date
            };
        }
    }
}
