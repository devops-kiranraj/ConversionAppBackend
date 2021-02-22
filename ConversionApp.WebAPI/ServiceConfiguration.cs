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

namespace ConversionApp.WebAPI
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICurrencyConversionHandler, CurrencyConversionHandler>();
            services.AddTransient<IExchangeRateDataStoreServices, ExchangeRateDataStoreServices>();
            services.AddHttpClient<IFixerDataService, FixerDataService>(client =>
            {
                client.BaseAddress = new Uri(configuration[MessageConstants.FIXER_BASEURL]);
            });
            services.AddDbContext<ConversionAppContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConversionAppContext"),
                options=>options.EnableRetryOnFailure()));          
            return services;
        }
    }
}
