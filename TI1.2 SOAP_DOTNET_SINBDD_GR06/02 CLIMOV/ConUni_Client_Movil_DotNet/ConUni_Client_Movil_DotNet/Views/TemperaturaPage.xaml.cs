using ConUni_Client_Movil_DotNet.Services;

namespace ConUni_Client_Movil_DotNet.Views;

public partial class TemperaturaPage : ContentPage
{
    private readonly ConversorService _conversorService;

    public TemperaturaPage(ConversorService conversorService)
    {
        InitializeComponent();
        _conversorService = conversorService;

        // Seleccionar valores por defecto
        PickerOrigen.SelectedIndex = 0;  // Celsius
        PickerDestino.SelectedIndex = 1; // Fahrenheit
    }

    private async void BtnConvertir_Clicked(object sender, EventArgs e)
    {
        // Validar entrada
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
            await DisplayAlert("⚠️ Error", "Seleccione unidades de origen y destino", "OK");
            return;
        }

        if (PickerOrigen.SelectedIndex == PickerDestino.SelectedIndex)
        {
            await DisplayAlert("⚠️ Error", "Las unidades deben ser diferentes", "OK");
            return;
        }

        // Mostrar loading
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

            // Mostrar resultado
            MostrarResultado(resultado);
        }
        catch (Exception ex)
        {
            await DisplayAlert("❌ Error", $"Error al realizar la conversión: {ex.Message}", "OK");
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
        // Celsius = 0, Fahrenheit = 1, Kelvin = 2

        // Desde Celsius
        if (origen == 0 && destino == 1) // Celsius → Fahrenheit
            return await _conversorService.CelsiusAFahrenheitAsync(valor);
        if (origen == 0 && destino == 2) // Celsius → Kelvin
            return await _conversorService.CelsiusAKelvinAsync(valor);

        // Desde Fahrenheit
        if (origen == 1 && destino == 0) // Fahrenheit → Celsius
            return await _conversorService.FahrenheitACelsiusAsync(valor);
        if (origen == 1 && destino == 2) // Fahrenheit → Kelvin
            return await _conversorService.FahrenheitAKelvinAsync(valor);

        // Desde Kelvin
        if (origen == 2 && destino == 0) // Kelvin → Celsius
            return await _conversorService.KelvinACelsiusAsync(valor);
        if (origen == 2 && destino == 1) // Kelvin → Fahrenheit
            return await _conversorService.KelvinAFahrenheitAsync(valor);

        throw new InvalidOperationException("Combinación de unidades no válida");
    }

    private void MostrarResultado(double resultado)
    {
        string unidadDestino = PickerDestino.SelectedIndex switch
        {
            0 => "°C",
            1 => "°F",
            2 => "K",
            _ => ""
        };

        LabelResultado.Text = $"{resultado:F4}";
        LabelUnidadResultado.Text = unidadDestino;
        FrameResultado.IsVisible = true;
    }

    private void EntryValor_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Ocultar resultado cuando cambia el valor
        FrameResultado.IsVisible = false;
    }

    private void OnConversionChanged(object sender, EventArgs e)
    {
        // Ocultar resultado cuando cambian las unidades
        FrameResultado.IsVisible = false;
    }
}