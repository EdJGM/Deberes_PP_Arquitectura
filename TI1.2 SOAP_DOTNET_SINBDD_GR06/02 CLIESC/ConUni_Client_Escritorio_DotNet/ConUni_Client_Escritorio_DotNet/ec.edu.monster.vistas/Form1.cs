using ConUni_Client_Escritorio_DotNet.localhost;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConUni_Client_Escritorio_DotNet
{
    /// <summary>
    /// Formulario principal del cliente de escritorio
    /// </summary>
    public partial class Form1 : Form
    {
        private ConversorUnidadesWS cliente;
        private bool estaAutenticado = false;
        private string usuarioActual = "";

        // Controles UI
        private Panel panelLogin;
        private Panel panelConversores;
        private TabControl tabConversores;

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblEstadoLogin;

        private Label lblUsuario;
        private Button btnCerrarSesion;

        public Form1()
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
            this.Text = "Conversor de Unidades - Cliente SOAP";
            this.Size = new Size(700, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Panel de Login
            panelLogin = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 240, 245)
            };

            var lblTitulo = new Label
            {
                Text = "🔐 Iniciar Sesión",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(250, 80),
                Size = new Size(200, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblUser = new Label
            {
                Text = "Usuario:",
                Location = new Point(200, 160),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10)
            };

            txtUsername = new TextBox
            {
                Location = new Point(290, 158),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 11)
            };

            var lblPass = new Label
            {
                Text = "Contraseña:",
                Location = new Point(200, 210),
                Size = new Size(90, 25),
                Font = new Font("Segoe UI", 10)
            };

            txtPassword = new TextBox
            {
                Location = new Point(290, 208),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 11),
                PasswordChar = '•'
            };

            btnLogin = new Button
            {
                Text = "Iniciar Sesión",
                Location = new Point(290, 260),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            lblEstadoLogin = new Label
            {
                Location = new Point(200, 320),
                Size = new Size(300, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9)
            };

            var lblCredenciales = new Label
            {
                Text = "💡 Credenciales de prueba:\nUsuario: MONSTER\nContraseña: MONSTER9",
                Location = new Point(225, 370),
                Size = new Size(250, 70),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };

            panelLogin.Controls.AddRange(new Control[] {
                lblTitulo, lblUser, txtUsername, lblPass, txtPassword,
                btnLogin, lblEstadoLogin, lblCredenciales
            });

            // Panel de Conversores
            panelConversores = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                BackColor = Color.White
            };

            var panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(0, 120, 215)
            };

            lblUsuario = new Label
            {
                Location = new Point(20, 15),
                Size = new Size(400, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            btnCerrarSesion = new Button
            {
                Text = "Cerrar Sesión",
                Location = new Point(550, 15),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(232, 17, 35),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.Click += BtnCerrarSesion_Click;

            panelHeader.Controls.AddRange(new Control[] { lblUsuario, btnCerrarSesion });

            // Tabs de conversores
            tabConversores = new TabControl
            {
                Location = new Point(20, 80),
                Size = new Size(640, 400),
                Font = new Font("Segoe UI", 10)
            };

            tabConversores.TabPages.Add(CrearTabTemperatura());
            tabConversores.TabPages.Add(CrearTabLongitud());
            tabConversores.TabPages.Add(CrearTabMasa());

            panelConversores.Controls.AddRange(new Control[] { panelHeader, tabConversores });

            this.Controls.AddRange(new Control[] { panelLogin, panelConversores });
        }

        private TabPage CrearTabTemperatura()
        {
            var tab = new TabPage("🌡️ Temperatura");

            var grpCelsius = CrearGrupoConversion("Celsius",
                new[] { "Fahrenheit", "Kelvin" }, 30, 30);
            var grpFahrenheit = CrearGrupoConversion("Fahrenheit",
                new[] { "Celsius", "Kelvin" }, 330, 30);
            var grpKelvin = CrearGrupoConversion("Kelvin",
                new[] { "Celsius", "Fahrenheit" }, 30, 210);

            // Eventos para Celsius
            grpCelsius.Tag = new Action<double, ComboBox, Label>((valor, combo, resultado) =>
            {
                if (combo.SelectedIndex == 0) // a Fahrenheit
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.CelsiusAFahrenheit(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F2} °F";
                }
                else // a Kelvin
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.CelsiusAKelvin(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F2} K";
                }
            });

            // Eventos para Fahrenheit
            grpFahrenheit.Tag = new Action<double, ComboBox, Label>((valor, combo, resultado) =>
            {
                if (combo.SelectedIndex == 0) // a Celsius
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.FahrenheitACelsius(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F2} °C";
                }
                else // a Kelvin
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.FahrenheitAKelvin(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F2} K";
                }
            });

            // Eventos para Kelvin
            grpKelvin.Tag = new Action<double, ComboBox, Label>((valor, combo, resultado) =>
            {
                if (combo.SelectedIndex == 0) // a Celsius
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.KelvinACelsius(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F2} °C";
                }
                else // a Fahrenheit
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.KelvinAFahrenheit(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F2} °F";
                }
            });

            tab.Controls.AddRange(new Control[] { grpCelsius, grpFahrenheit, grpKelvin });
            return tab;
        }

        private TabPage CrearTabLongitud()
        {
            var tab = new TabPage("📏 Longitud");

            var grpMetro = CrearGrupoConversion("Metro",
                new[] { "Kilómetro", "Milla" }, 30, 30);
            var grpKilometro = CrearGrupoConversion("Kilómetro",
                new[] { "Metro", "Milla" }, 330, 30);
            var grpMilla = CrearGrupoConversion("Milla",
                new[] { "Metro", "Kilómetro" }, 30, 210);

            grpMetro.Tag = new Action<double, ComboBox, Label>((valor, combo, resultado) =>
            {
                if (combo.SelectedIndex == 0)
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.MetroAKilometro(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F4} km";
                }
                else
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.MetroAMilla(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F4} mi";
                }
            });

            grpKilometro.Tag = new Action<double, ComboBox, Label>((valor, combo, resultado) =>
            {
                if (combo.SelectedIndex == 0)
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.KilometroAMetro(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F2} m";
                }
                else
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.KilometroAMilla(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F4} mi";
                }
            });

            grpMilla.Tag = new Action<double, ComboBox, Label>((valor, combo, resultado) =>
            {
                if (combo.SelectedIndex == 0)
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.MillaAMetro(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F2} m";
                }
                else
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.MillaAKilometro(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F4} km";
                }
            });

            tab.Controls.AddRange(new Control[] { grpMetro, grpKilometro, grpMilla });
            return tab;
        }

        private TabPage CrearTabMasa()
        {
            var tab = new TabPage("⚖️ Masa");

            var grpKilogramo = CrearGrupoConversion("Kilogramo",
                new[] { "Gramo", "Libra" }, 30, 30);
            var grpGramo = CrearGrupoConversion("Gramo",
                new[] { "Kilogramo", "Libra" }, 330, 30);
            var grpLibra = CrearGrupoConversion("Libra",
                new[] { "Kilogramo", "Gramo" }, 30, 210);

            grpKilogramo.Tag = new Action<double, ComboBox, Label>((valor, combo, resultado) =>
            {
                if (combo.SelectedIndex == 0)
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.KilogramoAGramo(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F2} g";
                }
                else
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.KilogramoALibra(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F4} lb";
                }
            });

            grpGramo.Tag = new Action<double, ComboBox, Label>((valor, combo, resultado) =>
            {
                if (combo.SelectedIndex == 0)
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.GramoAKilogramo(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F4} kg";
                }
                else
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.GramoALibra(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F4} lb";
                }
            });

            grpLibra.Tag = new Action<double, ComboBox, Label>((valor, combo, resultado) =>
            {
                if (combo.SelectedIndex == 0)
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.LibraAKilogramo(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F4} kg";
                }
                else
                {
                    double resultadoTemp;
                    bool resultadoSpecified;
                    cliente.LibraAGramo(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado.Text = $"{resultadoTemp:F2} g";
                }
            });

            tab.Controls.AddRange(new Control[] { grpKilogramo, grpGramo, grpLibra });
            return tab;
        }

        private GroupBox CrearGrupoConversion(string titulo, string[] destinos, int x, int y)
        {
            var grupo = new GroupBox
            {
                Text = $"Convertir desde {titulo}",
                Location = new Point(x, y),
                Size = new Size(280, 160),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            var txtValor = new TextBox
            {
                Location = new Point(15, 30),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10)
            };

            var cmbDestino = new ComboBox
            {
                Location = new Point(145, 30),
                Size = new Size(120, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbDestino.Items.AddRange(destinos);
            cmbDestino.SelectedIndex = 0;

            var btnConvertir = new Button
            {
                Text = "Convertir",
                Location = new Point(80, 70),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConvertir.FlatAppearance.BorderSize = 0;

            var lblResultado = new Label
            {
                Text = "Resultado: ---",
                Location = new Point(15, 115),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 110, 190),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnConvertir.Click += (s, e) =>
            {
                try
                {
                    if (double.TryParse(txtValor.Text, out double valor))
                    {
                        var accion = (Action<double, ComboBox, Label>)grupo.Parent.Controls
                            .Find(grupo.Name, false)[0].Tag;
                        accion?.Invoke(valor, cmbDestino, lblResultado);
                    }
                    else
                    {
                        MessageBox.Show("Por favor ingrese un número válido",
                            "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en la conversión:\n{ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            grupo.Controls.AddRange(new Control[] {
                txtValor, cmbDestino, btnConvertir, lblResultado
            });

            return grupo;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblEstadoLogin.Text = "Autenticando...";
                lblEstadoLogin.ForeColor = Color.Blue;
                Application.DoEvents();

                var respuesta = cliente.Login(txtUsername.Text, txtPassword.Text);

                if (respuesta.Exitoso)
                {
                    estaAutenticado = true;
                    usuarioActual = respuesta.Username;
                    lblUsuario.Text = $"👤 Bienvenido, {usuarioActual}";

                    panelLogin.Visible = false;
                    panelConversores.Visible = true;
                }
                else
                {
                    lblEstadoLogin.Text = "❌ " + respuesta.Mensaje;
                    lblEstadoLogin.ForeColor = Color.Red;
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

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Confirmar Cierre de Sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                estaAutenticado = false;
                usuarioActual = "";
                txtUsername.Clear();
                txtPassword.Clear();
                lblEstadoLogin.Text = "";

                panelConversores.Visible = false;
                panelLogin.Visible = true;
            }
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
            base.OnFormClosing(e);
        }
    }
}