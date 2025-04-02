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
        _currencies = await _currencyContext.Currencies.OrderBy(c => c.Code).ToListAsync(); ;
    }
    private async void LoadISAsync()
    {
        await GlobalConfig.LoadIDAsync();
    }
     
    private async void OnConvertCurrencyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CurrencyConverterPage(_currencies, _currencyContext));
    }
    private async void OnShowCurrenciesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShowDatabasePage(_currencies, _currencyContext));
    }
    private async void OnShowDataClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DataSelectionPage(_currencies,_currencyContext));
    }

}
