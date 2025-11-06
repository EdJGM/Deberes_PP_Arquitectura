
namespace ConUni_Client_Movil_DotNet.Services
{
    /// <summary>
    /// Orquestador automático de servicios de conversión
    /// Implementa fallback automático: .NET SOAP → Java SOAP → .NET REST → Java REST
    /// </summary>
    public class ServiceManager : IConversorService
    {
        private readonly List<IConversorService> _servicios;
        private IConversorService? _servicioActivo;
        private readonly SemaphoreSlim _semaforo;

        public ServiceManager()
        {
            // Orden de prioridad para fallback automático
            _servicios = new List<IConversorService>
            {
                new DotNetSoapService(),     // Prioridad 1: Más rápido
                new JavaSoapService(),       // Prioridad 2: Backup SOAP
                new DotNetRestService(),     // Prioridad 3: REST alternativo
                new JavaRestService()        // Prioridad 4: Última opción
            };

            _semaforo = new SemaphoreSlim(1, 1);
        }

        // ==================== PROPIEDADES ====================
        public string? UsuarioActual => _servicioActivo?.UsuarioActual;
        public bool UsuarioAutenticado => _servicioActivo?.UsuarioAutenticado ?? false;
        public string TipoServicio => _servicioActivo?.TipoServicio ?? "NO-CONECTADO";
        public bool EstaDisponible => _servicioActivo?.EstaDisponible ?? false;

        // ==================== GESTIÓN DE SERVICIOS ====================

        /// <summary>
        /// Encuentra automáticamente el primer servicio disponible
        /// </summary>
        public async Task<IConversorService?> DetectarServicioDisponibleAsync()
        {
            await _semaforo.WaitAsync();
            try
            {
                foreach (var servicio in _servicios)
                {
                    try
                    {
                        var disponible = await servicio.TestConexionAsync();
                        if (disponible)
                        {
                            return servicio;
                        }
                    }
                    catch
                    {
                        // Continuar con el siguiente servicio
                        continue;
                    }
                }
                return null;
            }
            finally
            {
                _semaforo.Release();
            }
        }

        /// <summary>
        /// Obtiene el estado de todos los servicios
        /// </summary>
        public async Task<List<EstadoServicio>> ObtenerEstadoServiciosAsync()
        {
            var estados = new List<EstadoServicio>();

            var tasks = _servicios.Select(async servicio =>
            {
                try
                {
                    var disponible = await servicio.TestConexionAsync();
                    return new EstadoServicio
                    {
                        TipoServicio = servicio.TipoServicio,
                        Disponible = disponible,
                        EsActivo = servicio == _servicioActivo,
                        Mensaje = disponible ? "✅ Disponible" : "❌ No disponible"
                    };
                }
                catch (Exception ex)
                {
                    return new EstadoServicio
                    {
                        TipoServicio = servicio.TipoServicio,
                        Disponible = false,
                        EsActivo = false,
                        Mensaje = $"❌ Error: {ex.Message}"
                    };
                }
            });

            var resultados = await Task.WhenAll(tasks);
            return resultados.ToList();
        }

