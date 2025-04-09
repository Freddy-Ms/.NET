# 1. Klasy stworzone dla macierzy
## a) Reprezentacja macierzy
```csharp
class Matrix
{
  public int Rows { get; }
  public int Cols { get; }
  public double[,] Data { get; }
}

```
Konstruktor tej klasy tworzy macierz o zadanym rozmiarze i automatycznie wypełnia ją losowymi wartościami z przedziału od 1 do 10. Klasa zawiera również funkcję umożliwiającą wyświetlenie zawartości macierzy w konsoli.
## b) Równoległe mnożenie macierzy przy użyciu biblioteki Parallel
```csharp
public static Matrix MultiplyWithParallel(Matrix a, Matrix b, int threadCount)
{
  if (a.Cols != b.Rows)
      throw new InvalidOperationException("Incompatible matrix sizes.");

  Matrix result = new Matrix(a.Rows, b.Cols);
  int totalCells = a.Rows * b.Cols;

  Parallel.For(0, totalCells, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, index =>
  {
    int row = index / b.Cols;
    int col = index % b.Cols;
    double sum = 0;
    for (int k = 0; k < a.Cols; k++)
        sum += a.Data[row, k] * b.Data[k, col];
    result.Data[row, col] = sum;
  });

return result;
}
```
Najpierw obliczamy liczbę wszystkich komórek w macierzy wynikowej. Następnie za pomocą `Parallel.For` równolegle wykonujemy obliczenia dla każdej z nich. Możemy również ustawić maksymalną liczbę wątków, jakich użyje `Parallel`. Biblioteka ta sama zarządza tworzeniem i synchronizacją wątków, co upraszcza kod i zmniejsza ryzyko błędów.

## c) Równoległe mnożenie macierzy przy użyciu biblioteki Thread
```csharp
public static Matrix MultiplyWithThreads(Matrix a, Matrix b, int threadCount)
{
  if (a.Cols != b.Rows)
    throw new InvalidOperationException("Incompatible matrix sizes.");

  Matrix result = new Matrix(a.Rows, b.Cols);
  int totalCells = a.Rows * b.Cols;
  int cellsPerThread = (int)Math.Ceiling((double)totalCells / threadCount); 

  Thread[] threads = new Thread[threadCount];

  void CalculateCell(int startIndex, int endIndex)
  {
    for (int index = startIndex; index < endIndex; index++)
    {
      int row = index / b.Cols;
      int col = index % b.Cols;
      double sum = 0;

      for (int k = 0; k < a.Cols; k++)
        sum += a.Data[row, k] * b.Data[k, col];
      result.Data[row, col] = sum;
    }
  }

  for (int i = 0; i < threadCount; i++)
  {
    int startIndex = i * cellsPerThread;
    int endIndex = Math.Min(startIndex + cellsPerThread, totalCells);
    threads[i] = new Thread(() => CalculateCell(startIndex, endIndex));
    threads[i].Start();
  }

  foreach (var thread in threads)
  {
     thread.Join();
  }

  return result;
}
```
W przypadku użycia klasy `Thread`, musimy samodzielnie podzielić pracę między wątki i dokładnie zdefiniować, które elementy macierzy ma przeliczyć każdy z nich. Daje nam to większą kontrolę nad wykonywanym kodem, ale wymaga więcej pracy i zwiększa ryzyko błędów. Na końcu musimy zadbać, by program poczekał na zakończenie wszystkich wątków (metoda `Join()`).
# 2. Porównanie wydajności: Parallel vs Threads
Badania przeprowadzono na procesorze Intel Core i7-14700F, a wyniki przedstawiają średnią z 50-krotnego powtórzenia pomiarów. Dodatkowo, system dysponował 64 GB pamięci RAM.
## a) Tabela czasów wykonania

