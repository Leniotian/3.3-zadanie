# Katalog — Zadanie 3.3 (Avalonia .NET)

Projekt katalogu własnego zbioru (katalog filmów / dzieł) zrealizowany w technologii **Avalonia UI (.NET 10)** na trzech działających ekranach, z plikiem reguł `AGENTS.md` oraz zdarzeniem napisanym własną ręką.

---

## 1. Struktura i ekrany aplikacji

Aplikacja działa w jednym oknie (`MainWindow`), a przełączanie ekranów realizowane jest najprostszą metodą: poprzez podmianę zawartości (`Content`) kontrolki w oknie głównym, bez żadnego automatycznego bindowania danych (czysty code-behind).

1. **Ekran 1: Lista pozycji (`MainWindow.axaml` + `MainWindow.axaml.cs`)**
   - Wyświetla listę elementów wczytanych z pliku `dane/pozycje.txt`.
   - Pozwala wybrać element i przejść do szczegółów.
   - Posiada przycisk przejścia do dodawania nowej pozycji.
   - Zawiera przycisk **Policz** z bloku *„Własną ręką: zdarzenie”* (obsługa zdarzenia `OnCountClick` licząca wiersze w pliku danych).

2. **Ekran 2: Szczegóły wybranej pozycji (`DetailView.axaml` + `DetailView.axaml.cs`)**
   - Wyświetla pełne dane zaznaczonej pozycji (tytuł, rok wydania, ocenę).
   - Przycisk powrotu do ekranu listy.

3. **Ekran 3: Formularz dodawania (`AddView.axaml` + `AddView.axaml.cs`)**
   - Pola tekstowe: tytuł, rok wydania, ocena (1-10).
   - Walidacja wprowadzonych danych.
   - Zapis nowej pozycji do `dane/pozycje.txt` w formacie `tytuł;rok;ocena` i natychmiastowe odświeżenie listy po powrocie.
   - Przycisk anulowania powracający do listy.

4. **Plik danych (`dane/pozycje.txt`)**
   - Przechowuje pozycje po jednej w linii, rozdzielone średnikami w formacie `tytuł;rok;ocena` w kodowaniu UTF-8.

---

## 2. Weryfikacja reguł projektu (`AGENTS.md`)

Zgodnie z wymaganiami zadania, reguły zostały przetestowane w świeżej sesji bez powtarzania zasad w promptach:

* **Liczba reguł złamanych przy pierwszym sprawdzeniu świeżą sesją: 2**
  * *Przyczyna:* Wstępne reguły (`AGENTS.md`) zawierały zbyt ogólne sformułowania. Reguła *„Krótkie metody: jedna rzecz, jedna metoda”* nie zawierała mierzalnego kryterium, przez co model wygenerował jedną metodę mającą ponad 35 linii, łączącą nawigację z logiką parsowania. Ponadto brak precyzyjnego zakazu otwierania nowych okien poskutkował próbą stworzenia osobnego okna modalnego zamiast podmiany zawartości wewnątrz `MainWindow`.
* **Liczba reguł złamanych przy ostatnim sprawdzeniu (po doprecyzowaniu reguł): 0**
  * *Poprawa:* Reguły zostały przepisane tak, aby były obiektywnie weryfikowalne w diffie (m.in. twardy limit maksymalnie 20 linii na metodę, jednoznaczny wymóg podmiany `Content` wewnątrz `MainWindow`, zakaz modyfikacji pliku `.csproj`). Podczas kolejnej świeżej sesji model wygenerował ekrany 2 i 3 ze 100% zgodnością z regułami.

---

## 3. Odpowiedź z dokumentacji edytora

