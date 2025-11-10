using ConUni_Soap_Dotnet_GR06.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConUni_Soap_Dotnet_GR06.Pruebas
{
    /// <summary>
    /// Clase para probar todos los conversores y autenticación
    /// </summary>
    public class PruebaConversorCompleto
    {
        public static void Main(string[] args)
        {
            PruebaAutenticacion();
            PruebaTemperatura();
            PruebaLongitud();
            PruebaMasa();
        }

        private static void PruebaAutenticacion()
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║  PRUEBAS DE AUTENTICACIÓN            ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Prueba login exitoso
            var usuario1 = AutenticacionService.Autenticar("MONSTER", "MONSTER9");
            Console.WriteLine($"✓ Login correcto: {(usuario1 != null ? "✓ EXITOSO" : "✗ FALLÓ")}");

            // Prueba login fallido
            var usuario2 = AutenticacionService.Autenticar("WRONG", "WRONG");
            Console.WriteLine($"✓ Login incorrecto rechazado: {(usuario2 == null ? "✓ EXITOSO" : "✗ FALLÓ")}");

            Console.WriteLine();
        }

        private static void PruebaTemperatura()
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║  PRUEBAS DE TEMPERATURA              ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            var conv = new ConversorTemperatura();

            // Celsius a Fahrenheit
            double c2f = conv.CelsiusAFahrenheit(0);
            Console.WriteLine($"0°C → {c2f}°F {(c2f == 32 ? "✓" : "✗")}");

            // Celsius a Kelvin
            double c2k = conv.CelsiusAKelvin(0);
            Console.WriteLine($"0°C → {c2k}K {(c2k == 273.15 ? "✓" : "✗")}");

            // Fahrenheit a Celsius
            double f2c = conv.FahrenheitACelsius(212);
            Console.WriteLine($"212°F → {f2c}°C {(f2c == 100 ? "✓" : "✗")}");

            Console.WriteLine();
        }

        private static void PruebaLongitud()
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║  PRUEBAS DE LONGITUD                 ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            var conv = new ConversorLongitud();

            // Metro a Kilómetro
            double m2km = conv.MetroAKilometro(1000);
            Console.WriteLine($"1000 m → {m2km} km {(m2km == 1.0 ? "✓" : "✗")}");

            // Kilómetro a Metro
            double km2m = conv.KilometroAMetro(1);
            Console.WriteLine($"1 km → {km2m} m {(km2m == 1000.0 ? "✓" : "✗")}");

            // Metro a Milla
            double m2mi = conv.MetroAMilla(1609.344);
            Console.WriteLine($"1609.344 m → {m2mi:F2} mi {(Math.Abs(m2mi - 1.0) < 0.01 ? "✓" : "✗")}");

            Console.WriteLine();
        }

        private static void PruebaMasa()
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║  PRUEBAS DE MASA                     ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            var conv = new ConversorMasa();

            // Kilogramo a Gramo
            double kg2g = conv.KilogramoAGramo(1);
            Console.WriteLine($"1 kg → {kg2g} g {(kg2g == 1000.0 ? "✓" : "✗")}");

            // Gramo a Kilogramo
            double g2kg = conv.GramoAKilogramo(1000);
            Console.WriteLine($"1000 g → {g2kg} kg {(g2kg == 1.0 ? "✓" : "✗")}");

            // Kilogramo a Libra
            double kg2lb = conv.KilogramoALibra(1);
            Console.WriteLine($"1 kg → {kg2lb:F2} lb {(Math.Abs(kg2lb - 2.20462) < 0.01 ? "✓" : "✗")}");

            Console.WriteLine();
        }
    }
}