Klasy opisujące zawartości tabel w bazie danych:
```
public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ICollection<ExchangeRate> ExchangeRates { get; set; } = new List<ExchangeRate>();
    }
```
Id -> klucz główny \
Code -> skrót waluty np. PLN \
Name -> pełna nazwa waluty np. złoty polski
```
public class ExchangeRate
    {
        [Required]
        public int TargetCurrencyId { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime Date { get; set; }

        public decimal Rate { get; set; }

        [NotMapped]
        public decimal ReverseRate => 1 / Rate;

        public Currency TargetCurrency { get; set; } = null!;
    }
```
TargertCurrencyId -> Docelowa waluta dla kursu \
Date -> data kursu \
Rate -> rata kursu

Klasa opisująca baze danych:
```
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
}

```
Wywołanie klasy powoduje wywołanie konstruktora, który się upewnia, że baza danych jest utworzona. \
Funkcja OnConfiguring używa ścieżki dla pliku oraz bazy danych, która jest lokalnie w pliku (SQLite). \
Funkcja OnModelCreating kreuje nam w tabeli dwa klucze główne. Umożliwia nam to wiele wpisów dla tej samej waluty w różnych datach.\
Jednocześnie tworzona jest relacja jeden do wielu, gdzie jedna waluta może mieć wiele wpisów w kursach walut, ale jeden wpis w kursie walutch odnosi się tylko to jednej waluty.

Funkcje do pobierania kursów oraz walut z API:
```
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
```
Funkcja przyjmuje jako parametry datę, walutę oraz baze danych. Pobiera odpowiednie dane z API i je deserializuje następnie dodaje do tabeli ExchangeRates nowy wpis.
```
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
```
Funkcja wysyła zapytanie do API, następnie odpowiedź jest deserializowana, a każda waluta jest dodawana do bazy danych.

W MAUI są zrobione walidację dla wszystkich UserInput oraz komunikaty w przypadku niepowodzeń zapytań API albo przypadków, kiedy API nie ma informacji o danym kursie w danym dniu.
