package ec.edu.monster.controller;

import ec.edu.monster.util.ConexionSOAP;

/**
 * Controlador para conversiones de masa
 * Gestiona todas las operaciones de conversión entre unidades de masa
 * @author josue
 */
public class MasaController {
    
    /**
     * Convierte Kilogramos a Gramos
     * @param kilogramos valor en kilogramos
     * @return String con el resultado formateado
     */
    public String kilogramoAGramo(double kilogramos) {
        try {
            double resultado = ConexionSOAP.getPort().kilogramoAGramo(kilogramos);
            return kilogramos + " kg = " + String.format("%.2f", resultado) + " g";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    /**
     * Convierte Gramos a Kilogramos
     * @param gramos valor en gramos
     * @return String con el resultado formateado
     */
    public String gramoAKilogramo(double gramos) {
        try {
            double resultado = ConexionSOAP.getPort().gramoAKilogramo(gramos);
            return gramos + " g = " + String.format("%.4f", resultado) + " kg";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    /**
     * Convierte Kilogramos a Libras
     * @param kilogramos valor en kilogramos
     * @return String con el resultado formateado
     */
    public String kilogramoALibra(double kilogramos) {
        try {
            double resultado = ConexionSOAP.getPort().kilogramoALibra(kilogramos);
            return kilogramos + " kg = " + String.format("%.4f", resultado) + " lb";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    /**
     * Convierte Libras a Kilogramos
     * @param libras valor en libras
     * @return String con el resultado formateado
     */
    public String libraAKilogramo(double libras) {
        try {
            double resultado = ConexionSOAP.getPort().libraAKilogramo(libras);
            return libras + " lb = " + String.format("%.4f", resultado) + " kg";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    /**
     * Convierte Gramos a Libras
     * @param gramos valor en gramos
     * @return String con el resultado formateado
     */
    public String gramoALibra(double gramos) {
        try {
            double resultado = ConexionSOAP.getPort().gramoALibra(gramos);
            return gramos + " g = " + String.format("%.6f", resultado) + " lb";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    /**
     * Convierte Libras a Gramos
     * @param libras valor en libras
     * @return String con el resultado formateado
     */
    public String libraAGramo(double libras) {
        try {
            double resultado = ConexionSOAP.getPort().libraAGramo(libras);
            return libras + " lb = " + String.format("%.2f", resultado) + " g";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
}