using SegundaHuella.Data;
using SegundaHuella.Models;

namespace SegundaHuella;

public partial class AnimalesEnAdopcion : ContentPage
{
    public AnimalesEnAdopcion()
    {
        InitializeComponent();
        AnimalesCollectionView.ItemsSource = AnimalData.Animales;
        HistorialButton.IsVisible = SesionData.EsProtectora;
        AnadirAnimalButton.IsVisible = SesionData.EsProtectora;
        HistorialButton.Clicked += OnHistorialClicked;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await AnimalStorage.CargarAsync();
    }

    private async void OnAnadirAnimalClicked(object sender, EventArgs e)
    {

        if (!SesionData.EsProtectora)
        {
            return;
        }

        await Navigation.PushAsync(new AnadirAnimalPage());
    }

    private async void OnHistorialClicked(object sender, EventArgs e)
    {
        if (!SesionData.EsProtectora)
        {
            return;
        }

        await Navigation.PushAsync(new HistorialAnimales());
    }

    private async void OnVerMasClicked(object sender, EventArgs e)
    {
        if (sender is Button { CommandParameter: Animal animal })
        {
            await Navigation.PushAsync(new AnimalDetailsPage(animal));
        }
    }
}