        /// <summary>
        /// Cambia manualmente a un servicio específico
        /// </summary>
        public async Task<bool> CambiarServicioAsync(string tipoServicio)
        {
            await _semaforo.WaitAsync();
            try
            {
                var servicio = _servicios.FirstOrDefault(s => s.TipoServicio == tipoServicio);
                if (servicio == null)
                {
                    return false;
                }

                var disponible = await servicio.TestConexionAsync();
                if (disponible)
                {
                    // Si hay usuario logueado, intentar trasladar la sesión
                    if (_servicioActivo?.UsuarioAutenticado == true)
                    {
                        var usuarioActual = _servicioActivo.UsuarioActual;
                        _servicioActivo.Logout();

                        // Intentar login en el nuevo servicio (asumimos MONSTER/MONSTER9)
                        var respuestaLogin = await servicio.LoginAsync("MONSTER", "MONSTER9");
                        if (respuestaLogin.Exitoso)
                        {
                            _servicioActivo = servicio;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        _servicioActivo = servicio;
                        return true;
                    }
                }

                return false;
            }
            finally
            {
                _semaforo.Release();
            }
        }

        // ==================== AUTENTICACIÓN ====================
        public async Task<RespuestaLogin> LoginAsync(string username, string password)
        {
            await _semaforo.WaitAsync();
            try
            {
                // Si ya hay un servicio activo, intentar usarlo primero
                if (_servicioActivo != null)
                {
                    try
                    {
                        var respuesta = await _servicioActivo.LoginAsync(username, password);
                        if (respuesta.Exitoso)
                        {
                            return respuesta;
                        }
                    }
                    catch
                    {
                        // Marcar servicio como no disponible y continuar con fallback
                        _servicioActivo = null;
                    }
                }

                // Intentar login con cada servicio hasta encontrar uno disponible
                foreach (var servicio in _servicios)
                {
                    try
                    {
                        var respuesta = await servicio.LoginAsync(username, password);
                        if (respuesta.Exitoso)
                        {
                            _servicioActivo = servicio;
                            return new RespuestaLogin(
                                true,
                                $"{respuesta.Mensaje} [Auto: {servicio.TipoServicio}]",
                                respuesta.Username,
                                $"AUTO-{servicio.TipoServicio}"
                            );
                        }
                    }
                    catch
                    {
                        // Continuar con el siguiente servicio
                        continue;
                    }
                }

                // Si ningún servicio funcionó
                return new RespuestaLogin(false, "❌ Ningún servicio disponible", "FALLBACK-FAILED");
            }
            finally
            {
                _semaforo.Release();
            }
        }

        public void Logout()
        {
            _servicioActivo?.Logout();
        }

        public async Task<bool> TestConexionAsync()
        {
            var servicioDisponible = await DetectarServicioDisponibleAsync();
            return servicioDisponible != null;
        }

        // ==================== MÉTODOS DE CONVERSIÓN DELEGADOS ====================
        // Todos los métodos delegan al servicio activo con fallback automático

        private async Task<T> EjecutarConFallbackAsync<T>(Func<IConversorService, Task<T>> operacion)
        {
            if (_servicioActivo == null)
            {
                throw new InvalidOperationException("No hay usuario autenticado o servicio disponible");
            }

            try
            {
                return await operacion(_servicioActivo);
            }
            catch
            {
                // Intentar fallback automático
                await _semaforo.WaitAsync();
                try
                {
                    var nuevoServicio = await DetectarServicioDisponibleAsync();
                    if (nuevoServicio != null && nuevoServicio != _servicioActivo)
                    {
                        // Trasladar sesión al nuevo servicio
                        var respuestaLogin = await nuevoServicio.LoginAsync("MONSTER", "MONSTER9");
                        if (respuestaLogin.Exitoso)
                        {
                            _servicioActivo.Logout();
                            _servicioActivo = nuevoServicio;
                            return await operacion(_servicioActivo);
                        }
                    }
                }
                finally
                {
                    _semaforo.Release();
                }

                // Si el fallback también falla, re-lanzar la excepción original
                throw;
            }
        }

        // ==================== TEMPERATURA ====================
        public async Task<double> CelsiusAFahrenheitAsync(double celsius) =>
            await EjecutarConFallbackAsync(s => s.CelsiusAFahrenheitAsync(celsius));

        public async Task<double> FahrenheitACelsiusAsync(double fahrenheit) =>
            await EjecutarConFallbackAsync(s => s.FahrenheitACelsiusAsync(fahrenheit));

        public async Task<double> CelsiusAKelvinAsync(double celsius) =>
            await EjecutarConFallbackAsync(s => s.CelsiusAKelvinAsync(celsius));

        public async Task<double> KelvinACelsiusAsync(double kelvin) =>
            await EjecutarConFallbackAsync(s => s.KelvinACelsiusAsync(kelvin));

        public async Task<double> FahrenheitAKelvinAsync(double fahrenheit) =>
            await EjecutarConFallbackAsync(s => s.FahrenheitAKelvinAsync(fahrenheit));

        public async Task<double> KelvinAFahrenheitAsync(double kelvin) =>
            await EjecutarConFallbackAsync(s => s.KelvinAFahrenheitAsync(kelvin));

        // ==================== LONGITUD ====================
        public async Task<double> MetroAKilometroAsync(double metros) =>
            await EjecutarConFallbackAsync(s => s.MetroAKilometroAsync(metros));

        public async Task<double> KilometroAMetroAsync(double kilometros) =>
            await EjecutarConFallbackAsync(s => s.KilometroAMetroAsync(kilometros));

        public async Task<double> MetroAMillaAsync(double metros) =>
            await EjecutarConFallbackAsync(s => s.MetroAMillaAsync(metros));

        public async Task<double> MillaAMetroAsync(double millas) =>
            await EjecutarConFallbackAsync(s => s.MillaAMetroAsync(millas));

        public async Task<double> KilometroAMillaAsync(double kilometros) =>
            await EjecutarConFallbackAsync(s => s.KilometroAMillaAsync(kilometros));

        public async Task<double> MillaAKilometroAsync(double millas) =>
            await EjecutarConFallbackAsync(s => s.MillaAKilometroAsync(millas));

        // ==================== MASA ====================
        public async Task<double> KilogramoAGramoAsync(double kilogramos) =>
            await EjecutarConFallbackAsync(s => s.KilogramoAGramoAsync(kilogramos));

        public async Task<double> GramoAKilogramoAsync(double gramos) =>
            await EjecutarConFallbackAsync(s => s.GramoAKilogramoAsync(gramos));

        public async Task<double> KilogramoALibraAsync(double kilogramos) =>
            await EjecutarConFallbackAsync(s => s.KilogramoALibraAsync(kilogramos));

        public async Task<double> LibraAKilogramoAsync(double libras) =>
            await EjecutarConFallbackAsync(s => s.LibraAKilogramoAsync(libras));

        public async Task<double> GramoALibraAsync(double gramos) =>
            await EjecutarConFallbackAsync(s => s.GramoALibraAsync(gramos));

        public async Task<double> LibraAGramoAsync(double libras) =>
            await EjecutarConFallbackAsync(s => s.LibraAGramoAsync(libras));

        // ==================== CLEANUP ====================
        public void Dispose()
        {
            foreach (var servicio in _servicios)
            {
                if (servicio is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _semaforo?.Dispose();
        }
    }

    /// <summary>
    /// Modelo para mostrar el estado de cada servicio
    /// </summary>
    public class EstadoServicio
    {
        public string TipoServicio { get; set; } = "";
        public bool Disponible { get; set; }
        public bool EsActivo { get; set; }
        public string Mensaje { get; set; } = "";
    }
}
