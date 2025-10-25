using ConUni_RestFul_Dotnet_GR06.Modelos;

namespace ConUni_RestFul_Dotnet_GR06.Servicios
{
    /// <summary>
    /// Servicio de autenticación con usuarios hardcodeados
    /// </summary>
    public class AutenticacionService
    {
        private static readonly List<Usuario> UsuariosRegistrados;

        static AutenticacionService()
        {
            UsuariosRegistrados = new List<Usuario>
            {
                new Usuario("MONSTER", "MONSTER9")
            };
        }

        /// <summary>
        /// Autentica un usuario con username y password
        /// </summary>
        public static Usuario Autenticar(string username, string password)
        {
            return UsuariosRegistrados.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.Ordinal) &&
                u.Password.Equals(password, StringComparison.Ordinal));
        }

        /// <summary>
        /// Verifica si un usuario existe
        /// </summary>
        public static bool ExisteUsuario(string username)
        {
            return UsuariosRegistrados.Any(u =>
                u.Username.Equals(username, StringComparison.Ordinal));
        }

        /// <summary>
        /// Obtiene todos los usuarios (sin contraseñas)
        /// </summary>
        public static List<Usuario> ObtenerTodosLosUsuarios()
        {
            return new List<Usuario>(UsuariosRegistrados);
        }
    }
}
