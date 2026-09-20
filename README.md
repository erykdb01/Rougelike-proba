# Roguelike 3D

Prosta gra roguelike 3D stworzona w **Unity**, z kamerą izometryczną, proceduralnie generowanymi poziomami i podstawowym AI przeciwników. Ten projekt to ćwiczenie edukacyjne — budowane od zera podczas nauki Unity i C#.

## Funkcje

- **Kamera izometryczna** — projekcja ortograficzna, stały kąt, płynnie podąża za graczem i centruje się na nim
- **Ruch gracza** — sterowanie WASD zmapowane na kąt kamery, dzięki czemu sterowanie jest intuicyjne z perspektywy izometrycznej
- **Proceduralne generowanie poziomów** — losowe, nienakładające się prostokątne pokoje połączone korytarzami o regulowanej szerokości
- **AI przeciwników** — przeciwnicy pojawiają się w pokojach wystarczająco oddalonych od pozycji startowej gracza; wykrywają gracza w konfigurowalnym zasięgu i ścigają go za pomocą prostego poruszania się

## Stos technologiczny

- **Silnik:** Unity (URP)
- **Język:** C#

## Struktura projektu

```
Assets/
├── Scenes/
│   └── SampleScene.unity
├── Scripts/
│   ├── PlayerMovement.cs     # Ruch WASD, względem kamery
│   ├── CameraFollow.cs       # Podążanie i centrowanie kamery izometrycznej
│   ├── LevelGenerator.cs     # Proceduralne generowanie pokoi/korytarzy, spawn gracza i przeciwnika
│   └── EnemyAI.cs            # Zasięg wykrywania + zachowanie pościgu
```

## Jak to działa

### Kamera i ruch
Kamera używa projekcji **ortograficznej** pod stałym kątem (np. `Rotation: X=30, Y=45`), żeby uzyskać klasyczny widok izometryczny. Input gracza jest obracany o ten sam kąt przed zastosowaniem do ruchu, dzięki czemu wciśnięcie W zawsze przesuwa postać "w górę" ekranu, niezależnie od diagonalnego ustawienia kamery.

### Generowanie poziomu
Poziomy są generowane na siatce 2D (`bool[,]`):
1. Losowana jest liczba prostokątnych pokoi o losowych rozmiarach, z ponawianiem próby umieszczenia, jeśli nakładają się na już istniejące pokoje (z marginesem bezpieczeństwa).
2. Środki pokoi są łączone po kolei korytarzami w kształcie litery L, o konfigurowalnej szerokości.
3. Siatka jest "rysowana" w świecie 3D poprzez tworzenie (instantiate) prefabów podłogi wszędzie tam, gdzie siatka ma wartość `true`.
4. Gracz jest umieszczany na środku pierwszego wygenerowanego pokoju.

### Przeciwnicy
Podczas generowania poziomu losowany jest pokój wystarczająco oddalony od pokoju startowego gracza, w którym pojawia się przeciwnik. Przeciwnik zaczyna w stanie `Idle` (bezczynny) i przełącza się na `Chasing` (pościg), gdy gracz wejdzie w jego zasięg wykrywania, po czym co klatkę porusza się w stronę gracza.

## Status

To projekt edukacyjny w trakcie rozwoju. Aktualny stan:

- [x] Kamera izometryczna + ruch gracza
- [x] Proceduralne generowanie pokoi/korytarzy (bez nakładania)
- [x] Spawn gracza w pokoju startowym
- [x] Podstawowe AI wykrywania i pościgu przeciwnika
- [ ] Ściany wokół pokoi/korytarzy
- [ ] System walki (atak gracza, HP przeciwnika, obrażenia)
- [ ] Wielu przeciwników
- [ ] Przedmioty / loot
- [ ] Pełna pętla roguelike (permadeath, progresja poziomów)

## Uruchomienie

1. Sklonuj repozytorium
2. Otwórz projekt w Unity (sprawdź `ProjectSettings` dla użytej wersji)
3. Otwórz `Assets/Scenes/SampleScene.unity`
4. Kliknij Play

## Licencja

Do ustalenia
