using ConversionApp.Core.Constants;
using ConversionApp.Core.DomainModels;
using ConversionApp.WebAPI.Models.Currency;
using ConversionApp.WebAPI.Models.Error;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ConversionApp.WebAPI.Factory
{
    public static class ModelConversion
    {
        public static CurrencyConversionDomainModel Create(CurrencyConverterRequest model)
        {
            return new CurrencyConversionDomainModel()
            {
                SourceAmount = model.Amount,
                SourceCurrency = model.SourceCurrency,
                TargetCurrency = model.TargetCurrency
            };
        }
        public static ConversionRateHistoryDomainModel Create(CurrencyRateHistoryRequest model)
        {
            return new ConversionRateHistoryDomainModel()
            {
                SourceCurrency = model.SourceCurrency,
                DateFrom = DateTime.Parse(model.DateFrom),
                DateTo = !string.IsNullOrEmpty(model.DateTo) ? DateTime.Parse(model.DateTo) : DateTime.Today
            };
        }
        public static IActionResult Create(CurrencyConversionDomainModel domainModel)
        {
            if (domainModel.IsError)
            {
                var errorResponse = new ErrorResponse()
                {
                    StatusCode = domainModel.StatusCode,
                    StatusMessage = domainModel.StatusMessage
                };
                return CreateResponse(errorResponse);
            }
            var validResponse = new CurrencyConverterResponse()
            {
                SourceAmount = domainModel.SourceAmount,
                TargetAmount = domainModel.TargetAmount,
                SourceCurrency = domainModel.SourceCurrency,
                TargetCurrency = domainModel.TargetCurrency
            };
            return CreateResponse(validResponse);
        }
        public static IActionResult CreateResponse<T>(T response)
        {
            return new ObjectResult(response);
        }
        public static IActionResult Create(ConversionRateHistoryDomainModel model)
        {
            if (model.IsError)
            {
                var errorResponse = new ErrorResponse()
                {
                    StatusCode = model.StatusCode,
                    StatusMessage = model.StatusMessage
                };
                return CreateResponse(errorResponse);
            }
            var response = model.ConversionRate?.Select(x => CreateHistory(x)).ToList();
            return CreateResponse(response);           
        }
        public static CurrencyRateHistoryResponse CreateHistory(CurrencyConversionDomainModel model)
        {
            return new CurrencyRateHistoryResponse()
            {
                Rate = model.TargetAmount,
                SourceCurrency = model.SourceCurrency,
                TargetCurrency = model.TargetCurrency,
                Date =model.Date
            };
        }


    }
}
