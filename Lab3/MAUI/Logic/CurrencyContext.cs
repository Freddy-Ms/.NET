using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MAUI.Logic
{
    public class CurrencyContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        public CurrencyContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppContext.BaseDirectory, "currency.db");
           // Debug.WriteLine($"Database path: {dbPath}");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExchangeRate>()
                .HasKey(e => new { e.TargetCurrencyId, e.Date });

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(e => e.TargetCurrency)
                .WithMany(c => c.ExchangeRates)
                .HasForeignKey(e => e.TargetCurrencyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public async Task InitializeDatabaseAsync()
        {
            Database.EnsureCreated();
            if (!Currencies.Any())
            {
                await CurrencyService.InitializeCurrenciesAsync(this);
            }    
        }
        public async Task FetchCurrencies()
        {
            await CurrencyService.InitializeCurrenciesIfNotExistAsync(this);
        }
    }
}
