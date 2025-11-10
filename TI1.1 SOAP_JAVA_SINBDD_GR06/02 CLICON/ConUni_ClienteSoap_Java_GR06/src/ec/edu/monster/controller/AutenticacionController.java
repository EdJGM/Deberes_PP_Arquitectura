package ec.edu.monster.controller;

import ec.edu.monster.util.ConexionSOAP;
import ec.edu.monster.util.LectorEntrada;
import ec.edu.monster.cliente.ws.RespuestaAutenticacion;

/**
 * Controlador para gestionar la autenticación de usuarios
 * @author josue
 */
public class AutenticacionController {
    
    private String usuarioActual;
    private boolean autenticado;
    private static final int MAX_INTENTOS = 3;
    
    public AutenticacionController() {
        this.autenticado = false;
        this.usuarioActual = null;
    }
    
    /**
     * Realiza el proceso de autenticación
     * @return true si la autenticación fue exitosa
     */
    public boolean autenticar() {
        mostrarCabecera();
        
        int intentos = MAX_INTENTOS;
        while (intentos > 0) {
            String username = LectorEntrada.leerLinea("\nUsuario: ");
            String password = LectorEntrada.leerLinea("Contraseña: ");
            
            try {
                RespuestaAutenticacion respuesta = ConexionSOAP.getPort().login(username, password);
                
                if (respuesta.isExitoso()) {
                    this.autenticado = true;
                    this.usuarioActual = respuesta.getUsername();
                    mostrarExito(respuesta.getMensaje());
                    return true;
                } else {
                    intentos--;
                    mostrarError(respuesta.getMensaje(), intentos);
                }
            } catch (Exception e) {
                intentos--;
                System.err.println("❌ Error al comunicarse con el servidor: " + e.getMessage());
                if (intentos > 0) {
                    System.out.println("   Intentos restantes: " + intentos);
                }
            }
        }
        
        return false;
    }
    
    /**
     * Obtiene el nombre del usuario actual
     * @return nombre de usuario
     */
    public String getUsuarioActual() {
        return usuarioActual;
    }
    
    /**
     * Verifica si el usuario está autenticado
     * @return true si está autenticado
     */
    public boolean isAutenticado() {
        return autenticado;
    }
    
    private void mostrarCabecera() {
        System.out.println("═══════════════════════════════════════");
        System.out.println("          AUTENTICACIÓN REQUERIDA       ");
        System.out.println("═══════════════════════════════════════");
    }
    
    private void mostrarExito(String mensaje) {
        System.out.println("\n✓ " + mensaje);
        System.out.println("✓ Bienvenido, " + usuarioActual + "!\n");
    }
    
    private void mostrarError(String mensaje, int intentosRestantes) {
        System.out.println("\n❌ " + mensaje);
        if (intentosRestantes > 0) {
            System.out.println("   Intentos restantes: " + intentosRestantes);
        }
    }
}