/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.vistas;

import javax.swing.*;
import java.awt.*;
import javax.swing.border.*;

/**
 * Menú principal del sistema de conversión
 * @author josue
 */
public class MenuPrincipalFrame extends JFrame {
    
    // Paleta de colores estandarizada
    private static final Color BG_PRIMARY = new Color(15, 23, 42);      
    private static final Color BG_SECONDARY = new Color(30, 41, 59);    
    private static final Color BG_CARD = new Color(51, 65, 85);         
    private static final Color TEXT_PRIMARY = new Color(241, 245, 249); 
    private static final Color TEXT_SECONDARY = new Color(148, 163, 184);
    private static final Color BORDER_COLOR = new Color(71, 85, 105);
    
    // Colores por categoría
    private static final Color COLOR_TEMPERATURA = new Color(239, 68, 68);      // #ef4444
    private static final Color COLOR_TEMPERATURA_HOVER = new Color(220, 38, 38);
    private static final Color COLOR_LONGITUD = new Color(59, 130, 246);        // #3b82f6
    private static final Color COLOR_LONGITUD_HOVER = new Color(37, 99, 235);
    private static final Color COLOR_MASA = new Color(16, 185, 129);            // #10b981
    private static final Color COLOR_MASA_HOVER = new Color(5, 150, 105);
    
    private String usuario;
    
    public MenuPrincipalFrame(String usuario) {
        this.usuario = usuario;
        initComponents();
    }
    
    private void initComponents() {
        setTitle("Conversor de Unidades - Menú Principal");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(800, 600);
        setLocationRelativeTo(null);
        setResizable(false);
        getContentPane().setBackground(BG_PRIMARY);
        
        JPanel mainPanel = new JPanel(new BorderLayout(0, 0));
        mainPanel.setBackground(BG_PRIMARY);
        
        // Header con usuario
        JPanel headerPanel = createHeaderPanel();
        mainPanel.add(headerPanel, BorderLayout.NORTH);
        
        // Panel central con las opciones
        JPanel centerPanel = createCenterPanel();
        mainPanel.add(centerPanel, BorderLayout.CENTER);
        
        // Footer con botón salir
        JPanel footerPanel = createFooterPanel();
        mainPanel.add(footerPanel, BorderLayout.SOUTH);
        
        add(mainPanel);
    }
    
