using ConUni_Client_Movil_DotNet.Services;

namespace ConUni_Client_Movil_DotNet.Views;

public partial class LoginPage : ContentPage
{
    private readonly ConversorService _conversorService;

    public LoginPage()
    {
        InitializeComponent();
        _conversorService = new ConversorService();
    }

    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        // Validar campos
        if (string.IsNullOrWhiteSpace(EntryUsuario.Text))
        {
            MostrarError("Por favor ingrese un usuario");
            return;
        }

        if (string.IsNullOrWhiteSpace(EntryPassword.Text))
        {
            MostrarError("Por favor ingrese una contraseña");
            return;
        }

        // Mostrar indicador de carga
        BtnLogin.IsEnabled = false;
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        LabelError.IsVisible = false;

        try
        {
            // Intentar login
            var (exitoso, mensaje) = await _conversorService.LoginAsync(
                EntryUsuario.Text.Trim(),
                EntryPassword.Text
            );

            if (exitoso)
            {
                // Login exitoso - navegar al menú principal
                await DisplayAlert("✓ Éxito", mensaje, "OK");

                // Navegar a MenuPage y pasar el servicio
                await Navigation.PushAsync(new MenuPage(_conversorService));
            }
            else
            {
                // Login fallido
                MostrarError(mensaje);
            }
        }
        catch (Exception ex)
        {
            MostrarError($"Error de conexión: {ex.Message}");
        }
        finally
        {
            // Ocultar indicador de carga
            BtnLogin.IsEnabled = true;
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
        }
    }

    private void MostrarError(string mensaje)
    {
        LabelError.Text = $"⚠️ {mensaje}";
        LabelError.IsVisible = true;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Limpiar campos
        EntryUsuario.Text = string.Empty;
        EntryPassword.Text = string.Empty;
        LabelError.IsVisible = false;
    }
}