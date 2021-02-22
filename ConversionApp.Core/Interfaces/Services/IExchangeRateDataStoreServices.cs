using ConversionApp.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConversionApp.Core.Interfaces.Services
{
    public interface IExchangeRateDataStoreServices
    {
        Task<ConversionRateDomainModel> StoreExchangeRate(ConversionRateDomainModel conversionRateDomainModel);
        ConversionRateHistoryDomainModel GetExchangeRate(ConversionRateHistoryDomainModel conversionRateHistoryDomainModel);
        bool GetExchangeRateStatus(ConversionRateHistoryDomainModel conversionRateHistoryDomainModel);
    }
}