| Rozmiar macierzy | Liczba wątków | Parallel   | Threads    |
|------------------|---------------|------------|------------|
| 100x100          | 1             | 7,92 ms    | 11,58 ms   |
|                  | 2             | 2,60 ms    | 12,76 ms   |
|                  | 4             | 1,42 ms    | 31,20 ms   |
|                  | 8             | 1,76 ms    | 48,38 ms   |
|                  | 16            | 2,34 ms    | 94,78 ms   |
|                  | 32            | 2,42 ms    | 152,14 ms  |
| 200x200          | 1             | 49,80 ms   | 66,48 ms   |
|                  | 2             | 26,62 ms   | 44,46 ms   |
|                  | 4             | 13,76 ms   | 45,60 ms   |
|                  | 8             | 12,08 ms   | 90,76 ms   |
|                  | 16            | 14,98 ms   | 169,80 ms  |
|                  | 32            | 15,40 ms   | 267,36 ms  |
| 500x500          | 1             | 768,36 ms  | 998,66 ms  |
|                  | 2             | 384,22 ms  | 504,82 ms  |
|                  | 4             | 200,28 ms  | 298,68 ms  |
|                  | 8             | 117,76 ms  | 271,76 ms  |
|                  | 16            | 113,78 ms  | 440,70 ms  |
|                  | 32            | 107,60 ms  | 682,92 ms  |
| 1000x1000        | 1             | 6499,10 ms | 7918,76 ms |
|                  | 2             | 3210,30 ms | 3892,58 ms |
|                  | 4             | 1584,98 ms | 1967,10 ms |
|                  | 8             | 860,56 ms  | 1214,54 ms |
|                  | 16            | 623,06 ms  | 1146,84 ms |
|                  | 32            | 580,70 ms  | 1600,18 ms |

