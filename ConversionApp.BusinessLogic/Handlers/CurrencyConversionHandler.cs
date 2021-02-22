using ConversionApp.Core.Constants;
using ConversionApp.Core.DomainModels;
using ConversionApp.Core.Interfaces.Handlers;
using ConversionApp.Core.Interfaces.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionApp.BusinessLogic.Handlers
{
    public class CurrencyConversionHandler: ICurrencyConversionHandler
    {
        private readonly IFixerDataService _fixerDataService;
        private readonly IExchangeRateDataStoreServices _dataStoreServices;
        public CurrencyConversionHandler(IFixerDataService fixerDataService, IExchangeRateDataStoreServices dataStoreServices)
        {
            _fixerDataService = fixerDataService;
            _dataStoreServices = dataStoreServices;
        }

        public async Task<CurrencyConversionDomainModel> ConvertCurrency(CurrencyConversionDomainModel currencyConversionDomainModel)
        {
            ConversionRateDomainModel conversionRateDomainModel;
            if (string.IsNullOrWhiteSpace(currencyConversionDomainModel.Date))
                conversionRateDomainModel = await _fixerDataService.GetExchangeRate();
            else
                conversionRateDomainModel = await _fixerDataService.GetExchangeRateHistory(new ConversionRateDomainModel { Date = currencyConversionDomainModel.Date});
            return ConvertCurrency(currencyConversionDomainModel, conversionRateDomainModel);
        }

        public async Task<CurrencyConversionDomainModel> LoadCurrencyExchangeRate()
        {
            #region Commented
            //var end = DateTime.Now;
            //var start = DateTime.Now.AddDays(-10);
            //var dates = Enumerable.Range(0, 1 + end.Subtract(start).Days)
            //    .Select(offset => start.AddDays(offset))
            //    .ToList();
            //ConversionRateDomainModel conversionRateDomainModel;

            //foreach (var date in dates)
            //{
            //    conversionRateDomainModel = await _fixerDataService.GetExchangeRateHistory(new ConversionRateDomainModel { Date = date.ToString("yyyy-MM-dd") });
            //    await _dataStoreServices.StoreExchangeRate(conversionRateDomainModel);
            //}
            #endregion

            var conversionRateDomainModel = await _fixerDataService.GetExchangeRate();
            var status = _dataStoreServices.GetExchangeRateStatus(new ConversionRateHistoryDomainModel { DateFrom = Convert.ToDateTime(conversionRateDomainModel.Date) });
            if(!status)
            {
                await _dataStoreServices.StoreExchangeRate(conversionRateDomainModel);
            }
            return null;
        }

        public ConversionRateHistoryDomainModel GetCurrencyRate(ConversionRateHistoryDomainModel conversionRateHistoryDomainModel)
        {
            return _dataStoreServices.GetExchangeRate(conversionRateHistoryDomainModel);
        }

        private CurrencyConversionDomainModel ConvertCurrency(CurrencyConversionDomainModel currencyConversionDomainModel, ConversionRateDomainModel conversionRateDomainModel)
        {            
            if (currencyConversionDomainModel != null && conversionRateDomainModel != null &&
                conversionRateDomainModel.Rates.TryGetValue(currencyConversionDomainModel.SourceCurrency, out decimal sourceCurrencyEuro)
                && conversionRateDomainModel.Rates.TryGetValue(currencyConversionDomainModel.TargetCurrency, out decimal destinationCurrencyEuro))
            {
                var convertedValue = (currencyConversionDomainModel.SourceAmount / sourceCurrencyEuro) * destinationCurrencyEuro;
                return new CurrencyConversionDomainModel()
                {
                    SourceAmount = currencyConversionDomainModel.SourceAmount,
                    TargetAmount = convertedValue,
                    SourceCurrency = currencyConversionDomainModel.SourceCurrency,
                    TargetCurrency = currencyConversionDomainModel.TargetCurrency
                };
            }
            else
            {
                return new CurrencyConversionDomainModel()
                {
                    IsError = true,
                    StatusCode = MessageConstants.STATUSCODE_CURRENCY_NOTFOUND,
                    StatusMessage = MessageConstants.VALIDATION_MSG_CURRENCY_NOTFOUND,
                };
            }
        }
    }
}
