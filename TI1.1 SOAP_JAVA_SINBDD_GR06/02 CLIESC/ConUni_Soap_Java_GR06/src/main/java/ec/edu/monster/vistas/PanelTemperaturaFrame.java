/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.vistas;

import ec.edu.monster.ws.client.ConversorUnidadesWS;
import ec.edu.monster.ws.client.ConversorUnidadesWS_Service;
import javax.swing.*;
import java.awt.*;
import javax.swing.border.*;
import java.text.DecimalFormat;

/**
 * Panel de conversión de temperaturas
 * @author josue
 */
public class PanelTemperaturaFrame extends JFrame {
    
    // Paleta de colores estandarizada
    private static final Color BG_PRIMARY = new Color(15, 23, 42);      
    private static final Color BG_SECONDARY = new Color(30, 41, 59);    
    private static final Color BG_CARD = new Color(51, 65, 85);         
    private static final Color TEXT_PRIMARY = new Color(241, 245, 249); 
    private static final Color TEXT_SECONDARY = new Color(148, 163, 184);
    private static final Color BORDER_COLOR = new Color(71, 85, 105);
    private static final Color COLOR_TEMPERATURA = new Color(239, 68, 68);
    private static final Color COLOR_TEMPERATURA_HOVER = new Color(220, 38, 38);
    
    private String usuario;
    private ConversorUnidadesWS servicio;
    
    private JTextField txtValor;
    private JComboBox<String> cmbOrigen;
    private JComboBox<String> cmbDestino;
    private JLabel lblResultado;
    private DecimalFormat df = new DecimalFormat("#,##0.00");
    
    public PanelTemperaturaFrame(String usuario) {
        this.usuario = usuario;
        initService();
        initComponents();
    }
    
    private void initService() {
        try {
            ConversorUnidadesWS_Service service = new ConversorUnidadesWS_Service();
            servicio = service.getConversorUnidadesWSPort();
        } catch (Exception e) {
            JOptionPane.showMessageDialog(this,
                "Error al conectar con el servidor",
                "Error",
                JOptionPane.ERROR_MESSAGE);
        }
    }
    
    private void initComponents() {
        setTitle("Conversión de Temperatura");
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
        setSize(800, 800);
        setLocationRelativeTo(null);
        setResizable(false);
        getContentPane().setBackground(BG_PRIMARY);
        
        JPanel mainPanel = new JPanel(new BorderLayout(0, 0));
        mainPanel.setBackground(BG_PRIMARY);
        
        // Header
        JPanel headerPanel = createHeaderPanel();
        mainPanel.add(headerPanel, BorderLayout.NORTH);
        
        // Panel central con formulario
        JPanel centerPanel = createCenterPanel();
        mainPanel.add(centerPanel, BorderLayout.CENTER);
        
        // Footer con botones
        JPanel footerPanel = createFooterPanel();
        mainPanel.add(footerPanel, BorderLayout.SOUTH);
        
        add(mainPanel);
    }
    
    private JPanel createHeaderPanel() {
        JPanel header = new JPanel();
        header.setLayout(new BorderLayout());
        header.setBackground(COLOR_TEMPERATURA);
        header.setBorder(BorderFactory.createEmptyBorder(25, 30, 25, 30));
        
        // Icono y título
        JPanel leftPanel = new JPanel();
        leftPanel.setLayout(new BoxLayout(leftPanel, BoxLayout.X_AXIS));
        leftPanel.setBackground(COLOR_TEMPERATURA);
        
        JLabel iconLabel = new JLabel("🌡️  ");
        iconLabel.setFont(new Font("Segoe UI Emoji", Font.PLAIN, 36));
        
        JPanel textPanel = new JPanel();
        textPanel.setLayout(new BoxLayout(textPanel, BoxLayout.Y_AXIS));
        textPanel.setBackground(COLOR_TEMPERATURA);
        
        JLabel lblTitulo = new JLabel("CONVERSIÓN DE TEMPERATURA");
        lblTitulo.setFont(new Font("Arial", Font.BOLD, 24));
        lblTitulo.setForeground(Color.WHITE);
        
        JLabel lblDesc = new JLabel("Celsius, Fahrenheit, Kelvin");
        lblDesc.setFont(new Font("Arial", Font.PLAIN, 14));
        lblDesc.setForeground(new Color(255, 255, 255, 180));
        
        textPanel.add(lblTitulo);
        textPanel.add(Box.createVerticalStrut(5));
        textPanel.add(lblDesc);
        
        leftPanel.add(iconLabel);
        leftPanel.add(textPanel);
        
        // Usuario
        JLabel lblUsuario = new JLabel(usuario);
        lblUsuario.setFont(new Font("Arial", Font.BOLD, 14));
        lblUsuario.setForeground(Color.WHITE);
        
        header.add(leftPanel, BorderLayout.WEST);
        header.add(lblUsuario, BorderLayout.EAST);
        
        return header;
    }
    
