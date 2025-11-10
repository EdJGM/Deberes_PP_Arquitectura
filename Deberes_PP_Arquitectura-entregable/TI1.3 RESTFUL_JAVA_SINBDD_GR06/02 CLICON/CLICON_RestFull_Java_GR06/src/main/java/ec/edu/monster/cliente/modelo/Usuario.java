
package ec.edu.monster.cliente.modelo;


// Este POJO debe coincidir EXACTAMENTE con el del servidor
public class Usuario {
    private String username;
    private String password;
    
    // IMPORTANTE: Constructor vacío para deserialización
    public Usuario() {
    }

    public Usuario(String username, String password) {
        this.username = username;
        this.password = password;
    }

    // Getters y Setters
    public String getUsername() { return username; }
    public void setUsername(String username) { this.username = username; }
    public String getPassword() { return password; }
    public void setPassword(String password) { this.password = password; }
    
    // ¡Eliminamos el campo 'nombre' y sus getters/setters!
}