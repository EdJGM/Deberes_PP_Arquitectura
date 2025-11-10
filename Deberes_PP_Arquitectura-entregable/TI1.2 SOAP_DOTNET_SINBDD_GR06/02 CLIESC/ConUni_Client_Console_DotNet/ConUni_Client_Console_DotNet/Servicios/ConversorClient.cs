using ConUni_Client_Console_DotNet.Utils;
using ConUniServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConUni_Client_Console_DotNet.Servicios
{
    /// <summary>
    /// Clase que maneja la lógica del cliente y comunicación con el servicio SOAP
    /// </summary>
    public class ConversorClient
    {
        private readonly ConversorUnidadesWSClient _cliente;
        private string? _usuarioActual;

        public ConversorClient()
        {
            // Crear cliente del servicio SOAP
            _cliente = new ConversorUnidadesWSClient(
                ConversorUnidadesWSClient.EndpointConfiguration.BasicHttpBinding_IConversorUnidadesWS
            );
        }

        public bool UsuarioAutenticado => !string.IsNullOrEmpty(_usuarioActual);

        /// <summary>
        /// Método principal que ejecuta el cliente
        /// </summary>
        public void Ejecutar()
        {
            try
            {
                if (!Login())
                {
                    MenuHelper.MostrarError("Autenticación fallida. Cerrando aplicación...");
                    return;
                }

                bool continuar = true;
                while (continuar)
                {
                    continuar = MostrarMenuPrincipal();
                }

                MenuHelper.MostrarInfo("¡Hasta pronto!");
            }
            catch (Exception ex)
            {
                MenuHelper.MostrarError($"Error crítico: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión
                if (_cliente.State == System.ServiceModel.CommunicationState.Opened)
                {
                    _cliente.Close();
                }
            }
        }

        /// <summary>
        /// Maneja el login del usuario
        /// </summary>
        private bool Login()
        {
            MenuHelper.MostrarEncabezado("SISTEMA DE CONVERSIÓN DE UNIDADES - LOGIN");

            Console.WriteLine();
            MenuHelper.MostrarInfo("Credenciales por defecto: MONSTER / MONSTER9");
            Console.WriteLine();
            MenuHelper.MostrarSeparador();

            int intentos = 3;
            while (intentos > 0)
            {
                string usuario = MenuHelper.LeerTexto("Usuario: ");
                string password = MenuHelper.LeerTexto("Contraseña: ");

                try
                {
                    var respuesta = _cliente.Login(usuario, password);

                    if (respuesta.Exitoso)
                    {
                        _usuarioActual = respuesta.Username;
                        Console.WriteLine();
                        MenuHelper.MostrarExito($"¡Bienvenido {_usuarioActual}!");
                        MenuHelper.PausarYContinuar();
                        return true;
                    }
                    else
                    {
                        intentos--;
                        MenuHelper.MostrarError($"{respuesta.Mensaje}. Intentos restantes: {intentos}");
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    MenuHelper.MostrarError($"Error al conectar con el servidor: {ex.Message}");
                    MenuHelper.MostrarInfo("Asegúrese que el servidor está ejecutándose.");
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Muestra el menú principal y maneja la navegación
        /// </summary>
        private bool MostrarMenuPrincipal()
        {
            MenuHelper.MostrarEncabezado($"MENÚ PRINCIPAL - Usuario: {_usuarioActual}");

            MenuHelper.MostrarOpcionMenu(1, "🌡️  Conversión de TEMPERATURA", ConsoleColor.Red);
            MenuHelper.MostrarOpcionMenu(2, "📏 Conversión de LONGITUD", ConsoleColor.Blue);
            MenuHelper.MostrarOpcionMenu(3, "⚖️  Conversión de MASA", ConsoleColor.Green);
            MenuHelper.MostrarOpcionMenu(0, "❌ Salir", ConsoleColor.DarkGray);

            int opcion = MenuHelper.LeerOpcion(0, 3);

            switch (opcion)
            {
                case 1:
                    MenuTemperatura();
                    break;
                case 2:
                    MenuLongitud();
                    break;
                case 3:
                    MenuMasa();
                    break;
                case 0:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Menú de conversión de temperatura
        /// </summary>
        private void MenuTemperatura()
        {
            MenuHelper.MostrarEncabezado("CONVERSIÓN DE TEMPERATURA");

            MenuHelper.MostrarOpcionMenu(1, "Celsius → Fahrenheit");
            MenuHelper.MostrarOpcionMenu(2, "Fahrenheit → Celsius");
            MenuHelper.MostrarOpcionMenu(3, "Celsius → Kelvin");
            MenuHelper.MostrarOpcionMenu(4, "Kelvin → Celsius");
            MenuHelper.MostrarOpcionMenu(5, "Fahrenheit → Kelvin");
            MenuHelper.MostrarOpcionMenu(6, "Kelvin → Fahrenheit");
            MenuHelper.MostrarOpcionMenu(0, "← Volver");

            int opcion = MenuHelper.LeerOpcion(0, 6);

            if (opcion == 0) return;

            double valor = MenuHelper.LeerValor("\nIngrese el valor: ");
            double resultado = 0;
            string unidadDestino = "";

            try
            {
                switch (opcion)
                {
                    case 1:
                        resultado = _cliente.CelsiusAFahrenheit(valor);
                        unidadDestino = "°F";
                        break;
                    case 2:
                        resultado = _cliente.FahrenheitACelsius(valor);
                        unidadDestino = "°C";
                        break;
                    case 3:
                        resultado = _cliente.CelsiusAKelvin(valor);
                        unidadDestino = "K";
                        break;
                    case 4:
                        resultado = _cliente.KelvinACelsius(valor);
                        unidadDestino = "°C";
                        break;
                    case 5:
                        resultado = _cliente.FahrenheitAKelvin(valor);
                        unidadDestino = "K";
                        break;
                    case 6:
                        resultado = _cliente.KelvinAFahrenheit(valor);
                        unidadDestino = "°F";
                        break;
                }

                MenuHelper.MostrarResultado(resultado, unidadDestino);
            }
            catch (Exception ex)
            {
                MenuHelper.MostrarError($"Error en la conversión: {ex.Message}");
            }

            MenuHelper.PausarYContinuar();
        }

        /// <summary>
        /// Menú de conversión de longitud
        /// </summary>
        private void MenuLongitud()
        {
            MenuHelper.MostrarEncabezado("CONVERSIÓN DE LONGITUD");

            MenuHelper.MostrarOpcionMenu(1, "Metro → Kilómetro");
            MenuHelper.MostrarOpcionMenu(2, "Kilómetro → Metro");
            MenuHelper.MostrarOpcionMenu(3, "Metro → Milla");
            MenuHelper.MostrarOpcionMenu(4, "Milla → Metro");
            MenuHelper.MostrarOpcionMenu(5, "Kilómetro → Milla");
            MenuHelper.MostrarOpcionMenu(6, "Milla → Kilómetro");
            MenuHelper.MostrarOpcionMenu(0, "← Volver");

            int opcion = MenuHelper.LeerOpcion(0, 6);

            if (opcion == 0) return;

            double valor = MenuHelper.LeerValor("\nIngrese el valor: ");
            double resultado = 0;
            string unidadDestino = "";

            try
            {
                switch (opcion)
                {
                    case 1:
                        resultado = _cliente.MetroAKilometro(valor);
                        unidadDestino = "km";
                        break;
                    case 2:
                        resultado = _cliente.KilometroAMetro(valor);
                        unidadDestino = "m";
                        break;
                    case 3:
                        resultado = _cliente.MetroAMilla(valor);
                        unidadDestino = "mi";
                        break;
                    case 4:
                        resultado = _cliente.MillaAMetro(valor);
                        unidadDestino = "m";
                        break;
                    case 5:
                        resultado = _cliente.KilometroAMilla(valor);
                        unidadDestino = "mi";
                        break;
                    case 6:
                        resultado = _cliente.MillaAKilometro(valor);
                        unidadDestino = "km";
                        break;
                }

                MenuHelper.MostrarResultado(resultado, unidadDestino);
            }
            catch (Exception ex)
            {
                MenuHelper.MostrarError($"Error en la conversión: {ex.Message}");
            }

            MenuHelper.PausarYContinuar();
        }

        /// <summary>
        /// Menú de conversión de masa
        /// </summary>
        private void MenuMasa()
        {
            MenuHelper.MostrarEncabezado("CONVERSIÓN DE MASA");

            MenuHelper.MostrarOpcionMenu(1, "Kilogramo → Gramo");
            MenuHelper.MostrarOpcionMenu(2, "Gramo → Kilogramo");
            MenuHelper.MostrarOpcionMenu(3, "Kilogramo → Libra");
            MenuHelper.MostrarOpcionMenu(4, "Libra → Kilogramo");
            MenuHelper.MostrarOpcionMenu(5, "Gramo → Libra");
            MenuHelper.MostrarOpcionMenu(6, "Libra → Gramo");
            MenuHelper.MostrarOpcionMenu(0, "← Volver");

            int opcion = MenuHelper.LeerOpcion(0, 6);

            if (opcion == 0) return;

            double valor = MenuHelper.LeerValor("\nIngrese el valor: ");
            double resultado = 0;
            string unidadDestino = "";

            try
            {
                switch (opcion)
                {
                    case 1:
                        resultado = _cliente.KilogramoAGramo(valor);
                        unidadDestino = "g";
                        break;
                    case 2:
                        resultado = _cliente.GramoAKilogramo(valor);
                        unidadDestino = "kg";
                        break;
                    case 3:
                        resultado = _cliente.KilogramoALibra(valor);
                        unidadDestino = "lb";
                        break;
                    case 4:
                        resultado = _cliente.LibraAKilogramo(valor);
                        unidadDestino = "kg";
                        break;
                    case 5:
                        resultado = _cliente.GramoALibra(valor);
                        unidadDestino = "lb";
                        break;
                    case 6:
                        resultado = _cliente.LibraAGramo(valor);
                        unidadDestino = "g";
                        break;
                }

                MenuHelper.MostrarResultado(resultado, unidadDestino);
            }
            catch (Exception ex)
            {
                MenuHelper.MostrarError($"Error en la conversión: {ex.Message}");
            }

            MenuHelper.PausarYContinuar();
        }
    }
}
