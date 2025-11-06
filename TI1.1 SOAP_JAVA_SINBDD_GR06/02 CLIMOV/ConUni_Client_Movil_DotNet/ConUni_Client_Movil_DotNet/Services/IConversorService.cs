
namespace ConUni_Client_Movil_DotNet.Services
{
    /// <summary>
    /// Interfaz común para todos los servicios de conversión
    /// Implementada por: DotNetSoapService, JavaSoapService, DotNetRestService, JavaRestService
    /// </summary>
    public interface IConversorService
    {
        // Propiedades de estado
        string? UsuarioActual { get; }
        bool UsuarioAutenticado { get; }
        string TipoServicio { get; } // "DOTNET-SOAP", "JAVA-SOAP", "DOTNET-REST", "JAVA-REST"
        bool EstaDisponible { get; } // Para health check

        // ==================== AUTENTICACIÓN ====================
        Task<RespuestaLogin> LoginAsync(string username, string password);
        void Logout();
        Task<bool> TestConexionAsync();

        // ==================== TEMPERATURA ====================
        Task<double> CelsiusAFahrenheitAsync(double celsius);
        Task<double> FahrenheitACelsiusAsync(double fahrenheit);
        Task<double> CelsiusAKelvinAsync(double celsius);
        Task<double> KelvinACelsiusAsync(double kelvin);
        Task<double> FahrenheitAKelvinAsync(double fahrenheit);
        Task<double> KelvinAFahrenheitAsync(double kelvin);

        // ==================== LONGITUD ====================
        Task<double> MetroAKilometroAsync(double metros);
        Task<double> KilometroAMetroAsync(double kilometros);
        Task<double> MetroAMillaAsync(double metros);
        Task<double> MillaAMetroAsync(double millas);
        Task<double> KilometroAMillaAsync(double kilometros);
        Task<double> MillaAKilometroAsync(double millas);

        // ==================== MASA ====================
        Task<double> KilogramoAGramoAsync(double kilogramos);
        Task<double> GramoAKilogramoAsync(double gramos);
        Task<double> KilogramoALibraAsync(double kilogramos);
        Task<double> LibraAKilogramoAsync(double libras);
        Task<double> GramoALibraAsync(double gramos);
        Task<double> LibraAGramoAsync(double libras);
    }

    /// <summary>
    /// Modelo común para respuestas de login
    /// </summary>
    public class RespuestaLogin
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = "";
        public string? Username { get; set; }
        public string TipoServicio { get; set; } = "";

        public RespuestaLogin() { }

        public RespuestaLogin(bool exitoso, string mensaje, string tipoServicio = "")
        {
            Exitoso = exitoso;
            Mensaje = mensaje;
            TipoServicio = tipoServicio;
        }

        public RespuestaLogin(bool exitoso, string mensaje, string? username, string tipoServicio)
        {
            Exitoso = exitoso;
            Mensaje = mensaje;
            Username = username;
            TipoServicio = tipoServicio;
        }
    }
}
