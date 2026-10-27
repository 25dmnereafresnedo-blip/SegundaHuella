namespace SegundaHuella
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void UsuarioClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginUsuario());
        }

        private void ProtectoraClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginProtectora());
        }
    }
}
