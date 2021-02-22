using ConversionApp.BusinessLogic.Handlers;
using ConversionApp.ConsoleApp.Factory;
using ConversionApp.ConsoleApp.Models.Currency;
using ConversionApp.Core.DomainModels;
using ConversionApp.Core.Interfaces.Handlers;
using ConversionApp.Core.Interfaces.Services;
using ConversionApp.ForexDataProvider.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ConversionApp.ConsoleApp
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("---------------Currency Conversion App---------------");
            var startup = new Startup();
            var service = startup.Provider.GetRequiredService<ICurrencyConversionHandler>();

            var responseModel1 = service.LoadCurrencyExchangeRate().Result;

            CurrencyConverterRequest currencyConverterRequest = GetInputs();
            if (currencyConverterRequest != null)
            {
                var domainModel = ModelConversion.Create(currencyConverterRequest);
                var responseModel = service.ConvertCurrency(domainModel).Result;
                ShowOuput(responseModel);
            }
            Console.ReadLine();
        }

        private static CurrencyConverterRequest GetInputs()
        {
            Console.WriteLine("Enter Source Currency Code:");            
            string sourceCurrency = Console.ReadLine().ToUpper();

            Console.WriteLine("Enter Target Currency Code:");
            string targetCurrency = Console.ReadLine().ToUpper();

            Console.WriteLine("Enter Conversion Amount:");
            string amount = Console.ReadLine();

            Console.WriteLine("Enter Date in format(yyyy-mm-dd)");
            string date = Console.ReadLine();

            var input = new CurrencyConverterRequest { SourceCurrency = sourceCurrency, TargetCurrency = targetCurrency, Amount = amount , Date =date };

            var context = new ValidationContext(input, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(input, context, validationResults, true);

            if (isValid)
                return input;
            else
                validationResults.ForEach(x => Console.WriteLine(x.ErrorMessage));
            return null;
        }

        private static void ShowOuput(CurrencyConversionDomainModel currencyConversionDomainModel)
        {
            if(currencyConversionDomainModel.IsError)
            {
                Console.WriteLine($"Error Code: {currencyConversionDomainModel.StatusCode}");
                Console.WriteLine($"Error Message: {currencyConversionDomainModel.StatusMessage}");
            }
            else
            {
                Console.WriteLine($"Source Currency Code: {currencyConversionDomainModel.SourceCurrency}");
                Console.WriteLine($"Target Currency Code: {currencyConversionDomainModel.TargetCurrency}");
                Console.WriteLine($"Input Amount        : {currencyConversionDomainModel.SourceAmount}");
                Console.WriteLine($"Output Amount       : {currencyConversionDomainModel.TargetAmount}");
            }
        }

    }
}
