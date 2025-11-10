package ec.edu.monster.controller;

import ec.edu.monster.util.ConexionSOAP;

/**
 * Controlador para conversiones de temperatura
 * @author josue
 */
public class TemperaturaController {
    
    /**
     * Convierte Celsius a Fahrenheit
     */
    public String celsiusAFahrenheit(double celsius) {
        try {
            double resultado = ConexionSOAP.getPort().celsiusAFahrenheit(celsius);
            return celsius + "°C = " + String.format("%.2f", resultado) + "°F";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    /**
     * Convierte Fahrenheit a Celsius
     */
    public String fahrenheitACelsius(double fahrenheit) {
        try {
            double resultado = ConexionSOAP.getPort().fahrenheitACelsius(fahrenheit);
            return fahrenheit + "°F = " + String.format("%.2f", resultado) + "°C";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    /**
     * Convierte Celsius a Kelvin
     */
    public String celsiusAKelvin(double celsius) {
        try {
            double resultado = ConexionSOAP.getPort().celsiusAKelvin(celsius);
            return celsius + "°C = " + String.format("%.2f", resultado) + "K";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    /**
     * Convierte Kelvin a Celsius
     */
    public String kelvinACelsius(double kelvin) {
        try {
            double resultado = ConexionSOAP.getPort().kelvinACelsius(kelvin);
            return kelvin + "K = " + String.format("%.2f", resultado) + "°C";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    /**
     * Convierte Fahrenheit a Kelvin
     */
    public String fahrenheitAKelvin(double fahrenheit) {
        try {
            double resultado = ConexionSOAP.getPort().fahrenheitAKelvin(fahrenheit);
            return fahrenheit + "°F = " + String.format("%.2f", resultado) + "K";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
    
    /**
     * Convierte Kelvin a Fahrenheit
     */
    public String kelvinAFahrenheit(double kelvin) {
        try {
            double resultado = ConexionSOAP.getPort().kelvinAFahrenheit(kelvin);
            return kelvin + "K = " + String.format("%.2f", resultado) + "°F";
        } catch (Exception e) {
            return "Error: " + e.getMessage();
        }
    }
}