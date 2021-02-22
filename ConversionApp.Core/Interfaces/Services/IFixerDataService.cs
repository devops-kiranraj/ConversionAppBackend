using ConversionApp.Core.DomainModels;
using System.Threading.Tasks;

namespace ConversionApp.Core.Interfaces.Services
{
    public interface IFixerDataService
    {
        Task<ConversionRateDomainModel> GetExchangeRate();
        Task<ConversionRateDomainModel> GetExchangeRateHistory(ConversionRateDomainModel conversionRateDomainModel);
    }
}
