using MAUI.Logic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace MAUI;
public partial class MainPage : ContentPage
{
    private CurrencyContext _currencyContext;
    private List<Currency> _currencies;
    public MainPage()
    {
        InitializeComponent();
        _currencyContext = new CurrencyContext();
        Initialize();
        LoadISAsync();
    }
    private async void Initialize()
    {
        await _currencyContext.InitializeDatabaseAsync();
        Debug.WriteLine("Database initialized successfully.");
        _currencies = await _currencyContext.Currencies.OrderBy(c => c.Code).ToListAsync(); ;
        SourceCurrencyPicker.ItemsSource = _currencies;
        TargetCurrencyPicker.ItemsSource = _currencies;
        TargetCurrencyPicker.SelectedIndex = 0;
    }
    private async void LoadISAsync()
    {
        await GlobalConfig.LoadIDAsync();
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
            await DisplayAlert("Błąd", "Kod waluty musi składać się z dokładnie 3 liter!", "OK");
            return;
        }

        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlert("Błąd", "Podaj pełną nazwę waluty!", "OK");
            return;
        }

        if (_currencyContext.Currencies.Any(c => c.Code == code))
        {
            await DisplayAlert("Błąd", "Ta waluta już istnieje w bazie!", "OK");
            return;
        }

        var newCurrency = new Currency { Code = code, Name = name };
        _currencyContext.Currencies.Add(newCurrency);
        await _currencyContext.SaveChangesAsync();

        await DisplayAlert("Sukces", "Dodano nową walutę!", "OK");

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
    private async void OnConvertClicked(object sender, EventArgs e)
    {
        if (!decimal.TryParse(AmountEntry.Text, out decimal amount))
        {
            ConversionResultLabel.Text = "Wprowadź poprawną kwotę!";
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
        Debug.WriteLine(result);
        ConversionResultLabel.Text = $"Wynik: {result} {targetCurrency.Code}";
    }

    private void OnSourceCurrencyChanged(object sender, EventArgs e)
    {
        if (SourceCurrencyPicker.SelectedItem.ToString() == "USD")
        {
            TargetCurrencyPicker.ItemsSource = _currencies;
        }
        else
        {
            TargetCurrencyPicker.ItemsSource = new List<Currency>{ _currencies.First(c => c.Code == "USD") };
        }
    }

    private void OnTargetCurrencyChanged(object sender, EventArgs e)
    {
        if (TargetCurrencyPicker.SelectedItem is Currency targetCurrency && targetCurrency.Code == "USD")
        {
            SourceCurrencyPicker.ItemsSource = _currencies;
        }
        else
        {
            SourceCurrencyPicker.ItemsSource = new List<Currency> { _currencies.First(c => c.Code == "USD") };
        }
    }
    private async void OnLoadDataClicked(object sender, EventArgs e)
    {
        var startDate = StartDatePicker.Date;
        var endDate = EndDatePicker.Date;

        var exchangeRates = await _currencyContext.ExchangeRates
            .Where(r => r.Date >= startDate && r.Date <= endDate)
            .OrderBy(r => r.Date)
            .ToListAsync();

        ResultsListView.ItemsSource = exchangeRates;
    }
}
