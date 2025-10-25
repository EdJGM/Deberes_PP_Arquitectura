using ConUniServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConUni_Client_Movil_DotNet.Services
{
    /// <summary>
    /// Servicio que encapsula las llamadas SOAP al servidor
    /// </summary>
    public class ConversorService
    {
        private readonly ConversorUnidadesWSClient _cliente;
        private string? _usuarioActual;

        public ConversorService()
        {
            // Crear cliente del servicio SOAP
            //_cliente = new ConversorUnidadesWSClient(
            //    ConversorUnidadesWSClient.EndpointConfiguration.BasicHttpBinding_IConversorUnidadesWS
            //);
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://192.168.1.4:61088/WebServices/ConversorUnidadesWS.svc");
            _cliente = new ConversorUnidadesWSClient(binding, endpoint);
        }

        public bool UsuarioAutenticado => !string.IsNullOrEmpty(_usuarioActual);
        public string? UsuarioActual => _usuarioActual;

        // ==================== AUTENTICACIÓN ====================

        public async Task<(bool exitoso, string mensaje)> LoginAsync(string username, string password)
        {
            try
            {
                var respuesta = await _cliente.LoginAsync(username, password);

                if (respuesta.Exitoso)
                {
                    _usuarioActual = respuesta.Username;
                    return (true, $"¡Bienvenido {_usuarioActual}!");
                }
                else
                {
                    return (false, respuesta.Mensaje);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al conectar: {ex.Message}");
            }
        }

        public void Logout()
        {
            _usuarioActual = null;
        }

        // ==================== TEMPERATURA ====================

        public async Task<double> CelsiusAFahrenheitAsync(double celsius)
        {
            return await _cliente.CelsiusAFahrenheitAsync(celsius);
        }

        public async Task<double> FahrenheitACelsiusAsync(double fahrenheit)
        {
            return await _cliente.FahrenheitACelsiusAsync(fahrenheit);
        }

        public async Task<double> CelsiusAKelvinAsync(double celsius)
        {
            return await _cliente.CelsiusAKelvinAsync(celsius);
        }

        public async Task<double> KelvinACelsiusAsync(double kelvin)
        {
            return await _cliente.KelvinACelsiusAsync(kelvin);
        }

        public async Task<double> FahrenheitAKelvinAsync(double fahrenheit)
        {
            return await _cliente.FahrenheitAKelvinAsync(fahrenheit);
        }

        public async Task<double> KelvinAFahrenheitAsync(double kelvin)
        {
            return await _cliente.KelvinAFahrenheitAsync(kelvin);
        }

        // ==================== LONGITUD ====================

        public async Task<double> MetroAKilometroAsync(double metros)
        {
            return await _cliente.MetroAKilometroAsync(metros);
        }

        public async Task<double> KilometroAMetroAsync(double kilometros)
        {
            return await _cliente.KilometroAMetroAsync(kilometros);
        }

        public async Task<double> MetroAMillaAsync(double metros)
        {
            return await _cliente.MetroAMillaAsync(metros);
        }

        public async Task<double> MillaAMetroAsync(double millas)
        {
            return await _cliente.MillaAMetroAsync(millas);
        }

        public async Task<double> KilometroAMillaAsync(double kilometros)
        {
            return await _cliente.KilometroAMillaAsync(kilometros);
        }

        public async Task<double> MillaAKilometroAsync(double millas)
        {
            return await _cliente.MillaAKilometroAsync(millas);
        }

        // ==================== MASA ====================

        public async Task<double> KilogramoAGramoAsync(double kilogramos)
        {
            return await _cliente.KilogramoAGramoAsync(kilogramos);
        }

        public async Task<double> GramoAKilogramoAsync(double gramos)
        {
            return await _cliente.GramoAKilogramoAsync(gramos);
        }

        public async Task<double> KilogramoALibraAsync(double kilogramos)
        {
            return await _cliente.KilogramoALibraAsync(kilogramos);
        }

        public async Task<double> LibraAKilogramoAsync(double libras)
        {
            return await _cliente.LibraAKilogramoAsync(libras);
        }

        public async Task<double> GramoALibraAsync(double gramos)
        {
            return await _cliente.GramoALibraAsync(gramos);
        }

        public async Task<double> LibraAGramoAsync(double libras)
        {
            return await _cliente.LibraAGramoAsync(libras);
        }
    }
}
