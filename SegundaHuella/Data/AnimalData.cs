using SegundaHuella.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SegundaHuella.Data
{
    internal static class AnimalData
    {
        public static ObservableCollection<Animal> Animales { get; set; } = new ObservableCollection<Animal>();
    }
}
