using SegundaHuella.Models;

namespace SegundaHuella;

public partial class AnimalDetailsPage : ContentPage
{
    private readonly Animal animal;

    public AnimalDetailsPage(Animal animal)
    {
        this.animal = animal;
        InitializeComponent();
        BindingContext = animal;
    }

    // Acción para el botón de volver atrás ("<")
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    // Acción para el botón de "Me interesa"
    private async void OnInteresaClicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Favoritos", $"Has añadido a {animal.Nombre} a tu lista de intereses.", "OK");
    }

    // Acción para el botón de "Contactar"
    private async void OnContactarClicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Contacto", $"Abriendo formulario para contactar con la protectora de {animal.Nombre}...", "OK");
    }
}