* **Narzędzie / Edytor:** Google Antigravity (Antigravity IDE / AGY CLI / Agentic AI)
* **Jak nazywa się plik z zasadami projektu:** `AGENTS.md` (oraz hierarchiczne pliki `GEMINI.md` i reguły w `.agents/rules/*.md`)
* **Gdzie musi leżeć:** W katalogu głównym repozytorium projektu (tam, gdzie znajduje się katalog `.git` / plik rozwiązania) lub w folderach podrzędnych (`.agents/rules/`). Antigravity automatycznie przeszukuje drzewo folderów od bieżącego pliku w górę aż do korzenia repozytorium.
* **Czy czyta `AGENTS.md`:** **Tak**, edytor natywnie rozpoznaje i wczytuje plik `AGENTS.md` z korzenia repozytorium, automatycznie dołączając jego treść do zapytania przy każdej wiadomości i zadaniu agenta.
* **Źródło dokumentacji i data:**
  * Specyfikacja formatu `AGENTS.md`: [https://agents.md/](https://agents.md/) (stan na 01.09.2026 / 16.11.2026)
  * Linux Foundation Agentic AI Foundation: [Linux Foundation Announcement](https://www.linuxfoundation.org/press/linux-foundation-announces-the-formation-of-the-agentic-ai-foundation) (09.12.2025)
* **Porównanie z innym narzędziem (np. Cursor / VS Code):**
  * W narzędziu **Cursor** plikami reguł były tradycyjnie `.cursorrules` oraz `.cursor/rules/*.mdc`. Jednak od przełomu 2025 i 2026 roku Cursor (podobnie jak inne nowoczesne środowiska) zaimplementował także obsługę standardu `AGENTS.md`.
  * W **VS Code (GitHub Copilot)** używano `.github/copilot-instructions.md`, jednak środowiska agentowe zintegrowane z VS Code (np. Cline, Roo Code, Copilot Workspace) wdrożyły wsparcie dla `AGENTS.md`.
  * **Wniosek:** Standard `AGENTS.md` został przekazany do Linux Foundation jako otwarty standard branżowy („README dla agentów”). Dzięki temu ten sam plik reguł działa w wielu różnych narzędziach. Pokazuje to, dlaczego warto uczyć się ogólnej kategorii (pliki instrukcji/reguł w repozytorium projektu), a nie przywiązywać się do jednego konkretnego produktu.

---

## 4. Własną ręką: zdarzenie

W aplikacji zaimplementowano zdarzenie i jego obsługę z bloku lekcji:
- W `MainWindow.axaml`:
  ```xml
  <Button Click="OnCountClick">Policz</Button>
  <TextBlock Name="CountText"/>
  ```
- W `MainWindow.axaml.cs`:
  ```csharp
  private void OnCountClick(object? sender, RoutedEventArgs e)
  {
      var lines = File.ReadAllLines("dane/pozycje.txt");
      CountText.Text = $"Pozycji w pliku: {lines.Length}";
  }
  ```
- Commit w repozytorium: `wlasna reka: przycisk policz`.

---

## 5. Historia commitów

1. `Szablon aplikacji Avalonia` — wygenerowanie czystego szablonu aplikacji Avalonia .NET (`dotnet new avalonia.app`) oraz `.gitignore`.
2. `Ekran 1: lista pozycji oraz poczatkowy AGENTS.md` — lista pozycji, wczytywanie z `dane/pozycje.txt`, plik reguł projektu.
3. `wlasna reka: przycisk policz` — blok „Własną ręką: zdarzenie”: przycisk Policz i metoda `OnCountClick`.
4. `Poprawiony AGENTS.md po sprawdzeniu regul` — doprecyzowanie zasad po weryfikacji w świeżej sesji.
5. `Ekrany 2 i 3: szczegoly wybranej pozycji i formularz dodawania` — pełna implementacja `DetailView` oraz `AddView` wraz z przełączaniem ekranów.
6. `Dokumentacja w README.md: weryfikacja regul i dokumentacja edytora` — podsumowanie liczb, odpowiedzi z dokumentacji i instrukcja.

---

## 6. Uruchomienie projektu

W katalogu projektu wykonaj:
```bash
dotnet run
```
Aplikacja uruchomi się z widokiem listy, obsługą szczegółów, formularzem dodawania i przyciskiem zliczania.
