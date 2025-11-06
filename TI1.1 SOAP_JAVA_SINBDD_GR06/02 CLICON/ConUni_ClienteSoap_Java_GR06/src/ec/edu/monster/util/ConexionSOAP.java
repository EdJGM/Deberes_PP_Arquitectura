package ec.edu.monster.util;

import ec.edu.monster.cliente.ws.ConversorUnidadesWS;
import ec.edu.monster.cliente.ws.ConversorUnidadesWS_Service;

/**
 * Gestiona la conexión con el servicio web SOAP
 * Singleton pattern para mantener una única conexión
 * @author josue
 */
public class ConexionSOAP {
    
    private static ConversorUnidadesWS_Service service;
    private static ConversorUnidadesWS port;
    private static boolean conectado = false;
    
    private ConexionSOAP() {
        // Constructor privado para Singleton
    }
    
    /**
     * Inicializa la conexión con el servicio web
     * @return true si la conexión fue exitosa
     */
    public static boolean inicializar() {
        try {
            service = new ConversorUnidadesWS_Service();
            port = service.getConversorUnidadesWSPort();
            conectado = true;
            System.out.println("✓ Conexión establecida con el servicio web\n");
            return true;
        } catch (Exception e) {
            System.err.println("❌ Error al conectar con el servicio web:");
            System.err.println("   Asegúrese de que el servidor esté ejecutándose en:");
            System.err.println("   http://localhost:8080/ConUni_Soap_Java_GR06/ConversorUnidadesWS");
            conectado = false;
            return false;
        }
    }
    
    /**
     * Obtiene el puerto del servicio web
     * @return puerto SOAP
     */
    public static ConversorUnidadesWS getPort() {
        if (!conectado) {
            throw new IllegalStateException("No hay conexión establecida con el servicio web");
        }
        return port;
    }
    
    /**
     * Verifica si hay conexión activa
     * @return true si está conectado
     */
    public static boolean isConectado() {
        return conectado;
    }
    
    /**
     * Cierra la conexión y libera recursos
     */
    public static void cerrar() {
        service = null;
        port = null;
        conectado = false;
    }
}