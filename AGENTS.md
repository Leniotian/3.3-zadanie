# Katalog — zasady projektu

- Interfejs użytkownika (etykiety, przyciski, komunikaty) wyłącznie po polsku.
- Nazwy plików, klas, właściwości i metod wyłącznie po angielsku.
- Jeden ekran to dokładnie jeden plik `.axaml` i jeden plik `.axaml.cs` (kontrolka `UserControl`).
- Przełączanie ekranów odbywa się w jednym oknie głównym (`MainWindow`) przez podmianę zawartości (`Content`), bez otwierania nowych okien.
- Nie dodawaj żadnych nowych zależności ani pakietów NuGet do `.csproj` — używaj tylko tego, co dostarcza szablon.
- Dane wczytujemy i zapisujemy wyłącznie w pliku `dane/pozycje.txt`, w formacie `tytuł;rok;ocena`.
- Kodowanie wszystkich plików: UTF-8.
- Nie modyfikuj plików, o które nie było bezpośredniej prośby.
- Każda metoda wykonuje jedną operację i ma maksymalnie 20 linii kodu.
- Logika ekranu znajduje się bezpośrednio w pliku `.axaml.cs` (code-behind), bez automatycznego data-bindingu / MVVM.