    private JPanel createHeaderPanel() {
        JPanel header = new JPanel(new BorderLayout());
        header.setBackground(BG_SECONDARY);
        header.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createMatteBorder(0, 0, 1, 0, BORDER_COLOR),
            BorderFactory.createEmptyBorder(20, 30, 20, 30)
        ));
        
        // Título
        JPanel titlePanel = new JPanel();
        titlePanel.setLayout(new BoxLayout(titlePanel, BoxLayout.Y_AXIS));
        titlePanel.setBackground(BG_SECONDARY);
        
        JLabel lblTitulo = new JLabel("CONVERSOR DE UNIDADES");
        lblTitulo.setFont(new Font("Arial", Font.BOLD, 28));
        lblTitulo.setForeground(TEXT_PRIMARY);
        
        JLabel lblSubtitulo = new JLabel("Seleccione el tipo de conversión");
        lblSubtitulo.setFont(new Font("Arial", Font.PLAIN, 14));
        lblSubtitulo.setForeground(TEXT_SECONDARY);
        
        titlePanel.add(lblTitulo);
        titlePanel.add(Box.createVerticalStrut(5));
        titlePanel.add(lblSubtitulo);
        
        // Usuario
        JLabel lblUsuario = new JLabel(usuario);
        lblUsuario.setFont(new Font("Arial", Font.BOLD, 16));
        lblUsuario.setForeground(TEXT_PRIMARY);
        
        header.add(titlePanel, BorderLayout.WEST);
        header.add(lblUsuario, BorderLayout.EAST);
        
        return header;
    }
    
    private JPanel createCenterPanel() {
        JPanel center = new JPanel();
        center.setLayout(new GridBagLayout());
        center.setBackground(BG_PRIMARY);
        center.setBorder(BorderFactory.createEmptyBorder(40, 40, 40, 40));
        
        GridBagConstraints gbc = new GridBagConstraints();
        gbc.gridwidth = 1;
        gbc.fill = GridBagConstraints.CENTER;
        gbc.insets = new Insets(10, 10, 10, 10);
        gbc.weightx = 1.0;
        gbc.weighty = 1.0;
        
        // Botones de conversión
        gbc.gridx = 0; gbc.gridy = 0;
        center.add(crearBotonCategoria(
            "🌡️", 
            "TEMPERATURA", 
            "Celsius, Fahrenheit, Kelvin",
            COLOR_TEMPERATURA,
            COLOR_TEMPERATURA_HOVER,
            e -> abrirTemperatura()
        ), gbc);
        
        gbc.gridx = 1; gbc.gridy = 0;
        center.add(crearBotonCategoria(
            "📏", 
            "LONGITUD", 
            "Metros, Kilómetros, Millas",
            COLOR_LONGITUD,
            COLOR_LONGITUD_HOVER,
            e -> abrirLongitud()
        ), gbc);
        
        gbc.gridx = 2; gbc.gridy = 0;
        center.add(crearBotonCategoria(
            "⚖️", 
            "MASA", 
            "Kilogramos, Gramos, Libras",
            COLOR_MASA,
            COLOR_MASA_HOVER,
            e -> abrirMasa()
        ), gbc);
        
        return center;
    }
    
    private JPanel crearBotonCategoria(String emoji, String titulo, String descripcion, 
                                       Color color, Color colorHover, 
                                       java.awt.event.ActionListener action) {
        JPanel card = new JPanel();
        card.setLayout(new BorderLayout(0, 15));
        card.setBackground(BG_CARD);
        card.setBorder(BorderFactory.createCompoundBorder(
            new LineBorder(BORDER_COLOR, 2, true),
            BorderFactory.createEmptyBorder(25, 25, 25, 25)
        ));
        card.setCursor(new Cursor(Cursor.HAND_CURSOR));
        
        // Icono
        JLabel iconLabel = new JLabel(emoji);
        iconLabel.setFont(new Font("Segoe UI Emoji", Font.PLAIN, 64));
        iconLabel.setHorizontalAlignment(SwingConstants.CENTER);
        
        // Texto
        JPanel textPanel = new JPanel();
        textPanel.setLayout(new BoxLayout(textPanel, BoxLayout.Y_AXIS));
        textPanel.setBackground(BG_CARD);
        
        JLabel lblTitulo = new JLabel(titulo);
        lblTitulo.setFont(new Font("Arial", Font.BOLD, 20));
        lblTitulo.setForeground(TEXT_PRIMARY);
        lblTitulo.setAlignmentX(Component.CENTER_ALIGNMENT);
        
        JLabel lblDesc = new JLabel("<html><center>" + descripcion + "</center></html>");
        lblDesc.setFont(new Font("Arial", Font.PLAIN, 12));
        lblDesc.setForeground(TEXT_SECONDARY);
        lblDesc.setHorizontalAlignment(SwingConstants.CENTER);
        lblDesc.setAlignmentX(Component.CENTER_ALIGNMENT);
        
        textPanel.add(lblTitulo);
        textPanel.add(Box.createVerticalStrut(5));
        textPanel.add(lblDesc);
        
        // Indicador de color
        JPanel colorBar = new JPanel();
        colorBar.setBackground(color);
        colorBar.setPreferredSize(new Dimension(0, 4));
        
        card.add(iconLabel, BorderLayout.NORTH);
        card.add(textPanel, BorderLayout.CENTER);
        card.add(colorBar, BorderLayout.SOUTH);
        
        // Efectos hover y click
        card.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseEntered(java.awt.event.MouseEvent evt) {
                card.setBackground(BG_SECONDARY);
                textPanel.setBackground(BG_SECONDARY);
                colorBar.setBackground(colorHover);
                card.setBorder(BorderFactory.createCompoundBorder(
                    new LineBorder(color, 2, true),
                    BorderFactory.createEmptyBorder(25, 25, 25, 25)
                ));
            }
            
            public void mouseExited(java.awt.event.MouseEvent evt) {
                card.setBackground(BG_CARD);
                textPanel.setBackground(BG_CARD);
                colorBar.setBackground(color);
                card.setBorder(BorderFactory.createCompoundBorder(
                    new LineBorder(BORDER_COLOR, 2, true),
                    BorderFactory.createEmptyBorder(25, 25, 25, 25)
                ));
            }
            
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                action.actionPerformed(null);
            }
        });
        
        return card;
    }
    
    private JPanel createFooterPanel() {
        JPanel footer = new JPanel(new FlowLayout(FlowLayout.RIGHT));
        footer.setBackground(BG_PRIMARY);
        footer.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createMatteBorder(1, 0, 0, 0, BORDER_COLOR),
            BorderFactory.createEmptyBorder(15, 30, 15, 30)
        ));
        
        JButton btnSalir = new JButton("Cerrar Sesión");
        btnSalir.setFont(new Font("Arial", Font.BOLD, 14));
        btnSalir.setForeground(BG_PRIMARY);
        btnSalir.setBackground(new Color(220, 38, 38)); // Red-600
        btnSalir.setBorder(BorderFactory.createEmptyBorder(12, 24, 12, 24));
        btnSalir.setFocusPainted(false);
        btnSalir.setCursor(new Cursor(Cursor.HAND_CURSOR));
        btnSalir.addActionListener(e -> salir());
        
        // Efecto hover
        btnSalir.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseEntered(java.awt.event.MouseEvent evt) {
                btnSalir.setBackground(new Color(185, 28, 28));
            }
            public void mouseExited(java.awt.event.MouseEvent evt) {
                btnSalir.setBackground(new Color(220, 38, 38));
            }
        });
        
        footer.add(btnSalir);
        return footer;
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
        Object[] opciones = {"Sí", "No"};
        int respuesta = JOptionPane.showOptionDialog(this,
            "¿Está seguro que desea cerrar sesión?",
            "Confirmar Salida",
            JOptionPane.YES_NO_OPTION,
            JOptionPane.QUESTION_MESSAGE,
            null,
            opciones,
            opciones[1]);
        
        if (respuesta == JOptionPane.YES_OPTION) {
            LoginFrame login = new LoginFrame();
            login.setVisible(true);
            this.dispose();
        }
    }
}