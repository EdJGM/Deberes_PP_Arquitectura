/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.modelo;

import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author josue
 */
public class AutenticacionService {
    private static List<Usuario> usuariosRegistrados;
    
    // Bloque estático para inicializar usuarios quemados
    static {
        usuariosRegistrados = new ArrayList<>();
        usuariosRegistrados.add(new Usuario("MONSTER", "MONSTER9"));
    }
    
    /**
     * Autentica un usuario con username y password
     * @param username nombre de usuario
     * @param password contraseña
     * @return Usuario si las credenciales son correctas, null si no
     */
    public static Usuario autenticar(String username, String password) {
        for (Usuario usuario : usuariosRegistrados) {
            if (usuario.getUsername().equals(username) && 
                usuario.getPassword().equals(password)) {
                return usuario;
            }
        }
        return null;
    }
    
    /**
     * Verifica si un usuario existe
     * @param username nombre de usuario
     * @return true si existe, false si no
     */
    public static boolean existeUsuario(String username) {
        for (Usuario usuario : usuariosRegistrados) {
            if (usuario.getUsername().equals(username)) {
                return true;
            }
        }
        return false;
    }
    
    /**
     * Obtiene todos los usuarios (sin contraseñas)
     * @return lista de usuarios
     */
    public static List<Usuario> obtenerTodosLosUsuarios() {
        return new ArrayList<>(usuariosRegistrados);
    }
}
