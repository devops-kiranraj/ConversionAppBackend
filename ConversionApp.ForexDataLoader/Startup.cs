using ConversionApp.BusinessLogic.Handlers;
using ConversionApp.Core.Constants;
using ConversionApp.Core.Interfaces.Handlers;
using ConversionApp.Core.Interfaces.Services;
using ConversionApp.ForexDataProvider.Services;
using ConversionApp.Persistance.DataStore.Context;
using ConversionApp.Persistance.DataStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConversionApp.ForexDataLoader
{
    public class Startup
    {
        public readonly IServiceProvider Provider;

        public Startup()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ICurrencyConversionHandler, CurrencyConversionHandler>();
            services.AddDbContext<ConversionAppContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConversionAppContext"),
                options => options.EnableRetryOnFailure()));
            services.AddSingleton<IExchangeRateDataStoreServices, ExchangeRateDataStoreServices>();
            services.AddHttpClient<IFixerDataService, FixerDataService>(client =>
            {
                client.BaseAddress = new Uri(configuration[MessageConstants.FIXER_BASEURL]);
            });
            Provider = services.BuildServiceProvider();
        }
    }
}