---
## b) Wykres porównawczy
![Zrzut ekranu 2025-04-08 130956](https://github.com/user-attachments/assets/341d4959-ac0b-4514-a224-f12d3e3426c5)

Można zauważyć, że dla małych macierzy (np. 100x100, 200x200) użycie większej liczby wątków przy zastosowaniu klasy `Thread` prowadzi do wydłużenia czasu wykonania, zamiast oczekiwanego przyspieszenia. Powodem są:
- **Wysokie koszty tworzenia i zarządzania wątkami** – dla niewielkich danych narzut związany z tworzeniem wielu wątków może przewyższyć korzyści z ich użycia.
- **Zwiększony narzut systemowy** – więcej wątków oznacza częstsze przełączanie kontekstu, co pochłania zasoby systemowe.
- **Zbyt drobne zadania** – jeśli każdy wątek ma bardzo mało do zrobienia, więcej czasu zajmuje synchronizacja niż rzeczywiste obliczenia.

Dodatkowo, nawet w przypadku większych macierzy, zauważamy, że przekroczenie pewnej liczby wątków (np. z 8 na 16 lub 32) nie przynosi już poprawy wydajności, a wręcz ją pogarsza.

Jest to efekt tzw. przesycenia wątkami. Procesor ma ograniczoną liczbę rdzeni (fizycznych i logicznych). Po osiągnięciu tej liczby, dalsze tworzenie wątków prowadzi do:

- wzrostu liczby przełączeń między wątkami,
- zwiększonego zużycia pamięci i zasobów systemowych,
- większego chaosu przy synchronizacji danych.

W praktyce, zbyt duża liczba wątków działa na niekorzyść — system więcej czasu poświęca na zarządzanie nimi, niż na właściwe obliczenia.



---

# 3. Porównanie 'Parallel' vs 'Threads'
## Parallel:
- Łatwiejsza w użyciu, zautomatyzowana obsługa wątków.
- Idealna do przetwarzania dużych zbiorów danych.
- Obsługuje ograniczenie liczby wątków przez parametr `MaxDegreeOfParallelism`.
- Biblioteka sama decyduje o najlepszym podziale pracy i synchronizacji.
- Lepsza optymalizacja na poziomie systemu operacyjnego.
## Threads:
- Daje pełną kontrolę nad tworzeniem i zarządzaniem wątkami.
- Wymaga ręcznego dzielenia pracy i synchronizacji.
- Można osiągnąć lepszą wydajność w specyficznych, bardziej złożonych scenariuszach.
- Większe ryzyko błędów.
- Potrzebuje więcej kodu i staranności przy implementacji.

# 4. Równoległe przetwarzanie obrazów za pomocą filtrów.

Dla przetwarzania obrazów został stworzone 4 filtry:

## `GrayscaleFilter` 
Filtr konwertujący obraz do odcieni szarości. Opiera się na percepcji ludzkiego oka, które różnie odbiera kolory — w szczególności:
- 29,9% czułości na czerwony
- 58,7% na zielony
- 11,4% na niebieski
Na tej podstawie dla każdego piksela obliczana jest wartość nasycenia (tzw. jasność) i ustawiany jest jednolity kolor RGB:
```csharp
Color pixelColor = original.GetPixel(x, y);
int grayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
result.SetPixel(x, y, grayColor);

```

## `NegativeFilter` 
Każdy piksel jest „negowany”, czyli jego kolor jest odwracany poprzez odjęcie wartości każdego kanału od 255:
```csharp
Color pixelColor = original.GetPixel(x, y);
Color negativeColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);
result.SetPixel(x, y, negativeColor);

```

## `ThresholdFilter` 
Filtr progowy. Działa podobnie jak `GrayscaleFilter`, jednak po obliczeniu wartości jasności, porównuje ją z określonym progiem (`threshold`). Piksele poniżej progu ustawiane są na czarne, a powyżej – na białe:
```csharp
Color pixelColor = original.GetPixel(x, y);
int grayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
Color thresholdColor = grayValue < threshold ? Color.Black : Color.White;
result.SetPixel(x, y, thresholdColor);

```

## `EdgeDetectionFilter` 
Ten filtr służy do wykrywania krawędzi w obrazie z wykorzystaniem operatora Sobela. W pierwszym kroku obraz jest przekształcany do skali szarości przy użyciu wcześniej zdefiniowanego filtra `GrayscaleFilter`.

Następnie wykorzystywane są dwie macierze konwolucyjne (maski Sobela), które badają zmiany intensywności jasności w kierunku poziomym (X) oraz pionowym (Y):
```csharp
Bitmap gray = GrayscaleFilter(original);

int[,] gx = new int[,]
{
  { 1, 0, -1 },
  { 2, 0, -2 },
  { 1, 0, -1 }
};

int[,] gy = new int[,]
{
  { 1, 2, 1 },
  { 0, 0, 0 },
  { -1, -2, -1 }
};
```
Dla każdego piksela (z wyłączeniem krawędzi obrazu) wykonywany jest splot — czyli przemnożenie okna 3x3 otaczającego dany piksel przez obie maski (osobno dla X i Y). Obliczany jest gradient — czyli intensywność zmiany jasności. Im wyższy gradient, tym większa szansa, że w tym miejscu znajduje się krawędź:
```csharp
for (int y = 1; y < gray.Height - 1; y++)
{
  for (int x = 1; x < gray.Width - 1; x++)
  {
    int pixelX = 0;
    int pixelY = 0;
    for (int j = -1; j <= 1; j++)
    {
      for (int i = -1; i <= 1; i++)
      {
        int grayValue = gray.GetPixel(x + i ,y + j).R;
        pixelX += gx[j + 1, i + 1] * grayValue;
        pixelY += gy[j + 1, i + 1] * grayValue;
      }
    }
    int edge = Math.Min(255,(int)Math.Sqrt(pixelX * pixelX + pixelY * pixelY));
    Color edgeColor = Color.FromArgb(edge, edge, edge);
    result.SetPixel(x, y, edgeColor);
  }
}
```
# Wygląd aplikacji wraz z przykładem

![488620693_1859857814775472_5776690384178029046_n](https://github.com/user-attachments/assets/ceed700e-2713-429a-9745-77a1991d197f)
