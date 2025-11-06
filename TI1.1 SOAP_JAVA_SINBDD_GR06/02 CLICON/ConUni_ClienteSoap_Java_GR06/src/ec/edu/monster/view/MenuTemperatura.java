package ec.edu.monster.view;

import ec.edu.monster.controller.TemperaturaController;
import ec.edu.monster.util.LectorEntrada;

/**
 * Vista del menú de temperatura
 * @author josue
 */
public class MenuTemperatura {
    
    private TemperaturaController controller;
    
    public MenuTemperatura() {
        this.controller = new TemperaturaController();
    }
    
    /**
     * Muestra el menú de conversiones de temperatura
     */
    public void mostrar() {
        mostrarOpciones();
        int opcion = LectorEntrada.leerEntero("Seleccione una opción: ");
        
        if (opcion == 0) return;
        
        double valor = LectorEntrada.leerDouble("Ingrese el valor: ");
        String resultado = procesarOpcion(opcion, valor);
        
        System.out.println("\n✓ Resultado: " + resultado + "\n");
    }
    
    private void mostrarOpciones() {
        System.out.println("╔════════════════════════════════════════════════════╗");
        System.out.println("║         CONVERSIONES DE TEMPERATURA                ║");
        System.out.println("╚════════════════════════════════════════════════════╝");
        System.out.println("  1. Celsius → Fahrenheit");
        System.out.println("  2. Fahrenheit → Celsius");
        System.out.println("  3. Celsius → Kelvin");
        System.out.println("  4. Kelvin → Celsius");
        System.out.println("  5. Fahrenheit → Kelvin");
        System.out.println("  6. Kelvin → Fahrenheit");
        System.out.println("  0. Volver");
        System.out.println("════════════════════════════════════════════════════");
    }
    
    private String procesarOpcion(int opcion, double valor) {
        switch (opcion) {
            case 1: return controller.celsiusAFahrenheit(valor);
            case 2: return controller.fahrenheitACelsius(valor);
            case 3: return controller.celsiusAKelvin(valor);
            case 4: return controller.kelvinACelsius(valor);
            case 5: return controller.fahrenheitAKelvin(valor);
            case 6: return controller.kelvinAFahrenheit(valor);
            default: return "❌ Opción inválida";
        }
    }
}