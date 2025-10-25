using ConUni_Client_Movil_DotNet.Services;

namespace ConUni_Client_Movil_DotNet.Views;

public partial class LongitudPage : ContentPage
{
    private readonly ConversorService _conversorService;

    public LongitudPage(ConversorService conversorService)
    {
        InitializeComponent();
        _conversorService = conversorService;

        PickerOrigen.SelectedIndex = 0;  // Metro
        PickerDestino.SelectedIndex = 1; // Kilómetro
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
        // Metro = 0, Kilómetro = 1, Milla = 2

        // Desde Metro
        if (origen == 0 && destino == 1) // Metro → Kilómetro
            return await _conversorService.MetroAKilometroAsync(valor);
        if (origen == 0 && destino == 2) // Metro → Milla
            return await _conversorService.MetroAMillaAsync(valor);

        // Desde Kilómetro
        if (origen == 1 && destino == 0) // Kilómetro → Metro
            return await _conversorService.KilometroAMetroAsync(valor);
        if (origen == 1 && destino == 2) // Kilómetro → Milla
            return await _conversorService.KilometroAMillaAsync(valor);

        // Desde Milla
        if (origen == 2 && destino == 0) // Milla → Metro
            return await _conversorService.MillaAMetroAsync(valor);
        if (origen == 2 && destino == 1) // Milla → Kilómetro
            return await _conversorService.MillaAKilometroAsync(valor);

        throw new InvalidOperationException("Combinación no válida");
    }

    private void MostrarResultado(double resultado)
    {
        string unidadDestino = PickerDestino.SelectedIndex switch
        {
            0 => "m",
            1 => "km",
            2 => "mi",
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