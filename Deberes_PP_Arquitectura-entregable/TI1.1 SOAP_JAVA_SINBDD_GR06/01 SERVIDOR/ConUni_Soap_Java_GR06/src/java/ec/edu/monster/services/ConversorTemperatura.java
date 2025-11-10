/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.services;

/**
 *
 * @author josue
 */
public class ConversorTemperatura {
    public double celsiusAFahrenheit(double celsius) {
        return (celsius * 9/5) + 32;
    }
    
    public double fahrenheitACelsius(double fahrenheit) {
        return (fahrenheit - 32) * 5/9;
    }
    
    public double celsiusAKelvin(double celsius) {
        return celsius + 273.15;
    }
    
    public double kelvinACelsius(double kelvin) {
        return kelvin - 273.15;
    }
    
    public double fahrenheitAKelvin(double fahrenheit) {
        return celsiusAKelvin(fahrenheitACelsius(fahrenheit));
    }
    
    public double kelvinAFahrenheit(double kelvin) {
        return celsiusAFahrenheit(kelvinACelsius(kelvin));
    }    
}
