using SegundaHuella.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SegundaHuella.Data
{
    internal class AnimalStorage
    {
        private static readonly string rutaArchivo =
            Path.Combine(FileSystem.Current.AppDataDirectory, "animales.json");

        public static bool DatosCargados { get; private set; }

        public static async Task GuardarAsync()
        {
            string json = JsonSerializer.Serialize(AnimalData.Animales);
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

            List<Animal>? animalesGuardados = JsonSerializer.Deserialize<List<Animal>>(json);
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
    }
}
