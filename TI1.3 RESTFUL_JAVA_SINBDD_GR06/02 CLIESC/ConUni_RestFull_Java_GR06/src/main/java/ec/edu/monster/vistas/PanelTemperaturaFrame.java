/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.vistas;

import ec.edu.monster.service.Cliente;
import javax.swing.*;
import java.awt.*;

/**
 * Panel de conversión de temperaturas
 * @author josue
 */
public class PanelTemperaturaFrame extends JFrame {
    
    private String usuario;
    private Cliente servicio;
    
    private JTextField txtValor;
    private JComboBox<String> cmbOrigen;
    private JComboBox<String> cmbDestino;
    private JLabel lblResultado;
    
    public PanelTemperaturaFrame(String usuario) {
        this.usuario = usuario;
        initComponents();
    }
    
    private void initComponents() {
        setTitle("Conversión de Temperatura");
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
        setSize(500, 450);
        setLocationRelativeTo(null);
        setResizable(false);
        
        JPanel mainPanel = new JPanel(new BorderLayout(10, 10));
        mainPanel.setBackground(new Color(236, 240, 241));
        mainPanel.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));
        
        // Header
        JPanel headerPanel = new JPanel();
        headerPanel.setBackground(new Color(231, 76, 60));
        headerPanel.setBorder(BorderFactory.createEmptyBorder(15, 20, 15, 20));
        
        JLabel lblTitulo = new JLabel("CONVERSIÓN DE TEMPERATURA");
        lblTitulo.setFont(new Font("Arial", Font.BOLD, 22));
        lblTitulo.setForeground(Color.WHITE);
        headerPanel.add(lblTitulo);
        
        // Panel central
        JPanel centerPanel = new JPanel(new GridBagLayout());
        centerPanel.setBackground(Color.WHITE);
        centerPanel.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(new Color(189, 195, 199), 1),
            BorderFactory.createEmptyBorder(30, 30, 30, 30)
        ));
        
        GridBagConstraints gbc = new GridBagConstraints();
        gbc.insets = new Insets(10, 10, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        
        // Valor
        JLabel lblValor = new JLabel("Valor a convertir:");
        lblValor.setFont(new Font("Arial", Font.BOLD, 14));
        gbc.gridx = 0;
        gbc.gridy = 0;
        gbc.gridwidth = 2;
        centerPanel.add(lblValor, gbc);
        
        txtValor = new JTextField(20);
        txtValor.setFont(new Font("Arial", Font.PLAIN, 16));
        txtValor.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(new Color(189, 195, 199)),
            BorderFactory.createEmptyBorder(8, 10, 8, 10)
        ));
        gbc.gridy = 1;
        centerPanel.add(txtValor, gbc);
        
        // Unidad origen
        JLabel lblOrigen = new JLabel("De:");
        lblOrigen.setFont(new Font("Arial", Font.BOLD, 14));
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        centerPanel.add(lblOrigen, gbc);
        
        String[] unidades = {"Celsius", "Fahrenheit", "Kelvin"};
        cmbOrigen = new JComboBox<>(unidades);
        cmbOrigen.setFont(new Font("Arial", Font.PLAIN, 14));
        gbc.gridx = 1;
        centerPanel.add(cmbOrigen, gbc);
        
        // Unidad destino
        JLabel lblDestino = new JLabel("A:");
        lblDestino.setFont(new Font("Arial", Font.BOLD, 14));
        gbc.gridx = 0;
        gbc.gridy = 3;
        centerPanel.add(lblDestino, gbc);
        
        cmbDestino = new JComboBox<>(unidades);
        cmbDestino.setFont(new Font("Arial", Font.PLAIN, 14));
        cmbDestino.setSelectedIndex(1);
        gbc.gridx = 1;
        centerPanel.add(cmbDestino, gbc);
        
        // Botón convertir
        JButton btnConvertir = new JButton("CONVERTIR");
        btnConvertir.setFont(new Font("Arial", Font.BOLD, 16));
        btnConvertir.setBackground(new Color(231, 76, 60));
        btnConvertir.setForeground(Color.WHITE);
        btnConvertir.setFocusPainted(false);
        btnConvertir.setBorderPainted(false);
        btnConvertir.setCursor(new Cursor(Cursor.HAND_CURSOR));
        btnConvertir.setPreferredSize(new Dimension(200, 40));
        btnConvertir.addActionListener(e -> convertir());
        
        gbc.gridx = 0;
        gbc.gridy = 4;
        gbc.gridwidth = 2;
        gbc.insets = new Insets(20, 10, 10, 10);
        centerPanel.add(btnConvertir, gbc);
        
        // Resultado
        JPanel resultPanel = new JPanel();
        resultPanel.setBackground(new Color(46, 204, 113));
        resultPanel.setBorder(BorderFactory.createEmptyBorder(15, 20, 15, 20));
        
        lblResultado = new JLabel("Resultado: ---");
        lblResultado.setFont(new Font("Arial", Font.BOLD, 18));
        lblResultado.setForeground(Color.WHITE);
        resultPanel.add(lblResultado);
        
        gbc.gridy = 5;
        gbc.insets = new Insets(20, 10, 10, 10);
        centerPanel.add(resultPanel, gbc);
        
        // Botón volver
        JButton btnVolver = new JButton("← Volver al Menú");
        btnVolver.setFont(new Font("Arial", Font.PLAIN, 12));
        btnVolver.setForeground(new Color(52, 73, 94));
        btnVolver.setContentAreaFilled(false);
        btnVolver.setBorderPainted(false);
        btnVolver.setCursor(new Cursor(Cursor.HAND_CURSOR));
        btnVolver.addActionListener(e -> dispose());
        
        JPanel footerPanel = new JPanel();
        footerPanel.setBackground(new Color(236, 240, 241));
        footerPanel.add(btnVolver);
        
        mainPanel.add(headerPanel, BorderLayout.NORTH);
        mainPanel.add(centerPanel, BorderLayout.CENTER);
        mainPanel.add(footerPanel, BorderLayout.SOUTH);
        
        add(mainPanel);
    }
    
    private void convertir() {
        try {
            double valor = Double.parseDouble(txtValor.getText().trim());
            String origen = (String) cmbOrigen.getSelectedItem();
            String destino = (String) cmbDestino.getSelectedItem();
            
            if (origen.equals(destino)) {
                lblResultado.setText("Resultado: " + String.format("%.2f", valor));
                return;
            }
            
            double resultado = 0;
            
            // Celsius a...
            if (origen.equals("Celsius") && destino.equals("Fahrenheit")) {
                resultado = servicio.CelsiusAFahrenheit(valor);
            } else if (origen.equals("Celsius") && destino.equals("Kelvin")) {
                resultado = servicio.CelsiusAKelvin(valor);
            }
            // Fahrenheit a...
            else if (origen.equals("Fahrenheit") && destino.equals("Celsius")) {
                resultado = servicio.FahrenheitACelsius(valor);
            } else if (origen.equals("Fahrenheit") && destino.equals("Kelvin")) {
                resultado = servicio.FahrenheitAKelvin(valor);
            }
            // Kelvin a...
            else if (origen.equals("Kelvin") && destino.equals("Celsius")) {
                resultado = servicio.KelvinACelsius(valor);
            } else if (origen.equals("Kelvin") && destino.equals("Fahrenheit")) {
                resultado = servicio.KelvinAFahrenheit(valor);
            }
            
            lblResultado.setText("Resultado: " + String.format("%.2f", resultado) + " " + destino);
            
        } catch (NumberFormatException e) {
            JOptionPane.showMessageDialog(this,
                "Por favor ingrese un número válido",
                "Error de Formato",
                JOptionPane.ERROR_MESSAGE);
        } catch (Exception e) {
            JOptionPane.showMessageDialog(this,
                "Error al realizar la conversión: " + e.getMessage(),
                "Error",
                JOptionPane.ERROR_MESSAGE);
        }
    }
}