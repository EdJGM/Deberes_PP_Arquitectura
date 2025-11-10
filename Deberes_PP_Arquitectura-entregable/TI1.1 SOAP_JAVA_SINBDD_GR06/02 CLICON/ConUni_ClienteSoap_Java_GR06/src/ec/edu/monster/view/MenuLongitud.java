package ec.edu.monster.view;

import ec.edu.monster.controller.LongitudController;
import ec.edu.monster.util.LectorEntrada;

/**
 * Vista del menú de longitud
 * @author josue
 */
public class MenuLongitud {
    
    private LongitudController controller;
    
    public MenuLongitud() {
        this.controller = new LongitudController();
    }
    
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
        System.out.println("║          CONVERSIONES DE LONGITUD                  ║");
        System.out.println("╚════════════════════════════════════════════════════╝");
        System.out.println("  1. Metro → Kilómetro");
        System.out.println("  2. Kilómetro → Metro");
        System.out.println("  3. Metro → Milla");
        System.out.println("  4. Milla → Metro");
        System.out.println("  5. Kilómetro → Milla");
        System.out.println("  6. Milla → Kilómetro");
        System.out.println("  0. Volver");
        System.out.println("════════════════════════════════════════════════════");
    }
    
    private String procesarOpcion(int opcion, double valor) {
        switch (opcion) {
            case 1: return controller.metroAKilometro(valor);
            case 2: return controller.kilometroAMetro(valor);
            case 3: return controller.metroAMilla(valor);
            case 4: return controller.millaAMetro(valor);
            case 5: return controller.kilometroAMilla(valor);
            case 6: return controller.millaAKilometro(valor);
            default: return "❌ Opción inválida";
        }
    }
}