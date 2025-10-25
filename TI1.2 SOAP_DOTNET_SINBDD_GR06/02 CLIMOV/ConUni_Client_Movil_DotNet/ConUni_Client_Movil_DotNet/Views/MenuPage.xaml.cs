using ConUni_Client_Movil_DotNet.Services;

namespace ConUni_Client_Movil_DotNet.Views;

public partial class MenuPage : ContentPage
{
    private readonly ConversorService _conversorService;

    public MenuPage(ConversorService conversorService)
    {
        InitializeComponent();
        _conversorService = conversorService;

        // Mostrar nombre de usuario
        LabelUsuario.Text = _conversorService.UsuarioActual ?? "Usuario";
    }

    private async void OnTemperaturaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TemperaturaPage(_conversorService));
    }

    private async void OnLongitudClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LongitudPage(_conversorService));
    }

    private async void OnMasaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MasaPage(_conversorService));
    }

    private async void BtnCerrarSesion_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Cerrar Sesión",
            "¿Está seguro que desea cerrar sesión?",
            "Sí",
            "No"
        );

        if (confirmar)
        {
            _conversorService.Logout();
            await Navigation.PopToRootAsync();
        }
    }

    protected override bool OnBackButtonPressed()
    {
        // Prevenir volver atrás con el botón back
        return true;
    }
}