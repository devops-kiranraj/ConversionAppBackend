using ConversionApp.Core.Interfaces.Handlers;
using ConversionApp.Persistance.DataStore.Context;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConversionApp.ForexDataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Currency Exchange Rate Loader Batch Started");

            var startup = new Startup();
            var service = startup.Provider.GetRequiredService<ICurrencyConversionHandler>();

            var context = startup.Provider.GetService<ConversionAppContext>();
            context.Database.EnsureCreated();
            var responseModel1 = service.LoadCurrencyExchangeRate().Result;

            Console.ReadLine();
        }
    }
}
