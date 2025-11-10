using ConUni_Client_Movil_DotNet.Services;

namespace ConUni_Client_Movil_DotNet.Views;

public partial class MasaPage : ContentPage
{
    private readonly ServiceManager _serviceManager;

    public MasaPage(ServiceManager serviceManager)
    {
        InitializeComponent();
        _serviceManager = serviceManager;

        PickerOrigen.SelectedIndex = 0;  // Kilogramo
        PickerDestino.SelectedIndex = 1; // Gramo
    }

    private async void BtnConvertir_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryValor.Text))
        {
            await DisplayAlert("⚠️ Error", "Por favor ingrese un valor", "OK");
            return;
        }

        if (!double.TryParse(EntryValor.Text, out double valor))
        {
            await DisplayAlert("⚠️ Error", "Por favor ingrese un número válido", "OK");
            return;
        }

        if (PickerOrigen.SelectedIndex == -1 || PickerDestino.SelectedIndex == -1)
        {
            await DisplayAlert("⚠️ Error", "Seleccione unidades", "OK");
            return;
        }

        if (PickerOrigen.SelectedIndex == PickerDestino.SelectedIndex)
        {
            await DisplayAlert("⚠️ Error", "Las unidades deben ser diferentes", "OK");
            return;
        }

        BtnConvertir.IsEnabled = false;
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        FrameResultado.IsVisible = false;

        try
        {
            double resultado = await RealizarConversion(
                valor,
                PickerOrigen.SelectedIndex,
                PickerDestino.SelectedIndex
            );

            MostrarResultado(resultado);
        }
        catch (Exception ex)
        {
            await DisplayAlert("❌ Error", $"Error: {ex.Message}", "OK");
        }
        finally
        {
            BtnConvertir.IsEnabled = true;
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
        }
    }

    private async Task<double> RealizarConversion(double valor, int origen, int destino)
    {
        // Kilogramo = 0, Gramo = 1, Libra = 2

        // Desde Kilogramo
        if (origen == 0 && destino == 1) // Kilogramo → Gramo
            return await _serviceManager.KilogramoAGramoAsync(valor);
        if (origen == 0 && destino == 2) // Kilogramo → Libra
            return await _serviceManager.KilogramoALibraAsync(valor);

        // Desde Gramo
        if (origen == 1 && destino == 0) // Gramo → Kilogramo
            return await _serviceManager.GramoAKilogramoAsync(valor);
        if (origen == 1 && destino == 2) // Gramo → Libra
            return await _serviceManager.GramoALibraAsync(valor);

        // Desde Libra
        if (origen == 2 && destino == 0) // Libra → Kilogramo
            return await _serviceManager.LibraAKilogramoAsync(valor);
        if (origen == 2 && destino == 1) // Libra → Gramo
            return await _serviceManager.LibraAGramoAsync(valor);

        throw new InvalidOperationException("Combinación no válida");
    }

    private void MostrarResultado(double resultado)
    {
        string unidadDestino = PickerDestino.SelectedIndex switch
        {
            0 => "kg",
            1 => "g",
            2 => "lb",
            _ => ""
        };

        LabelResultado.Text = $"{resultado:F4}";
        LabelUnidadResultado.Text = unidadDestino;
        FrameResultado.IsVisible = true;
    }

    private void EntryValor_TextChanged(object sender, TextChangedEventArgs e)
    {
        FrameResultado.IsVisible = false;
    }

    private void OnConversionChanged(object sender, EventArgs e)
    {
        FrameResultado.IsVisible = false;
    }
}