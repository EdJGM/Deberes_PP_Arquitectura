using ConUni_Client_Console_DotNet.Servicios;
using ConUni_Client_Console_DotNet.Utils;
using System;

namespace ConUni_Client_Console_DotNet
{
    /// <summary>
    /// Aplicación cliente de consola para el servicio de conversión de unidades
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Configurar consola
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Cliente Conversor de Unidades - .NET";

            try
            {
                MostrarBanner();

                // Crear y ejecutar cliente
                var cliente = new ConversorClient();
                cliente.Ejecutar();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ Error fatal: {ex.Message}");
                Console.WriteLine($"\nDetalles técnicos:\n{ex.StackTrace}");
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine("\n\nPresione cualquier tecla para salir...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Muestra el banner de bienvenida
        /// </summary>
        static void MostrarBanner()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
╔════════════════════════════════════════════════════════════╗
║                                                            ║
║     ██████╗ ██████╗ ███╗   ██╗██╗   ██╗███╗   ██╗██╗      ║
║    ██╔════╝██╔═══██╗████╗  ██║██║   ██║████╗  ██║██║      ║
║    ██║     ██║   ██║██╔██╗ ██║██║   ██║██╔██╗ ██║██║      ║
║    ██║     ██║   ██║██║╚██╗██║██║   ██║██║╚██╗██║██║      ║
║    ╚██████╗╚██████╔╝██║ ╚████║╚██████╔╝██║ ╚████║██║      ║
║     ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝      ║
║                                                            ║
║          SISTEMA DE CONVERSIÓN DE UNIDADES                ║
║              Cliente de Consola - .NET                    ║
║                                                            ║
╚════════════════════════════════════════════════════════════╝
");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("          Conectando al servidor SOAP...\n");
            Console.ResetColor();

            System.Threading.Thread.Sleep(1000);
        }
    }
}