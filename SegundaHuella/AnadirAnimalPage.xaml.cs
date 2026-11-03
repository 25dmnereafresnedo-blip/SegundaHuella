using SegundaHuella.Data;
using SegundaHuella.Models;

namespace SegundaHuella;

public partial class AnadirAnimalPage : ContentPage
{

    private FileResult? imagenSeleccionada;

    private Entry OtraEspecieControl => this.FindByName<Entry>("OtraEspecieEntry");

    public AnadirAnimalPage()
    {
        InitializeComponent();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        bool esOtraEspecie = EspeciePicker.SelectedItem?.ToString() == "Otro";

        if (string.IsNullOrWhiteSpace(NombreEntry.Text) ||
            EspeciePicker.SelectedItem is null ||
            (esOtraEspecie && string.IsNullOrWhiteSpace(OtraEspecieControl.Text)) ||
            string.IsNullOrWhiteSpace(EdadEntry.Text) ||
            TamanoPicker.SelectedItem is null)
        {
            await DisplayAlertAsync("Datos incompletos", "Completa todos los campos para añadir la mascota.", "OK");
            return;
        }

        string especie = esOtraEspecie
            ? OtraEspecieControl.Text.Trim()
            : EspeciePicker.SelectedItem.ToString()!;

        string imagen = "";

        if (imagenSeleccionada is not null)
        {
            string extension = Path.GetExtension(imagenSeleccionada.FileName);
            string nombreArchivo = $"{Guid.NewGuid()}{extension}";
            imagen = Path.Combine(FileSystem.AppDataDirectory, nombreArchivo);

            await using Stream origen = await imagenSeleccionada.OpenReadAsync();
            await using FileStream destino = File.Create(imagen);

            await origen.CopyToAsync(destino);
        }

        AnimalData.Animales.Add(new Animal
        {
            Nombre = NombreEntry.Text.Trim(),
            Especie = especie,
            Edad = EdadEntry.Text.Trim(),
            Tamaño = TamanoPicker.SelectedItem.ToString()!,
            Imagen = imagen ?? ""
        });

        await AnimalStorage.GuardarAsync();

        await Navigation.PopAsync();
    }

    private void OnEspecieChanged(object sender, EventArgs e)
    {
        OtraEspecieControl.IsVisible = EspeciePicker.SelectedItem?.ToString() == "Otro";

        if (!OtraEspecieControl.IsVisible)
        {
            OtraEspecieControl.Text = string.Empty;
        }
    }

    private async void OnSeleccionarImagenClicked(object sender, EventArgs e)
    {
        imagenSeleccionada = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Selecciona una imagen",
            FileTypes = FilePickerFileType.Images
        });

    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
