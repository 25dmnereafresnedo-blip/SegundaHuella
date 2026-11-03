namespace SegundaHuella;

public class ContactoPage : ContentPage
{
    private readonly Entry nombreEntry;
    private readonly Entry apellidoEntry;
    private readonly Entry edadEntry;
    private readonly Entry direccionEntry;
    private readonly Entry correoEntry;
    private readonly Entry telefonoEntry;

    public ContactoPage(string nombreAnimal)
    {
        Title = "Contactar con la protectora";
        BackgroundColor = Colors.White;

        nombreEntry = CrearEntrada("Nombre");
        apellidoEntry = CrearEntrada("Apellido");
        edadEntry = CrearEntrada("Edad", Keyboard.Numeric);
        direccionEntry = CrearEntrada("Dirección");
        correoEntry = CrearEntrada("Gmail", Keyboard.Email);
        telefonoEntry = CrearEntrada("Teléfono", Keyboard.Telephone);

        Button mandarButton = new()
        {
            BackgroundColor = Colors.DarkCyan,
            CornerRadius = 24,
            FontAttributes = FontAttributes.Bold,
            Text = "Mandar solicitud",
            TextColor = Colors.White
        };
        mandarButton.Clicked += OnMandarClicked;

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 24,
                Spacing = 16,
                Children =
                {
                    new Label
                    {
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 28,
                        Text = "Formulario de contacto",
                        TextColor = Colors.DarkCyan
                    },
                    new Label
                    {
                        FontSize = 16,
                        Text = $"Completa tus datos para contactar por {nombreAnimal}.",
                        TextColor = Colors.Gray
                    },
                    nombreEntry,
                    apellidoEntry,
                    edadEntry,
                    direccionEntry,
                    correoEntry,
                    telefonoEntry,
                    mandarButton
                }
            }
        };
    }

    private static Entry CrearEntrada(string placeholder, Keyboard? keyboard = null)
    {
        return new Entry
        {
            Placeholder = placeholder,
            TextColor = Colors.Black,
            Keyboard = keyboard ?? Keyboard.Default
        };
    }

    private async void OnMandarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nombreEntry.Text) ||
            string.IsNullOrWhiteSpace(apellidoEntry.Text) ||
            string.IsNullOrWhiteSpace(edadEntry.Text) ||
            string.IsNullOrWhiteSpace(direccionEntry.Text) ||
            string.IsNullOrWhiteSpace(correoEntry.Text) ||
            string.IsNullOrWhiteSpace(telefonoEntry.Text))
        {
            await DisplayAlertAsync(
                "Datos incompletos",
                "Completa todos los campos antes de mandar la solicitud.",
                "OK");
            return;
        }

        await DisplayAlertAsync(
            "Solicitud enviada",
            "Tu solicitud se ha enviado correctamente.",
            "OK");

        await Navigation.PopAsync();
        await Navigation.PopAsync();
    }
}