    private JPanel createCenterPanel() {
        JPanel center = new JPanel();
        center.setLayout(new BoxLayout(center, BoxLayout.Y_AXIS));
        center.setBackground(BG_PRIMARY);
        center.setBorder(BorderFactory.createEmptyBorder(30, 40, 30, 40));

        // Card del formulario
        JPanel formCard = new JPanel();
        formCard.setLayout(new BoxLayout(formCard, BoxLayout.Y_AXIS));
        formCard.setBackground(BG_CARD);
        formCard.setBorder(BorderFactory.createCompoundBorder(
            new LineBorder(BORDER_COLOR, 2, true),
            BorderFactory.createEmptyBorder(30, 30, 30, 30)
        ));
        formCard.setMaximumSize(new Dimension(520, 650));
        formCard.setAlignmentX(Component.CENTER_ALIGNMENT);

        // Valor a convertir
        JLabel lblValor = new JLabel("Valor a convertir");
        lblValor.setFont(new Font("Arial", Font.BOLD, 14));
        lblValor.setForeground(TEXT_SECONDARY);
        lblValor.setAlignmentX(Component.CENTER_ALIGNMENT);

        txtValor = new JTextField();
        estilizarTextField(txtValor);
        txtValor.setAlignmentX(Component.CENTER_ALIGNMENT);

        // Unidad de origen
        JLabel lblOrigen = new JLabel("Unidad de origen");
        lblOrigen.setFont(new Font("Arial", Font.BOLD, 14));
        lblOrigen.setForeground(TEXT_SECONDARY);
        lblOrigen.setAlignmentX(Component.CENTER_ALIGNMENT);

        String[] unidades = {"Celsius", "Fahrenheit", "Kelvin"};
        cmbOrigen = new JComboBox<>(unidades);
        estilizarComboBox(cmbOrigen);
        cmbOrigen.setAlignmentX(Component.CENTER_ALIGNMENT);
        
        // Icono de conversión
        JLabel lblFlecha = new JLabel("⬇️");
        lblFlecha.setFont(new Font("Segoe UI Emoji", Font.PLAIN, 32));
        lblFlecha.setAlignmentX(Component.CENTER_ALIGNMENT);
        lblFlecha.setForeground(COLOR_TEMPERATURA);
        
        // Unidad de destino
        JLabel lblDestino = new JLabel("Unidad de destino");
        lblDestino.setFont(new Font("Arial", Font.BOLD, 14));
        lblDestino.setForeground(TEXT_SECONDARY);
        lblDestino.setAlignmentX(Component.CENTER_ALIGNMENT);
        
        cmbDestino = new JComboBox<>(unidades);
        estilizarComboBox(cmbDestino);
        cmbDestino.setSelectedIndex(1); // Fahrenheit por defecto
        cmbDestino.setAlignmentX(Component.CENTER_ALIGNMENT);

        // Separador
        JSeparator separator = new JSeparator();
        separator.setForeground(BORDER_COLOR);
        separator.setMaximumSize(new Dimension(520, 1));
        separator.setAlignmentX(Component.CENTER_ALIGNMENT);

        // Resultado
        JPanel resultPanel = new JPanel();
        resultPanel.setLayout(new BoxLayout(resultPanel, BoxLayout.Y_AXIS));
        resultPanel.setBackground(BG_SECONDARY);
        resultPanel.setBorder(BorderFactory.createCompoundBorder(
            new LineBorder(COLOR_TEMPERATURA, 2, true),
            BorderFactory.createEmptyBorder(20, 20, 20, 20)
        ));
        resultPanel.setMaximumSize(new Dimension(520, 120));
        resultPanel.setAlignmentX(Component.CENTER_ALIGNMENT);

        JLabel lblResultadoTitle = new JLabel("RESULTADO");
        lblResultadoTitle.setFont(new Font("Arial", Font.BOLD, 12));
        lblResultadoTitle.setForeground(TEXT_SECONDARY);
        lblResultadoTitle.setAlignmentX(Component.CENTER_ALIGNMENT);
        
        lblResultado = new JLabel("---");
        lblResultado.setFont(new Font("Arial", Font.BOLD, 32));
        lblResultado.setForeground(COLOR_TEMPERATURA);
        lblResultado.setAlignmentX(Component.CENTER_ALIGNMENT);
        
        resultPanel.add(lblResultadoTitle);
        resultPanel.add(Box.createVerticalStrut(10));
        resultPanel.add(lblResultado);
        
        // Agregar componentes al card
        formCard.add(lblValor);
        formCard.add(Box.createVerticalStrut(8));
        formCard.add(txtValor);
        formCard.add(Box.createVerticalStrut(20));
        
        formCard.add(lblOrigen);
        formCard.add(Box.createVerticalStrut(8));
        formCard.add(cmbOrigen);
        formCard.add(Box.createVerticalStrut(15));
        
        formCard.add(lblFlecha);
        formCard.add(Box.createVerticalStrut(15));
        
        formCard.add(lblDestino);
        formCard.add(Box.createVerticalStrut(8));
        formCard.add(cmbDestino);
        formCard.add(Box.createVerticalStrut(25));
        
        formCard.add(separator);
        formCard.add(Box.createVerticalStrut(25));
        
        formCard.add(resultPanel);
        
        center.add(formCard);
        return center;
    }
    
