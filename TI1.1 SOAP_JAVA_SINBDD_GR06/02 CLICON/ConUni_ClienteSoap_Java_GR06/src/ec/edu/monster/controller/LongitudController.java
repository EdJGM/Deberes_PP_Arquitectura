package ec.edu.monster.controller;

import ec.edu.monster.util.ConexionSOAP;

/**
 * Controlador para conversiones de longitud
 * @author josue
 */
public class LongitudController {
    
    public String metroAKilometro(double metros) {
        try {
            double resultado = ConexionSOAP.getPort().metroAKilometro(metros);
            return metros + " m = " + String.format("%.4f", resultado) + " km";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    public String kilometroAMetro(double kilometros) {
        try {
            double resultado = ConexionSOAP.getPort().kilometroAMetro(kilometros);
            return kilometros + " km = " + String.format("%.2f", resultado) + " m";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    public String metroAMilla(double metros) {
        try {
            double resultado = ConexionSOAP.getPort().metroAMilla(metros);
            return metros + " m = " + String.format("%.4f", resultado) + " mi";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    public String millaAMetro(double millas) {
        try {
            double resultado = ConexionSOAP.getPort().millaAMetro(millas);
            return millas + " mi = " + String.format("%.2f", resultado) + " m";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    public String kilometroAMilla(double kilometros) {
        try {
            double resultado = ConexionSOAP.getPort().kilometroAMilla(kilometros);
            return kilometros + " km = " + String.format("%.4f", resultado) + " mi";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    public String millaAKilometro(double millas) {
        try {
            double resultado = ConexionSOAP.getPort().millaAKilometro(millas);
            return millas + " mi = " + String.format("%.4f", resultado) + " km";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
}