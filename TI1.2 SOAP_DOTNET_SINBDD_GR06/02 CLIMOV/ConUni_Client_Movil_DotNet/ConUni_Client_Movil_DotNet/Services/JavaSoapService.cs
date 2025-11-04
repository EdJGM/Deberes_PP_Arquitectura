using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Xml;
namespace ConUni_Client_Movil_DotNet.Services
{
    /// <summary>
    /// Implementación del servicio SOAP Java
    /// </summary>
    public class JavaSoapService : IConversorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private string? _usuarioActual;
        private bool _disponible;

        public JavaSoapService(string baseUrl = "http://192.168.1.4:8080/ConUni_Soap_Java_GR06/ConversorUnidadesWS")
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            _baseUrl = baseUrl;
            _disponible = true;

            // Solo agregar SOAPAction, Content-Type se maneja en SendSoapRequestAsync
            _httpClient.DefaultRequestHeaders.Add("SOAPAction", "");
        }

        // ==================== PROPIEDADES ====================
        public string? UsuarioActual => _usuarioActual;
        public bool UsuarioAutenticado => !string.IsNullOrEmpty(_usuarioActual);
        public string TipoServicio => "JAVA-SOAP";
        public bool EstaDisponible => _disponible;

        // ==================== AUTENTICACIÓN ====================
        public async Task<RespuestaLogin> LoginAsync(string username, string password)
        {
            try
            {
                var soapEnvelope = CreateLoginSoapEnvelope(username, password);
                var response = await SendSoapRequestAsync(soapEnvelope, "login");

                var exitoso = ParseBooleanResponse(response, "exitoso");
                var mensaje = ParseStringResponse(response, "mensaje");
                var usuarioRespuesta = ParseStringResponse(response, "username");

                if (exitoso)
                {
                    _usuarioActual = usuarioRespuesta ?? username;
                    _disponible = true;
                    return new RespuestaLogin(true, $"¡Bienvenido {_usuarioActual}! (SOAP Java)", _usuarioActual, TipoServicio);
                }
                else
                {
                    return new RespuestaLogin(false, mensaje ?? "Credenciales inválidas", TipoServicio);
                }
            }
            catch (Exception ex)
            {
                _disponible = false;
                return new RespuestaLogin(false, $"Error SOAP Java: {ex.Message}", TipoServicio);
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
                var respuesta = await LoginAsync("MONSTER", "MONSTER9");
                _disponible = respuesta.Exitoso;
                if (respuesta.Exitoso)
                {
                    Logout(); // No mantener la sesión del test
                }
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
            var soapEnvelope = CreateConversionSoapEnvelope("celsiusAFahrenheit", "celsius", celsius);
            var response = await SendSoapRequestAsync(soapEnvelope, "celsiusAFahrenheit");
            return ParseDoubleResponse(response);
        }

        public async Task<double> FahrenheitACelsiusAsync(double fahrenheit)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("fahrenheitACelsius", "fahrenheit", fahrenheit);
            var response = await SendSoapRequestAsync(soapEnvelope, "fahrenheitACelsius");
            return ParseDoubleResponse(response);
        }

        public async Task<double> CelsiusAKelvinAsync(double celsius)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("celsiusAKelvin", "celsius", celsius);
            var response = await SendSoapRequestAsync(soapEnvelope, "celsiusAKelvin");
            return ParseDoubleResponse(response);
        }

        public async Task<double> KelvinACelsiusAsync(double kelvin)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("kelvinACelsius", "kelvin", kelvin);
            var response = await SendSoapRequestAsync(soapEnvelope, "kelvinACelsius");
            return ParseDoubleResponse(response);
        }

        public async Task<double> FahrenheitAKelvinAsync(double fahrenheit)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("fahrenheitAKelvin", "fahrenheit", fahrenheit);
            var response = await SendSoapRequestAsync(soapEnvelope, "fahrenheitAKelvin");
            return ParseDoubleResponse(response);
        }

        public async Task<double> KelvinAFahrenheitAsync(double kelvin)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("kelvinAFahrenheit", "kelvin", kelvin);
            var response = await SendSoapRequestAsync(soapEnvelope, "kelvinAFahrenheit");
            return ParseDoubleResponse(response);
        }

        // ==================== LONGITUD ====================
        public async Task<double> MetroAKilometroAsync(double metros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("metroAKilometro", "metros", metros);
            var response = await SendSoapRequestAsync(soapEnvelope, "metroAKilometro");
            return ParseDoubleResponse(response);
        }

        public async Task<double> KilometroAMetroAsync(double kilometros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("kilometroAMetro", "kilometros", kilometros);
            var response = await SendSoapRequestAsync(soapEnvelope, "kilometroAMetro");
            return ParseDoubleResponse(response);
        }

        public async Task<double> MetroAMillaAsync(double metros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("metroAMilla", "metros", metros);
            var response = await SendSoapRequestAsync(soapEnvelope, "metroAMilla");
            return ParseDoubleResponse(response);
        }

        public async Task<double> MillaAMetroAsync(double millas)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("millaAMetro", "millas", millas);
            var response = await SendSoapRequestAsync(soapEnvelope, "millaAMetro");
            return ParseDoubleResponse(response);
        }

        public async Task<double> KilometroAMillaAsync(double kilometros)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("kilometroAMilla", "kilometros", kilometros);
            var response = await SendSoapRequestAsync(soapEnvelope, "kilometroAMilla");
            return ParseDoubleResponse(response);
        }

        public async Task<double> MillaAKilometroAsync(double millas)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("millaAKilometro", "millas", millas);
            var response = await SendSoapRequestAsync(soapEnvelope, "millaAKilometro");
            return ParseDoubleResponse(response);
        }

        // ==================== MASA ====================
        public async Task<double> KilogramoAGramoAsync(double kilogramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("kilogramoAGramo", "kilogramos", kilogramos);
            var response = await SendSoapRequestAsync(soapEnvelope, "kilogramoAGramo");
            return ParseDoubleResponse(response);
        }

        public async Task<double> GramoAKilogramoAsync(double gramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("gramoAKilogramo", "gramos", gramos);
            var response = await SendSoapRequestAsync(soapEnvelope, "gramoAKilogramo");
            return ParseDoubleResponse(response);
        }

        public async Task<double> KilogramoALibraAsync(double kilogramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("kilogramoALibra", "kilogramos", kilogramos);
            var response = await SendSoapRequestAsync(soapEnvelope, "kilogramoALibra");
            return ParseDoubleResponse(response);
        }

        public async Task<double> LibraAKilogramoAsync(double libras)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("libraAKilogramo", "libras", libras);
            var response = await SendSoapRequestAsync(soapEnvelope, "libraAKilogramo");
            return ParseDoubleResponse(response);
        }

        public async Task<double> GramoALibraAsync(double gramos)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("gramoALibra", "gramos", gramos);
            var response = await SendSoapRequestAsync(soapEnvelope, "gramoALibra");
            return ParseDoubleResponse(response);
        }

        public async Task<double> LibraAGramoAsync(double libras)
        {
            if (!UsuarioAutenticado) throw new UnauthorizedAccessException("Usuario no autenticado");
            var soapEnvelope = CreateConversionSoapEnvelope("libraAGramo", "libras", libras);
            var response = await SendSoapRequestAsync(soapEnvelope, "libraAGramo");
            return ParseDoubleResponse(response);
        }

        // ==================== MÉTODOS HELPER SOAP ====================
        private string CreateLoginSoapEnvelope(string username, string password)
        {
            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <ns2:login xmlns:ns2=""http://ws.monster.edu.ec/"">
      <username>{username}</username>
      <password>{password}</password>
    </ns2:login>
  </soap:Body>
</soap:Envelope>";
        }

        private string CreateConversionSoapEnvelope(string operation, string paramName, double value)
        {
            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <ns2:{operation} xmlns:ns2=""http://ws.monster.edu.ec/"">
      <{paramName}>{value}</{paramName}>
    </ns2:{operation}>
  </soap:Body>
</soap:Envelope>";
        }

        private async Task<string> SendSoapRequestAsync(string soapEnvelope, string operation)
        {
            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
      
    // Limpiar headers existentes y agregar los correctos
       content.Headers.Clear();
       content.Headers.Add("Content-Type", "text/xml; charset=utf-8");
            content.Headers.Add("SOAPAction", $"http://ws.monster.edu.ec/{operation}");

            var response = await _httpClient.PostAsync(_baseUrl, content);
    response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private bool ParseBooleanResponse(string soapResponse, string elementName)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(soapResponse);
               
    // Buscar el elemento directamente
                var node = doc.GetElementsByTagName(elementName)?[0];
                if (node != null && bool.TryParse(node.InnerText, out var result))
                {
                    return result;
                }
            
                // Buscar en loginResponse si no se encontró directamente
                var loginResponseNode = doc.GetElementsByTagName("loginResponse")?[0];
                 if (loginResponseNode != null)
                {
                    foreach (XmlNode child in loginResponseNode.ChildNodes)
                    {
                     if (child.Name == elementName && bool.TryParse(child.InnerText, out var childResult))
                   {
                      return childResult;
                    }
                  }
                }
           return false;
            }
            catch
            {
                return false;
            }
        }

        private string? ParseStringResponse(string soapResponse, string elementName)
        {
        try
     {
        var doc = new XmlDocument();
  doc.LoadXml(soapResponse);
         
   // Buscar el elemento directamente
        var node = doc.GetElementsByTagName(elementName)?[0];
      if (node != null)
          {
           return node.InnerText;
           }
  
       // Buscar en loginResponse si no se encontró directamente
       var loginResponseNode = doc.GetElementsByTagName("loginResponse")?[0];
        if (loginResponseNode != null)
      {
        foreach (XmlNode child in loginResponseNode.ChildNodes)
        {
       if (child.Name == elementName)
            {
    return child.InnerText;
          }
           }
                }
   
       return null;
            }
            catch
            {
return null;
            }
  }

        private double ParseDoubleResponse(string soapResponse)
        {
          try
   {
       var doc = new XmlDocument();
        doc.LoadXml(soapResponse);

         // Buscar el nodo de retorno en respuestas de operaciones
 var responseNodes = doc.GetElementsByTagName("*Response");
        if (responseNodes.Count > 0)
          {
               var responseNode = responseNodes[0];
       if (responseNode.FirstChild != null && double.TryParse(responseNode.FirstChild.InnerText, out var result))
          {
              return result;
            }
   }

            // Buscar directamente por "return" 
                var returnNode = doc.GetElementsByTagName("return")?[0];
           if (returnNode != null && double.TryParse(returnNode.InnerText, out var directResult))
           {
           return directResult;
    }

             throw new Exception("No se pudo parsear la respuesta SOAP");
  }
        catch (Exception ex)
   {
          throw new Exception($"Error parseando respuesta SOAP: {ex.Message}");
   }
        }

        // ==================== CLEANUP ====================
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
