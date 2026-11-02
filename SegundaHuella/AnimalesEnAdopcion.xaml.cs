using SegundaHuella.Data;
using SegundaHuella.Models;

namespace SegundaHuella;

public partial class AnimalesEnAdopcion : ContentPage
{
    public AnimalesEnAdopcion()
    {
        InitializeComponent();
        AnimalesCollectionView.ItemsSource = AnimalData.Animales;
        AnadirAnimalButton.IsVisible = SesionData.EsProtectora;
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

    private async void OnVerMasClicked(object sender, EventArgs e)
    {
        if (sender is Button { CommandParameter: Animal animal })
        {
            await Navigation.PushAsync(new AnimalDetailsPage(animal));
        }
    }
}