using SegundaHuella.Data;
using SegundaHuella.Models;

namespace SegundaHuella;

public partial class RegistroProtectora : ContentPage
{
    public RegistroProtectora()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ProtectoraStorage.CargarAsync();
    }

    private async void RegistrarseClicked(object sender, EventArgs e)
    {
        string nombre = NombreEntry.Text?.Trim() ?? "";
        string direccion = DireccionEntry.Text?.Trim() ?? "";
        string contrasenia = PassEntry.Text ?? "";

        if (nombre == "" || direccion == "" || contrasenia == "")
        {
            await DisplayAlertAsync("Error", "Rellena todos los campos", "Aceptar");
            return;
        }

        foreach (Protectora p in ProtectoraData.Protectoras)
        {
            if (p.Nombre == nombre)
            {
                await DisplayAlertAsync("Error", "Ya existe una protectora con ese nombre", "Aceptar");
                return;
            }
        }

        Protectora nuevaProtectora = new Protectora
        {
            Nombre = nombre,
            Direccion = direccion,
            Contrasenia = contrasenia
        };

        ProtectoraData.Protectoras.Add(nuevaProtectora);
        await ProtectoraStorage.GuardarAsync();

        await DisplayAlertAsync("Correcto", "Protectora registrada", "Aceptar");

        NombreEntry.Text = "";
        DireccionEntry.Text = "";
        PassEntry.Text = "";

        await Navigation.PopAsync();
    }
}
