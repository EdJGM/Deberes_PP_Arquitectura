package ec.edu.monster.view;

import ec.edu.monster.controller.MasaController;
import ec.edu.monster.util.LectorEntrada;

/**
 * Vista del menú de masa
 * @author josue
 */
public class MenuMasa {
    
    private MasaController controller;
    
    public MenuMasa() {
        this.controller = new MasaController();
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
        System.out.println("║            CONVERSIONES DE MASA                    ║");
        System.out.println("╚════════════════════════════════════════════════════╝");
        System.out.println("  1. Kilogramo → Gramo");
        System.out.println("  2. Gramo → Kilogramo");
        System.out.println("  3. Kilogramo → Libra");
        System.out.println("  4. Libra → Kilogramo");
        System.out.println("  5. Gramo → Libra");
        System.out.println("  6. Libra → Gramo");
        System.out.println("  0. Volver");
        System.out.println("════════════════════════════════════════════════════");
    }
    
    private String procesarOpcion(int opcion, double valor) {
        switch (opcion) {
            case 1: return controller.kilogramoAGramo(valor);
            case 2: return controller.gramoAKilogramo(valor);
            case 3: return controller.kilogramoALibra(valor);
            case 4: return controller.libraAKilogramo(valor);
            case 5: return controller.gramoALibra(valor);
            case 6: return controller.libraAGramo(valor);
            default: return "❌ Opción inválida";
        }
    }
}