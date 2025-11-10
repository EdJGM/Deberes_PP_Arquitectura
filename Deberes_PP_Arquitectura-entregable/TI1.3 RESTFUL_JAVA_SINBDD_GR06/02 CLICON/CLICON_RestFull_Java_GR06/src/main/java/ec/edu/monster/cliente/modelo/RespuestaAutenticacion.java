/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.cliente.modelo;


// Este POJO debe coincidir con el del servidor
public class RespuestaAutenticacion {
    private boolean exitoso;
    private String mensaje;
    private String username;

    // Constructor vacío para deserialización
    public RespuestaAutenticacion() {
    }

    // Getters y Setters que coinciden con el servidor
    public boolean isExitoso() { return exitoso; }
    public void setExitoso(boolean exitoso) { this.exitoso = exitoso; }
    public String getMensaje() { return mensaje; }
    public void setMensaje(String mensaje) { this.mensaje = mensaje; }
    public String getUsername() { return username; }
    public void setUsername(String username) { this.username = username; }
}