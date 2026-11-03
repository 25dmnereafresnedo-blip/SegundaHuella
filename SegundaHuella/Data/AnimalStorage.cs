using SegundaHuella.Models;
using System.Text.Json;

namespace SegundaHuella.Data;

public static class AnimalStorage
{
    private static readonly string rutaArchivo =
        Path.Combine(FileSystem.Current.AppDataDirectory, "animales.json");

    private static readonly string rutaHistorial =
        Path.Combine(FileSystem.Current.AppDataDirectory, "animales_eliminados.json");

    public static bool DatosCargados { get; private set; }
    public static bool HistorialCargado { get; private set; }

    public static async Task GuardarAsync()
    {
        string json = JsonSerializer.Serialize(AnimalData.Animales);
        await File.WriteAllTextAsync(rutaArchivo, json);
    }

    public static async Task GuardarHistorialAsync()
    {
        string json = JsonSerializer.Serialize(AnimalData.AnimalesEliminados);
        await File.WriteAllTextAsync(rutaHistorial, json);
    }

    public static async Task CargarAsync()
    {
        if (DatosCargados)
        {
            return;
        }

        DatosCargados = true;

        if (!File.Exists(rutaArchivo))
        {
            return;
        }

        string json = await File.ReadAllTextAsync(rutaArchivo);

        List<Animal>? animalesGuardados =
            JsonSerializer.Deserialize<List<Animal>>(json);

        if (animalesGuardados is null)
        {
            return;
        }

        AnimalData.Animales.Clear();

        foreach (Animal animal in animalesGuardados)
        {
            AnimalData.Animales.Add(animal);
        }
    }

    public static async Task CargarHistorialAsync()
    {
        if (HistorialCargado)
        {
            return;
        }

        HistorialCargado = true;

        if (!File.Exists(rutaHistorial))
        {
            return;
        }

        string json = await File.ReadAllTextAsync(rutaHistorial);

        List<Animal>? animalesEliminadosGuardados =
            JsonSerializer.Deserialize<List<Animal>>(json);

        if (animalesEliminadosGuardados is null)
        {
            return;
        }

        AnimalData.AnimalesEliminados.Clear();

        foreach (Animal animal in animalesEliminadosGuardados)
        {
            AnimalData.AnimalesEliminados.Add(animal);
        }
    }
}