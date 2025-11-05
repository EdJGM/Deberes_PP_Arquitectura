/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.cliente.controlador;


import ec.edu.monster.cliente.modelo.RespuestaAutenticacion;
import ec.edu.monster.cliente.modelo.Usuario;
import ec.edu.monster.cliente.servicio.ConversorClienteService;
import ec.edu.monster.cliente.vista.ConsoleView;
import jakarta.ws.rs.ProcessingException;
import jakarta.ws.rs.WebApplicationException;
import jakarta.ws.rs.core.Response;

public class AppController {

    private final ConsoleView vista;
    private final ConversorClienteService servicio;
    private Usuario usuarioLogueado;

    public AppController() {
        this.vista = new ConsoleView();
        this.servicio = new ConversorClienteService();
    }

    /**
     * Inicia la aplicación
     */
    public void iniciar() {
        if (!manejarLogin()) {
            vista.mostrarError("Login fallido. El programa terminará.");
            servicio.close();
            return;
        }
        
        vista.mostrarMensaje("¡Bienvenido, " + usuarioLogueado.getUsername() + "!");
        
        ejecutarMenuPrincipal();
        
        // Al salir del bucle, cerrar el cliente
        servicio.close();
        vista.mostrarMensaje("Cerrando conexión. Adiós.");
    }

    /**
     * Valida al usuario contra el API
     */
    private boolean manejarLogin() {
        try {
            Usuario credenciales = vista.pedirCredenciales();
            RespuestaAutenticacion respuesta = servicio.login(credenciales.getUsername(), credenciales.getPassword());
            
            if (respuesta.isExitoso()) {
                // Construimos un Usuario local con el username que devuelve el servidor
                Usuario u = new Usuario();
                u.setUsername(respuesta.getUsername());
                this.usuarioLogueado = u;
                return true;
            } else {
                vista.mostrarError(respuesta.getMensaje());
                return false;
            }
            
        } catch (ProcessingException e) {
            // Error de conexión (Servidor caído)
            vista.mostrarError("No se pudo conectar al servidor. Verifique que esté encendido.");
            return false;
        } catch (Exception e) {
            vista.mostrarError("Ocurrió un error inesperado en el login: " + e.getMessage());
            return false;
        }
    }

    /**
     * Bucle principal del menú
     */
    private void ejecutarMenuPrincipal() {
        boolean salir = false;
        while (!salir) {
            int opcion = vista.mostrarMenuPrincipal();
            
            // Validamos la ejecución de la API
            try {
                switch (opcion) {
                    case 1:
                        convertirCelsiusAFahrenheit();
                        break;
                    case 2:
                        convertirKilometrosAMetros();
                        break;
                    case 3:
                        convertirKilogramosALibras();
                        break;
                    case 0:
                        salir = true;
                        break;
                    default:
                        vista.mostrarError("Opción no válida.");
                }
            } catch (WebApplicationException e) {
                // Errores del API (ej. 404 Not Found si la URL está mal)
                Response res = e.getResponse();
                vista.mostrarError("Error del servidor (HTTP " + res.getStatus() + 
                                   "): " + e.getMessage());
            } catch (ProcessingException e) {
                // Servidor se cayó en medio de la operación
                vista.mostrarError("Se perdió la conexión con el servidor.");
                salir = true; // Forzamos salida
            } catch (Exception e) {
                vista.mostrarError("Error inesperado: " + e.getMessage());
            }
        }
    }

    // --- Métodos de casos de uso ---
    
    private void convertirCelsiusAFahrenheit() {
        double celsius = vista.pedirValor("grados Celsius");
        double fahrenheit = servicio.celsiusAFahrenheit(celsius);
        vista.mostrarMensaje(celsius + "°C son " + String.format("%.2f", fahrenheit) + "°F");
    }

    private void convertirKilometrosAMetros() {
        double km = vista.pedirValor("kilómetros");
        double m = servicio.kilometroAMetro(km);
        vista.mostrarMensaje(km + " km son " + String.format("%.2f", m) + " m");
    }

    private void convertirKilogramosALibras() {
        double kg = vista.pedirValor("kilogramos");
        double lb = servicio.kilogramoALibra(kg);
        vista.mostrarMensaje(kg + " kg son " + String.format("%.2f", lb) + " lb");
    }
}