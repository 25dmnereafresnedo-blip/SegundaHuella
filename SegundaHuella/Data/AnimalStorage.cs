using SegundaHuella.Models;
using System.Text.Json;

namespace SegundaHuella.Data;

public static class ProtectoraStorage
{
    private static readonly string rutaArchivo =
        Path.Combine(FileSystem.Current.AppDataDirectory, "protectoras.json");

    public static bool DatosCargados { get; private set; }

    public static async Task GuardarAsync()
    {
        string json = JsonSerializer.Serialize(ProtectoraData.Protectoras);
        await File.WriteAllTextAsync(rutaArchivo, json);
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

        List<Protectora>? protectorasGuardadas = JsonSerializer.Deserialize<List<Protectora>>(json);
        if (protectorasGuardadas is null)
        {
            return;
        }

        ProtectoraData.Protectoras.Clear();

        foreach (Protectora protectora in protectorasGuardadas)
        {
            ProtectoraData.Protectoras.Add(protectora);
        }
    }
}
