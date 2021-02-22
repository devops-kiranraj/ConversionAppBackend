using ConversionApp.Core.DomainModels;
using System.Threading.Tasks;

namespace ConversionApp.Core.Interfaces.Handlers
{
    public interface ICurrencyConversionHandler
    {
        Task<CurrencyConversionDomainModel> ConvertCurrency(CurrencyConversionDomainModel currencyConversionDomainModel);
        Task<CurrencyConversionDomainModel> LoadCurrencyExchangeRate();
        ConversionRateHistoryDomainModel GetCurrencyRate(ConversionRateHistoryDomainModel conversionRateHistoryDomainModel);


    }
}
