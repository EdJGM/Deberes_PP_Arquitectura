
package ec.edu.monster.modelo;

/**
 *
 * @author josue
 */
public class RespuestaAutenticacion {
    private boolean exitoso;
    private String mensaje;
    private String username;
    
    public RespuestaAutenticacion() {
    }
    
    public RespuestaAutenticacion(boolean exitoso, String mensaje) {
        this.exitoso = exitoso;
        this.mensaje = mensaje;
    }
    
    public RespuestaAutenticacion(boolean exitoso, String mensaje, Usuario usuario) {
        this.exitoso = exitoso;
        this.mensaje = mensaje;
        if (usuario != null) {
            this.username = usuario.getUsername();
        }
    }
    
    // Getters y Setters
    public boolean isExitoso() {
        return exitoso;
    }
    
    public void setExitoso(boolean exitoso) {
        this.exitoso = exitoso;
    }
    
    public String getMensaje() {
        return mensaje;
    }
    
    public void setMensaje(String mensaje) {
        this.mensaje = mensaje;
    }
    
    public String getUsername() {
        return username;
    }
    
    public void setUsername(String username) {
        this.username = username;
    }
}
