namespace ConUni_RestFul_Dotnet_GR06.Modelos
{
    /// <summary>
    /// DTO para respuestas de autenticación
    /// </summary>
    public class RespuestaAutenticacion
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public string Username { get; set; }

        public RespuestaAutenticacion()
        {
        }

        public RespuestaAutenticacion(bool exitoso, string mensaje)
        {
            Exitoso = exitoso;
            Mensaje = mensaje;
        }

        public RespuestaAutenticacion(bool exitoso, string mensaje, Usuario usuario)
        {
            Exitoso = exitoso;
            Mensaje = mensaje;
            if (usuario != null)
            {
                Username = usuario.Username;
            }
        }
    }
}
