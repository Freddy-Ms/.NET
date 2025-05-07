# Edycja gotowej strony z pogodami

Aby możliwe było filtrowanie, dodałem dodatkową zmienną, która zawsze przechowuje oryginalnie wygenerowane pogody.
```C#
private WeatherForecast[]? forecasts;
private WeatherForecast[]? originalForecasts;
```
Następnie dodałem funkcję, polegająca na odfiltrowaniu pogody, zresetowania filtrowania oraz filtrowania po nazwie.
```C#
private void Filter_Out_Under_15()
    {
        forecasts = originalForecasts?.Where(f => f.TemperatureC >= 15).ToArray();
        warm_days = forecasts?.Length ?? 0;
    }

    private void ResetForecasts()
    {
        forecasts = originalForecasts;
        warm_days = forecasts?.Count(f => f.TemperatureC > 15) ?? 0;
    }

    private void Input(ChangeEventArgs arg)
    {
        searchTerm = arg.Value?.ToString() ?? string.Empty;

        forecasts = originalForecasts?
            .Where(f => f.Summary.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToArray();

        warm_days = forecasts?.Count(f => f.TemperatureC > 15) ?? 0;
    }
```

W klasie HTML dodałem napis, który pokazuje ile jest ciepłych dni oraz dwa przyciski, które służą do filtrowania. Również swoje miejsce znalazło pole tekstowe, które filtruje po nazwie:
```C#
<div class="mb-3">
        <input class="form-control" @oninput="Input" placeholder="Filtruj po nazwie (Summary)" />
    </div>
[...]
 <p>Number of warm days: @warm_days </p>
    <div class="mb-3">
        <button class="btn btn-primary me-2" @onclick="Filter_Out_Under_15">Pokaż tylko dni ≥ 15°C</button>
        <button class="btn btn-secondary" @onclick="ResetForecasts">Resetuj</button>
    </div>

```

# Strona z filmami
Na samym początku stworzyłem klasę, która posiada następujące pola:

```C#
public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
        public float? Rate { get; set; }
        public string? ImageUrl { get; set; }

    }
```
Dzięki tak stworzonej klasie, możliwe jest automatyczne wygenerowanie CRUD'a z gotowymi stronami. Następnie zedytowałem te strony zgodnie z wymaganiami.
Pierwszą zmianą było, że podczas dodawania oceny nie podmienia się ona na nową, tylko jest liczona średnia arytmetyczna nowej oraz starej oceny.
W widoku istnieje możliwość sortowania po każdej kolumnie (nazwa filmu, data oraz ocena). Takie rzeczy jak szczegóły filmu nie zostają wyświetlane na liście.
```C#
<table class="table table-striped mt-3">
    <thead>
        <tr>
            <th>
                <button class="btn btn-link" @onclick="SortByTitle">
                    Title
                </button>
            </th>
            <th>
                <button class="btn btn-link" @onclick="SortByReleaseDate">
                    Release Date
                </button>
            </th>
            <th>
                <button class="btn btn-link" @onclick="SortByRating">
                    Rating
                </button>
            </th>
            <th>Actions</th>
        </tr>
    </thead>

[...]
private void SortByTitle()
    {
        isReleaseDateAscending = false;
        isRatingDescending = false;

        if (isTitleAscending)
        {
            movies = movies.OrderByDescending(m => m.Title).ToList();
        }
        else
        {
            movies = movies.OrderBy(m => m.Title).ToList();
        }

        isTitleAscending = !isTitleAscending;

    }
```
Reszta sortowań napisana jest w identyczny sposób. Pojedyńcze kliknięcie powoduję sortowanie rosnące, kolejne kliknięcie na tą samą kolumne powoduje sortowanie w odwrotną stronę.
Dla niezalogowanego użytownika zabrano możliwość dostępu do niektórych stron oraz w pasku nawigacyjnym te podstrony się nie wyświetlały.
Zostało to zrobione za pomocą następujących funkcji:
```C#
@attribute [Authorize]

<Authorized> </Authorized>
<NotAuthorized> </NotAuthorized>
```
Dzięki temu osoba niezalogowana nie ma dostępu do poszczególych stron (nawet jeżeli wpisze w przeglądarke odpowiedni endpoint).

Na samym końcu dodałem do klasy Movie nowe pole, które przechowuje stringa w postaci URL zdjęcia filmu, który wyświetla się w szczegółach filmu.
# Autoryzacja logowania za pomocą Google
```C#

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme, GoogleOptions =>
 {
     GoogleOptions.ClientId = builder.Configuration["ClientId"];
     GoogleOptions.ClientSecret = builder.Configuration["ClientSecret"];
 })
    .AddIdentityCookies();

```
Wystarczy zainstalować odpowiednie pakiety Nuget oraz dodać ten fragment kodu do Program.cs. "ClientId" oraz "ClientSecret" zostały dodane jako dotnet users secret, ponieważ są to wrażliwe dane.
