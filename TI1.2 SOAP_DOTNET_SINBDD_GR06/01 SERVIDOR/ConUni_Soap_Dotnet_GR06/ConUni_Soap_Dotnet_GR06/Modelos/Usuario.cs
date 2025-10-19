using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConUni_Soap_Dotnet_GR06.Modelos
{
    /// <summary>
    /// Modelo de usuario para autenticación
    /// </summary>
    public class Usuario
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Usuario()
        {
        }

        public Usuario(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return $"Usuario{{username='{Username}'}}";
        }
    }
}