using ConversionApp.ForexDataProvider.Models;
using ConversionApp.ForexDataProvider.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConversionApp.ForexDataProvider.Tests
{
    public class FixerDataServicesTests
    {        
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetExchangeRateTest_SuccessRequest()
        {
            var fakeHttpClient = CreateFakeHttpClientResponse(ConversionResponse(), HttpStatusCode.OK);
            var fixerDataService = new FixerDataService(fakeHttpClient);
            var response = await fixerDataService.GetExchangeRate();
            Assert.IsNotNull(response);
            Assert.AreEqual(1613693466, response.TimeStamp);
            Assert.AreEqual("2021-02-19", response.Date);
            Assert.IsNotNull(response.Rates);
            Assert.AreEqual(3, response.Rates.Count);
        }

        #region Private
        private class FakeHttpMessageHandler : DelegatingHandler
        {
            private readonly HttpResponseMessage _fakeResponse;
            
            public FakeHttpMessageHandler(HttpResponseMessage responseMessage)
            {
                _fakeResponse = responseMessage;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_fakeResponse);
            }
        }

        private HttpClient CreateFakeHttpClientResponse<T>(T data, HttpStatusCode statusCode)
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = statusCode,
                Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
            });

            return new HttpClient(fakeHttpMessageHandler)
            {
                BaseAddress = new Uri("https://testurl")
            };
        }

        private ConversionResponse ConversionResponse()
        {
            return new ConversionResponse()
            {
                BaseCurrency ="EUR",
                Date = "2021-02-19",
                Success = true,
                Timestamp = 1613693466,
                Rates = new System.Collections.Generic.Dictionary<string, decimal>()
                {
                     { "EUR",1m },
                     { "NOK",10.5642m },
                     { "GBP",0.865653m }
                }
            };
        }
        #endregion
    }
}
