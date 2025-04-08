# Porównanie wydajności: Parallel vs Threads
Badania przeprowadzono na procesorze Intel Core i7-14700F, a wyniki przedstawiają średnią z 50-krotnego powtórzenia pomiarów. Dodatkowo, system dysponował 64 GB pamięci RAM.
## Tabela czasów wykonania

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

## Dlaczego czas wykonania rośnie przy większej liczbie wątków (dla małych macierzy)?

Dla małych rozmiarów macierzy (np. **100x100**, **200x200**), wersja oparta na `Threads` staje się wolniejsza, gdy liczba wątków rośnie. Oto dlaczego:

### Narzut tworzenia wątków
Tworzenie i zarządzanie wieloma wątkami ma swoją cenę. Dla małych zadań narzut ten może przewyższyć zysk z równoległości.

### Przełączanie kontekstu
Duża liczba wątków powoduje częste przełączanie między nimi przez CPU, co wiąże się z dodatkowym narzutem systemowym.

### Nierównomierne obciążenie
Gdy dane są małe, podział pracy na wiele wątków sprawia, że każdy wątek ma bardzo mało do zrobienia, a koszt synchronizacji rośnie.

### Przeciążenie CPU
Jeśli liczba wątków przekracza liczbę fizycznych rdzeni, CPU musi dzielić się między wątki — skutkuje to kolidowaniem, stratą w cache i gorszą wydajnością.

---

## Dlaczego `Parallel` działa lepiej?

Większość bibliotek `Parallel`  korzysta z pul wątków i inteligentnego planowania zadań. Dzięki temu unika się kosztu ciągłego tworzenia wątków.

---

## Wykres porównawczy


![Zrzut ekranu 2025-04-08 130956](https://github.com/user-attachments/assets/341d4959-ac0b-4514-a224-f12d3e3426c5)
