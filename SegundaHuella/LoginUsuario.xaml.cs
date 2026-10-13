namespace SegundaHuella;

public partial class LoginUsuario : ContentPage
{
    public LoginUsuario()
    {
        InitializeComponent();
    }
    private void NombreChanged(object sender, TextChangedEventArgs e)
    {
        string nombre = e.NewTextValue.ToString() ?? "";
        if (nombre == "Nerea")
        {
            PassEntry.IsVisible = true;
            IniciarSesionButton.IsVisible = true;
        }
        else
        {
            PassEntry.IsVisible = false;
            IniciarSesionButton.IsVisible = false;
        }
    }
    private void IniciarSesionClicked(object sender, EventArgs e)
    {
        string password = PassEntry.Text ?? "";
        if (password == "1234")
        {
            Page actual = this;
            Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(actual);
        }
        else
        {
            DisplayAlertAsync("Error", "Contraseña incorrecta", "Volver");
        }
    }
    private void RegistrarClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistroUsuario());
    }
}