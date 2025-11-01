/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.vistas;

import ec.edu.monster.modelo.RespuestaAutenticacion;
import ec.edu.monster.service.Cliente;
import ec.edu.monster.vistas.MenuPrincipalFrame;
import javax.swing.*;
import java.awt.*;
import javax.swing.border.*;

/**
 * Pantalla de Login para autenticación de usuarios
 * @author josue
 */
public class LoginFrame extends JFrame {

    // Paleta de colores estandarizada
    private static final Color BG_PRIMARY = new Color(15, 23, 42);      // #0f172a
    private static final Color BG_SECONDARY = new Color(30, 41, 59);    // #1e293b
    private static final Color BG_CARD = new Color(51, 65, 85);         // #334155
    private static final Color TEXT_PRIMARY = new Color(241, 245, 249); // #f1f5f9
    private static final Color TEXT_SECONDARY = new Color(148, 163, 184); // #94a3b8
    private static final Color ACCENT_LOGIN = new Color(139, 92, 246);  // #8b5cf6
    private static final Color ACCENT_LOGIN_HOVER = new Color(124, 58, 237); // #7c3aed
    private static final Color BORDER_COLOR = new Color(71, 85, 105);   // #475569

    private JTextField txtUsuario;
    private JPasswordField txtPassword;
    private JButton btnLogin;
    private JButton btnLimpiar;
    private Cliente servicio;

    public LoginFrame() {
        initComponents();
    }

    private void initComponents() {
        setTitle("Conversor de Unidades - Login");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(800, 700);
        setLocationRelativeTo(null);
        setResizable(false);
        getContentPane().setBackground(BG_PRIMARY);

        // Panel principal
        JPanel mainPanel = new JPanel();
        mainPanel.setLayout(new BoxLayout(mainPanel, BoxLayout.Y_AXIS));
        mainPanel.setBackground(BG_PRIMARY);
        mainPanel.setBorder(BorderFactory.createEmptyBorder(30, 30, 30, 30));

        // Logo/Header
        JPanel headerPanel = createHeaderPanel();
        mainPanel.add(headerPanel);
        mainPanel.add(Box.createVerticalStrut(30));

        // Panel con la imagen (izquierda)
        JPanel imagePanel = new JPanel(new BorderLayout());
        imagePanel.setBackground(BG_PRIMARY);

        ImageIcon icon = new ImageIcon("src/main/resources/img.png");
        Image img = icon.getImage().getScaledInstance(350, 350, Image.SCALE_SMOOTH);
        JLabel imagenLabel = new JLabel(new ImageIcon(img));
        imagenLabel.setHorizontalAlignment(SwingConstants.CENTER);
        imagenLabel.setVerticalAlignment(SwingConstants.CENTER);
        imagePanel.add(imagenLabel, BorderLayout.CENTER);

        // Card del formulario (derecha)
        JPanel formCard = createFormCard();

        // Contenedor horizontal
        JPanel horizontalPanel = new JPanel();
        horizontalPanel.setLayout(new BoxLayout(horizontalPanel, BoxLayout.X_AXIS));
        horizontalPanel.setBackground(BG_PRIMARY);
        horizontalPanel.setAlignmentX(Component.CENTER_ALIGNMENT);

        // Centrado vertical y separación equilibrada
        horizontalPanel.add(Box.createHorizontalGlue());
        horizontalPanel.add(imagePanel);
        horizontalPanel.add(Box.createHorizontalStrut(40));
        horizontalPanel.add(formCard);
        horizontalPanel.add(Box.createHorizontalGlue());

        mainPanel.add(horizontalPanel);
        mainPanel.add(Box.createVerticalStrut(30));

        // Info de credenciales
        JPanel infoPanel = createInfoPanel();
        mainPanel.add(infoPanel);

        add(mainPanel);

        // Enter para login
        txtPassword.addActionListener(e -> login());
    }

