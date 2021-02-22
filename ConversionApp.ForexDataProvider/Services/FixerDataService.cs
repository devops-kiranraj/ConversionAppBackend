using ConversionApp.Core.Constants;
using ConversionApp.Core.DomainModels;
using ConversionApp.Core.Interfaces.Services;
using ConversionApp.ForexDataProvider.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConversionApp.ForexDataProvider.Services
{
    public class FixerDataService : FixerApiClient<FixerDataService>, IFixerDataService
    {
        private readonly Dictionary<string, string> defaultQueryString;
        public FixerDataService(HttpClient httpClient): base (httpClient: httpClient) 
        {
            defaultQueryString = new Dictionary<string, string>()
            {
                { "access_key","f2b1ab739afc3dcfc89bd05000b8cd34" }
            };
        }

        public async Task<ConversionRateDomainModel> GetExchangeRate()
        {
            var exchangeRateResponse = await GetAsync<ConversionResponse>(MessageConstants.FIXER_EXCHANGERATE_ACTION, defaultQueryString);
            return DataModelFactory.Create(exchangeRateResponse);
        }

        public async Task<ConversionRateDomainModel> GetExchangeRateHistory(ConversionRateDomainModel conversionRateDomainModel)
        {
            var exchangeRateResponse = await GetAsync<ConversionResponse>(conversionRateDomainModel.Date, defaultQueryString);
            return DataModelFactory.Create(exchangeRateResponse);
        }
    }
}
