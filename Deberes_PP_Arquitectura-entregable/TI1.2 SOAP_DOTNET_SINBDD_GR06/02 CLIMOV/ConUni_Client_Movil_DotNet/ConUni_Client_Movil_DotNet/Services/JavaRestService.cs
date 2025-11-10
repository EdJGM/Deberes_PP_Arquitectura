using System.Text;
using System.Text.Json;

namespace ConUni_Client_Movil_DotNet.Services
{
    /// <summary>
    /// Implementación del servicio REST Java
    /// </summary>
    public class JavaRestService : IConversorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private string? _usuarioActual;
        private bool _disponible;

        public JavaRestService(string baseUrl = "http://10.63.57.155:8080/ConUni_RestFull_Java_GR06/api/ConversorUnidades")
        {
            _httpClient = new HttpClient
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
        public string TipoServicio => "JAVA-REST";
        public bool EstaDisponible => _disponible;

        // ==================== AUTENTICACIÓN ====================
        public async Task<RespuestaLogin> LoginAsync(string username, string password)
        {
            try
            {
                var loginRequest = new
                {
                    username = username,  // Java usa lowercase
                    password = password
                };

                var json = JsonSerializer.Serialize(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var respuestaAuth = JsonSerializer.Deserialize<RespuestaAutenticacionJavaRest>(responseJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (respuestaAuth != null && respuestaAuth.exitoso)
                    {
                        _usuarioActual = respuestaAuth.username ?? username;
                        _disponible = true;
                        return new RespuestaLogin(true, $"¡Bienvenido {_usuarioActual}! (REST Java)", _usuarioActual, TipoServicio);
                    }
                    else
                    {
                        return new RespuestaLogin(false, respuestaAuth?.mensaje ?? "Credenciales inválidas", TipoServicio);
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return new RespuestaLogin(false, "Credenciales inválidas", TipoServicio);
                }
                else
                {
                    return new RespuestaLogin(false, $"Error HTTP: {response.StatusCode}", TipoServicio);
                }
            }
            catch (Exception ex)
            {
                _disponible = false;
                return new RespuestaLogin(false, $"Error REST Java: {ex.Message}", TipoServicio);
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
                // Test directo al health endpoint - igual que DotNetRestService
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
            return await GetConversionAsync($"celsiusAFahrenheit/{celsius}");
        }

        public async Task<double> FahrenheitACelsiusAsync(double fahrenheit)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"fahrenheitACelsius/{fahrenheit}");
        }

        public async Task<double> CelsiusAKelvinAsync(double celsius)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"celsiusAKelvin/{celsius}");
        }

        public async Task<double> KelvinACelsiusAsync(double kelvin)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"kelvinACelsius/{kelvin}");
        }

        public async Task<double> FahrenheitAKelvinAsync(double fahrenheit)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"fahrenheitAKelvin/{fahrenheit}");
        }

        public async Task<double> KelvinAFahrenheitAsync(double kelvin)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"kelvinAFahrenheit/{kelvin}");
        }

        // ==================== LONGITUD ====================
        public async Task<double> MetroAKilometroAsync(double metros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"metroAKilometro/{metros}");
        }

        public async Task<double> KilometroAMetroAsync(double kilometros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"kilometroAMetro/{kilometros}");
        }

        public async Task<double> MetroAMillaAsync(double metros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"metroAMilla/{metros}");
        }

        public async Task<double> MillaAMetroAsync(double millas)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"millaAMetro/{millas}");
        }

        public async Task<double> KilometroAMillaAsync(double kilometros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"kilometroAMilla/{kilometros}");
        }

        public async Task<double> MillaAKilometroAsync(double millas)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"millaAKilometro/{millas}");
        }

        // ==================== MASA ====================
        public async Task<double> KilogramoAGramoAsync(double kilogramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"kilogramoAGramo/{kilogramos}");
        }

        public async Task<double> GramoAKilogramoAsync(double gramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"gramoAKilogramo/{gramos}");
        }

        public async Task<double> KilogramoALibraAsync(double kilogramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"kilogramoALibra/{kilogramos}");
        }

        public async Task<double> LibraAKilogramoAsync(double libras)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"libraAKilogramo/{libras}");
        }

        public async Task<double> GramoALibraAsync(double gramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"gramoALibra/{gramos}");
        }

        public async Task<double> LibraAGramoAsync(double libras)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            return await GetConversionAsync($"libraAGramo/{libras}");
        }

        // ==================== MÉTODOS HELPER REST ====================
        private async Task<double> GetConversionAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                // La API Java REST devuelve directamente el número
                if (double.TryParse(content, out var result))
                {
                    return result;
                }

                // Algunas veces puede venir en formato JSON, intentar parsearlo
                try
                {
                    var jsonDoc = JsonDocument.Parse(content);
                    if (jsonDoc.RootElement.ValueKind == JsonValueKind.Number)
                    {
                        return jsonDoc.RootElement.GetDouble();
                    }
                }
                catch
                {
                    // Si no es JSON válido, continuar con el error original
                }

                throw new Exception($"No se pudo parsear la respuesta: {content}");
            }
            catch (HttpRequestException ex)
            {
                _disponible = false;
                throw new Exception($"Error de conexión REST Java: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en conversión REST Java: {ex.Message}");
            }
        }

        // ==================== MODELOS DTO ====================
        private class RespuestaAutenticacionJavaRest
        {
            public bool exitoso { get; set; }  // Java usa lowercase
            public string? mensaje { get; set; }
            public string? username { get; set; }
        }

        // ==================== CLEANUP ====================
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
