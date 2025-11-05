/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.cliente.vista;


import ec.edu.monster.cliente.modelo.Usuario;
import java.util.InputMismatchException;
import java.util.Scanner;

public class ConsoleView {

    private final Scanner scanner;

    public ConsoleView() {
        this.scanner = new Scanner(System.in);
    }

    public void mostrarMensaje(String mensaje) {
        System.out.println(mensaje);
    }

    public void mostrarError(String error) {
        System.err.println("ERROR: " + error);
    }

    public Usuario pedirCredenciales() {
        System.out.println("--- INICIO DE SESIÓN ---");
        System.out.print("Usuario: ");
        String user = scanner.nextLine();
        System.out.print("Contraseña: ");
        String pass = scanner.nextLine();
        return new Usuario(user, pass);
    }

    public int mostrarMenuPrincipal() {
        System.out.println("\n--- MENÚ CONVERSOR ---");
        System.out.println("1. Celsius a Fahrenheit");
        System.out.println("2. Kilómetros a Metros");
        System.out.println("3. Kilogramos a Libras");
        System.out.println("... (agrega más opciones)");
        System.out.println("0. Salir");
        System.out.print("Elija una opción: ");
        
        return leerEnteroValidado();
    }

    public double pedirValor(String prompt) {
        System.out.print("Ingrese " + prompt + ": ");
        return leerDoubleValidado();
    }

    // --- Métodos de Validación de Entrada ---
    
    private int leerEnteroValidado() {
        while (true) {
            try {
                return Integer.parseInt(scanner.nextLine());
            } catch (NumberFormatException e) {
                mostrarError("Debe ingresar un número entero.");
                System.out.print("Intente de nuevo: ");
            }
        }
    }

    private double leerDoubleValidado() {
        while (true) {
            try {
                return Double.parseDouble(scanner.nextLine());
            } catch (NumberFormatException e) {
                mostrarError("Debe ingresar un número válido (ej. 10.5).");
                System.out.print("Intente de nuevo: ");
            }
        }
    }
}