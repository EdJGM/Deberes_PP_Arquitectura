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

namespace ConUni_Client_Escritorio_DotNet.ec.edu.monster.vistas
{
    public partial class PanelTemperaturaForm : Form
    {
        private static readonly Color BG_PRIMARY = ColorTranslator.FromHtml("#0f172a");
        private static readonly Color BG_SECONDARY = ColorTranslator.FromHtml("#1e293b");
        private static readonly Color BG_CARD = ColorTranslator.FromHtml("#334155");
        private static readonly Color TEXT_PRIMARY = ColorTranslator.FromHtml("#f1f5f9");
        private static readonly Color TEXT_SECONDARY = ColorTranslator.FromHtml("#94a3b8");
        private static readonly Color BORDER_COLOR = ColorTranslator.FromHtml("#475569");
        private static readonly Color COLOR_TEMPERATURA = ColorTranslator.FromHtml("#ef4444");
        private static readonly Color COLOR_TEMPERATURA_HOVER = ColorTranslator.FromHtml("#dc2626");

        private string usuario;
        private ConversorUnidadesWS servicio;
        private TextBox txtValor;
        private ComboBox cmbOrigen;
        private ComboBox cmbDestino;
        private Label lblResultado;

        public PanelTemperaturaForm(string usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            InicializarServicio();
            ConfigurarInterfaz();
        }

