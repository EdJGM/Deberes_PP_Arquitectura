/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/GenericResource.java to edit this template
 */
package ec.edu.monster.controlador;

import ec.edu.monster.modelo.RespuestaAutenticacion;
import ec.edu.monster.modelo.Usuario;
import ec.edu.monster.services.AutenticacionService;
import ec.edu.monster.services.ConversorLongitud;
import ec.edu.monster.services.ConversorMasa;
import ec.edu.monster.services.ConversorTemperatura;
import jakarta.ws.rs.core.Context;
import jakarta.ws.rs.core.UriInfo;
import jakarta.ws.rs.Consumes;
import jakarta.ws.rs.GET;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.PUT;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.PathParam;
import jakarta.ws.rs.core.MediaType;

/**
 * REST Web Service
 *
 * @author josue
 */
@Path("ConversorUnidades")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class ConversorUnidadesResource {

    private ConversorTemperatura conversorTemp = new ConversorTemperatura();
    private ConversorLongitud conversorLong = new ConversorLongitud();
    private ConversorMasa conversorMasa = new ConversorMasa();

    // ==================== HEALTH CHECK ====================
    @GET
    @Path("/health")
    @Produces(MediaType.APPLICATION_JSON)
    public java.util.Map<String, Object> healthCheck() {
        java.util.Map<String, Object> response = new java.util.LinkedHashMap<>();

        response.put("status", "OK");
        response.put("timestamp", java.time.Instant.now().toString());
        response.put("version", "1.0.0");

        java.util.Map<String, Boolean> services = new java.util.LinkedHashMap<>();
        services.put("temperatura", true);
        services.put("longitud", true);
        services.put("masa", true);
        services.put("autenticacion", true);

        response.put("servicesAvailable", services);

        return response;
    }   
    // ==================== AUTENTICACIÓN ====================
    @POST
    @Path("/login")
    public RespuestaAutenticacion login(Usuario user) {
        Usuario usuario = AutenticacionService.autenticar(user.getUsername(), user.getPassword());

        if (usuario != null) {
            return new RespuestaAutenticacion(true, "Login exitoso", usuario);
        } else {
            return new RespuestaAutenticacion(false, "Credenciales inválidas");
        }
    }

    // ==================== TEMPERATURA ====================
    @GET
    @Path("/celsiusAFahrenheit/{celsius}")
    public double celsiusAFahrenheit(@PathParam("celsius") double celsius) {
        return conversorTemp.celsiusAFahrenheit(celsius);
    }

    @GET
    @Path("/fahrenheitACelsius/{fahrenheit}")
    public double fahrenheitACelsius(@PathParam("fahrenheit") double fahrenheit) {
        return conversorTemp.fahrenheitACelsius(fahrenheit);
    }

    @GET
    @Path("/celsiusAKelvin/{celsius}")
    public double celsiusAKelvin(@PathParam("celsius") double celsius) {
        return conversorTemp.celsiusAKelvin(celsius);
    }

    @GET
    @Path("/kelvinACelsius/{kelvin}")
    public double kelvinACelsius(@PathParam("kelvin") double kelvin) {
        return conversorTemp.kelvinACelsius(kelvin);
    }

    @GET
    @Path("/fahrenheitAKelvin/{fahrenheit}")
    public double fahrenheitAKelvin(@PathParam("fahrenheit") double fahrenheit) {
        return conversorTemp.fahrenheitAKelvin(fahrenheit);
    }

    @GET
    @Path("/kelvinAFahrenheit/{kelvin}")
    public double kelvinAFahrenheit(@PathParam("kelvin") double kelvin) {
        return conversorTemp.kelvinAFahrenheit(kelvin);
    }

    // ==================== LONGITUD ====================
    @GET
    @Path("/metroAKilometro/{metros}")
    public double metroAKilometro(@PathParam("metros") double metros) {
        return conversorLong.metroAKilometro(metros);
    }

    @GET
    @Path("/kilometroAMetro/{kilometros}")
    public double kilometroAMetro(@PathParam("kilometros") double kilometros) {
        return conversorLong.kilometroAMetro(kilometros);
    }

    @GET
    @Path("/metroAMilla/{metros}")
    public double metroAMilla(@PathParam("metros") double metros) {
        return conversorLong.metroAMilla(metros);
    }

    @GET
    @Path("/millaAMetro/{millas}")
    public double millaAMetro(@PathParam("millas") double millas) {
        return conversorLong.millaAMetro(millas);
    }

    @GET
    @Path("/kilometroAMilla/{kilometros}")
    public double kilometroAMilla(@PathParam("kilometros") double kilometros) {
        return conversorLong.kilometroAMilla(kilometros);
    }

    @GET
    @Path("/millaAKilometro/{millas}")
    public double millaAKilometro(@PathParam("millas") double millas) {
        return conversorLong.millaAKilometro(millas);
    }

    // ==================== MASA ====================
    @GET
    @Path("/kilogramoAGramo/{kilogramos}")
    public double kilogramoAGramo(@PathParam("kilogramos") double kilogramos) {
        return conversorMasa.kilogramoAGramo(kilogramos);
    }

    @GET
    @Path("/gramoAKilogramo/{gramos}")
    public double gramoAKilogramo(@PathParam("gramos") double gramos) {
        return conversorMasa.gramoAKilogramo(gramos);
    }

    @GET
    @Path("/kilogramoALibra/{kilogramos}")
    public double kilogramoALibra(@PathParam("kilogramos") double kilogramos) {
        return conversorMasa.kilogramoALibra(kilogramos);
    }

    @GET
    @Path("/libraAKilogramo/{libras}")
    public double libraAKilogramo(@PathParam("libras") double libras) {
        return conversorMasa.libraAKilogramo(libras);
    }

    @GET
    @Path("/gramoALibra/{gramos}")
    public double gramoALibra(@PathParam("gramos") double gramos) {
        return conversorMasa.gramoALibra(gramos);
    }

    @GET
    @Path("/libraAGramo/{libras}")
    public double libraAGramo(@PathParam("libras") double libras) {
        return conversorMasa.libraAGramo(libras);
    }
}