    private JPanel createFooterPanel() {
        JPanel footer = new JPanel(new FlowLayout(FlowLayout.CENTER, 15, 0));
        footer.setBackground(BG_PRIMARY);
        footer.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createMatteBorder(1, 0, 0, 0, BORDER_COLOR),
            BorderFactory.createEmptyBorder(20, 30, 20, 30)
        ));
        
        JButton btnConvertir = crearBoton("Convertir", COLOR_TEMPERATURA, COLOR_TEMPERATURA_HOVER);
        btnConvertir.addActionListener(e -> convertir());
        
        JButton btnLimpiar = crearBotonSecundario("Limpiar");
        btnLimpiar.addActionListener(e -> limpiar());
        
        JButton btnVolver = crearBotonSecundario("← Volver");
        btnVolver.addActionListener(e -> dispose());
        
        footer.add(btnConvertir);
        footer.add(btnLimpiar);
        footer.add(btnVolver);
        
        return footer;
    }
    
    private void estilizarTextField(JTextField field) {
        field.setFont(new Font("Arial", Font.PLAIN, 16));
        field.setForeground(TEXT_PRIMARY);
        field.setBackground(BG_SECONDARY);
        field.setCaretColor(TEXT_PRIMARY);
        field.setBorder(BorderFactory.createCompoundBorder(
            new LineBorder(BORDER_COLOR, 2, true),
            BorderFactory.createEmptyBorder(12, 15, 12, 15)
        ));
        field.setMaximumSize(new Dimension(520, 50));
        field.setAlignmentX(Component.LEFT_ALIGNMENT);
    }
    
    private void estilizarComboBox(JComboBox<String> combo) {
        combo.setFont(new Font("Arial", Font.PLAIN, 14));
        combo.setForeground(BG_PRIMARY);
        combo.setBackground(BG_SECONDARY);
        combo.setBorder(new LineBorder(BORDER_COLOR, 2, true));
        combo.setMaximumSize(new Dimension(520, 45));
        combo.setAlignmentX(Component.LEFT_ALIGNMENT);
    }
    
    private JButton crearBoton(String texto, Color bg, Color bgHover) {
        JButton btn = new JButton(texto);
        btn.setFont(new Font("Arial", Font.BOLD, 14));
        btn.setForeground(BG_PRIMARY);
        btn.setBackground(bg);
        btn.setBorder(BorderFactory.createEmptyBorder(14, 30, 14, 30));
        btn.setFocusPainted(false);
        btn.setCursor(new Cursor(Cursor.HAND_CURSOR));
        
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
        btn.setBorder(BorderFactory.createCompoundBorder(
            new LineBorder(BORDER_COLOR, 2, true),
            BorderFactory.createEmptyBorder(12, 28, 12, 28)
        ));
        btn.setFocusPainted(false);
        btn.setCursor(new Cursor(Cursor.HAND_CURSOR));
        
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
    
    private void convertir() {
        try {
            double valor = Double.parseDouble(txtValor.getText().trim());
            String origen = (String) cmbOrigen.getSelectedItem();
            String destino = (String) cmbDestino.getSelectedItem();
            
            if (origen.equals(destino)) {
                lblResultado.setText(df.format(valor));
                return;
            }
            
            double resultado = 0;
            
            // Conversiones
            if (origen.equals("Celsius") && destino.equals("Fahrenheit")) {
                resultado = servicio.celsiusAFahrenheit(valor);
            } else if (origen.equals("Celsius") && destino.equals("Kelvin")) {
                resultado = servicio.celsiusAKelvin(valor);
            } else if (origen.equals("Fahrenheit") && destino.equals("Celsius")) {
                resultado = servicio.fahrenheitACelsius(valor);
            } else if (origen.equals("Fahrenheit") && destino.equals("Kelvin")) {
                resultado = servicio.fahrenheitAKelvin(valor);
            } else if (origen.equals("Kelvin") && destino.equals("Celsius")) {
                resultado = servicio.kelvinACelsius(valor);
            } else if (origen.equals("Kelvin") && destino.equals("Fahrenheit")) {
                resultado = servicio.kelvinAFahrenheit(valor);
            }
            
            lblResultado.setText(df.format(resultado) + " " + obtenerSimbolo(destino));
            
        } catch (NumberFormatException e) {
            JOptionPane.showMessageDialog(this,
                "Por favor ingrese un valor numérico válido",
                "Error de Entrada",
                JOptionPane.ERROR_MESSAGE);
        } catch (Exception e) {
            JOptionPane.showMessageDialog(this,
                "Error al realizar la conversión: " + e.getMessage(),
                "Error",
                JOptionPane.ERROR_MESSAGE);
        }
    }
    
    private String obtenerSimbolo(String unidad) {
        switch (unidad) {
            case "Celsius": return "°C";
            case "Fahrenheit": return "°F";
            case "Kelvin": return "K";
            default: return "";
        }
    }
    
    private void limpiar() {
        txtValor.setText("");
        cmbOrigen.setSelectedIndex(0);
        cmbDestino.setSelectedIndex(1);
        lblResultado.setText("---");
        txtValor.requestFocus();
    }
}