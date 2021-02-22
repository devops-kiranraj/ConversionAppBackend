using ConversionApp.ForexDataProvider.Models;
using NUnit.Framework;

namespace ConversionApp.ForexDataProvider.Tests
{
    public class DataModelFactoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DomainModelCreateTest_ValidRequest()
        {
            var response = DataModelFactory.Create(ConversionResponseRequest());
            Assert.IsNotNull(response);
            Assert.AreEqual(1613693466, response.TimeStamp);
            Assert.AreEqual("2021-02-19", response.Date);
            Assert.IsNotNull(response.Rates);
            Assert.AreEqual(3, response.Rates.Count);
        }

        [Test]
        public void DomainModelCreateTest_EmptyRateRequest()
        {
            var response = DataModelFactory.Create(ConversionResponseRequest_EmptyRates());
            Assert.IsNull(response);
        }

        [Test]
        public void DomainModelCreateTest_NullRequest()
        {
            var response = DataModelFactory.Create(null);
            Assert.IsNull(response);
        }

        private ConversionResponse ConversionResponseRequest()
        {
            return new ConversionResponse
            {
                BaseCurrency = "EUR",
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

        private ConversionResponse ConversionResponseRequest_EmptyRates()
        {
            return new ConversionResponse
            {
                BaseCurrency = "EUR",
                Date = "2021-02-19",
                Success = true,
                Timestamp = 1613693466
            };
        }
    }
}