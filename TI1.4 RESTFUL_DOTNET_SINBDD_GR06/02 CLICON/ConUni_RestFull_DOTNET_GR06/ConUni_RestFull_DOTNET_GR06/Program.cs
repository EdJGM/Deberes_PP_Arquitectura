using ConUni_RestFull_DOTNET_GR06.ec.edu.monster.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConUni_RestFull_DOTNET_GR06
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            MostrarBanner();

            var cliente = new ConversorApiCliente("http://localhost:7041");

            // Verificar conexión con el servidor
            Console.WriteLine("Verificando conexión con el servidor...");
            bool servidorActivo = await cliente.VerificarSaludAsync();

            if (!servidorActivo)
            {
                Console.WriteLine("\n❌ ERROR: No se puede conectar al servidor.");
                Console.WriteLine("   Asegúrate de que el servidor esté ejecutándose en http://localhost:5174");
                Console.WriteLine("\nPresiona cualquier tecla para salir...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("✓ Servidor conectado correctamente\n");

            // Login
            bool autenticado = false;
            while (!autenticado)
            {
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║         INICIO DE SESIÓN             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");

                Console.Write("\nUsuario: ");
                string username = Console.ReadLine() ?? "";

                Console.Write("Contraseña: ");
                string password = LeerPasswordOculto();

                autenticado = await cliente.LoginAsync(username, password);

                if (!autenticado)
                {
                    Console.WriteLine("\n¿Deseas intentar nuevamente? (S/N): ");
                    if (Console.ReadKey().Key != ConsoleKey.S)
                    {
                        return;
                    }
                    Console.Clear();
                    MostrarBanner();
                }
            }

            // Menú principal
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                MostrarMenuPrincipal(cliente.ObtenerUsuarioActual());

                var opcion = Console.ReadKey(true).Key;
                Console.WriteLine();

                switch (opcion)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        await MenuTemperatura(cliente);
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        await MenuLongitud(cliente);
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        await MenuMasa(cliente);
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        await cliente.MostrarTiposConversionAsync();
                        EsperarTecla();
                        break;

                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.Escape:
                        continuar = false;
                        Console.WriteLine("\n👋 ¡Hasta pronto!");
                        break;

                    default:
                        Console.WriteLine("Opción inválida");
                        EsperarTecla();
                        break;
                }
            }
        }

        static void MostrarBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                    ║");
            Console.WriteLine("║     CONVERSOR DE UNIDADES - CLIENTE CONSOLA        ║");
            Console.WriteLine("║              Grupo 06 - RestFul .NET               ║");
            Console.WriteLine("║                                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void MostrarMenuPrincipal(string usuario)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"╔════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  Usuario: {usuario.PadRight(40)} ║");
            Console.WriteLine("╠════════════════════════════════════════════════════╣");
            Console.ResetColor();
            Console.WriteLine("║                   MENÚ PRINCIPAL                   ║");
            Console.WriteLine("╠════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                    ║");
            Console.WriteLine("║  [1] Conversiones de Temperatura                   ║");
            Console.WriteLine("║  [2] Conversiones de Longitud                      ║");
            Console.WriteLine("║  [3] Conversiones de Masa                          ║");
            Console.WriteLine("║  [4] Ver tipos de conversión disponibles           ║");
            Console.WriteLine("║                                                    ║");
            Console.WriteLine("║  [0] Salir                                         ║");
            Console.WriteLine("║                                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.Write("\nSelecciona una opción: ");
        }

        static async Task MenuTemperatura(ConversorApiCliente cliente)
        {
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║   CONVERSIONES DE TEMPERATURA        ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("\n[1] Celsius → Fahrenheit");
            Console.WriteLine("[2] Fahrenheit → Celsius");
            Console.WriteLine("[3] Celsius → Kelvin");
            Console.WriteLine("[4] Kelvin → Celsius");
            Console.WriteLine("[5] Fahrenheit → Kelvin");
            Console.WriteLine("[6] Kelvin → Fahrenheit");
            Console.Write("\nSelecciona conversión: ");

            var opcion = Console.ReadKey().Key;
            Console.WriteLine("\n");

            string origen = "", destino = "", simboloOrigen = "", simboloDestino = "";

            switch (opcion)
            {
                case ConsoleKey.D1: origen = "celsius"; destino = "fahrenheit"; simboloOrigen = "°C"; simboloDestino = "°F"; break;
                case ConsoleKey.D2: origen = "fahrenheit"; destino = "celsius"; simboloOrigen = "°F"; simboloDestino = "°C"; break;
                case ConsoleKey.D3: origen = "celsius"; destino = "kelvin"; simboloOrigen = "°C"; simboloDestino = "K"; break;
                case ConsoleKey.D4: origen = "kelvin"; destino = "celsius"; simboloOrigen = "K"; simboloDestino = "°C"; break;
                case ConsoleKey.D5: origen = "fahrenheit"; destino = "kelvin"; simboloOrigen = "°F"; simboloDestino = "K"; break;
                case ConsoleKey.D6: origen = "kelvin"; destino = "fahrenheit"; simboloOrigen = "K"; simboloDestino = "°F"; break;
                default:
                    Console.WriteLine("Opción inválida");
                    EsperarTecla();
                    return;
            }

            Console.Write($"Ingresa el valor en {origen}: ");
            if (double.TryParse(Console.ReadLine(), out double valor))
            {
                var resultado = await cliente.ConvertirTemperaturaAsync(origen, destino, valor);
                if (resultado.HasValue)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n✓ Resultado: {valor} {simboloOrigen} = {resultado.Value:F2} {simboloDestino}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Valor inválido");
            }

            EsperarTecla();
        }

        static async Task MenuLongitud(ConversorApiCliente cliente)
        {
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║   CONVERSIONES DE LONGITUD           ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("\n[1] Metro → Kilómetro");
            Console.WriteLine("[2] Kilómetro → Metro");
            Console.WriteLine("[3] Metro → Milla");
            Console.WriteLine("[4] Milla → Metro");
            Console.WriteLine("[5] Kilómetro → Milla");
            Console.WriteLine("[6] Milla → Kilómetro");
            Console.Write("\nSelecciona conversión: ");

            var opcion = Console.ReadKey().Key;
            Console.WriteLine("\n");

            string origen = "", destino = "", simboloOrigen = "", simboloDestino = "";

            switch (opcion)
            {
                case ConsoleKey.D1: origen = "metro"; destino = "kilometro"; simboloOrigen = "m"; simboloDestino = "km"; break;
                case ConsoleKey.D2: origen = "kilometro"; destino = "metro"; simboloOrigen = "km"; simboloDestino = "m"; break;
                case ConsoleKey.D3: origen = "metro"; destino = "milla"; simboloOrigen = "m"; simboloDestino = "mi"; break;
                case ConsoleKey.D4: origen = "milla"; destino = "metro"; simboloOrigen = "mi"; simboloDestino = "m"; break;
                case ConsoleKey.D5: origen = "kilometro"; destino = "milla"; simboloOrigen = "km"; simboloDestino = "mi"; break;
                case ConsoleKey.D6: origen = "milla"; destino = "kilometro"; simboloOrigen = "mi"; simboloDestino = "km"; break;
                default:
                    Console.WriteLine("Opción inválida");
                    EsperarTecla();
                    return;
            }

            Console.Write($"Ingresa el valor en {origen}: ");
            if (double.TryParse(Console.ReadLine(), out double valor))
            {
                var resultado = await cliente.ConvertirLongitudAsync(origen, destino, valor);
                if (resultado.HasValue)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n✓ Resultado: {valor} {simboloOrigen} = {resultado.Value:F4} {simboloDestino}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Valor inválido");
            }

            EsperarTecla();
        }

        static async Task MenuMasa(ConversorApiCliente cliente)
        {
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║   CONVERSIONES DE MASA               ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("\n[1] Kilogramo → Gramo");
            Console.WriteLine("[2] Gramo → Kilogramo");
            Console.WriteLine("[3] Kilogramo → Libra");
            Console.WriteLine("[4] Libra → Kilogramo");
            Console.WriteLine("[5] Gramo → Libra");
            Console.WriteLine("[6] Libra → Gramo");
            Console.Write("\nSelecciona conversión: ");

            var opcion = Console.ReadKey().Key;
            Console.WriteLine("\n");

            string origen = "", destino = "", simboloOrigen = "", simboloDestino = "";

            switch (opcion)
            {
                case ConsoleKey.D1: origen = "kilogramo"; destino = "gramo"; simboloOrigen = "kg"; simboloDestino = "g"; break;
                case ConsoleKey.D2: origen = "gramo"; destino = "kilogramo"; simboloOrigen = "g"; simboloDestino = "kg"; break;
                case ConsoleKey.D3: origen = "kilogramo"; destino = "libra"; simboloOrigen = "kg"; simboloDestino = "lb"; break;
                case ConsoleKey.D4: origen = "libra"; destino = "kilogramo"; simboloOrigen = "lb"; simboloDestino = "kg"; break;
                case ConsoleKey.D5: origen = "gramo"; destino = "libra"; simboloOrigen = "g"; simboloDestino = "lb"; break;
                case ConsoleKey.D6: origen = "libra"; destino = "gramo"; simboloOrigen = "lb"; simboloDestino = "g"; break;
                default:
                    Console.WriteLine("Opción inválida");
                    EsperarTecla();
                    return;
            }

            Console.Write($"Ingresa el valor en {origen}: ");
            if (double.TryParse(Console.ReadLine(), out double valor))
            {
                var resultado = await cliente.ConvertirMasaAsync(origen, destino, valor);
                if (resultado.HasValue)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n✓ Resultado: {valor} {simboloOrigen} = {resultado.Value:F4} {simboloDestino}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Valor inválido");
            }

            EsperarTecla();
        }

        static string LeerPasswordOculto()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        static void EsperarTecla()
        {
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey(true);
        }
    }
}