        private void InicializarServicio()
        {
            try
            {
                servicio = new ConversorUnidadesWS();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con el servidor:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "Conversión de Temperatura";
            this.Size = new Size(800, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = BG_PRIMARY;

            var mainPanel = new Panel { Dock = DockStyle.Fill, BackColor = BG_PRIMARY };
            var headerPanel = CrearHeaderPanel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 80;
            var centerPanel = CrearCenterPanel();
            centerPanel.Dock = DockStyle.Fill;
            var footerPanel = CrearFooterPanel();
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Height = 80;

            mainPanel.Controls.AddRange(new Control[] { headerPanel, centerPanel, footerPanel });
            this.Controls.Add(mainPanel);
        }

        private Panel CrearHeaderPanel()
        {
            var header = new Panel
            {
                BackColor = COLOR_TEMPERATURA,
                Padding = new Padding(30, 25, 30, 25)
            };

            var iconLabel = new Label
            {
                Text = "🌡️  ",
                Font = new Font("Segoe UI Emoji", 36, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(30, 15)
            };

            var lblTitulo = new Label
            {
                Text = "CONVERSIÓN DE TEMPERATURA",
                Font = new Font("Arial", 15, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(150, 15)
            };

            var lblDesc = new Label
            {
                Text = "Celsius, Fahrenheit, Kelvin",
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(150, 50)
            };

            var lblUsuario = new Label
            {
                Text = usuario,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(680, 30)
            };

            header.Controls.AddRange(new Control[] { iconLabel, lblTitulo, lblDesc, lblUsuario });
            return header;
        }

        private Panel CrearCenterPanel()
        {
            var center = new Panel
            {
                BackColor = BG_PRIMARY,
                Padding = new Padding(40, 30, 40, 30)
            };

            var formCard = new Panel
            {
                BackColor = BG_CARD,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(140, 100),
                Size = new Size(520, 600)
            };

            int yPos = 30;

            var lblValor = new Label
            {
                Text = "Valor a convertir",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(190, yPos)
            };
            yPos += 35;

            txtValor = new TextBox
            {
                Font = new Font("Arial", 16, FontStyle.Regular),
                ForeColor = TEXT_PRIMARY,
                BackColor = BG_SECONDARY,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(50, yPos),
                Size = new Size(420, 50)
            };
            yPos += 70;

            var lblOrigen = new Label
            {
                Text = "Unidad de origen",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(190, yPos)
            };
            yPos += 35;

            string[] unidades = { "Celsius", "Fahrenheit", "Kelvin" };
            cmbOrigen = new ComboBox
            {
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = TEXT_PRIMARY,
                BackColor = BG_SECONDARY,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(50, yPos),
                Size = new Size(420, 45)
            };
            cmbOrigen.Items.AddRange(unidades);
            cmbOrigen.SelectedIndex = 0;
            yPos += 65;

            var lblFlecha = new Label
            {
                Text = "⬇️",
                Font = new Font("Segoe UI Emoji", 32, FontStyle.Regular),
                ForeColor = COLOR_TEMPERATURA,
                AutoSize = true,
                Location = new Point(240, yPos)
            };
            yPos += 55;

            var lblDestino = new Label
            {
                Text = "Unidad de destino",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(185, yPos)
            };
            yPos += 35;

            cmbDestino = new ComboBox
            {
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = TEXT_PRIMARY,
                BackColor = BG_SECONDARY,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(50, yPos),
                Size = new Size(420, 45)
            };
            cmbDestino.Items.AddRange(unidades);
            cmbDestino.SelectedIndex = 1;
            yPos += 75;

            var resultPanel = new Panel
            {
                BackColor = BG_SECONDARY,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(50, yPos),
                Size = new Size(420, 120)
            };

            var lblResultadoTitle = new Label
            {
                Text = "RESULTADO",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(170, 20)
            };

            lblResultado = new Label
            {
                Text = "---",
                Font = new Font("Arial", 32, FontStyle.Bold),
                ForeColor = COLOR_TEMPERATURA,
                AutoSize = true,
                Location = new Point(180, 50)
            };

            resultPanel.Controls.AddRange(new Control[] { lblResultadoTitle, lblResultado });

            formCard.Controls.AddRange(new Control[] {
                lblValor, txtValor, lblOrigen, cmbOrigen, lblFlecha,
                lblDestino, cmbDestino, resultPanel
            });

            center.Controls.Add(formCard);
            return center;
        }

        private Panel CrearFooterPanel()
        {
            var footer = new Panel
            {
                BackColor = BG_PRIMARY,
                Padding = new Padding(30, 20, 30, 20)
            };

            var btnConvertir = CrearBoton("Convertir", COLOR_TEMPERATURA, COLOR_TEMPERATURA_HOVER);
            btnConvertir.Location = new Point(180, 20);
            btnConvertir.Size = new Size(140, 45);
            btnConvertir.Click += (s, e) => Convertir();

            var btnLimpiar = CrearBotonSecundario("Limpiar");
            btnLimpiar.Location = new Point(335, 20);
            btnLimpiar.Size = new Size(140, 45);
            btnLimpiar.Click += (s, e) => Limpiar();

            var btnVolver = CrearBotonSecundario("← Volver");
            btnVolver.Location = new Point(490, 20);
            btnVolver.Size = new Size(140, 45);
            btnVolver.Click += (s, e) => this.Close();

            footer.Controls.AddRange(new Control[] { btnConvertir, btnLimpiar, btnVolver });
            return footer;
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
            btn.FlatAppearance.BorderSize = 2;
            btn.MouseEnter += (s, e) => btn.BackColor = BORDER_COLOR;
            btn.MouseLeave += (s, e) => btn.BackColor = BG_SECONDARY;
            return btn;
        }

        private void Convertir()
        {
            try
            {
                if (!double.TryParse(txtValor.Text.Trim(), out double valor))
                {
                    MessageBox.Show("Por favor ingrese un valor numérico válido",
                        "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string origen = cmbOrigen.SelectedItem.ToString();
                string destino = cmbDestino.SelectedItem.ToString();

                if (origen == destino)
                {
                    lblResultado.Text = valor.ToString("F2");
                    return;
                }

                double resultado = 0;
                double resultadoTemp;
                bool resultadoSpecified;

                if (origen == "Celsius" && destino == "Fahrenheit")
                {
                    servicio.CelsiusAFahrenheit(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado = resultadoTemp;
                }
                else if (origen == "Celsius" && destino == "Kelvin")
                {
                    servicio.CelsiusAKelvin(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado = resultadoTemp;
                }
                else if (origen == "Fahrenheit" && destino == "Celsius")
                {
                    servicio.FahrenheitACelsius(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado = resultadoTemp;
                }
                else if (origen == "Fahrenheit" && destino == "Kelvin")
                {
                    servicio.FahrenheitAKelvin(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado = resultadoTemp;
                }
                else if (origen == "Kelvin" && destino == "Celsius")
                {
                    servicio.KelvinACelsius(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado = resultadoTemp;
                }
                else if (origen == "Kelvin" && destino == "Fahrenheit")
                {
                    servicio.KelvinAFahrenheit(valor, true, out resultadoTemp, out resultadoSpecified);
                    resultado = resultadoTemp;
                }

                string simbolo = destino == "Celsius" ? "°C" : destino == "Fahrenheit" ? "°F" : "K";
                lblResultado.Text = $"{resultado:F2} {simbolo}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar la conversión:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            txtValor.Clear();
            cmbOrigen.SelectedIndex = 0;
            cmbDestino.SelectedIndex = 1;
            lblResultado.Text = "---";
            txtValor.Focus();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (servicio != null)
            {
                try
                {
                    servicio.Dispose();
                }
                catch { }
            }
            base.OnFormClosing(e);
        }
    }
}
