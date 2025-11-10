package ec.edu.monster.util;

import java.util.Scanner;

/**
 * Utilidad para lectura y validación de entradas del usuario
 * Singleton pattern

 */
public class LectorEntrada {
    
    private static Scanner scanner;
    
    private LectorEntrada() {
        // Constructor privado para Singleton
    }
    
    /**
     * Obtiene la instancia del Scanner
     * @return Scanner global
     */
    public static Scanner getScanner() {
        if (scanner == null) {
            scanner = new Scanner(System.in);
        }
        return scanner;
    }
    
    /**
     * Lee un número entero del usuario
     * @return entero ingresado, -1 si hay error
     */
    public static int leerEntero() {
        try {
            return Integer.parseInt(getScanner().nextLine());
        } catch (NumberFormatException e) {
            return -1;
        }
    }
    
    /**
     * Lee un número entero con mensaje personalizado
     * @param mensaje mensaje a mostrar
     * @return entero ingresado, -1 si hay error
     */
    public static int leerEntero(String mensaje) {
        System.out.print(mensaje);
        return leerEntero();
    }
    
    /**
     * Lee un número double del usuario
     * @return double ingresado, 0 si hay error
     */
    public static double leerDouble() {
        try {
            return Double.parseDouble(getScanner().nextLine());
        } catch (NumberFormatException e) {
            System.out.println("❌ Valor inválido, usando 0");
            return 0;
        }
    }
    
    /**
     * Lee un número double con mensaje personalizado
     * @param mensaje mensaje a mostrar
     * @return double ingresado, 0 si hay error
     */
    public static double leerDouble(String mensaje) {
        System.out.print(mensaje);
        return leerDouble();
    }
    
    /**
     * Lee una línea de texto
     * @return texto ingresado
     */
    public static String leerLinea() {
        return getScanner().nextLine();
    }
    
    /**
     * Lee una línea de texto con mensaje personalizado
     * @param mensaje mensaje a mostrar
     * @return texto ingresado
     */
    public static String leerLinea(String mensaje) {
        System.out.print(mensaje);
        return leerLinea();
    }
    
    /**
     * Cierra el scanner
     */
    public static void cerrar() {
        if (scanner != null) {
            scanner.close();
            scanner = null;
        }
    }
}