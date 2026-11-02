using SegundaHuella.Data;
using SegundaHuella.Models;

namespace SegundaHuella;

public partial class AnadirAnimalPage : ContentPage
{

    private FileResult? imagenSeleccionada;

    public AnadirAnimalPage()
    {
        InitializeComponent();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NombreEntry.Text) ||
            string.IsNullOrWhiteSpace(EspecieEntry.Text) ||
            string.IsNullOrWhiteSpace(EdadEntry.Text) ||
            string.IsNullOrWhiteSpace(TamanoEntry.Text))
        {
            await DisplayAlertAsync("Datos incompletos", "Completa todos los campos para añadir la mascota.", "OK");
            return;
        }

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
            Especie = EspecieEntry.Text.Trim(),
            Edad = EdadEntry.Text.Trim(),
            Tamaño = TamanoEntry.Text.Trim(),
            Imagen = imagen ?? ""
        });

        await AnimalStorage.GuardarAsync();

        await Navigation.PopAsync();
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
