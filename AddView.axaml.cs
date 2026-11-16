using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Katalog;

public partial class AddView : UserControl
{
    private readonly Action _onFinish;

    public AddView()
    {
        InitializeComponent();
        _onFinish = () => { };
    }

    public AddView(Action onFinish)
    {
        InitializeComponent();
        _onFinish = onFinish;
    }

    private void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        var title = (TitleInput.Text ?? "").Trim();
        var yearText = (YearInput.Text ?? "").Trim();
        var ratingText = (RatingInput.Text ?? "").Trim();

        if (!ValidateInput(title, yearText, ratingText, out var year, out var rating))
        {
            return;
        }

        SaveItemToFile(title, year, rating);
        _onFinish();
    }

    private bool ValidateInput(string title, string yearText, string ratingText, out int year, out int rating)
    {
        year = 0;
        rating = 0;
        if (string.IsNullOrWhiteSpace(title))
        {
            ErrorText.Text = "Tytuł nie może być pusty.";
            return false;
        }
        if (!int.TryParse(yearText, out year) || year < 1800 || year > 2100)
        {
            ErrorText.Text = "Wprowadź poprawny rok (np. 2024).";
            return false;
        }
        if (!int.TryParse(ratingText, out rating) || rating < 1 || rating > 10)
        {
            ErrorText.Text = "Wprowadź ocenę w skali od 1 do 10.";
            return false;
        }
        return true;
    }

    private void SaveItemToFile(string title, int year, int rating)
    {
        var line = $"{title};{year};{rating}{Environment.NewLine}";
        File.AppendAllText("dane/pozycje.txt", line);
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        _onFinish();
    }
}
