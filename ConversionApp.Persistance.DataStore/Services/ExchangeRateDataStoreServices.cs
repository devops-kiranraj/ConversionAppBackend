using ConversionApp.Core.DomainModels;
using ConversionApp.Core.Interfaces.Services;
using ConversionApp.Persistance.DataStore.Context;
using ConversionApp.Persistance.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversionApp.Persistance.DataStore.Services
{
    public class ExchangeRateDataStoreServices : IExchangeRateDataStoreServices
    {
        private readonly ConversionAppContext _context;

        public ExchangeRateDataStoreServices(ConversionAppContext context)
        {
            _context = context;
        }

        public async Task<ConversionRateDomainModel> StoreExchangeRate(ConversionRateDomainModel conversionRateDomainModel)
        {            
            var entityList = DataModelFactory.Create(conversionRateDomainModel, GetCurrencies());            
            _context.CurrencyForexRateEntity.AddRange(entityList);
            await _context.SaveChangesAsync();
            return conversionRateDomainModel;
        }

        public IEnumerable<CurrencyEntity> GetCurrencies()
        {
            var entity = _context.Currency.AsEnumerable();
            return entity;
        }

        public ConversionRateHistoryDomainModel GetExchangeRate(ConversionRateHistoryDomainModel conversionRateHistoryDomainModel)
        {            
            var entity = _context.CurrencyForexRateEntity.Where(x=> x.Date>= conversionRateHistoryDomainModel.DateFrom 
                                                            && x.Date <= conversionRateHistoryDomainModel.DateTo
                                                            && x.Currency.Code == conversionRateHistoryDomainModel.SourceCurrency)
                                                        .Include(x=>x.Currency).AsEnumerable();
            conversionRateHistoryDomainModel.ConversionRate = DataModelFactory.Create(entity);
            return conversionRateHistoryDomainModel;
        }

        public bool GetExchangeRateStatus(ConversionRateHistoryDomainModel conversionRateHistoryDomainModel)
        {
            var status = _context.CurrencyForexRateEntity.Any(x => x.Date == conversionRateHistoryDomainModel.DateFrom);
            return status;
        }

    }
}
