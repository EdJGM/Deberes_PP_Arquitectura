using ConUni_RestFul_Dotnet_GR06.Modelos;
using ConUni_RestFul_Dotnet_GR06.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConUni_RestFul_Dotnet_GR06.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversorUnidades : ControllerBase
    {
        private readonly ConversorTemperatura _temp = new();
        private readonly ConversorLongitud _long = new();
        private readonly ConversorMasa _masa = new();

        // ==================== AUTENTICACIÓN ====================
        [HttpPost("login")]
        public ActionResult<RespuestaAutenticacion> Login([FromBody] Usuario user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new RespuestaAutenticacion(false, "Usuario y Contraseña son requeridos"));
            }

            var usuarioAutenticado = AutenticacionService.Autenticar(user.Username, user.Password);

            if (usuarioAutenticado != null)
            {
                return Ok(new RespuestaAutenticacion(true, "Login exitoso", usuarioAutenticado));
            }

            return Unauthorized(new RespuestaAutenticacion(false, "Credenciales inválidas"));
        }

        // ==================== CONVERSIONES DE TEMPERATURA ====================
        [HttpGet("temperatura/celsius-fahrenheit/{celsius}")]
        public ActionResult<double> CelsiusAFahrenheit(double celsius)
        {
            try
            {
                var resultado = _temp.CelsiusAFahrenheit(celsius);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("temperatura/fahrenheit-celsius/{fahrenheit}")]
        public ActionResult<double> FahrenheitACelsius(double fahrenheit)
        {
            try
            {
                var resultado = _temp.FahrenheitACelsius(fahrenheit);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("temperatura/celsius-kelvin/{celsius}")]
        public ActionResult<double> CelsiusAKelvin(double celsius)
        {
            try
            {
                var resultado = _temp.CelsiusAKelvin(celsius);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("temperatura/kelvin-celsius/{kelvin}")]
        public ActionResult<double> KelvinACelsius(double kelvin)
        {
            try
            {
                var resultado = _temp.KelvinACelsius(kelvin);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("temperatura/fahrenheit-kelvin/{fahrenheit}")]
        public ActionResult<double> FahrenheitAKelvin(double fahrenheit)
        {
            try
            {
                var resultado = _temp.FahrenheitAKelvin(fahrenheit);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("temperatura/kelvin-fahrenheit/{kelvin}")]
        public ActionResult<double> KelvinAFahrenheit(double kelvin)
        {
            try
            {
                var resultado = _temp.KelvinAFahrenheit(kelvin);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        // ==================== CONVERSIONES DE LONGITUD ====================
        [HttpGet("longitud/metro-kilometro/{metros}")]
        public ActionResult<double> MetroAKilometro(double metros)
        {
            try
            {
                var resultado = _long.MetroAKilometro(metros);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("longitud/kilometro-metro/{kilometros}")]
        public ActionResult<double> KilometroAMetro(double kilometros)
        {
            try
            {
                var resultado = _long.KilometroAMetro(kilometros);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("longitud/metro-milla/{metros}")]
        public ActionResult<double> MetroAMilla(double metros)
        {
            try
            {
                var resultado = _long.MetroAMilla(metros);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("longitud/milla-metro/{millas}")]
        public ActionResult<double> MillaAMetro(double millas)
        {
            try
            {
                var resultado = _long.MillaAMetro(millas);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("longitud/kilometro-milla/{kilometros}")]
        public ActionResult<double> KilometroAMilla(double kilometros)
        {
            try
            {
                var resultado = _long.KilometroAMilla(kilometros);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("longitud/milla-kilometro/{millas}")]
        public ActionResult<double> MillaAKilometro(double millas)
        {
            try
            {
                var resultado = _long.MillaAKilometro(millas);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        // ==================== CONVERSIONES DE MASA ====================
        [HttpGet("masa/kilogramo-gramo/{kilogramos}")]
        public ActionResult<double> KilogramoAGramo(double kilogramos)
        {
            try
            {
                var resultado = _masa.KilogramoAGramo(kilogramos);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("masa/gramo-kilogramo/{gramos}")]
        public ActionResult<double> GramoAKilogramo(double gramos)
        {
            try
            {
                var resultado = _masa.GramoAKilogramo(gramos);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("masa/kilogramo-libra/{kilogramos}")]
        public ActionResult<double> KilogramoALibra(double kilogramos)
        {
            try
            {
                var resultado = _masa.KilogramoALibra(kilogramos);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("masa/libra-kilogramo/{libras}")]
        public ActionResult<double> LibraAKilogramo(double libras)
        {
            try
            {
                var resultado = _masa.LibraAKilogramo(libras);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("masa/gramo-libra/{gramos}")]
        public ActionResult<double> GramoALibra(double gramos)
        {
            try
            {
                var resultado = _masa.GramoALibra(gramos);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        [HttpGet("masa/libra-gramo/{libras}")]
        public ActionResult<double> LibraAGramo(double libras)
        {
            try
            {
                var resultado = _masa.LibraAGramo(libras);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }

        // ==================== ENDPOINTS DE INFORMACIÓN ====================
        [HttpGet("tipos-conversion")]
        public ActionResult<object> ObtenerTiposConversion()
        {
            return Ok(new
            {
                temperatura = new[]
                {
                    "celsius-fahrenheit", "fahrenheit-celsius",
                    "celsius-kelvin", "kelvin-celsius",
                    "fahrenheit-kelvin", "kelvin-fahrenheit"
                },
                longitud = new[]
                {
                    "metro-kilometro", "kilometro-metro",
                    "metro-milla", "milla-metro",
                    "kilometro-milla", "milla-kilometro"
                },
                masa = new[]
                {
                    "kilogramo-gramo", "gramo-kilogramo",
                    "kilogramo-libra", "libra-kilogramo",
                    "gramo-libra", "libra-gramo"
                }
            });
        }

        [HttpGet("health")]
        public ActionResult<object> HealthCheck()
        {
            return Ok(new
            {
                status = "OK",
                timestamp = DateTime.UtcNow,
                version = "1.0.0",
                servicesAvailable = new
                {
                    temperatura = true,
                    longitud = true,
                    masa = true,
                    autenticacion = true
                }
            });
        }
    }
}