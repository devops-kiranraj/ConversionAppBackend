using ConversionApp.Core.DomainModels;
using ConversionApp.Core.Interfaces.Services;
using ConversionApp.Persistance.DataStore.Context;
using ConversionApp.Persistance.DataStore.Entities;
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
            //StoreCurrencies(DataModelFactory.CreateCurencyModel(conversionRateDomainModel));
            var entityList = DataModelFactory.Create(conversionRateDomainModel);
            var entityList1 = DataModelFactory.Create(conversionRateDomainModel, GetCurrencies());            
            _context.CurrencyForexRateEntity.AddRange(entityList1);
            await _context.SaveChangesAsync();
            _context.CurrencyExchangeRate.AddRange(entityList);
            await _context.SaveChangesAsync();
            return conversionRateDomainModel;
        }

        public IEnumerable<CurrencyEntity> GetCurrencies()
        {
            var entity = _context.Currency.AsEnumerable();
            return entity;
        }

        public IEnumerable<CurrencyEntity> StoreCurrencies(IEnumerable<CurrencyDomainModel> currencies)
        {            
            foreach(var crncy in currencies)
            {
                if(_context.Currency.FirstOrDefault(x => x.Code.Equals(crncy.Code)) != null)
                {
                    _context.Currency.Add(new CurrencyEntity { Code = crncy.Code });
                    _context.SaveChangesAsync();
                }
            }            
            return _context.Currency.AsEnumerable();
        }

        public ConversionRateHistoryDomainModel GetExchangeRate(ConversionRateHistoryDomainModel conversionRateHistoryDomainModel)
        {            
            var entity = _context.CurrencyExchangeRate.Where(x=> x.Date>= conversionRateHistoryDomainModel.DateFrom 
                                                            && x.Date <= conversionRateHistoryDomainModel.DateTo
                                                            && x.Curerncy == conversionRateHistoryDomainModel.SourceCurrency).AsEnumerable();
            conversionRateHistoryDomainModel.ConversionRate = DataModelFactory.Create(entity);
            return conversionRateHistoryDomainModel;
        }

        public bool GetExchangeRateStatus(ConversionRateHistoryDomainModel conversionRateHistoryDomainModel)
        {
            var status = _context.CurrencyExchangeRate.Any(x => x.Date == conversionRateHistoryDomainModel.DateFrom);
            return status;
        }

    }
}
