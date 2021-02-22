using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConversionApp.ForexDataProvider
{
    public class FixerApiClient<TModel>
    {
        private readonly HttpClient _httpClient;

        public FixerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> GetAsync<TResponse>(string action,Dictionary<string, string> queryString = null)
        {
            try
            {
                if (queryString != null && queryString.Any(s => !string.IsNullOrWhiteSpace(s.Value)))
                {
                    action = $"{action}?{string.Join("&", queryString.Where(t => !string.IsNullOrWhiteSpace(t.Value)).Select(x => x.Key + "=" + x.Value))}";
                }

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, action);

                var httpResponse = await _httpClient.SendAsync(requestMessage);

                var content = await httpResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(content);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return default;
        }
    }
}
