using SegundaHuella.Data;
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

        if (SesionData.EsProtectora)
        {
            InteresaButton.IsVisible = true;
            ContactarButton.IsVisible = true;

            InteresaButton.Text = "Adoptado";
            ContactarButton.Text = "No disponible";

            
        }
    }

    // Acción para el botón de volver atrás
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnInteresaClicked(object sender, EventArgs e)
    {
        if (SesionData.EsProtectora)
        {
            bool confirmar = await DisplayAlertAsync(
                "Eliminar animal",
                $"¿Quieres marcar a {animal.Nombre} como adoptado y eliminarlo?",
                "Sí",
                "No");

            if (!confirmar)
            {
                return;
            }

            animal.EstadoHistorial = "Adoptado";
            AnimalData.AnimalesEliminados.Add(animal);
            AnimalData.Animales.Remove(animal);
            await AnimalStorage.GuardarAsync();
            await AnimalStorage.GuardarHistorialAsync();
            await Navigation.PopAsync();
            return;
        }

        await DisplayAlertAsync("Favoritos", $"Has añadido a {animal.Nombre} a tu lista de intereses.", "OK");
    }

    private async void OnContactarClicked(object sender, EventArgs e)
    {
        if (SesionData.EsProtectora)
        {
            bool confirmar = await DisplayAlertAsync(
                "Eliminar animal",
                $"¿Quieres marcar a {animal.Nombre} como no disponible y eliminarlo?",
                "Sí",
                "No");

            if (!confirmar)
            {
                return;
            }

            animal.EstadoHistorial = "No disponible";
            AnimalData.AnimalesEliminados.Add(animal);
            AnimalData.Animales.Remove(animal);
            await AnimalStorage.GuardarAsync();
            await AnimalStorage.GuardarHistorialAsync();
            await Navigation.PopAsync();
            return;
        }

        await DisplayAlertAsync("Contacto", $"Abriendo formulario para contactar con la protectora de {animal.Nombre}...", "OK");
    }
}