using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAUI.Logic;
using Microsoft.EntityFrameworkCore;

namespace MAUI
{
    public partial class DataSelectionPage : ContentPage
    {
        private CurrencyContext _currencyContext;
        private List<Currency> _currencies;

        public DataSelectionPage(List<Currency> currencies,CurrencyContext currencyContext)
        {
            InitializeComponent();
            _currencyContext = currencyContext;
            _currencies = currencies;

            CurrencyPicker.ItemsSource = _currencies;
        }
        private async void OnLoadDataClicked(object sender, EventArgs e)
        {
            var startDate = StartDatePicker.Date;
            var endDate = EndDatePicker.Date;
            var selectedCurrency = (Currency)CurrencyPicker.SelectedItem;

            if (selectedCurrency == null)
            {
                await DisplayAlert("Error", "Please select a currency.", "OK");
                return;
            }
            if (startDate > endDate)
            {
                await DisplayAlert("Error", "Start date cannot be later than end date.", "OK");
                return;
            }
            if (endDate > DateTime.Today)
            {
                await DisplayAlert("Error", "End date cannot be in the future.", "OK");
                return;
            }
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var exchangeRate = await CurrencyService.GetExchangeRateAsync(date.ToString("yyyy-MM-dd"), selectedCurrency.Code, _currencyContext);
                if (exchangeRate == null)
                {
                    await DisplayAlert("Error", $"No exchange rate for {selectedCurrency.Code} on {date}.", "OK");
                    return;
                }
                exchangeRates.Add(exchangeRate);
            }
            ResultsListView.ItemsSource = exchangeRates;
        }
    }
}
