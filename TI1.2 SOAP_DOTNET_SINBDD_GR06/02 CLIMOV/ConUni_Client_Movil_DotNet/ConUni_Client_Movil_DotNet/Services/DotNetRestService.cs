using System.Text;
using System.Text.Json;

namespace ConUni_Client_Movil_DotNet.Services
{
    /// <summary>
    /// Implementación del servicio REST .NET
    /// </summary>
    public class DotNetRestService : IConversorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private string? _usuarioActual;
        private bool _disponible;

        public DotNetRestService(string baseUrl = "https://192.168.1.4:7041/api/ConversorUnidades")
        {
            // Solo en desarrollo - ignorar certificados SSL
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            
            _httpClient = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            _baseUrl = baseUrl;
            _disponible = true;

            // Headers REST estándar
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        // ==================== PROPIEDADES ====================
        public string? UsuarioActual => _usuarioActual;
        public bool UsuarioAutenticado => !string.IsNullOrEmpty(_usuarioActual);
        public string TipoServicio => "DOTNET-REST";
        public bool EstaDisponible => _disponible;

        // ==================== AUTENTICACIÓN ====================
        public async Task<RespuestaLogin> LoginAsync(string username, string password)
        {
            try
            {
                var loginRequest = new
                {
                    Username = username,
                    Password = password
                };

                var json = JsonSerializer.Serialize(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var respuestaAuth = JsonSerializer.Deserialize<RespuestaAutenticacionRest>(responseJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (respuestaAuth != null && respuestaAuth.Exitoso)
                    {
                        _usuarioActual = respuestaAuth.Username ?? username;
                        _disponible = true;
                        return new RespuestaLogin(true, $"¡Bienvenido {_usuarioActual}! (REST .NET)", _usuarioActual, TipoServicio);
                    }
                    else
                    {
                        return new RespuestaLogin(false, respuestaAuth?.Mensaje ?? "Credenciales inválidas", TipoServicio);
                    }
                }
                else
                {
                    return new RespuestaLogin(false, $"Error HTTP: {response.StatusCode}", TipoServicio);
                }
            }
            catch (Exception ex)
            {
                _disponible = false;
                return new RespuestaLogin(false, $"Error REST .NET: {ex.Message}", TipoServicio);
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
                // Test directo al health endpoint
                var healthResponse = await _httpClient.GetAsync($"{_baseUrl}/health");
                if (healthResponse.IsSuccessStatusCode)
                {
                    _disponible = true;
                    return true;
                }
    
                _disponible = false;
                return false;
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
            return await GetConversionAsync($"temperatura/celsius-fahrenheit/{celsius}");
        }

        public async Task<double> FahrenheitACelsiusAsync(double fahrenheit)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"temperatura/fahrenheit-celsius/{fahrenheit}");
        }

        public async Task<double> CelsiusAKelvinAsync(double celsius)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"temperatura/celsius-kelvin/{celsius}");
        }

        public async Task<double> KelvinACelsiusAsync(double kelvin)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"temperatura/kelvin-celsius/{kelvin}");
        }

        public async Task<double> FahrenheitAKelvinAsync(double fahrenheit)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"temperatura/fahrenheit-kelvin/{fahrenheit}");
        }

        public async Task<double> KelvinAFahrenheitAsync(double kelvin)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"temperatura/kelvin-fahrenheit/{kelvin}");
        }

        // ==================== LONGITUD ====================
        public async Task<double> MetroAKilometroAsync(double metros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"longitud/metro-kilometro/{metros}");
        }

        public async Task<double> KilometroAMetroAsync(double kilometros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"longitud/kilometro-metro/{kilometros}");
        }

        public async Task<double> MetroAMillaAsync(double metros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"longitud/metro-milla/{metros}");
        }

        public async Task<double> MillaAMetroAsync(double millas)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"longitud/milla-metro/{millas}");
        }

        public async Task<double> KilometroAMillaAsync(double kilometros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"longitud/kilometro-milla/{kilometros}");
        }

        public async Task<double> MillaAKilometroAsync(double millas)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"longitud/milla-kilometro/{millas}");
        }

        // ==================== MASA ====================
        public async Task<double> KilogramoAGramoAsync(double kilogramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"masa/kilogramo-gramo/{kilogramos}");
        }

        public async Task<double> GramoAKilogramoAsync(double gramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"masa/gramo-kilogramo/{gramos}");
        }

        public async Task<double> KilogramoALibraAsync(double kilogramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"masa/kilogramo-libra/{kilogramos}");
        }

        public async Task<double> LibraAKilogramoAsync(double libras)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"masa/libra-kilogramo/{libras}");
        }

        public async Task<double> GramoALibraAsync(double gramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"masa/gramo-libra/{gramos}");
        }

        public async Task<double> LibraAGramoAsync(double libras)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"masa/libra-gramo/{libras}");
        }

        // ==================== MÉTODOS HELPER REST ====================
        private async Task<double> GetConversionAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                // La API .NET REST devuelve directamente el número
                if (double.TryParse(content, out var result))
                {
                    return result;
                }

                throw new Exception($"No se pudo parsear la respuesta: {content}");
            }
            catch (HttpRequestException ex)
            {
                _disponible = false;
                throw new Exception($"Error de conexión REST .NET: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en conversión REST .NET: {ex.Message}");
            }
        }

        // ==================== MODELOS DTO ====================
        private class RespuestaAutenticacionRest
        {
            public bool Exitoso { get; set; }
            public string? Mensaje { get; set; }
            public string? Username { get; set; }
        }

        // ==================== CLEANUP ====================
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
