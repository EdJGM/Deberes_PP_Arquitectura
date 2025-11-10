package ec.edu.monster.view;

import ec.edu.monster.util.LectorEntrada;

/**
 * Vista del menú principal
 * @author josue
 */
public class MenuPrincipal {
    
    private MenuTemperatura menuTemperatura;
    private MenuLongitud menuLongitud;
    private MenuMasa menuMasa;
    
    public MenuPrincipal() {
        this.menuTemperatura = new MenuTemperatura();
        this.menuLongitud = new MenuLongitud();
        this.menuMasa = new MenuMasa();
    }
    
    /**
     * Muestra el menú principal y gestiona la navegación
     */
    public void mostrar() {
        boolean continuar = true;
        
        while (continuar) {
            mostrarOpciones();
            int opcion = LectorEntrada.leerEntero("Seleccione una opción: ");
            System.out.println();
            
            switch (opcion) {
                case 1:
                    menuTemperatura.mostrar();
                    break;
                case 2:
                    menuLongitud.mostrar();
                    break;
                case 3:
                    menuMasa.mostrar();
                    break;
                case 0:
                    continuar = false;
                    break;
                default:
                    System.out.println("❌ Opción inválida\n");
            }
        }
    }
    
    private void mostrarOpciones() {
        System.out.println("╔════════════════════════════════════════════════════╗");
        System.out.println("║              MENÚ PRINCIPAL                        ║");
        System.out.println("╚════════════════════════════════════════════════════╝");
        System.out.println("  1. Conversiones de Temperatura");
        System.out.println("  2. Conversiones de Longitud");
        System.out.println("  3. Conversiones de Masa");
        System.out.println("  0. Salir");
        System.out.println("════════════════════════════════════════════════════");
    }
}