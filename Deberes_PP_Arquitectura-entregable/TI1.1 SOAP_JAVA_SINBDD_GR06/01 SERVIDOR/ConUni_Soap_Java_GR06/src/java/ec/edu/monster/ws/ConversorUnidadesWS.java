/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.ws;

import ec.edu.monster.services.AutenticacionService;
import ec.edu.monster.modelo.RespuestaAutenticacion;
import ec.edu.monster.modelo.Usuario;
import ec.edu.monster.services.ConversorTemperatura;
import ec.edu.monster.services.ConversorLongitud;
import ec.edu.monster.services.ConversorMasa;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import jakarta.jws.WebService;
/**
 *
 * @author josue
 */
@WebService(serviceName = "ConversorUnidadesWS")
public class ConversorUnidadesWS {

    private ConversorTemperatura conversorTemp = new ConversorTemperatura();
    private ConversorLongitud conversorLong = new ConversorLongitud();
    private ConversorMasa conversorMasa = new ConversorMasa();
    
    // ==================== AUTENTICACIÓN ====================
    @WebMethod(operationName = "login")
    public RespuestaAutenticacion login(
            @WebParam(name = "username") String username,
            @WebParam(name = "password") String password) {

        Usuario usuario = AutenticacionService.autenticar(username, password);

        if (usuario != null) {
            return new RespuestaAutenticacion(true, "Login exitoso", usuario);
        } else {
            return new RespuestaAutenticacion(false, "Credenciales inválidas");
        }
    }
    
    // ==================== TEMPERATURA ====================
    @WebMethod(operationName = "celsiusAFahrenheit")
    public double celsiusAFahrenheit(@WebParam(name = "celsius") double celsius) {
        return conversorTemp.celsiusAFahrenheit(celsius);
    }
    
    @WebMethod(operationName = "fahrenheitACelsius")
    public double fahrenheitACelsius(@WebParam(name = "fahrenheit") double fahrenheit) {
        return conversorTemp.fahrenheitACelsius(fahrenheit);
    }
    
    @WebMethod(operationName = "celsiusAKelvin")
    public double celsiusAKelvin(@WebParam(name = "celsius") double celsius) {
        return conversorTemp.celsiusAKelvin(celsius);
    }
    
    @WebMethod(operationName = "kelvinACelsius")
    public double kelvinACelsius(@WebParam(name = "kelvin") double kelvin) {
        return conversorTemp.kelvinACelsius(kelvin);
    }
    
    @WebMethod(operationName = "fahrenheitAKelvin")
    public double fahrenheitAKelvin(@WebParam(name = "fahrenheit") double fahrenheit) {
        return conversorTemp.fahrenheitAKelvin(fahrenheit);
    }
    
    @WebMethod(operationName = "kelvinAFahrenheit")
    public double kelvinAFahrenheit(@WebParam(name = "kelvin") double kelvin) {
        return conversorTemp.kelvinAFahrenheit(kelvin);
    }
    
    // ==================== LONGITUD ====================
    @WebMethod(operationName = "metroAKilometro")
    public double metroAKilometro(@WebParam(name = "metros") double metros) {
        return conversorLong.metroAKilometro(metros);
    }
    
    @WebMethod(operationName = "kilometroAMetro")
    public double kilometroAMetro(@WebParam(name = "kilometros") double kilometros) {
        return conversorLong.kilometroAMetro(kilometros);
    }
    
    @WebMethod(operationName = "metroAMilla")
    public double metroAMilla(@WebParam(name = "metros") double metros) {
        return conversorLong.metroAMilla(metros);
    }
    
    @WebMethod(operationName = "millaAMetro")
    public double millaAMetro(@WebParam(name = "millas") double millas) {
        return conversorLong.millaAMetro(millas);
    }
    
    @WebMethod(operationName = "kilometroAMilla")
    public double kilometroAMilla(@WebParam(name = "kilometros") double kilometros) {
        return conversorLong.kilometroAMilla(kilometros);
    }
    
    @WebMethod(operationName = "millaAKilometro")
    public double millaAKilometro(@WebParam(name = "millas") double millas) {
        return conversorLong.millaAKilometro(millas);
    }
    
    // ==================== MASA ====================
    @WebMethod(operationName = "kilogramoAGramo")
    public double kilogramoAGramo(@WebParam(name = "kilogramos") double kilogramos) {
        return conversorMasa.kilogramoAGramo(kilogramos);
    }
    
    @WebMethod(operationName = "gramoAKilogramo")
    public double gramoAKilogramo(@WebParam(name = "gramos") double gramos) {
        return conversorMasa.gramoAKilogramo(gramos);
    }
    
    @WebMethod(operationName = "kilogramoALibra")
    public double kilogramoALibra(@WebParam(name = "kilogramos") double kilogramos) {
        return conversorMasa.kilogramoALibra(kilogramos);
    }
    
    @WebMethod(operationName = "libraAKilogramo")
    public double libraAKilogramo(@WebParam(name = "libras") double libras) {
        return conversorMasa.libraAKilogramo(libras);
    }
    
    @WebMethod(operationName = "gramoALibra")
    public double gramoALibra(@WebParam(name = "gramos") double gramos) {
        return conversorMasa.gramoALibra(gramos);
    }
    
    @WebMethod(operationName = "libraAGramo")
    public double libraAGramo(@WebParam(name = "libras") double libras) {
        return conversorMasa.libraAGramo(libras);
    }
}