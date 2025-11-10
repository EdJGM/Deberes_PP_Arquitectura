/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.service;

import com.fasterxml.jackson.databind.ObjectMapper;
import ec.edu.monster.modelo.RespuestaAutenticacion;
import ec.edu.monster.modelo.Usuario;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;

/**
 *
 * @author josue
 */
public class Cliente {
    
    private static final String BASE_URL = "http://10.63.57.155:8080/ConUni_RestFull_Java_GR06/api/ConversorUnidades";
    private static final HttpClient client = HttpClient.newHttpClient();
    private static final ObjectMapper mapper = new ObjectMapper();
    
        // ==================== LOGIN ====================
    public static RespuestaAutenticacion login(String user, String pass) throws Exception {
        Usuario u = new Usuario();
        u.setUsername(user);
        u.setPassword(pass);

        String json = mapper.writeValueAsString(u);

        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/login"))
                .header("Content-Type", "application/json")
                .POST(HttpRequest.BodyPublishers.ofString(json))
                .build();

        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());

        return mapper.readValue(response.body(), RespuestaAutenticacion.class);
    }
    
    // ==================== TEMPERATURA ====================
    public static double CelsiusAFahrenheit(double celsius) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/celsiusAFahrenheit/" + celsius))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double FahrenheitACelsius(double fahrenheit) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/fahrenheitACelsius/" + fahrenheit))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double CelsiusAKelvin(double celsius) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/celsiusAKelvin/" + celsius))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double KelvinACelsius(double kelvin) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/kelvinACelsius/" + kelvin))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double FahrenheitAKelvin(double fahrenheit) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/fahrenheitAKelvin/" + fahrenheit))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double KelvinAFahrenheit(double kelvin) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/kelvinAFahrenheit/" + kelvin))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    // ==================== LONGITUD ====================
    public static double MetroAKilometro(double metros) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/metroAKilometro/" + metros))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double KilometroAMetro(double kilometros) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/kilometroAMetro/" + kilometros))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double MetroAMilla(double metros) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/metroAMilla/" + metros))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double MillaAMetro(double millas) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/millaAMetro/" + millas))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double KilometroAMilla(double kilometros) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/kilometroAMilla/" + kilometros))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double MillaAKilometro(double millas) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/millaAKilometro/" + millas))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    // ==================== MASA ====================
    public static double KilogramoAGramo(double kilogramos) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/kilogramoAGramo/" + kilogramos))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double GramoAKilogramo(double gramos) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/gramoAKilogramo/" + gramos))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double KilogramoALibra(double kilogramos) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/kilogramoALibra/" + kilogramos))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double LibraAKilogramo(double libras) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/libraAKilogramo/" + libras))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double GramoALibra(double gramos) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/gramoALibra/" + gramos))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double LibraAGramo(double libras) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/libraAGramo/" + libras))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }   
}
