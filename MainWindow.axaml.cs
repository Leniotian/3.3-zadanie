using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Katalog;

public class CatalogItem
{
    public string Title { get; set; } = "";
    public int Year { get; set; }
    public int Rating { get; set; }

    public override string ToString() => $"{Title} ({Year}) — Ocena: {Rating}/10";
}

public partial class MainWindow : Window
{
    private readonly List<CatalogItem> _items = new();
    private object? _listContent;

    public MainWindow()
    {
        InitializeComponent();
        _listContent = MainContainer.Content;
        LoadItems();
    }

    public void ShowListView()
    {
        MainContainer.Content = _listContent;
        LoadItems();
    }

    private void LoadItems()
    {
        _items.Clear();
        if (!File.Exists("dane/pozycje.txt"))
        {
            return;
        }

        var lines = File.ReadAllLines("dane/pozycje.txt");
        foreach (var line in lines)
        {
            var parts = line.Split(';');
            if (parts.Length >= 3 &&
                int.TryParse(parts[1], out var year) &&
                int.TryParse(parts[2], out var rating))
            {
                _items.Add(new CatalogItem
                {
                    Title = parts[0],
                    Year = year,
                    Rating = rating
                });
            }
        }

        ItemsListBox.ItemsSource = null;
        ItemsListBox.ItemsSource = _items;
    }

    private void OnDetailsClick(object? sender, RoutedEventArgs e)
    {
        if (ItemsListBox.SelectedItem is not CatalogItem selected)
        {
            StatusText.Text = "Wybierz pozycję z listy, aby zobaczyć szczegóły.";
            return;
        }

        StatusText.Text = "";
        MainContainer.Content = new DetailView(selected, ShowListView);
    }

    private void OnAddClick(object? sender, RoutedEventArgs e)
    {
        StatusText.Text = "";
        MainContainer.Content = new AddView(ShowListView);
    }

    private void OnCountClick(object? sender, RoutedEventArgs e)
    {
        var lines = File.ReadAllLines("dane/pozycje.txt");
        CountText.Text = $"Pozycji w pliku: {lines.Length}";
    }
}
