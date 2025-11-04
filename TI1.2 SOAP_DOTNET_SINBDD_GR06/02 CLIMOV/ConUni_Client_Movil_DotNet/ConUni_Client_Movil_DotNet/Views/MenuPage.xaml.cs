using ConUni_Client_Movil_DotNet.Services;

namespace ConUni_Client_Movil_DotNet.Views;

public partial class MenuPage : ContentPage
{
    private readonly ServiceManager _serviceManager;

    public MenuPage(ServiceManager serviceManager)
    {
        InitializeComponent();
        _serviceManager = serviceManager;

        // Mostrar nombre de usuario
        LabelUsuario.Text = _serviceManager.UsuarioActual ?? "Usuario";
    }

    private async void OnTemperaturaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TemperaturaPage(_serviceManager));
    }

    private async void OnLongitudClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LongitudPage(_serviceManager));
    }

    private async void OnMasaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MasaPage(_serviceManager));
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
            _serviceManager.Logout();
            await Navigation.PopToRootAsync();
        }
    }

    protected override bool OnBackButtonPressed()
    {
        // Prevenir volver atrás con el botón back
        return true;
    }
}