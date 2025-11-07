
using ConUni_Client_Escritorio_DotNet.localhost;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConUni_Client_Escritorio_DotNet.ec.edu.monster.vistas
{
    /// <summary>
    /// Formulario de Login con diseño moderno e imagen
    /// </summary>
    public partial class LoginForm : Form
    {
        // Paleta de colores estandarizada
        private static readonly Color BG_PRIMARY = ColorTranslator.FromHtml("#0f172a");
        private static readonly Color BG_SECONDARY = ColorTranslator.FromHtml("#1e293b");
        private static readonly Color BG_CARD = ColorTranslator.FromHtml("#334155");
        private static readonly Color TEXT_PRIMARY = ColorTranslator.FromHtml("#f1f5f9");
        private static readonly Color TEXT_SECONDARY = ColorTranslator.FromHtml("#94a3b8");
        private static readonly Color ACCENT_LOGIN = ColorTranslator.FromHtml("#8b5cf6");
        private static readonly Color ACCENT_LOGIN_HOVER = ColorTranslator.FromHtml("#7c3aed");
        private static readonly Color BORDER_COLOR = ColorTranslator.FromHtml("#475569");

        private ConversorUnidadesWS cliente;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnLimpiar;
        private Label lblEstadoLogin;

        public LoginForm()
        {
            InitializeComponent();
            InicializarCliente();
            ConfigurarInterfaz();
        }

        private void InicializarCliente()
        {
            try
            {
                cliente = new ConversorUnidadesWS();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con el servidor:\n{ex.Message}",
                    "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "Conversor de Unidades - Login";
            this.Size = new Size(1000, 750);  // Aumentado el ancho para la imagen
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = BG_PRIMARY;

            // Panel principal
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = BG_PRIMARY,
                Padding = new Padding(30)
            };

            // Header
            var headerPanel = CrearHeaderPanel();
            headerPanel.Location = new Point(250, 20);
            headerPanel.Size = new Size(400, 120);

            // Panel horizontal para imagen y formulario
            var contentPanel = new Panel
            {
                Location = new Point(80, 160),
                Size = new Size(840, 400),
                BackColor = BG_PRIMARY
            };

            // Panel de la imagen (IZQUIERDA)
            var imagePanel = CrearImagePanel();
            imagePanel.Location = new Point(0, 0);
            imagePanel.Size = new Size(380, 380);

            // Card del formulario (DERECHA)
            var formCard = CrearFormCard();
            formCard.Location = new Point(400, 30);
            formCard.Size = new Size(380, 320);

            contentPanel.Controls.AddRange(new Control[] { imagePanel, formCard });

            // Panel de información
            var infoPanel = CrearInfoPanel();
            infoPanel.Location = new Point(240, 580);
            infoPanel.Size = new Size(500, 80);

            mainPanel.Controls.AddRange(new Control[] { headerPanel, contentPanel, infoPanel });
            this.Controls.Add(mainPanel);

            // Enter para login
            txtPassword.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    Login();
                    e.Handled = true;
                }
            };
        }

        private Panel CrearHeaderPanel()
        {
            var panel = new Panel
            {
                BackColor = BG_PRIMARY
            };

            var iconLabel = new Label
            {
                Text = "🔐",
                Font = new Font("Segoe UI Emoji", 35, FontStyle.Regular),
                ForeColor = ACCENT_LOGIN,
                AutoSize = true,
                Location = new Point(180, 0)
            };

            var titulo = new Label
            {
                Text = "CONVERSOR DE UNIDADES",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = TEXT_PRIMARY,
                AutoSize = true,
                Location = new Point(5, 60)
            };

            var subtitulo = new Label
            {
                Text = "Sistema de Conversión - ESPE",
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(80, 95)
            };

            panel.Controls.AddRange(new Control[] { iconLabel, titulo, subtitulo });
            return panel;
        }

        private Panel CrearImagePanel()
        {
            var panel = new Panel
            {
                BackColor = BG_PRIMARY,
                BorderStyle = BorderStyle.None
            };

            var pictureBox = new PictureBox
            {
                Size = new Size(350, 350),
                Location = new Point(15, 15),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = BG_PRIMARY
            };

            try
            {
                // Intenta cargar la imagen desde diferentes ubicaciones
                string[] posiblesRutas = new string[]
                {
                    // Opción 1: Desde la carpeta Resources
                   // Path.Combine(Application.StartupPath, "Resources", "img.png"),
                    //Path.Combine(Application.StartupPath, "Resources", "logo.png"),
                    
                    // Opción 2: Desde la carpeta Images
                    Path.Combine(Application.StartupPath, "images", "img.png"),
                    Path.Combine(Application.StartupPath, "images", "logo.png"),
                    
                    // Opción 3: Desde la raíz
                    Path.Combine(Application.StartupPath, "img.png"),
                    Path.Combine(Application.StartupPath, "logo.png"),
                    
                    // Opción 4: Ruta relativa al proyecto (modo debug)
                    Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "Resources", "img.png"),
                    Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "images", "img.png")
                };

                bool imagenCargada = false;
                foreach (string ruta in posiblesRutas)
                {
                    if (File.Exists(ruta))
                    {
                        pictureBox.Image = Image.FromFile(ruta);
                        imagenCargada = true;
                        break;
                    }
                }

                // Si no se encuentra la imagen, muestra un placeholder
                if (!imagenCargada)
                {
                    // Crear una imagen placeholder
                    var placeholderLabel = new Label
                    {
                        Text = "🖼️\n\nImagen del\nConversor",
                        Font = new Font("Arial", 24, FontStyle.Bold),
                        ForeColor = TEXT_SECONDARY,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(350, 350),
                        Location = new Point(15, 15),
                        BackColor = BG_CARD,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    panel.Controls.Add(placeholderLabel);
                    return panel;
                }
            }
            catch (Exception ex)
            {
                // Si hay error al cargar, muestra un mensaje
                var errorLabel = new Label
                {
                    Text = $"❌\n\nError al cargar\nla imagen\n\n{ex.Message}",
                    Font = new Font("Arial", 12, FontStyle.Regular),
                    ForeColor = Color.Red,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(350, 350),
                    Location = new Point(15, 15),
                    BackColor = BG_CARD,
                    BorderStyle = BorderStyle.FixedSingle
                };
                panel.Controls.Add(errorLabel);
                return panel;
            }

            panel.Controls.Add(pictureBox);
            return panel;
        }

        private Panel CrearFormCard()
        {
            var card = new Panel
            {
                BackColor = BG_CARD,
                BorderStyle = BorderStyle.FixedSingle
            };

            card.Padding = new Padding(30);

            var lblUsuario = new Label
            {
                Text = "Usuario",
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(140, 30)
            };

            txtUsername = new TextBox
            {
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = TEXT_PRIMARY,
                BackColor = BG_SECONDARY,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(70, 60),
                Size = new Size(240, 30)
            };

            var lblPassword = new Label
            {
                Text = "Contraseña",
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(130, 110)
            };

            txtPassword = new TextBox
            {
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = TEXT_PRIMARY,
                BackColor = BG_SECONDARY,
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '•',
                Location = new Point(70, 140),
                Size = new Size(240, 30)
            };

            btnLogin = CrearBoton("Iniciar Sesión", ACCENT_LOGIN, ACCENT_LOGIN_HOVER);
            btnLogin.Location = new Point(40, 190);
            btnLogin.Size = new Size(140, 45);
            btnLogin.Click += (s, e) => Login();

            btnLimpiar = CrearBotonSecundario("Limpiar");
            btnLimpiar.Location = new Point(190, 190);
            btnLimpiar.Size = new Size(140, 45);
            btnLimpiar.Click += (s, e) => Limpiar();

            lblEstadoLogin = new Label
            {
                Text = "",
                Font = new Font("Arial", 9, FontStyle.Regular),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(120, 250),
                MaximumSize = new Size(300, 0)
            };

            card.Controls.AddRange(new Control[] {
                lblUsuario, txtUsername, lblPassword, txtPassword,
                btnLogin, btnLimpiar, lblEstadoLogin
            });

            return card;
        }

        private Panel CrearInfoPanel()
        {
            var panel = new Panel
            {
                BackColor = BG_SECONDARY,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15)
            };

            var infoLabel = new Label
            {
                Text = "💡 Credenciales de prueba:",
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(150, 10)
            };

            var credenciales = new Label
            {
                Text = "Usuario: MONSTER  |  Contraseña: MONSTER9",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(80, 35)
            };

            panel.Controls.AddRange(new Control[] { infoLabel, credenciales });
            return panel;
        }

        private Button CrearBoton(string texto, Color bg, Color bgHover)
        {
            var btn = new Button
            {
                Text = texto,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = bg,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) => btn.BackColor = bgHover;
            btn.MouseLeave += (s, e) => btn.BackColor = bg;

            return btn;
        }

        private Button CrearBotonSecundario(string texto)
        {
            var btn = new Button
            {
                Text = texto,
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = TEXT_PRIMARY,
                BackColor = BG_SECONDARY,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderColor = BORDER_COLOR;
            btn.FlatAppearance.BorderSize = 1;

            btn.MouseEnter += (s, e) => btn.BackColor = BORDER_COLOR;
            btn.MouseLeave += (s, e) => btn.BackColor = BG_SECONDARY;

            return btn;
        }

        private void Login()
        {
            string usuario = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                lblEstadoLogin.Text = "⚠️ Por favor ingrese usuario y contraseña";
                lblEstadoLogin.ForeColor = Color.Orange;
                return;
            }

            try
            {
                lblEstadoLogin.Text = "Autenticando...";
                lblEstadoLogin.ForeColor = Color.Blue;
                Application.DoEvents();

                var respuesta = cliente.Login(usuario, password);

                if (respuesta.Exitoso)
                {
                    MessageBox.Show($"¡Bienvenido {respuesta.Username}!",
                        "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var menu = new MenuPrincipalForm(respuesta.Username);
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    lblEstadoLogin.Text = "❌ " + respuesta.Mensaje;
                    lblEstadoLogin.ForeColor = Color.Red;
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                lblEstadoLogin.Text = "❌ Error de conexión";
                lblEstadoLogin.ForeColor = Color.Red;
                MessageBox.Show($"Error al autenticar:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (cliente != null)
            {
                try
                {
                    cliente.Dispose();
                }
                catch { }
            }
            Application.Exit();
            base.OnFormClosing(e);
        }
    }
}
