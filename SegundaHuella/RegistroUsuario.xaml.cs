using SegundaHuella.Data;
using SegundaHuella.Models;
using System;

namespace SegundaHuella;

public partial class RegistroUsuario : ContentPage
{
    public RegistroUsuario()
    {
        InitializeComponent();
    }

    private async void RegistrarClicked(object sender, EventArgs e)
    {
        string nombre = NombreEntry.Text?.Trim() ?? "";
        string apellido = ApellidoEntry.Text?.Trim() ?? "";
        string direccion = DireccionEntry.Text?.Trim() ?? "";
        string contrasenia = PassEntry.Text ?? "";
        string edad = EdadEntry.Text?.Trim() ?? "";

        if (nombre == "" || apellido == "" || direccion == "" || contrasenia == "" || edad == "")
        {
            await DisplayAlert("Error", "Por favor, rellene todos los campos correctamente.", "OK");
            return;
        }

        UsuarioData.Usuarios.Add(new Usuario
        {
            Nombre = nombre,
            Apellido = apellido,
            Edad = edad,
            Direccion = direccion,
            Contrasenia = contrasenia
        });

        await UsuarioStorage.GuardarAsync();

        await DisplayAlert("Correcto", "Usuario registrado con éxito", "OK");

        NombreEntry.Text = "";
        ApellidoEntry.Text = "";
        EdadEntry.Text = "";
        DireccionEntry.Text = "";
        PassEntry.Text = "";

        await Navigation.PopAsync();
    }
}
