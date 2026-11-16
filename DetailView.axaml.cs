using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Katalog;

public partial class DetailView : UserControl
{
    private readonly Action _onBack;

    public DetailView()
    {
        InitializeComponent();
        _onBack = () => { };
    }

    public DetailView(CatalogItem item, Action onBack)
    {
        InitializeComponent();
        _onBack = onBack;
        SetItemDetails(item);
    }

    private void SetItemDetails(CatalogItem item)
    {
        TitleText.Text = item.Title;
        YearText.Text = item.Year.ToString();
        RatingText.Text = $"{item.Rating} / 10";
    }

    private void OnBackClick(object? sender, RoutedEventArgs e)
    {
        _onBack();
    }
}
