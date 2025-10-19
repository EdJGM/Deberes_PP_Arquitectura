/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.services;

/**
 *
 * @author josue
 */
public class ConversorMasa {
    
    // Kilogramo <-> Gramo
    public double kilogramoAGramo(double kilogramos) {
        return kilogramos * 1000.0;
    }
    
    public double gramoAKilogramo(double gramos) {
        return gramos / 1000.0;
    }
    
    // Kilogramo <-> Libra
    public double kilogramoALibra(double kilogramos) {
        return kilogramos * 2.20462;
    }
    
    public double libraAKilogramo(double libras) {
        return libras / 2.20462;
    }
    
    // Gramo <-> Libra
    public double gramoALibra(double gramos) {
        return kilogramoALibra(gramoAKilogramo(gramos));
    }
    
    public double libraAGramo(double libras) {
        return kilogramoAGramo(libraAKilogramo(libras));
    }
}