using ConUni_RestFull_DOTNET_GR06.ec.edu.monster.modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConUni_RestFull_DOTNET_GR06.ec.edu.monster.servicios
{
    /// <summary>
    /// Cliente HTTP para consumir la API REST de conversión de unidades
    /// </summary>
    public class ConversorApiCliente
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private bool _autenticado = false;
        private string _usuarioActual = "";

        public ConversorApiCliente(string baseUrl = "http://localhost:5174")
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        // ==================== AUTENTICACIÓN ====================

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var usuario = new Usuario(username, password);
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/ConversorUnidades/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<RespuestaAutenticacion>(jsonResponse);

                    if (respuesta != null && respuesta.Exitoso)
                    {
                        _autenticado = true;
                        _usuarioActual = respuesta.Username;
                        Console.WriteLine($"\n✓ {respuesta.Mensaje}");
                        Console.WriteLine($"  Usuario: {respuesta.Username}");
                        return true;
                    }
                }

                Console.WriteLine("\n✗ Credenciales inválidas");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error al autenticar: {ex.Message}");
                return false;
            }
        }

        public bool EstaAutenticado() => _autenticado;
        public string ObtenerUsuarioActual() => _usuarioActual;

        // ==================== CONVERSIONES DE TEMPERATURA ====================

        public async Task<double?> ConvertirTemperaturaAsync(string origen, string destino, double valor)
        {
            try
            {
                var endpoint = $"/api/ConversorUnidades/temperatura/{origen.ToLower()}-{destino.ToLower()}/{valor}";
                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsStringAsync();
                    return double.Parse(resultado);
                }

                Console.WriteLine($"Error en la conversión: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        // ==================== CONVERSIONES DE LONGITUD ====================

        public async Task<double?> ConvertirLongitudAsync(string origen, string destino, double valor)
        {
            try
            {
                var endpoint = $"/api/ConversorUnidades/longitud/{origen.ToLower()}-{destino.ToLower()}/{valor}";
                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsStringAsync();
                    return double.Parse(resultado);
                }

                Console.WriteLine($"Error en la conversión: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        // ==================== CONVERSIONES DE MASA ====================

        public async Task<double?> ConvertirMasaAsync(string origen, string destino, double valor)
        {
            try
            {
                var endpoint = $"/api/ConversorUnidades/masa/{origen.ToLower()}-{destino.ToLower()}/{valor}";
                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsStringAsync();
                    return double.Parse(resultado);
                }

                Console.WriteLine($"Error en la conversión: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        // ==================== INFORMACIÓN ====================

        public async Task<bool> VerificarSaludAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/ConversorUnidades/health");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task MostrarTiposConversionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/ConversorUnidades/tipos-conversion");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("\nTipos de conversión disponibles:");
                    Console.WriteLine(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
