using ConversionApp.Core.Constants;
using ConversionApp.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConversionApp.Persistance.DataStore.Entities
{
    public static class DataModelFactory
    {
        public static List<CurrencyForexRateEntity> Create(ConversionRateDomainModel conversionRateDomainModel, IEnumerable<CurrencyEntity> currencies)
        {
            List<CurrencyForexRateEntity> currencyRates = new List<CurrencyForexRateEntity>();
            if (conversionRateDomainModel != null && conversionRateDomainModel.Rates != null && conversionRateDomainModel.Rates.Any())
            {
                foreach(var rate in conversionRateDomainModel.Rates)
                {
                    var currency = currencies.FirstOrDefault(x => x.Code == rate.Key);
                    currencyRates.Add(Create(currency, rate.Value, conversionRateDomainModel.Date,rate.Key));
                }
                return currencyRates;
            }
            return default;
        }

        public static IEnumerable<CurrencyDomainModel> CreateCurencyModel(ConversionRateDomainModel conversionRateDomainModel)
        {            
            if (conversionRateDomainModel != null && conversionRateDomainModel.Rates != null && conversionRateDomainModel.Rates.Any())
            {
               return conversionRateDomainModel.Rates.ToList().Select(x=> new CurrencyDomainModel{ Code =x.Key});                
            }
            return default;
        }

        public static List<CurrencyConversionDomainModel> Create(IEnumerable<CurrencyForexRateEntity> conversionRateEntity)
        {
            if (conversionRateEntity != null && conversionRateEntity.Any())
            {
                return conversionRateEntity.Select(Create).ToList();
            }
            return default;
        }

        private static CurrencyForexRateEntity Create(CurrencyEntity currency, decimal rate, string date, string currencyCode)
        {
            if (DateTime.TryParse(date, out DateTime dateTime))
            {
                if (currency == null)
                {
                    return new CurrencyForexRateEntity
                    {
                        Date = dateTime,
                        Currency = new CurrencyEntity { Code = currencyCode },
                        ConversionRate = rate
                    };
                }
                else
                {
                    return new CurrencyForexRateEntity
                    {
                        Date = dateTime,
                        CurrencyId = currency.Id,
                        ConversionRate = rate
                    };
                }
            }
            return null;
        }

        private static CurrencyConversionDomainModel Create(CurrencyForexRateEntity currencyExchangeRateEntity)
        {
            if (currencyExchangeRateEntity != null)
            {
                return new CurrencyConversionDomainModel
                {
                    Date = currencyExchangeRateEntity.Date.ToString(MessageConstants.DATEFORMAT),
                    TargetCurrency = currencyExchangeRateEntity.Currency.Code,
                    TargetAmount = currencyExchangeRateEntity.ConversionRate
                };
            }
            return default;            
        }
    }

}
