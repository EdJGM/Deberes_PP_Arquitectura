package ec.edu.monster.cliente;

import ec.edu.monster.controller.AutenticacionController;
import ec.edu.monster.util.ConexionSOAP;
import ec.edu.monster.view.MenuPrincipal;

/**
 * Clase principal del cliente SOAP
 * Punto de entrada de la aplicación
 */
public class ClienteConsolaSOAP {
    
    public static void main(String[] args) {
        mostrarBanner();
        
        // Inicializar conexión SOAP
        if (!ConexionSOAP.inicializar()) {
            System.err.println("No se pudo establecer conexión. Saliendo...");
            return;
        }
        
        // Autenticación
        AutenticacionController authController = new AutenticacionController();
        if (!authController.autenticar()) {
            System.out.println("\n❌ No se pudo autenticar. Saliendo...");
            ConexionSOAP.cerrar();
            return;
        }
        
        // Mostrar menú principal
        MenuPrincipal menu = new MenuPrincipal();
        menu.mostrar();
        
        // Despedida
        System.out.println("\n¡Hasta luego, " + authController.getUsuarioActual() + "!");
        ConexionSOAP.cerrar();
    }
    
    private static void mostrarBanner() {
           System.out.println("   ╔═══════════════════════════════════════════════════════════╗");
        System.out.println("   ║                                                           ║");
        System.out.println("   ║        ██████╗ ██████╗ ███╗   ██╗██╗   ██╗              ║");
        System.out.println("   ║       ██╔════╝██╔═══██╗████╗  ██║██║   ██║              ║");
        System.out.println("   ║       ██║     ██║   ██║██╔██╗ ██║██║   ██║              ║");
        System.out.println("   ║       ██║     ██║   ██║██║╚██╗██║╚██╗ ██╔╝              ║");
        System.out.println("   ║       ╚██████╗╚██████╔╝██║ ╚████║ ╚████╔╝               ║");
        System.out.println("   ║        ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝  ╚═══╝                ║");
        System.out.println("   ║                                                           ║");
        System.out.println("   ║          CONVERSOR DE UNIDADES - SOAP CLIENTE             ║");
        System.out.println("   ║                    Grupo 06                               ║");
        System.out.println("   ║                                                           ║");
        System.out.println("   ╚═══════════════════════════════════════════════════════════╝");
    }
}