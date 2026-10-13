namespace SegundaHuella;

public partial class RegistroUsuario : ContentPage
{
    private string nombre = "";
    private string apellido = "";
    private int edad = 0; 
    private string direccion = "";

    public RegistroUsuario()
    {
        InitializeComponent();
    }

    private void NombreChanged(object sender, TextChangedEventArgs e)
    {
        nombre = e.NewTextValue ?? "";
    }

    private void ApellidoChanged(object sender, TextChangedEventArgs e)
    {
        apellido = e.NewTextValue ?? "";
    }

    private void EdadChanged(object sender, TextChangedEventArgs e)
    {
        if (!int.TryParse(e.NewTextValue, out edad))
        {
            edad = 0;
        }
    }

    private void DireccionChanged(object sender, TextChangedEventArgs e)
    {
        direccion = e.NewTextValue ?? "";
    }
}
