using SegundaHuella.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SegundaHuella.Data
{
    internal class UsuarioStorage
    {
        private static readonly string rutaArchivo =
            Path.Combine(FileSystem.Current.AppDataDirectory, "usuarios.json");

        public static bool DatosCargados { get; private set; }

        public static async Task GuardarAsync()
        {
            string json = JsonSerializer.Serialize(UsuarioData.Usuarios);
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

            List<Usuario>? usuariosGuardados = JsonSerializer.Deserialize<List<Usuario>>(json);
            if (usuariosGuardados is null)
            {
                return;
            }

            UsuarioData.Usuarios.Clear();

            foreach (Usuario usuario in usuariosGuardados)
            {
                UsuarioData.Usuarios.Add(usuario);
            }
        }
    }
}
