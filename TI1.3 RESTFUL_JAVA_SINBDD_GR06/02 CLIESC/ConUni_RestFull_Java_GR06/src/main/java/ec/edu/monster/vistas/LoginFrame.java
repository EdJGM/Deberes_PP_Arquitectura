/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.vistas;

import ec.edu.monster.vistas.MenuPrincipalFrame;
import ec.edu.monster.service.Cliente;
import ec.edu.monster.modelo.RespuestaAutenticacion;
import ec.edu.monster.modelo.Usuario;
import com.fasterxml.jackson.databind.ObjectMapper;
import javax.swing.*;
import java.awt.*;

/**
 * Pantalla de Login para autenticación de usuarios
 * @author josue
 */
public class LoginFrame extends JFrame {
    
    private JTextField txtUsuario;
    private JPasswordField txtPassword;
    private JButton btnLogin;
    private JButton btnLimpiar;
    private Cliente servicio; //usando restfull
    
    public LoginFrame() {
        initComponents();
    }
    
    private void initComponents() {
        setTitle("Sistema de Conversión de Unidades - Login");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(450, 350);
        setLocationRelativeTo(null);
        setResizable(false);
        
        // Panel principal con fondo
        JPanel mainPanel = new JPanel();
        mainPanel.setLayout(new BorderLayout(10, 10));
        mainPanel.setBackground(new Color(240, 240, 240));
        mainPanel.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));
        
        // Panel del título
        JPanel headerPanel = new JPanel();
        headerPanel.setBackground(new Color(41, 128, 185));
        headerPanel.setBorder(BorderFactory.createEmptyBorder(20, 10, 20, 10));
        
        JLabel lblTitulo = new JLabel("CONVERSOR DE UNIDADES");
        lblTitulo.setFont(new Font("Arial", Font.BOLD, 24));
        lblTitulo.setForeground(Color.WHITE);
        lblTitulo.setHorizontalAlignment(SwingConstants.CENTER);
        headerPanel.add(lblTitulo);
        
        // Panel del formulario
        JPanel formPanel = new JPanel(new GridBagLayout());
        formPanel.setBackground(Color.WHITE);
        formPanel.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(new Color(189, 195, 199), 1),
            BorderFactory.createEmptyBorder(30, 30, 30, 30)
        ));
        
        GridBagConstraints gbc = new GridBagConstraints();
        gbc.insets = new Insets(10, 10, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        
        // Usuario
        JLabel lblUsuario = new JLabel("Usuario:");
        lblUsuario.setFont(new Font("Arial", Font.BOLD, 14));
        gbc.gridx = 0;
        gbc.gridy = 0;
        gbc.weightx = 0.3;
        formPanel.add(lblUsuario, gbc);
        
        txtUsuario = new JTextField(15);
        txtUsuario.setFont(new Font("Arial", Font.PLAIN, 14));
        txtUsuario.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(new Color(189, 195, 199)),
            BorderFactory.createEmptyBorder(5, 10, 5, 10)
        ));
        gbc.gridx = 1;
        gbc.weightx = 0.7;
        formPanel.add(txtUsuario, gbc);
        
        // Contraseña
        JLabel lblPassword = new JLabel("Contraseña:");
        lblPassword.setFont(new Font("Arial", Font.BOLD, 14));
        gbc.gridx = 0;
        gbc.gridy = 1;
        gbc.weightx = 0.3;
        formPanel.add(lblPassword, gbc);
        
        txtPassword = new JPasswordField(15);
        txtPassword.setFont(new Font("Arial", Font.PLAIN, 14));
        txtPassword.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(new Color(189, 195, 199)),
            BorderFactory.createEmptyBorder(5, 10, 5, 10)
        ));
        gbc.gridx = 1;
        gbc.weightx = 0.7;
        formPanel.add(txtPassword, gbc);
        
        // Panel de botones
        JPanel buttonPanel = new JPanel(new FlowLayout(FlowLayout.CENTER, 10, 0));
        buttonPanel.setBackground(Color.WHITE);
        
        btnLogin = new JButton("Iniciar Sesión");
        btnLogin.setFont(new Font("Arial", Font.BOLD, 14));
        btnLogin.setBackground(new Color(46, 204, 113));
        btnLogin.setForeground(Color.WHITE);
        btnLogin.setFocusPainted(false);
        btnLogin.setBorderPainted(false);
        btnLogin.setPreferredSize(new Dimension(140, 35));
        btnLogin.setCursor(new Cursor(Cursor.HAND_CURSOR));
        btnLogin.addActionListener(e -> login());
        
        btnLimpiar = new JButton("Limpiar");
        btnLimpiar.setFont(new Font("Arial", Font.BOLD, 14));
        btnLimpiar.setBackground(new Color(149, 165, 166));
        btnLimpiar.setForeground(Color.WHITE);
        btnLimpiar.setFocusPainted(false);
        btnLimpiar.setBorderPainted(false);
        btnLimpiar.setPreferredSize(new Dimension(140, 35));
        btnLimpiar.setCursor(new Cursor(Cursor.HAND_CURSOR));
        btnLimpiar.addActionListener(e -> limpiar());
        
        buttonPanel.add(btnLogin);
        buttonPanel.add(btnLimpiar);
        
        gbc.gridx = 0;
        gbc.gridy = 2;
        gbc.gridwidth = 2;
        gbc.insets = new Insets(20, 10, 10, 10);
        formPanel.add(buttonPanel, gbc);
        
        // Info panel
        JPanel infoPanel = new JPanel();
        infoPanel.setBackground(new Color(240, 240, 240));
        JLabel lblInfo = new JLabel("Usuario por defecto: MONSTER / MONSTER9");
        lblInfo.setFont(new Font("Arial", Font.ITALIC, 11));
        lblInfo.setForeground(new Color(127, 140, 141));
        infoPanel.add(lblInfo);
        
        // Agregar paneles al frame
        mainPanel.add(headerPanel, BorderLayout.NORTH);
        mainPanel.add(formPanel, BorderLayout.CENTER);
        mainPanel.add(infoPanel, BorderLayout.SOUTH);
        
        add(mainPanel);
        
        // Enter para login
        txtPassword.addActionListener(e -> login());
    }
    
    private void login() {
        String usuario = txtUsuario.getText().trim();
        String password = new String(txtPassword.getPassword());
        
        if (usuario.isEmpty() || password.isEmpty()) {
            JOptionPane.showMessageDialog(this,
                "Por favor ingrese usuario y contraseña",
                "Campos Vacíos",
                JOptionPane.WARNING_MESSAGE);
            return;
        }
        
        try {
            RespuestaAutenticacion respuesta = servicio.login(usuario, password);
            
            if (respuesta.isExitoso()) {
                JOptionPane.showMessageDialog(this,
                    "¡Bienvenido " + respuesta.getUsername() + "!",
                    "Login Exitoso",
                    JOptionPane.INFORMATION_MESSAGE);
                
                // Abrir menú principal
                MenuPrincipalFrame menu = new MenuPrincipalFrame(respuesta.getUsername());
                menu.setVisible(true);
                this.dispose();
            } else {
                JOptionPane.showMessageDialog(this,
                    respuesta.getMensaje(),
                    "Error de Autenticación",
                    JOptionPane.ERROR_MESSAGE);
                limpiar();
            }
        } catch (Exception e) {
            JOptionPane.showMessageDialog(this,
                "Error al conectar con el servidor: " + e.getMessage(),
                "Error",
                JOptionPane.ERROR_MESSAGE);
        }
    }
    
    private void limpiar() {
        txtUsuario.setText("");
        txtPassword.setText("");
        txtUsuario.requestFocus();
    }
    
    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> {
            try {
                UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
            } catch (Exception e) {
                e.printStackTrace();
            }
            new LoginFrame().setVisible(true);
        });
    }
}