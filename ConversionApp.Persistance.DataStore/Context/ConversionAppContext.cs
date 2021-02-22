using ConversionApp.Persistance.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConversionApp.Persistance.DataStore.Context
{
    public class ConversionAppContext: DbContext
    {
        public ConversionAppContext(DbContextOptions<ConversionAppContext> options) : base(options)
        {
        }
        public DbSet<CurrencyExchangeRateEntity> CurrencyExchangeRate { get; set; }
        public DbSet<CurrencyEntity> Currency { get; set; }
        public DbSet<CurrencyForexRateEntity> CurrencyForexRateEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyExchangeRateEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CurrencyEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CurrencyForexRateEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
