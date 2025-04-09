# Aplikacja do rozwiązywania problemu plecakowego

## Wstęp

Podczas pierwszych zajęć laboratoryjnych naszym zadaniem było stworzenie aplikacji rozwiązującej problem plecakowy. Projekt został podzielony na trzy główne etapy:

1. Implementacja algorytmu rozwiązującego problem plecakowy.
2. Implementacja testów jednostkowych.
3. Stworzenie graficznego interfejsu użytkownika (GUI).

---

## Zadanie 1 – Rozwiązanie problemu

W ramach tego etapu powstały trzy klasy:

- **`Item`** – przechowuje dane pojedynczego przedmiotu: indeks, wagę, wartość oraz stosunek wartości do wagi.
- **`Result`** – przechowuje wynik rozwiązania: łączną wagę, wartość i listę wybranych przedmiotów.
- **`Problem`** – przechowuje dane problemu (liczbę przedmiotów, ich instancje) oraz odpowiada za ich generowanie i sortowanie.

### Klasa `Item`

```csharp
class Item
{
    public int Index { get; set; }
    public int Weight { get; set; }
    public int Value { get; set; }
    public double ValueToWeightRatio { get; set; }

    public Item(int index,int weight, int value)
    {
        Weight = weight;
        Value = value;
        Index = index;
        ValueToWeightRatio = (double)Value / (double)Weight;
    }
}
```

Konstruktor automatycznie wylicza stosunek wartości do wagi, aby można było sortować przedmioty wg opłacalności.

---

### Klasa `Result`

```csharp
class Result
{
    private int totalValue;
    private int totalWeight;
    private List<Item> takeItems;

    public Result(int totalValue, int totalWeight, List<Item> takeItems)
    {
        this.totalValue = totalValue;
        this.totalWeight = totalWeight;
        this.takeItems = takeItems;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Total Value: " + totalValue);
        sb.AppendLine("Total Weight: " + totalWeight);
        sb.AppendLine("Take Items:");
        for (int i = 0; i < takeItems.Count; i++)
        {
            sb.AppendLine("Nr: " + takeItems[i].Index);
        }
        return sb.ToString();
    }
}
```

Metoda `ToString()` ułatwia prezentację wyników użytkownikowi w aplikacji.

---

### Klasa `Problem`

```csharp
class Problem
{
    private int numberOfItems;
    private int maxWeight;
    private int maxValue;
    private List<Item> items;

    public Problem(int numberOfItems, int maxWeight, int maxValue, int seed)
    {
        this.numberOfItems = numberOfItems;
        this.maxWeight = maxWeight;
        this.maxValue = maxValue;
        items = new List<Item>();
        this.generateItems(seed);
    }

    public Problem(int numberOfItems, List<Item> items)
    {
        this.numberOfItems = numberOfItems;
        this.items = items;
    }

    private void generateItems(int seed)
    {
        Random random = new Random(seed);
        for (int i = 0; i < numberOfItems; i++)
        {
            items.Add(new Item((i+1), random.Next(1, maxWeight), random.Next(1, maxValue)));
        }
    }

    private void SortByValueToWeightRatio()
    {
        items.Sort((x, y) => (y.ValueToWeightRatio).CompareTo(x.ValueToWeightRatio));
    }
}
```

Klasa `Problem` posiada dwa konstruktory – jeden do generowania danych losowych, drugi do testów z wcześniej przygotowaną listą. Kluczową funkcją jest sortowanie elementów wg stosunku wartości do wagi.

---

## Zadanie 2 – Testy

W projekcie zaimplementowano zarówno testy z instrukcji, jak i własne przypadki testowe.

### Testy z instrukcji:

- **Sprawdzenie, czy zwrócono co najmniej jeden element, jeśli istnieje przedmiot spełniający warunki:**
```csharp
int number = result.nrOfItems();
Assert.IsTrue(number >= 1);
```

- **Sprawdzenie, czy rozwiązanie jest puste, jeśli żaden przedmiot nie spełnia warunków:**
```csharp
int expectedOutput = 0;
int output = result.nrOfItems();
Assert.AreEqual(expectedOutput, output);
```

