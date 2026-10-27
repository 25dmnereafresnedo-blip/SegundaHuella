using SegundaHuella.Models;
using System.Collections.ObjectModel;

namespace SegundaHuella.Data;

public static class ProtectoraData
{
    public static ObservableCollection<Protectora> Protectoras { get; } = new ObservableCollection<Protectora>();
}
