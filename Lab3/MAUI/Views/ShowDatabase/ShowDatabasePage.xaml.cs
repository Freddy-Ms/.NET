using Microsoft.Maui.Controls;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAUI.Logic;
using Microsoft.EntityFrameworkCore;
using System;

namespace MAUI
{
    public partial class ShowDatabasePage : ContentPage
    {
        private List<Currency> _currencies;
        private List<ExchangeRate> _exchangeRates;
        private CurrencyContext _currencyContext;

        public ShowDatabasePage(List<Currency> currencies, CurrencyContext currencyContext)
        {
            InitializeComponent();
            _currencies = currencies;
            _currencyContext = currencyContext;
            LoadCurrencies();
            LoadExchangeRates();
            LoadCurrencyPicker();
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            await _currencyContext.FetchCurrencies();
            LoadCurrencies();
        }

        private async void OnAddCurrencyClicked(object sender, EventArgs e)
        {
            string code = CurrencyCodeEntry.Text?.Trim().ToUpper() ?? "";
            string name = CurrencyNameEntry.Text?.Trim() ?? "";

            if (code.Length != 3)
            {
                await DisplayAlert("Error", "Currency code must be exactly 3 letters!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Error", "Please provide the full currency name!", "OK");
                return;
            }

            if (_currencyContext.Currencies.Any(c => c.Code == code))
            {
                await DisplayAlert("Error", "This currency already exists in the database!", "OK");
                return;
            }

            var newCurrency = new Currency { Code = code, Name = name };
            _currencyContext.Currencies.Add(newCurrency);
            await _currencyContext.SaveChangesAsync();

            await DisplayAlert("Success", "New currency added!", "OK");

            LoadCurrencies();
        }

        private async void LoadCurrencies()
        {
            _currencies = await _currencyContext.Currencies.OrderBy(c => c.Code).ToListAsync();
            CurrencyListView.ItemsSource = _currencies;

        }

        private async void OnDeleteCurrencyClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var currency = button?.BindingContext as Currency;

            if (currency != null)
            {
                _currencyContext.Currencies.Remove(currency);
                await _currencyContext.SaveChangesAsync();
                LoadCurrencies();
            }
        }

        private async void LoadExchangeRates()
        {
            _exchangeRates = await _currencyContext.ExchangeRates.OrderBy(r => r.TargetCurrency.Code).ToListAsync();
            ExchangeRatesListView.ItemsSource = _exchangeRates;
        }

        private async void OnDeleteExchangeRateClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var exchangeRate = button?.BindingContext as ExchangeRate;

            if (exchangeRate != null)
            {
                _currencyContext.ExchangeRates.Remove(exchangeRate);
                await _currencyContext.SaveChangesAsync();
                LoadExchangeRates();
            }
        }

        private async void OnFilterExchangeRatesClicked(object sender, EventArgs e)
        {
            DateTime startDate = StartDatePicker.Date;
            DateTime endDate = EndDatePicker.Date;
            string selectedCurrency = CurrencyPicker.SelectedItem as string;

            var query = _currencyContext.ExchangeRates.AsQueryable();

            if (selectedCurrency != null)
            {
                query = query.Where(rate => rate.TargetCurrency.Code == selectedCurrency);
            }

            query = query.Where(rate => rate.Date >= startDate && rate.Date <= endDate);

            _exchangeRates = await query.OrderBy(rate => rate.Date).ToListAsync();
            ExchangeRatesListView.ItemsSource = _exchangeRates;
        }
        private async void DeleteFilters(object sender, EventArgs e)
        {
            StartDatePicker.Date = DateTime.Today;
            EndDatePicker.Date = DateTime.Today;
            CurrencyPicker.SelectedItem = null;
            _exchangeRates = await _currencyContext.ExchangeRates.OrderBy(rate => rate.Date).ToListAsync();
            ExchangeRatesListView.ItemsSource = _exchangeRates;
        }
        private async void AddToDatabase(object sender, EventArgs e)
        {
            string code = CurrencyPicker2.SelectedItem as string;
            string rate = CurrencyRate.Text?.Trim() ?? "";
            DateTime date = DatePicker.Date;
            if (string.IsNullOrEmpty(code))
            {
                await DisplayAlert("Error", "Please select a currency!", "OK");
                return;
            }
            if (string.IsNullOrEmpty(rate))
            {
                await DisplayAlert("Error", "Please provide the exchange rate!", "OK");
                return;
            }
            if (date > DateTime.Today)
            {
                await DisplayAlert("Error", "Date cannot be in the future!", "OK");
                return;
            }
            if (!decimal.TryParse(rate, out decimal rateValue))
            {
                await DisplayAlert("Error", "Invalid exchange rate!", "OK");
                return;
            }
            var currency = _currencyContext.Currencies.FirstOrDefault(c => c.Code == code);
            if (currency == null)
            {
                await DisplayAlert("Error", "Currency not found!", "OK");
                return;
            }
            var exchangeRate = new ExchangeRate
            {
                TargetCurrency = currency,
                Date = date,
                Rate = rateValue
            };
            _currencyContext.ExchangeRates.Add(exchangeRate);
            await _currencyContext.SaveChangesAsync();
            await DisplayAlert("Success", "Exchange rate added!", "OK");
            LoadExchangeRates();
        }
        private async void LoadCurrencyPicker()
        {
            var currencyCodes = await _currencyContext.Currencies.Select(c => c.Code).ToListAsync();
            CurrencyPicker.ItemsSource = currencyCodes;
            CurrencyPicker2.ItemsSource = currencyCodes;
        }
    }
}
