/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.services;

/**
 *
 * @author josue
 */
public class ConversorLongitud {
    
    // Metro <-> Kilómetro
    public double metroAKilometro(double metros) {
        return metros / 1000.0;
    }
    
    public double kilometroAMetro(double kilometros) {
        return kilometros * 1000.0;
    }
    
    // Metro <-> Milla
    public double metroAMilla(double metros) {
        return metros / 1609.344;
    }
    
    public double millaAMetro(double millas) {
        return millas * 1609.344;
    }
    
    // Kilómetro <-> Milla
    public double kilometroAMilla(double kilometros) {
        return metroAMilla(kilometroAMetro(kilometros));
    }
    
    public double millaAKilometro(double millas) {
        return metroAKilometro(millaAMetro(millas));
    }
}
