using ConUniServiceReference;
using System.ServiceModel;

namespace ConUni_Client_Movil_DotNet.Services
{
    /// <summary>
    /// Implementación del servicio SOAP .NET
    /// </summary>
    public class DotNetSoapService : IConversorService
    {
        private readonly ConversorUnidadesWSClient _cliente;
        private string? _usuarioActual;
        private bool _disponible;

        public DotNetSoapService()
        {
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress("http://10.63.57.155:61088/WebServices/ConversorUnidadesWS.svc");
                _cliente = new ConversorUnidadesWSClient(binding, endpoint);
                _disponible = true;
            }
            catch
            {
                _disponible = false;
            }
        }

        // ==================== PROPIEDADES ====================
        public string? UsuarioActual => _usuarioActual;
        public bool UsuarioAutenticado => !string.IsNullOrEmpty(_usuarioActual);
        public string TipoServicio => "DOTNET-SOAP";
        public bool EstaDisponible => _disponible;

        // ==================== AUTENTICACIÓN ====================
        public async Task<RespuestaLogin> LoginAsync(string username, string password)
        {
            try
            {
                var respuesta = await _cliente.LoginAsync(username, password);

                if (respuesta.Exitoso)
                {
                    _usuarioActual = respuesta.Username;
                    _disponible = true;
                    return new RespuestaLogin(true, $"¡Bienvenido {_usuarioActual}! (SOAP .NET)", _usuarioActual, TipoServicio);
                }
                else
                {
                    return new RespuestaLogin(false, respuesta.Mensaje, TipoServicio);
                }
            }
            catch (Exception ex)
            {
                _disponible = false;
                return new RespuestaLogin(false, $"Error SOAP .NET: {ex.Message}", TipoServicio);
            }
        }

        public void Logout()
        {
            _usuarioActual = null;
        }

        public async Task<bool> TestConexionAsync()
        {
            try
            {
                // Test simple: intentar login con credenciales válidas
                var respuesta = await _cliente.LoginAsync("MONSTER", "MONSTER9");
                _disponible = respuesta.Exitoso;
                return _disponible;
            }
            catch
            {
                _disponible = false;
                return false;
            }
        }

        // ==================== TEMPERATURA ====================
        public async Task<double> CelsiusAFahrenheitAsync(double celsius)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.CelsiusAFahrenheitAsync(celsius);
        }

        public async Task<double> FahrenheitACelsiusAsync(double fahrenheit)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.FahrenheitACelsiusAsync(fahrenheit);
        }

        public async Task<double> CelsiusAKelvinAsync(double celsius)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.CelsiusAKelvinAsync(celsius);
        }

        public async Task<double> KelvinACelsiusAsync(double kelvin)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.KelvinACelsiusAsync(kelvin);
        }

        public async Task<double> FahrenheitAKelvinAsync(double fahrenheit)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.FahrenheitAKelvinAsync(fahrenheit);
        }

        public async Task<double> KelvinAFahrenheitAsync(double kelvin)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.KelvinAFahrenheitAsync(kelvin);
        }

        // ==================== LONGITUD ====================
        public async Task<double> MetroAKilometroAsync(double metros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.MetroAKilometroAsync(metros);
        }

        public async Task<double> KilometroAMetroAsync(double kilometros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.KilometroAMetroAsync(kilometros);
        }

        public async Task<double> MetroAMillaAsync(double metros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.MetroAMillaAsync(metros);
        }

        public async Task<double> MillaAMetroAsync(double millas)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.MillaAMetroAsync(millas);
        }

        public async Task<double> KilometroAMillaAsync(double kilometros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.KilometroAMillaAsync(kilometros);
        }

        public async Task<double> MillaAKilometroAsync(double millas)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.MillaAKilometroAsync(millas);
        }

        // ==================== MASA ====================
        public async Task<double> KilogramoAGramoAsync(double kilogramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.KilogramoAGramoAsync(kilogramos);
        }

        public async Task<double> GramoAKilogramoAsync(double gramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.GramoAKilogramoAsync(gramos);
        }

        public async Task<double> KilogramoALibraAsync(double kilogramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.KilogramoALibraAsync(kilogramos);
        }

        public async Task<double> LibraAKilogramoAsync(double libras)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.LibraAKilogramoAsync(libras);
        }

        public async Task<double> GramoALibraAsync(double gramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.GramoALibraAsync(gramos);
        }

        public async Task<double> LibraAGramoAsync(double libras)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await _cliente.LibraAGramoAsync(libras);
        }

        // ==================== CLEANUP ====================
        public void Dispose()
        {
            try
            {
                _cliente?.Close();
            }
            catch
            {
                _cliente?.Abort();
            }
        }
    }
}
