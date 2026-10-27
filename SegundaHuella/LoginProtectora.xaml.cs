using SegundaHuella.Data;
using SegundaHuella.Models;

namespace SegundaHuella;

public partial class LoginProtectora : ContentPage
{
    public LoginProtectora()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ProtectoraStorage.CargarAsync();
    }

    private Protectora? BuscarProtectora(string nombre)
    {
        foreach (Protectora p in ProtectoraData.Protectoras)
        {
            if (p.Nombre == nombre)
            {
                return p;
            }
        }
        return null;
    }

    private void NombreChanged(object sender, TextChangedEventArgs e)
    {
        string nombre = e.NewTextValue?.Trim() ?? "";

        if (BuscarProtectora(nombre) != null)
        {
            PassEntry.IsVisible = true;
            PassEntry.HeightRequest = 50;
            IniciarSesionButton.IsVisible = true;
        }
        else
        {
            PassEntry.IsVisible = false;
            PassEntry.HeightRequest = 0;
            PassEntry.Text = "";
            IniciarSesionButton.IsVisible = false;
        }
    }

    private async void IniciarSesionClicked(object sender, EventArgs e)
    {
        string nombre = NombreEntry.Text?.Trim() ?? "";
        string password = PassEntry.Text ?? "";

        Protectora? protectora = BuscarProtectora(nombre);

        if (protectora != null && protectora.Contrasenia == password)
        {
            await Navigation.PushAsync(new AnimalesEnAdopcion());
            Navigation.RemovePage(this);
        }
        else
        {
            await DisplayAlertAsync("Error", "Contraseña incorrecta", "Volver");
        }
    }

    private async void RegistrarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroProtectora());
    }
}
