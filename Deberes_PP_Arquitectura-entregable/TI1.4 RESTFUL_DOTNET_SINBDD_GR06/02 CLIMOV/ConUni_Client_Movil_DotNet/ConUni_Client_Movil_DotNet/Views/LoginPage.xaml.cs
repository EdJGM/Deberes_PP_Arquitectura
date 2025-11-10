using ConUni_Client_Movil_DotNet.Services;

namespace ConUni_Client_Movil_DotNet.Views;

public partial class LoginPage : ContentPage
{
    private IConversorService? _servicioSeleccionado;

    public LoginPage()
    {
        InitializeComponent();
     
        // Seleccionar .NET SOAP por defecto
        PickerTipoServicio.SelectedIndex = 0;
        _servicioSeleccionado = new DotNetSoapService();
    }

    // Evento para manejar cambio de servicio
    private async void PickerTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
  var picker = sender as Picker;
     var selectedIndex = picker.SelectedIndex;

        LabelEstadoServicio.Text = "⏳ Verificando servicio...";
        LabelEstadoServicio.TextColor = Color.FromArgb("#FFC107");

     try
        {
    // Crear instancia del servicio seleccionado
       _servicioSeleccionado = selectedIndex switch
     {
  0 => new DotNetSoapService(),     // .NET SOAP
                1 => new JavaSoapService(),       // Java SOAP  
         2 => new DotNetRestService(),     // .NET REST
        3 => new JavaRestService(),       // Java REST
     _ => new DotNetSoapService()      // Fallback
   };

            // Test de conexión del servicio específico
            var disponible = await _servicioSeleccionado.TestConexionAsync();
      
            if (disponible)
       {
    LabelEstadoServicio.Text = $"✅ {_servicioSeleccionado.TipoServicio} disponible";
   LabelEstadoServicio.TextColor = Color.FromArgb("#28A745");
        }
            else
         {
           LabelEstadoServicio.Text = $"⚠️ {_servicioSeleccionado.TipoServicio} no disponible";
   LabelEstadoServicio.TextColor = Color.FromArgb("#DC3545");
            }
        }
     catch (Exception ex)
        {
     LabelEstadoServicio.Text = "❌ Error al verificar servicio";
            LabelEstadoServicio.TextColor = Color.FromArgb("#DC3545");
        }
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

        if (_servicioSeleccionado == null)
        {
       MostrarError("Por favor seleccione un tipo de servicio");
 return;
    }

        // Mostrar indicador de carga
        BtnLogin.IsEnabled = false;
        LoadingIndicator.IsVisible = true;
    LoadingIndicator.IsRunning = true;
        LabelError.IsVisible = false;

        try
     {
// Usar el servicio seleccionado específicamente
    var respuesta = await _servicioSeleccionado.LoginAsync(
     EntryUsuario.Text.Trim(),
   EntryPassword.Text
      );

  if (respuesta.Exitoso)
   {
         await DisplayAlert("✓ Éxito", respuesta.Mensaje, "OK");
           
                // Crear ServiceManager y configurarlo con el servicio exitoso
     var serviceManager = new ServiceManager();
      await serviceManager.CambiarServicioAsync(_servicioSeleccionado.TipoServicio);

                // Hacer login en el ServiceManager
              await serviceManager.LoginAsync(EntryUsuario.Text.Trim(), EntryPassword.Text);
           
   await Navigation.PushAsync(new MenuPage(serviceManager));
          }
            else
         {
   MostrarError(respuesta.Mensaje);
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