- **Test poprawności rozwiązania dla konkretnej instancji:**
```csharp
List<Item> items = new List<Item>()
{
    new Item(1,5,10),
    new Item(2,10,15),
    new Item(3,15,15),
    new Item(4,8,12),
    new Item(5,15,10)
};
Assert.AreEqual(38, outputWeight);
Assert.AreEqual(52, outputValue);
```

---

### Własne testy:

- **Czy suma wag przedmiotów nie przekracza pojemności plecaka:**
```csharp
Assert.IsTrue(result.GetTotalWeight() <= capacity);
```

- **Czy rozwiązanie jest puste, gdy wszystkie przedmioty są zbyt ciężkie:**
```csharp
Assert.AreEqual(0, result.nrOfItems());
Assert.AreEqual(0, result.TotalValue);
Assert.AreEqual(0, result.GetTotalWeight());
```

- **Czy wszystkie przedmioty mieszczą się w plecaku, jeśli ich łączna waga < pojemność:**
```csharp
Assert.AreEqual(3, result.nrOfItems());
Assert.AreEqual(60, result.TotalValue);
Assert.AreEqual(30, result.GetTotalWeight());
```

- **Test ekstremalny dla dużej liczby przedmiotów:**
```csharp
[TestMethod]
public void TestExtremeScenario()
{
    int numberOfItems = 10000; 
    int capacity = 5000; 

    Problem problem = new Problem(numberOfItems, 500, 1000, 13);
    Result result = problem.Solve(capacity);

    Assert.IsTrue(result.GetTotalWeight() <= capacity);
    Assert.IsTrue(result.TotalValue > 0);

    List<Item> takenItemsInOrder = result.GetItems();
    for (int i = 1; i < takenItemsInOrder.Count; i++)
    {
        Assert.IsTrue(takenItemsInOrder[i - 1].ValueToWeightRatio >= 
                      takenItemsInOrder[i].ValueToWeightRatio);
    }
}
```

> Łącznie wykonano 10 testów. Wszystkie zakończyły się sukcesem.

![testy](https://github.com/user-attachments/assets/46a050c8-e0e5-4752-a978-b0b738f5c574)


---

## Zadanie 3 – GUI

Interfejs użytkownika umożliwia:

1. Walidację danych wejściowych (pola zmieniają kolor na czerwony w przypadku błędnych danych).
2. Generowanie instancji problemu i jego rozwiązania po kliknięciu przycisku.

### Obsługa walidacji:

```csharp
private void isGreaterThan0(object sender, EventArgs e)
{
    TextBox textBox = (TextBox)sender;
    if (int.TryParse(textBox.Text, out int number))
    {
        textBox.BackColor = (number <= 0) ? Color.Red : Color.White;
    }
    else
    {
        textBox.BackColor = Color.Red;
    }
}
```

### Obsługa przycisku:

```csharp
private void SumbitButton_Click(object sender, EventArgs e)
{
    if (Seed.BackColor == Color.Red || MaxValue.BackColor == Color.Red ||
        MaxWeight.BackColor == Color.Red || Capacity.BackColor == Color.Red ||
        nrOfItems.BackColor == Color.Red || Seed.Text == "" || 
        MaxValue.Text == "" || MaxWeight.Text == "" || Capacity.Text == "" ||
        nrOfItems.Text == "")
    {
        MessageBox.Show("Prosze wprowadzic prawidlowe wartosci!", "Blad", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    else
    {
        int seed = int.Parse(Seed.Text);
        int numberOfItems = int.Parse(nrOfItems.Text);
        int capacity = int.Parse(Capacity.Text);
        int maxWeight = int.Parse(MaxWeight.Text);
        int maxValue = int.Parse(MaxValue.Text);

        Problem problem = new Problem(numberOfItems, maxWeight, maxValue, seed);
        InstanceTextBox.Text = problem.ToString();
        Result result = problem.Solve(capacity);
        ResultTextBox.Text = result.ToString();
    }
}
```

---

![GUI](https://github.com/user-attachments/assets/572c9977-bf41-40b3-b181-cb71244e7ffe)
