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
    public partial class MenuPrincipalForm : Form
    {
        private static readonly Color BG_PRIMARY = ColorTranslator.FromHtml("#0f172a");
        private static readonly Color BG_SECONDARY = ColorTranslator.FromHtml("#1e293b");
        private static readonly Color BG_CARD = ColorTranslator.FromHtml("#334155");
        private static readonly Color TEXT_PRIMARY = ColorTranslator.FromHtml("#f1f5f9");
        private static readonly Color TEXT_SECONDARY = ColorTranslator.FromHtml("#94a3b8");
        private static readonly Color BORDER_COLOR = ColorTranslator.FromHtml("#475569");
        private static readonly Color COLOR_TEMPERATURA = ColorTranslator.FromHtml("#ef4444");
        private static readonly Color COLOR_TEMPERATURA_HOVER = ColorTranslator.FromHtml("#dc2626");
        private static readonly Color COLOR_LONGITUD = ColorTranslator.FromHtml("#3b82f6");
        private static readonly Color COLOR_LONGITUD_HOVER = ColorTranslator.FromHtml("#2563eb");
        private static readonly Color COLOR_MASA = ColorTranslator.FromHtml("#10b981");
        private static readonly Color COLOR_MASA_HOVER = ColorTranslator.FromHtml("#059669");

        private string usuario;

        public MenuPrincipalForm(string usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            ConfigurarInterfaz();
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "Conversor de Unidades - Menú Principal";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = BG_PRIMARY;

            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = BG_PRIMARY
            };

            var headerPanel = CrearHeaderPanel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 80;

            var centerPanel = CrearCenterPanel();
            centerPanel.Dock = DockStyle.Fill;

            var footerPanel = CrearFooterPanel();
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Height = 70;

            mainPanel.Controls.AddRange(new Control[] { headerPanel, centerPanel, footerPanel });
            this.Controls.Add(mainPanel);
        }

        private Panel CrearHeaderPanel()
        {
            var header = new Panel
            {
                BackColor = BG_SECONDARY,
                Padding = new Padding(30, 20, 30, 20)
            };

            var lblTitulo = new Label
            {
                Text = "CONVERSOR DE UNIDADES",
                Font = new Font("Arial", 21, FontStyle.Bold),
                ForeColor = TEXT_PRIMARY,
                AutoSize = true,
                Location = new Point(30, 10)
            };

            var lblSubtitulo = new Label
            {
                Text = "Seleccione el tipo de conversión",
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(30, 45)
            };

            var lblUsuario = new Label
            {
                Text = usuario,
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = TEXT_PRIMARY,
                AutoSize = true,
                Location = new Point(630, 30)
            };

            header.Controls.AddRange(new Control[] { lblTitulo, lblSubtitulo, lblUsuario });
            return header;
        }

        private Panel CrearCenterPanel()
        {
            var center = new Panel
            {
                BackColor = BG_PRIMARY,
                Padding = new Padding(40)
            };

            var btnTemperatura = CrearBotonCategoria(
                "🌡️",
                "TEMPERATURA",
                "Celsius, Fahrenheit, Kelvin",
                COLOR_TEMPERATURA,
                COLOR_TEMPERATURA_HOVER,
                (s, e) => AbrirTemperatura()
            );
            btnTemperatura.Location = new Point(40, 100);
            btnTemperatura.Size = new Size(220, 280);

            var btnLongitud = CrearBotonCategoria(
                "📏",
                "LONGITUD",
                "Metros, Kilómetros, Millas",
                COLOR_LONGITUD,
                COLOR_LONGITUD_HOVER,
                (s, e) => AbrirLongitud()
            );
            btnLongitud.Location = new Point(290, 100);
            btnLongitud.Size = new Size(220, 280);

            var btnMasa = CrearBotonCategoria(
                "⚖️",
                "MASA",
                "Kilogramos, Gramos, Libras",
                COLOR_MASA,
                COLOR_MASA_HOVER,
                (s, e) => AbrirMasa()
            );
            btnMasa.Location = new Point(540, 100);
            btnMasa.Size = new Size(220, 280);

            center.Controls.AddRange(new Control[] { btnTemperatura, btnLongitud, btnMasa });
            return center;
        }

        private Panel CrearBotonCategoria(string emoji, string titulo, string descripcion,
                                         Color color, Color colorHover, EventHandler clickHandler)
        {
            var card = new Panel
            {
                BackColor = BG_CARD,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            var iconLabel = new Label
            {
                Text = emoji,
                Font = new Font("Segoe UI Emoji", 40, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(75, 30)
            };

            var lblTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Arial", 15, FontStyle.Bold),
                ForeColor = TEXT_PRIMARY,
                AutoSize = true,
                Location = new Point(30, 130)
            };

            var lblDesc = new Label
            {
                Text = descripcion,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = TEXT_SECONDARY,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(200, 40),
                Location = new Point(10, 170)
            };

            var colorBar = new Panel
            {
                BackColor = color,
                Dock = DockStyle.Bottom,
                Height = 4
            };

            card.Controls.AddRange(new Control[] { iconLabel, lblTitulo, lblDesc, colorBar });

            card.MouseEnter += (s, e) =>
            {
                card.BackColor = BG_SECONDARY;
                colorBar.BackColor = colorHover;
            };

            card.MouseLeave += (s, e) =>
            {
                card.BackColor = BG_CARD;
                colorBar.BackColor = color;
            };

            card.Click += clickHandler;

            foreach (Control ctrl in card.Controls)
            {
                ctrl.Click += clickHandler;
                ctrl.MouseEnter += (s, e) =>
                {
                    card.BackColor = BG_SECONDARY;
                    colorBar.BackColor = colorHover;
                };
                ctrl.MouseLeave += (s, e) =>
                {
                    card.BackColor = BG_CARD;
                    colorBar.BackColor = color;
                };
            }

            return card;
        }

        private Panel CrearFooterPanel()
        {
            var footer = new Panel
            {
                BackColor = BG_PRIMARY,
                Padding = new Padding(30, 15, 30, 15)
            };

            var btnSalir = new Button
            {
                Text = "Cerrar Sesión",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = ColorTranslator.FromHtml("#dc2626"),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 40),
                Location = new Point(630, 15),
                Cursor = Cursors.Hand
            };

            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.Click += (s, e) => Salir();

            btnSalir.MouseEnter += (s, e) => btnSalir.BackColor = ColorTranslator.FromHtml("#b91c1c");
            btnSalir.MouseLeave += (s, e) => btnSalir.BackColor = ColorTranslator.FromHtml("#dc2626");

            footer.Controls.Add(btnSalir);
            return footer;
        }

        private void AbrirTemperatura()
        {
            var form = new PanelTemperaturaForm(usuario);
            form.Show();
        }

        private void AbrirLongitud()
        {
            var form = new PanelLongitudForm(usuario);
            form.Show();
        }

        private void AbrirMasa()
        {
            var form = new PanelMasaForm(usuario);
            form.Show();
        }

        private void Salir()
        {
            var resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Confirmar Salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                var login = new LoginForm();
                login.Show();
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
            base.OnFormClosing(e);
        }
    }
}