    private JPanel createHeaderPanel() {
        JPanel panel = new JPanel();
        panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
        panel.setBackground(BG_PRIMARY);
        panel.setMaximumSize(new Dimension(600, 150));

        // Icono (simulando un icono de login)
        JLabel iconLabel = new JLabel("🔐");
        iconLabel.setFont(new Font("Segoe UI Emoji", Font.PLAIN, 48));
        iconLabel.setForeground(ACCENT_LOGIN);
        iconLabel.setAlignmentX(Component.CENTER_ALIGNMENT);

        JLabel titulo = new JLabel("CONVERSOR DE UNIDADES");
        titulo.setFont(new Font("Arial", Font.BOLD, 24));
        titulo.setForeground(TEXT_PRIMARY);
        titulo.setAlignmentX(Component.CENTER_ALIGNMENT);

        JLabel subtitulo = new JLabel("Sistema de Conversión - ESPE");
        subtitulo.setFont(new Font("Arial", Font.PLAIN, 14));
        subtitulo.setForeground(TEXT_SECONDARY);
        subtitulo.setAlignmentX(Component.CENTER_ALIGNMENT);

        panel.add(iconLabel);
        panel.add(Box.createVerticalStrut(10));
        panel.add(titulo);
        panel.add(Box.createVerticalStrut(5));
        panel.add(subtitulo);

        return panel;
    }

    private JPanel createFormCard() {
        JPanel card = new JPanel();
        card.setLayout(new BoxLayout(card, BoxLayout.Y_AXIS));
        card.setBackground(BG_CARD);
        card.setBorder(BorderFactory.createCompoundBorder(
                new LineBorder(BORDER_COLOR, 1, true),
                BorderFactory.createEmptyBorder(30, 30, 30, 30)
        ));
        card.setMaximumSize(new Dimension(600, 300));

        // Usuario
        JLabel lblUsuario = new JLabel("Usuario");
        lblUsuario.setFont(new Font("Arial", Font.PLAIN, 14));
        lblUsuario.setForeground(TEXT_SECONDARY);
        lblUsuario.setAlignmentX(Component.CENTER_ALIGNMENT);

        txtUsuario = new JTextField(20);
        estilizarTextField(txtUsuario);
        txtUsuario.setAlignmentX(Component.CENTER_ALIGNMENT);

        // Contraseña
        JLabel lblPassword = new JLabel("Contraseña");
        lblPassword.setFont(new Font("Arial", Font.PLAIN, 14));
        lblPassword.setForeground(TEXT_SECONDARY);
        lblPassword.setAlignmentX(Component.CENTER_ALIGNMENT);

        txtPassword = new JPasswordField(20);
        estilizarTextField(txtPassword);
        txtPassword.setAlignmentX(Component.CENTER_ALIGNMENT);

        // Botones
        JPanel buttonPanel = new JPanel();
        buttonPanel.setLayout(new GridLayout(1, 2, 10, 0));
        buttonPanel.setBackground(BG_CARD);
        buttonPanel.setMaximumSize(new Dimension(400, 45));

        btnLogin = crearBoton("Iniciar Sesión", ACCENT_LOGIN, ACCENT_LOGIN_HOVER);
        btnLogin.addActionListener(e -> login());

        btnLimpiar = crearBotonSecundario("Limpiar");
        btnLimpiar.addActionListener(e -> limpiar());

        buttonPanel.add(btnLogin);
        buttonPanel.add(btnLimpiar);

        // Agregar componentes al card
        card.add(lblUsuario);
        card.add(Box.createVerticalStrut(8));
        card.add(txtUsuario);
        card.add(Box.createVerticalStrut(20));
        card.add(lblPassword);
        card.add(Box.createVerticalStrut(8));
        card.add(txtPassword);
        card.add(Box.createVerticalStrut(30));
        card.add(buttonPanel);

        return card;
    }

    private JPanel createInfoPanel() {
        JPanel panel = new JPanel();
        panel.setBackground(BG_SECONDARY);
        panel.setBorder(BorderFactory.createCompoundBorder(
                new LineBorder(BORDER_COLOR, 1, true),
                BorderFactory.createEmptyBorder(15, 20, 15, 20)
        ));
        panel.setMaximumSize(new Dimension(800, 70));

        JLabel infoLabel = new JLabel("<html><center>💡 Credenciales:</center></html>");
        JLabel infoCredenciales = new JLabel("<html><center><b>Usuario:</b> MONSTER  <b>Contraseña:</b> MONSTER9</center></html>");
        infoLabel.setFont(new Font("Arial", Font.PLAIN, 12));
        infoLabel.setForeground(TEXT_SECONDARY);
        infoLabel.setHorizontalAlignment(SwingConstants.CENTER);
        infoCredenciales.setFont(new Font("Arial", Font.BOLD, 12));
        infoCredenciales.setForeground(TEXT_SECONDARY);
        infoCredenciales.setHorizontalAlignment(SwingConstants.CENTER);

        panel.add(infoLabel);
        panel.add(Box.createVerticalStrut(5));
        panel.add(infoCredenciales);
        return panel;
    }

