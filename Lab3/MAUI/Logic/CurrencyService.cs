using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MAUI.Logic
{
    public class CurrencyService
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task InitializeCurrenciesAsync(CurrencyContext context)
        {
            string call = "https://openexchangerates.org/api/currencies.json";
            string response = await client.GetStringAsync(call);
            var currDict = JsonSerializer.Deserialize<Dictionary<string, string>>(response);

            if (currDict != null)
            {
                foreach (var entry in currDict)
                {
                    var currency = new Currency
                    {
                        Code = entry.Key,
                        Name = entry.Value
                    };
                    context.Currencies.Add(currency);
                }
                await context.SaveChangesAsync();
            }
        }
        public static async Task InitializeCurrenciesIfNotExistAsync(CurrencyContext context)
        {
            string call = "https://openexchangerates.org/api/currencies.json";
            string response = await client.GetStringAsync(call);
            var currDict = JsonSerializer.Deserialize<Dictionary<string, string>>(response);

            if (currDict != null)
            {
                foreach (var entry in currDict)
                {
                    var existingCurrency = await context.Currencies
                        .FirstOrDefaultAsync(c => c.Code == entry.Key);

                    if (existingCurrency == null)
                    {
                        var currency = new Currency
                        {
                            Code = entry.Key,
                            Name = entry.Value
                        };

                        context.Currencies.Add(currency);
                    }
                }

                await context.SaveChangesAsync();
            }
        }
        public static async Task<ExchangeRate> GetExchangeRateFromApiAsync(string date, string CurrencyCode, CurrencyContext context)
        {
            string url = $"https://openexchangerates.org/api/historical/{date}.json?app_id={GlobalConfig.ID}&symbols={CurrencyCode}";
            Debug.WriteLine($"Pobieranie kursu z {url}");
            try
            {
                string response = await client.GetStringAsync(url);

                var info = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(response);

                decimal rate = info["rates"].GetProperty(CurrencyCode).GetDecimal();

                var targetCurrency = await context.Currencies
                    .FirstOrDefaultAsync(c => c.Code == CurrencyCode);
                if (targetCurrency == null)
                {
                    return null;
                }

                var exchangeRate = new ExchangeRate
                {
                    TargetCurrencyId = targetCurrency.Id,
                    Date = DateTime.Parse(date),
                    Rate = rate,
                    TargetCurrency = targetCurrency
                };
                return exchangeRate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania kursu: {ex.Message}");
            }

            return null;
        }

        public static async Task<ExchangeRate> GetExchangeRateAsync(string date, string CurrencyCode, CurrencyContext context)
        {
            DateTime parsedDate;
            if (!DateTime.TryParse(date, out parsedDate))
            {
                return null;
            }

            var exchangeRate = await context.ExchangeRates
                .Where(e => e.TargetCurrency.Code == CurrencyCode && e.Date.Date == parsedDate.Date)
                .FirstOrDefaultAsync();

            if (exchangeRate == null)
            {
                exchangeRate = await GetExchangeRateFromApiAsync(date, CurrencyCode, context);
                
                if (exchangeRate != null)
                {
                    context.ExchangeRates.Add(exchangeRate);
                    await context.SaveChangesAsync();
                }
            }
            return exchangeRate;
        }

        

    }
}
