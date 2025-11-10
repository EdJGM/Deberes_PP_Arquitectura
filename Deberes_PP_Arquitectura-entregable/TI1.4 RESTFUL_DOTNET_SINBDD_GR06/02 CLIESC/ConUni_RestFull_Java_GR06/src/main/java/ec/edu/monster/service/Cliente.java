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
// SSL imports para ignorar certificados (uso local con autofirmado)
import javax.net.ssl.SSLContext;
import javax.net.ssl.SSLParameters;
import javax.net.ssl.TrustManager;
import javax.net.ssl.X509TrustManager;
import java.security.SecureRandom;
import java.security.cert.X509Certificate;

/**
 *
 * @author josue
 */
public class Cliente {
    
    static {
        System.setProperty("jdk.internal.httpclient.disableHostnameVerification", "true");
        System.out.println("[Cliente] HostnameVerification desactivada (propiedad de sistema)");
    }

    private static final String DEFAULT_BASE_URL = "https://10.63.57.155:7041/api/ConversorUnidades";
    private static final String BASE_URL = resolveBaseUrl();
    private static final HttpClient client = createInsecureHttpClient();
    private static final ObjectMapper mapper = new ObjectMapper();
    
    // Explicación: El error "No subject alternative names matching IP address" indica que el certificado TLS
    // del servidor no tiene una entrada Subject Alternative Name (SAN) que coincida con 10.63.57.155.
    // Aunque aquí forzamos la desactivación de verificación, la solución correcta en producción es
    // generar/instalar un certificado con SAN que incluya IP (IP.1 = 10.63.57.155) o un DNS que se use en la URL.

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
                .uri(URI.create(BASE_URL + "/temperatura/celsius-fahrenheit/" + celsius))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double FahrenheitACelsius(double fahrenheit) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/temperatura/fahrenheit-celsius/" + fahrenheit))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double CelsiusAKelvin(double celsius) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/temperatura/celsius-kelvin/" + celsius))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double KelvinACelsius(double kelvin) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/temperatura/kelvin-celsius/" + kelvin))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double FahrenheitAKelvin(double fahrenheit) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/temperatura/fahrenheit-kelvin/" + fahrenheit))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double KelvinAFahrenheit(double kelvin) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/temperatura/kelvin-fahrenheit/" + kelvin))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    // ==================== LONGITUD ====================
    public static double MetroAKilometro(double metros) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/longitud/metro-kilometro/" + metros))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double KilometroAMetro(double kilometros) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/longitud/kilometro-metro/" + kilometros))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double MetroAMilla(double metros) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/longitud/metro-milla/" + metros))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double MillaAMetro(double millas) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/longitud/milla-metro/" + millas))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double KilometroAMilla(double kilometros) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/longitud/kilometro-milla/" + kilometros))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double MillaAKilometro(double millas) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/longitud/milla-kilometro/" + millas))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    // ==================== MASA ====================
    public static double KilogramoAGramo(double kilogramos) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/masa/kilogramo-gramo/" + kilogramos))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double GramoAKilogramo(double gramos) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/masa/gramo-kilogramo/" + gramos))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double KilogramoALibra(double kilogramos) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/masa/kilogramo-libra/" + kilogramos))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double LibraAKilogramo(double libras) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/masa/libra-kilogramo/" + libras))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double GramoALibra(double gramos) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/masa/gramo-libra/" + gramos))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    public static double LibraAGramo(double libras) throws Exception {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(BASE_URL + "/masa/libra-gramo/" + libras))
                .GET()
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        return Double.parseDouble(response.body());
    }

    private static String resolveBaseUrl() {
        String sysProp = System.getProperty("API_BASE_URL");
        String envVar = System.getenv("API_BASE_URL");
        String url = sysProp != null && !sysProp.isBlank() ? sysProp : (envVar != null && !envVar.isBlank() ? envVar : DEFAULT_BASE_URL);
        System.out.println("[Cliente] Usando BASE_URL=" + url);
        return url.endsWith("/") ? url.substring(0, url.length()-1) : url; // quitar slash final si existe
    }

    private static HttpClient createInsecureHttpClient() {
        try {
            TrustManager[] trustAllCerts = new TrustManager[]{
                new X509TrustManager() {
                    @Override
                    public void checkClientTrusted(X509Certificate[] chain, String authType) {}
                    @Override
                    public void checkServerTrusted(X509Certificate[] chain, String authType) {}
                    @Override
                    public X509Certificate[] getAcceptedIssuers() { return new X509Certificate[0]; }
                }
            };
            SSLContext sslContext = SSLContext.getInstance("TLS");
            sslContext.init(null, trustAllCerts, new SecureRandom());
            SSLParameters sslParams = new SSLParameters();
            sslParams.setEndpointIdentificationAlgorithm(null);
            HttpClient hc = HttpClient.newBuilder()
                    .sslContext(sslContext)
                    .sslParameters(sslParams)
                    .build();
            System.out.println("[Cliente] HttpClient inseguro creado (ignora certificados y hostname)");
            return hc;
        } catch (Exception e) {
            throw new RuntimeException("No se pudo crear el HttpClient inseguro", e);
        }
    }
}
