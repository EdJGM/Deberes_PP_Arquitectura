using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConUni_Client_Console_DotNet.Utils
{
    /// <summary>
    /// Clase auxiliar para manejo de menús en consola
    /// </summary>
    public static class MenuHelper
    {
        public static void MostrarEncabezado(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {titulo.PadRight(44)}  ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void MostrarSeparador()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("────────────────────────────────────────────────");
            Console.ResetColor();
        }

        public static void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarInfo(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"ℹ {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarResultado(double valor, string unidad)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n══════════════════════════════════════════════");
            Console.WriteLine($"  RESULTADO: {valor:F4} {unidad}");
            Console.WriteLine($"══════════════════════════════════════════════");
            Console.ResetColor();
        }

        public static int LeerOpcion(int min, int max)
        {
            while (true)
            {
                Console.Write("\nSeleccione una opción: ");
                if (int.TryParse(Console.ReadLine(), out int opcion) &&
                    opcion >= min && opcion <= max)
                {
                    return opcion;
                }
                MostrarError($"Por favor ingrese un número entre {min} y {max}");
            }
        }

        public static double LeerValor(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                if (double.TryParse(Console.ReadLine(), out double valor))
                {
                    return valor;
                }
                MostrarError("Por favor ingrese un número válido");
            }
        }

        public static string LeerTexto(string mensaje)
        {
            Console.Write(mensaje);
            return Console.ReadLine() ?? string.Empty;
        }

        public static void PausarYContinuar()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Presione cualquier tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void MostrarOpcionMenu(int numero, string descripcion, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"  [{numero}] ");
            Console.ForegroundColor = color;
            Console.WriteLine(descripcion);
            Console.ResetColor();
        }
    }
}
