/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.cliente.servicio;


import ec.edu.monster.cliente.modelo.RespuestaAutenticacion;
import ec.edu.monster.cliente.modelo.Usuario;
import jakarta.ws.rs.client.Client;
import jakarta.ws.rs.client.ClientBuilder;
import jakarta.ws.rs.client.Entity;
import jakarta.ws.rs.client.WebTarget;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.ProcessingException; // Para errores de conexión
import jakarta.ws.rs.WebApplicationException; // Para errores 404, 500

public class ConversorClienteService {

    private final Client client;
    private final WebTarget targetBase;

    // La URL base que nos diste
    private static final String URL_BASE = "http://10.205.246.155:8080/ConUni_RestFull_Java_GR06/api/ConversorUnidades";

    public ConversorClienteService() {
        this.client = ClientBuilder.newClient();
        this.targetBase = client.target(URL_BASE);
    }

    /**
     * Llama al endpoint POST /login
     */
    public RespuestaAutenticacion login(String username, String password) 
            throws ProcessingException, WebApplicationException {
        
        Usuario usuarioLogin = new Usuario(username, password);
        
        return targetBase.path("/login")
                         .request(MediaType.APPLICATION_JSON)
                         .post(Entity.json(usuarioLogin), RespuestaAutenticacion.class);
    }

    /**
     * Llama al endpoint GET /celsiusAFahrenheit/{celsius}
     */
    public double celsiusAFahrenheit(double celsius) 
            throws ProcessingException, WebApplicationException {
        
        // Construye la URL: .../ConversorUnidades/celsiusAFahrenheit/VALOR
        WebTarget target = targetBase.path("/celsiusAFahrenheit")
                                     .path(String.valueOf(celsius));
        
        // El servidor produce JSON, pero tu método devuelve 'double'.
        // JAX-RS lo manejará, pero es más seguro pedir JSON y obtener un Double.
        // Si el servidor envía "10.5" (como texto JSON), get(Double.class) funciona.
        return target.request(MediaType.APPLICATION_JSON)
                     .get(Double.class);
    }

    /**
     * Llama al endpoint GET /kilometroAMetro/{kilometros}
     */
    public double kilometroAMetro(double kilometros) 
            throws ProcessingException, WebApplicationException {
        
        WebTarget target = targetBase.path("/kilometroAMetro")
                                     .path(String.valueOf(kilometros));
        
        return target.request(MediaType.APPLICATION_JSON)
                     .get(Double.class);
    }
    
    /**
     * Llama al endpoint GET /kilogramoALibra/{kilogramos}
     */
    public double kilogramoALibra(double kilogramos) 
            throws ProcessingException, WebApplicationException {
        
        WebTarget target = targetBase.path("/kilogramoALibra")
                                     .path(String.valueOf(kilogramos));
        
        return target.request(MediaType.APPLICATION_JSON)
                     .get(Double.class);
    }
    
    // ... Aquí implementarías los demás métodos ...
    
    public void close() {
        client.close(); // Buena práctica cerrar el cliente al final
    }
}