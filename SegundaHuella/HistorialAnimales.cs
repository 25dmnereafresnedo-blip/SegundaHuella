using SegundaHuella.Data;
using SegundaHuella.Models;
using Microsoft.Maui.Controls.Shapes;

namespace SegundaHuella;

public class HistorialAnimales : ContentPage
{
    private readonly CollectionView historialCollectionView;

    public HistorialAnimales()
    {
        Title = "Historial";
        BackgroundColor = Colors.White;

        historialCollectionView = new CollectionView
        {
            EmptyView = "Todavía no hay animales eliminados",
            ItemTemplate = new DataTemplate(() =>
            {
                Label nombreLabel = new()
                {
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 22,
                    TextColor = Colors.Black
                };
                nombreLabel.SetBinding(Label.TextProperty, nameof(Animal.Nombre));

                Label especieLabel = CrearDatoLabel(nameof(Animal.Especie), "Especie: {0}");
                Label edadLabel = CrearDatoLabel(nameof(Animal.Edad), "Edad: {0}");
                Label tamanoLabel = CrearDatoLabel(nameof(Animal.Tamaño), "Tamaño: {0}");

                Label estadoLabel = new()
                {
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#FF5A5F")
                };
                estadoLabel.SetBinding(Label.TextProperty, new Binding(nameof(Animal.EstadoHistorial), stringFormat: "Estado: {0}"));

                VerticalStackLayout datosLayout = new()
                {
                    Spacing = 7,
                    Children = { nombreLabel, especieLabel, edadLabel, tamanoLabel, estadoLabel }
                };

                Image imagen = new()
                {
                    Aspect = Aspect.AspectFill,
                    HeightRequest = 180,
                    WidthRequest = 140
                };
                imagen.SetBinding(Image.SourceProperty, nameof(Animal.Imagen));

                Grid contenido = new()
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition(GridLength.Auto),
                        new ColumnDefinition(GridLength.Star)
                    },
                    ColumnSpacing = 16
                };
                contenido.Add(imagen, 0, 0);
                contenido.Add(datosLayout, 1, 0);

                Border tarjeta = new()
                {
                    Margin = new Thickness(0, 4),
                    Padding = 16,
                    BackgroundColor = Colors.White,
                    Stroke = Colors.DarkCyan,
                    StrokeShape = new RoundRectangle { CornerRadius = 10 },
                    Content = contenido
                };

                return tarjeta;
            })
        };

        historialCollectionView.ItemsSource = AnimalData.AnimalesEliminados;

        Button volverButton = new()
        {
            BackgroundColor = Colors.White,
            BorderColor = Colors.DarkCyan,
            BorderWidth = 2,
            CornerRadius = 20,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Start,
            Text = "VOLVER",
            TextColor = Colors.DarkCyan
        };
        volverButton.Clicked += OnVolverClicked;

        Content = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 16,
            Children =
            {
                volverButton,
                new Label
                {
                    FontSize = 32,
                    HorizontalOptions = LayoutOptions.Center,
                    Text = "Historial de animales",
                    TextColor = Color.FromArgb("#008F87"),
                    VerticalOptions = LayoutOptions.Center
                },
                historialCollectionView
            }
        };
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await AnimalStorage.CargarHistorialAsync();
    }

    private static Label CrearDatoLabel(string propiedad, string formato)
    {
        Label label = new() { TextColor = Colors.Black };
        label.SetBinding(Label.TextProperty, new Binding(propiedad, stringFormat: formato));
        return label;
    }
}
