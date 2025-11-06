using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConUni_RestFull_DOTNET_GR06.ec.edu.monster.modelo
{ /// <summary>
  /// Modelo de usuario para autenticación
  /// </summary>
    public class Usuario
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Usuario() { }

        public Usuario(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
