using ConversionApp.Core.Interfaces.Handlers;
using ConversionApp.WebAPI.Factory;
using ConversionApp.WebAPI.Models.Currency;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConversionApp.WebAPI.Controllers
{
    [Route("currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyConversionHandler _currencyConversionHandler;
        public CurrencyController(ICurrencyConversionHandler currencyConversionHandler)
        {
            _currencyConversionHandler = currencyConversionHandler;
        }
              

        [HttpPost("convert")]
        public async Task<IActionResult> ProcessConvertCurrency([FromBody]CurrencyConverterRequest currencyConverterRequest)
        {
            var domainModel = ModelConversion.Create(currencyConverterRequest);
            var responseModel = await _currencyConversionHandler.ConvertCurrency(domainModel);
            return ModelConversion.Create(responseModel);
        }

        [HttpGet("convertion/history")]
        public IActionResult GetCurrencyConvertionHistory(
            [FromQuery] string curencyCode, 
            [FromQuery] string fromdate, 
            [FromQuery] string todate)
        {
            var request = new CurrencyRateHistoryRequest { SourceCurrency = curencyCode, DateFrom = fromdate, DateTo = todate };
            var domainModel = ModelConversion.Create(request);
            var responseModel = _currencyConversionHandler.GetCurrencyRate(domainModel);
            return ModelConversion.Create(responseModel);
        }
    }
}