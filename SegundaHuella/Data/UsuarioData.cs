using SegundaHuella.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SegundaHuella.Data
{
    internal static class UsuarioData
    {
        public static ObservableCollection<Usuario> Usuarios { get; set; } = new ObservableCollection<Usuario>();
    }
}