    private void estilizarTextField(JTextField field) {
        field.setFont(new Font("Arial", Font.PLAIN, 14));
        field.setForeground(TEXT_PRIMARY);
        field.setBackground(BG_SECONDARY);
        field.setCaretColor(TEXT_PRIMARY);
        field.setBorder(BorderFactory.createCompoundBorder(
                new LineBorder(BORDER_COLOR, 1, true),
                BorderFactory.createEmptyBorder(10, 12, 10, 12)
        ));
        field.setMaximumSize(new Dimension(400, 45));
        field.setAlignmentX(Component.LEFT_ALIGNMENT);
    }

    private JButton crearBoton(String texto, Color bg, Color bgHover) {
        JButton btn = new JButton(texto);
        btn.setFont(new Font("Arial", Font.BOLD, 14));
        btn.setForeground(BG_PRIMARY);
        btn.setBackground(bg);
        btn.setBorder(BorderFactory.createEmptyBorder(12, 20, 12, 20));
        btn.setFocusPainted(false);
        btn.setCursor(new Cursor(Cursor.HAND_CURSOR));

        // Efecto hover
        btn.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseEntered(java.awt.event.MouseEvent evt) {
                btn.setBackground(bgHover);
            }
            public void mouseExited(java.awt.event.MouseEvent evt) {
                btn.setBackground(bg);
            }
        });

        return btn;
    }

    private JButton crearBotonSecundario(String texto) {
        JButton btn = new JButton(texto);
        btn.setFont(new Font("Arial", Font.PLAIN, 14));
        btn.setForeground(BG_PRIMARY);
        btn.setBackground(BG_SECONDARY);
        btn.setBorder(new LineBorder(BORDER_COLOR, 1, true));
        btn.setFocusPainted(false);
        btn.setCursor(new Cursor(Cursor.HAND_CURSOR));

        // Efecto hover
        btn.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseEntered(java.awt.event.MouseEvent evt) {
                btn.setBackground(BORDER_COLOR);
            }
            public void mouseExited(java.awt.event.MouseEvent evt) {
                btn.setBackground(BG_SECONDARY);
            }
        });

        return btn;
    }

    private void login() {
        String usuario = txtUsuario.getText().trim();
        String password = new String(txtPassword.getPassword());

        if (usuario.isEmpty() || password.isEmpty()) {
            mostrarAdvertencia("Campos vacíos",
                    "Por favor ingrese usuario y contraseña");
            return;
        }

        try {
            RespuestaAutenticacion respuesta = servicio.login(usuario, password);

            if (respuesta.isExitoso()) {
                mostrarExito("Login exitoso",
                        "¡Bienvenido " + respuesta.getUsername() + "!");

                MenuPrincipalFrame menu = new MenuPrincipalFrame(respuesta.getUsername());
                menu.setVisible(true);
                this.dispose();
            } else {
                mostrarError("Error de autenticación", respuesta.getMensaje());
                limpiar();
            }
        } catch (Exception e) {
            mostrarError("Error de conexión",
                    "No se pudo conectar con el servidor:\n" + e.getMessage());
        }
    }

    private void limpiar() {
        txtUsuario.setText("");
        txtPassword.setText("");
        txtUsuario.requestFocus();
    }

    // Métodos auxiliares para mensajes con estilo
    private void mostrarExito(String titulo, String mensaje) {
        JOptionPane.showMessageDialog(this, mensaje, titulo,
                JOptionPane.INFORMATION_MESSAGE);
    }

    private void mostrarError(String titulo, String mensaje) {
        JOptionPane.showMessageDialog(this, mensaje, titulo,
                JOptionPane.ERROR_MESSAGE);
    }

    private void mostrarAdvertencia(String titulo, String mensaje) {
        JOptionPane.showMessageDialog(this, mensaje, titulo,
                JOptionPane.WARNING_MESSAGE);
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