/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.pruebas;

import ec.edu.monster.services.ConversorTemperatura;
import ec.edu.monster.services.ConversorLongitud;
import ec.edu.monster.services.ConversorMasa;
import ec.edu.monster.services.AutenticacionService;
import ec.edu.monster.modelo.Usuario;
/**
 *
 * @author josue
 */
public class PruebaConversorCompleto {
    
    public static void main(String[] args) {
        pruebaAutenticacion();
        pruebaTemperatura();
        pruebaLongitud();
        pruebaMasa();
    }
    
    private static void pruebaAutenticacion() {
        System.out.println("╔══════════════════════════════════════╗");
        System.out.println("║  PRUEBAS DE AUTENTICACIÓN            ║");
        System.out.println("╚══════════════════════════════════════╝\n");
        
        // Prueba login exitoso
        Usuario usuario1 = AutenticacionService.autenticar("MONSTER", "MONSTER9");
        System.out.println("✓ Login correcto: " + (usuario1 != null ? "✓ EXITOSO" : "✗ FALLÓ"));
        
        // Prueba login fallido
        Usuario usuario2 = AutenticacionService.autenticar("WRONG", "WRONG");
        System.out.println("✓ Login incorrecto rechazado: " + (usuario2 == null ? "✓ EXITOSO" : "✗ FALLÓ"));
        
        System.out.println();
    }
    
    private static void pruebaTemperatura() {
        System.out.println("╔══════════════════════════════════════╗");
        System.out.println("║  PRUEBAS DE TEMPERATURA              ║");
        System.out.println("╚══════════════════════════════════════╝\n");
        
        ConversorTemperatura conv = new ConversorTemperatura();
        
        // Celsius a Fahrenheit
        double c2f = conv.celsiusAFahrenheit(0);
        System.out.println("0°C → " + c2f + "°F " + (c2f == 32 ? "✓" : "✗"));
        
        // Celsius a Kelvin
        double c2k = conv.celsiusAKelvin(0);
        System.out.println("0°C → " + c2k + "K " + (c2k == 273.15 ? "✓" : "✗"));
        
        // Fahrenheit a Celsius
        double f2c = conv.fahrenheitACelsius(212);
        System.out.println("212°F → " + f2c + "°C " + (f2c == 100 ? "✓" : "✗"));
        
        System.out.println();
    }
    
    private static void pruebaLongitud() {
        System.out.println("╔══════════════════════════════════════╗");
        System.out.println("║  PRUEBAS DE LONGITUD                 ║");
        System.out.println("╚══════════════════════════════════════╝\n");
        
        ConversorLongitud conv = new ConversorLongitud();
        
        // Metro a Kilómetro
        double m2km = conv.metroAKilometro(1000);
        System.out.println("1000 m → " + m2km + " km " + (m2km == 1.0 ? "✓" : "✗"));
        
        // Kilómetro a Metro
        double km2m = conv.kilometroAMetro(1);
        System.out.println("1 km → " + km2m + " m " + (km2m == 1000.0 ? "✓" : "✗"));
        
        // Metro a Milla
        double m2mi = conv.metroAMilla(1609.344);
        System.out.println("1609.344 m → " + String.format("%.2f", m2mi) + " mi " + 
                          (Math.abs(m2mi - 1.0) < 0.01 ? "✓" : "✗"));
        
        System.out.println();
    }
    
    private static void pruebaMasa() {
        System.out.println("╔══════════════════════════════════════╗");
        System.out.println("║  PRUEBAS DE MASA                     ║");
        System.out.println("╚══════════════════════════════════════╝\n");
        
        ConversorMasa conv = new ConversorMasa();
        
        // Kilogramo a Gramo
        double kg2g = conv.kilogramoAGramo(1);
        System.out.println("1 kg → " + kg2g + " g " + (kg2g == 1000.0 ? "✓" : "✗"));
        
        // Gramo a Kilogramo
        double g2kg = conv.gramoAKilogramo(1000);
        System.out.println("1000 g → " + g2kg + " kg " + (g2kg == 1.0 ? "✓" : "✗"));
        
        // Kilogramo a Libra
        double kg2lb = conv.kilogramoALibra(1);
        System.out.println("1 kg → " + String.format("%.2f", kg2lb) + " lb " + 
                          (Math.abs(kg2lb - 2.20462) < 0.01 ? "✓" : "✗"));
        
        System.out.println();
    }
}
