package ec.edu.monster.service;

import javax.net.ssl.SSLSocketFactory;
import javax.net.ssl.SSLSocket;
import javax.net.ssl.SSLSession;
import java.security.cert.Certificate;
import java.security.cert.X509Certificate;
import java.util.Collection;
import java.util.List;

/**
 * Herramienta de depuración para imprimir el certificado presentado por el servidor
 * y los Subject Alternative Names (SAN). Úsalo para confirmar por qué falla
 * la verificación de hostname con la IP.
 *
 * Ejecución:
 *   java -cp target/classes ec.edu.monster.service.SslDebug 10.63.57.155 7041
 */
public class SslDebug {
    public static void main(String[] args) {
        if (args.length < 2) {
            System.out.println("Uso: java SslDebug <host> <puerto>");
            return;
        }
        String host = args[0];
        int port = Integer.parseInt(args[1]);
        System.out.println("[SslDebug] Conectando a " + host + ":" + port + " ...");
        try {
            SSLSocketFactory factory = (SSLSocketFactory) SSLSocketFactory.getDefault();
            try (SSLSocket socket = (SSLSocket) factory.createSocket(host, port)) {
                socket.startHandshake();
                SSLSession session = socket.getSession();
                Certificate[] chain = session.getPeerCertificates();
                System.out.println("[SslDebug] Certificados recibidos: " + chain.length);
                for (int i = 0; i < chain.length; i++) {
                    if (chain[i] instanceof X509Certificate cert) {
                        System.out.println("----- Certificado " + i + " -----");
                        System.out.println("Subject: " + cert.getSubjectX500Principal());
                        System.out.println("Issuer : " + cert.getIssuerX500Principal());
                        System.out.println("Expires: " + cert.getNotAfter());

                        try {
                            Collection<List<?>> san = cert.getSubjectAlternativeNames();
                            if (san == null) {
                                System.out.println("SAN: (ninguno)");
                            } else {
                                System.out.println("SAN:");
                                for (List<?> item : san) {
                                    // item[0] = tipo (2=DNS, 7=IP), item[1] = valor
                                    System.out.println("  Tipo=" + item.get(0) + " Valor=" + item.get(1));
                                }
                            }
                        } catch (Exception e) {
                            System.out.println("No se pudo leer SAN: " + e.getMessage());
                        }
                    } else {
                        System.out.println("Certificado " + i + " no es X509Certificate");
                    }
                }
            }
            System.out.println("[SslDebug] Hecho.");
        } catch (Exception e) {
            System.out.println("[SslDebug] Error: " + e.getClass().getName() + ": " + e.getMessage());
            e.printStackTrace(System.out);
        }
    }
}

