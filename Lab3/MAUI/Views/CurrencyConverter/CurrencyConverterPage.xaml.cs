using Microsoft.Maui.Controls;
using System.Diagnostics;
using MAUI.Logic;

namespace MAUI
{
    public partial class CurrencyConverterPage : ContentPage
    {
        private List<Currency> _currencies;
        private CurrencyContext _currencyContext;

        public CurrencyConverterPage(List<Currency> currencies, CurrencyContext currencyContext)
        {
            InitializeComponent();
            _currencies = currencies;
            _currencyContext = currencyContext;

            SourceCurrencyPicker.ItemsSource = _currencies;
            TargetCurrencyPicker.ItemsSource = _currencies;

            SourceCurrencyPicker.SelectedIndex = -1;
            TargetCurrencyPicker.SelectedIndex = -1;
        }

        private async void OnConvertClicked(object sender, EventArgs e)
        {
            if (!decimal.TryParse(AmountEntry.Text, out decimal amount))
            {
                ConversionResultLabel.Text = "Wprowadź poprawną kwotę!";
                return;
            }

            if (SourceCurrencyPicker.SelectedItem == null || TargetCurrencyPicker.SelectedItem == null)
            {
                ConversionResultLabel.Text = "Wybierz obie waluty!";
                return;
            }

            var sourceCurrency = (Currency)SourceCurrencyPicker.SelectedItem;
            var targetCurrency = (Currency)TargetCurrencyPicker.SelectedItem;
            var date = DatePicker.Date.ToString("yyyy-MM-dd");
            var Currency = (sourceCurrency.Code != "USD") ? sourceCurrency : targetCurrency;
            var exchangeRate = await CurrencyService.GetExchangeRateAsync(date, Currency.Code, _currencyContext);
            if (exchangeRate == null)
            {
                ConversionResultLabel.Text = "Brak kursu wymiany z API!";
                return;
            }

            decimal result = 0;
            if (sourceCurrency.Code == "USD")
            {
                result = amount * exchangeRate.Rate;
            }
            else if (targetCurrency.Code == "USD")
            {
                result = amount * exchangeRate.ReverseRate;
            }
            ConversionResultLabel.Text = $"Wynik: {result} {targetCurrency.Code}";
        }

        private void OnSourceCurrencyChanged(object sender, EventArgs e)
        {
            if (SourceCurrencyPicker.SelectedItem is Currency sourceCurrency)
            {
                var targetCurrency = TargetCurrencyPicker.SelectedItem as Currency;         
                if (targetCurrency != null && targetCurrency.Code != "USD" && sourceCurrency.Code != "USD")
                {
                    TargetCurrencyPicker.SelectedIndex = -1;
                }
            }
        }

        private void OnTargetCurrencyChanged(object sender, EventArgs e)
        {
            if (TargetCurrencyPicker.SelectedItem is Currency targetCurrency)
            {
                var sourceCurrency = SourceCurrencyPicker.SelectedItem as Currency;     
                if (sourceCurrency != null && sourceCurrency.Code != "USD" && targetCurrency.Code != "USD")
                {
                    SourceCurrencyPicker.SelectedIndex = -1;
                }
            }
        }

    }
}
