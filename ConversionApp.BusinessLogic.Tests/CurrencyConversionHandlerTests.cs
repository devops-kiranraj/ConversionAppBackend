using ConversionApp.BusinessLogic.Handlers;
using ConversionApp.Core.Constants;
using ConversionApp.Core.DomainModels;
using ConversionApp.Core.Interfaces.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ConversionApp.BusinessLogic.Tests
{
    public class CurrencyConversionHandlerTests
    {
        private CurrencyConversionHandler _currencyConversionHandler;
        private Mock<IFixerDataService> _mockFixerDataService;
        private Mock<IExchangeRateDataStoreServices> _mockExchangeRateDataStoreService;
        [SetUp]
        public void Setup()
        {
            _mockFixerDataService = new Mock<IFixerDataService>();
            _mockExchangeRateDataStoreService = new Mock<IExchangeRateDataStoreServices>();


        }

        [Test]
        public async Task ConvertCurrencyTest_Success()
        {
            _currencyConversionHandler = new CurrencyConversionHandler(_mockFixerDataService.Object, _mockExchangeRateDataStoreService.Object);
            _mockFixerDataService.Setup(x => x.GetExchangeRate()).Returns(Task.FromResult(CreateConversionRateDomainModelResponse()));
            var response = await _currencyConversionHandler.ConvertCurrency(CreateCurrencyConversion());
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsError);
            Assert.AreEqual(8.456065439062325084159855959m, response.TargetAmount);
        }

        [Test]
        public async Task ConvertCurrencyHistoryTest_Success()
        {
            _currencyConversionHandler = new CurrencyConversionHandler(_mockFixerDataService.Object, _mockExchangeRateDataStoreService.Object);
            _mockFixerDataService.Setup(x => x.GetExchangeRateHistory(It.IsAny<ConversionRateDomainModel>())).Returns(Task.FromResult(CreateConversionRateDomainModelHistoryResponse()));
            var response = await _currencyConversionHandler.ConvertCurrency(CreateCurrencyConversionHistoryRequest());
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsError);
            Assert.AreEqual(8.456065439062325084159855959m, response.TargetAmount);
        }

        [Test]
        public async Task ConvertCurrencyTest_Error()
        {
            _currencyConversionHandler = new CurrencyConversionHandler(_mockFixerDataService.Object, _mockExchangeRateDataStoreService.Object);
            _mockFixerDataService.Setup(x => x.GetExchangeRate()).Returns(Task.FromResult(CreateConversionRateDomainModelResponse()));
            var response = await _currencyConversionHandler.ConvertCurrency(CreateCurrencyConversionErrorRequest());
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsError);
            Assert.AreEqual(MessageConstants.STATUSCODE_CURRENCY_NOTFOUND, response.StatusCode);
            Assert.AreEqual(MessageConstants.VALIDATION_MSG_CURRENCY_NOTFOUND, response.StatusMessage);
        }

        [Test]
        public async Task ConvertCurrencyHistoryTest_Error()
        {
            _currencyConversionHandler = new CurrencyConversionHandler(_mockFixerDataService.Object, _mockExchangeRateDataStoreService.Object);
            _mockFixerDataService.Setup(x => x.GetExchangeRateHistory(It.IsAny<ConversionRateDomainModel>())).Returns(Task.FromResult(CreateConversionRateDomainModelResponse()));
            var response = await _currencyConversionHandler.ConvertCurrency(CreateCurrencyConversionHistoryErrorRequest());
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsError);
            Assert.AreEqual(MessageConstants.STATUSCODE_CURRENCY_NOTFOUND, response.StatusCode);
            Assert.AreEqual(MessageConstants.VALIDATION_MSG_CURRENCY_NOTFOUND, response.StatusMessage);
        }

        #region Private
        private CurrencyConversionDomainModel CreateCurrencyConversion()
        {
            return new CurrencyConversionDomainModel()
            {
                SourceAmount = 100m,
                SourceCurrency ="NOK",
                TargetCurrency = "GBP"
            };
        }

        private CurrencyConversionDomainModel CreateCurrencyConversionErrorRequest()
        {
            return new CurrencyConversionDomainModel()
            {
                SourceAmount = 100m,
                SourceCurrency = "AED",
                TargetCurrency = "GBP"
            };
        }

        private CurrencyConversionDomainModel CreateCurrencyConversionHistoryRequest()
        {
            return new CurrencyConversionDomainModel()
            {
                SourceAmount = 100m,
                SourceCurrency = "AED",
                TargetCurrency = "GBP",
                Date = "2021-02-18"
            };
        }

        private CurrencyConversionDomainModel CreateCurrencyConversionHistoryErrorRequest()
        {
            return new CurrencyConversionDomainModel()
            {
                SourceAmount = 100m,
                SourceCurrency = "AED",
                TargetCurrency = "GBP",
                Date = "2021-02-18"
            };
        }

        private ConversionRateDomainModel CreateConversionRateDomainModelHistoryResponse()
        {
            return new ConversionRateDomainModel()
            {
                BaseCurrency = "EUR",
                Date = "2021-02-19",
                TimeStamp = 1613693466,
                Rates = new System.Collections.Generic.Dictionary<string, decimal>()
                {
                     { "EUR",1m },
                     { "AED",10.237066m },
                     { "GBP",0.865653m }
                }
            };
        }

        private ConversionRateDomainModel CreateConversionRateDomainModelResponse()
        {
            return new ConversionRateDomainModel()
            {
                BaseCurrency = "EUR",
                Date = "2021-02-19",
                TimeStamp = 1613693466,
                Rates = new System.Collections.Generic.Dictionary<string, decimal>()
                {
                     { "EUR",1m },
                     { "NOK",10.237066m },
                     { "GBP",0.865653m }
                }
            };
        }
        #endregion
    }
}