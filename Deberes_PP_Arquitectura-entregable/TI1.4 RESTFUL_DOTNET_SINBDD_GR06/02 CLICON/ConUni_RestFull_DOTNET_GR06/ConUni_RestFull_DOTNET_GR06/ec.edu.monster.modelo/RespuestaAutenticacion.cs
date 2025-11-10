using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConUni_RestFull_DOTNET_GR06.ec.edu.monster.modelo
{
    /// <summary>
    /// DTO para respuestas de autenticación
    /// </summary>
    public class RespuestaAutenticacion
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public string Username { get; set; }
    }
}
