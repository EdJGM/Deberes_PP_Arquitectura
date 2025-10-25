/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.vistas;

import javax.swing.*;
import java.awt.*;

/**
 * Menú principal del sistema de conversión
 * @author josue
 */
public class MenuPrincipalFrame extends JFrame {
    
    private String usuario;
    
    public MenuPrincipalFrame(String usuario) {
        this.usuario = usuario;
        initComponents();
    }
    
    private void initComponents() {
        setTitle("Sistema de Conversión de Unidades - Menú Principal");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(600, 500);
        setLocationRelativeTo(null);
        setResizable(false);
        
        // Panel principal
        JPanel mainPanel = new JPanel();
        mainPanel.setLayout(new BorderLayout(10, 10));
        mainPanel.setBackground(new Color(236, 240, 241));
        mainPanel.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));
        
        // Header
        JPanel headerPanel = new JPanel(new BorderLayout());
        headerPanel.setBackground(new Color(52, 73, 94));
        headerPanel.setBorder(BorderFactory.createEmptyBorder(15, 20, 15, 20));
        
        JLabel lblTitulo = new JLabel("CONVERSOR DE UNIDADES");
        lblTitulo.setFont(new Font("Arial", Font.BOLD, 26));
        lblTitulo.setForeground(Color.WHITE);
        
        JLabel lblUsuario = new JLabel("Usuario: " + usuario);
        lblUsuario.setFont(new Font("Arial", Font.PLAIN, 14));
        lblUsuario.setForeground(new Color(236, 240, 241));
        
        headerPanel.add(lblTitulo, BorderLayout.WEST);
        headerPanel.add(lblUsuario, BorderLayout.EAST);
        
        // Panel central con botones
        JPanel centerPanel = new JPanel(new GridBagLayout());
        centerPanel.setBackground(new Color(236, 240, 241));
        GridBagConstraints gbc = new GridBagConstraints();
        gbc.insets = new Insets(15, 15, 15, 15);
        gbc.fill = GridBagConstraints.BOTH;
        gbc.weightx = 1.0;
        gbc.weighty = 1.0;
        
        // Botón Temperatura
        JButton btnTemperatura = crearBotonMenu(
            "TEMPERATURA",
            "Celsius, Fahrenheit, Kelvin",
            new Color(231, 76, 60),
            0, 0, gbc
        );
        btnTemperatura.addActionListener(e -> abrirTemperatura());
        centerPanel.add(btnTemperatura, gbc);
        
        // Botón Longitud
        JButton btnLongitud = crearBotonMenu(
            "LONGITUD",
            "Metro, Kilómetro, Milla",
            new Color(52, 152, 219),
            1, 0, gbc
        );
        btnLongitud.addActionListener(e -> abrirLongitud());
        centerPanel.add(btnLongitud, gbc);
        
        // Botón Masa
        JButton btnMasa = crearBotonMenu(
            "MASA",
            "Kilogramo, Gramo, Libra",
            new Color(46, 204, 113),
            0, 1, gbc
        );
        btnMasa.addActionListener(e -> abrirMasa());
        centerPanel.add(btnMasa, gbc);
        
        // Botón Salir
        JButton btnSalir = crearBotonMenu(
            "SALIR",
            "Cerrar sesión",
            new Color(149, 165, 166),
            1, 1, gbc
        );
        btnSalir.addActionListener(e -> salir());
        centerPanel.add(btnSalir, gbc);
        
        // Footer
        JPanel footerPanel = new JPanel();
        footerPanel.setBackground(new Color(236, 240, 241));
        JLabel lblFooter = new JLabel("Seleccione el tipo de conversión que desea realizar");
        lblFooter.setFont(new Font("Arial", Font.ITALIC, 12));
        lblFooter.setForeground(new Color(127, 140, 141));
        footerPanel.add(lblFooter);
        
        // Agregar al frame
        mainPanel.add(headerPanel, BorderLayout.NORTH);
        mainPanel.add(centerPanel, BorderLayout.CENTER);
        mainPanel.add(footerPanel, BorderLayout.SOUTH);
        
        add(mainPanel);
    }
    
    private JButton crearBotonMenu(String titulo, String descripcion, Color color, 
                                   int gridx, int gridy, GridBagConstraints gbc) {
        JButton btn = new JButton();
        btn.setLayout(new BorderLayout(10, 5));
        btn.setBackground(color);
        btn.setForeground(Color.WHITE);
        btn.setFocusPainted(false);
        btn.setBorderPainted(false);
        btn.setCursor(new Cursor(Cursor.HAND_CURSOR));
        btn.setPreferredSize(new Dimension(250, 150));
        
        // Panel interno del botón
        JPanel btnPanel = new JPanel(new BorderLayout(5, 5));
        btnPanel.setOpaque(false);
        btnPanel.setBorder(BorderFactory.createEmptyBorder(10, 15, 10, 15));
        
        JLabel lblTitulo = new JLabel(titulo);
        lblTitulo.setFont(new Font("Arial", Font.BOLD, 20));
        lblTitulo.setForeground(Color.WHITE);
        lblTitulo.setHorizontalAlignment(SwingConstants.CENTER);
        
        JLabel lblDesc = new JLabel("<html><center>" + descripcion + "</center></html>");
        lblDesc.setFont(new Font("Arial", Font.PLAIN, 13));
        lblDesc.setForeground(new Color(255, 255, 255, 200));
        lblDesc.setHorizontalAlignment(SwingConstants.CENTER);
        
        btnPanel.add(lblTitulo, BorderLayout.CENTER);
        btnPanel.add(lblDesc, BorderLayout.SOUTH);
        
        btn.add(btnPanel);
        
        // Efecto hover
        btn.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseEntered(java.awt.event.MouseEvent evt) {
                btn.setBackground(color.darker());
            }
            public void mouseExited(java.awt.event.MouseEvent evt) {
                btn.setBackground(color);
            }
        });
        
        gbc.gridx = gridx;
        gbc.gridy = gridy;
        
        return btn;
    }
    
    private void abrirTemperatura() {
        PanelTemperaturaFrame frame = new PanelTemperaturaFrame(usuario);
        frame.setVisible(true);
    }
    
    private void abrirLongitud() {
        PanelLongitudFrame frame = new PanelLongitudFrame(usuario);
        frame.setVisible(true);
    }
    
    private void abrirMasa() {
        PanelMasaFrame frame = new PanelMasaFrame(usuario);
        frame.setVisible(true);
    }
    
    private void salir() {
        int respuesta = JOptionPane.showConfirmDialog(this,
            "¿Está seguro que desea cerrar sesión?",
            "Confirmar Salida",
            JOptionPane.YES_NO_OPTION,
            JOptionPane.QUESTION_MESSAGE);
        
        if (respuesta == JOptionPane.YES_OPTION) {
            LoginFrame login = new LoginFrame();
            login.setVisible(true);
            this.dispose();
        }
    }
}