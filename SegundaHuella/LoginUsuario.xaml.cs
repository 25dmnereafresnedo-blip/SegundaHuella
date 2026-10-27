using SegundaHuella.Data;
using SegundaHuella.Models;
using System;

namespace SegundaHuella;

public partial class LoginUsuario : ContentPage
{
    public LoginUsuario()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UsuarioStorage.CargarAsync();
    }

    private Usuario? BuscarUsuario(string nombre)
    {
        foreach (Usuario u in UsuarioData.Usuarios)
        {
            if (u.Nombre.Trim().Equals(nombre, StringComparison.OrdinalIgnoreCase))
            {
                return u;
            }
        }
        return null;
    }

    private void NombreChanged(object sender, TextChangedEventArgs e)
    {
        string nombre = e.NewTextValue?.Trim() ?? "";

        if (BuscarUsuario(nombre) != null)
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

        Usuario? usuario = BuscarUsuario(nombre);

        if (usuario != null && usuario.Contrasenia == password)
        {
            await Navigation.PushAsync(new AnimalesEnAdopcion());
            Navigation.RemovePage(this);
        }
        else
        {
            await DisplayAlert("Error", "Contraseña incorrecta", "Volver");
        }
    }

    private async void RegistrarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroUsuario());
    }
}
