using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Examen_final_parte_que_me_toco;

namespace OxxoPOS_GUI
{
    public partial class Form1 : Form
    {
        private static readonly Color Rojo = ColorTranslator.FromHtml("#D11B1B");
        private static readonly Color Amarillo = ColorTranslator.FromHtml("#FDB813");
        private static readonly Color Fondo = ColorTranslator.FromHtml("#FFF8E1");

        private ListaEnlazadaDoble listaVouchers = new ListaEnlazadaDoble();
        private Label lblActual = new Label();
        private int indiceActual = -1;

        public Form1()
        {
            InitializeComponent();
            AplicarEstilo();
        }

        private Label CrearTitulo(string texto, int x, int y, int ancho)
        {
            Label l = new Label
            {
                Text = texto,
                ForeColor = Rojo,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.BottomLeft
            };
            l.SetBounds(x, y, ancho, 26);
            return l;
        }

        private Button CrearBotonNavegacion(string texto, int x, int y, int ancho)
        {
            Button b = new Button
            {
                Text = texto,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Amarillo,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            b.FlatAppearance.BorderColor = Rojo;
            b.FlatAppearance.BorderSize = 2;
            b.SetBounds(x, y, ancho, 46);
            return b;
        }

        private void AplicarEstilo()
        {
            ClientSize = new Size(1000, 640);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Fondo;

            // Banner
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Height = 120;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            string ruta = System.IO.Path.Combine(Application.StartupPath, "banner.png");
            if (System.IO.File.Exists(ruta))
                pictureBox1.Image = Image.FromFile(ruta);

            // Columna izquierda
            Controls.Add(CrearTitulo("Código del voucher", 30, 135, 400));
            textBox1.Font = new Font("Segoe UI", 12F);
            textBox1.SetBounds(30, 165, 400, 32);

            button1.SetBounds(30, 215, 400, 56);
            button1.BackColor = Rojo;
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Amarillo;
            button1.FlatAppearance.BorderSize = 3;
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            Button btnAnterior = CrearBotonNavegacion("◄ Anterior", 30, 290, 195);
            btnAnterior.Click += (s, e) => Navegar(-1);
            Button btnSiguiente = CrearBotonNavegacion("Siguiente ►", 235, 290, 195);
            btnSiguiente.Click += (s, e) => Navegar(1);

            Controls.Add(btnAnterior);
            Controls.Add(btnSiguiente);

            Controls.Add(CrearTitulo("Comprobante actual", 30, 350, 400));
            lblActual.AutoSize = false;
            lblActual.BackColor = Color.White;
            lblActual.BorderStyle = BorderStyle.FixedSingle;
            lblActual.Font = new Font("Segoe UI", 10.5F);
            lblActual.Padding = new Padding(8);
            lblActual.TextAlign = ContentAlignment.TopLeft;
            lblActual.Text = "(ninguno)";
            lblActual.SetBounds(30, 380, 400, 150);
            Controls.Add(lblActual);

            // Columna derecha
            Controls.Add(CrearTitulo("Historial de vouchers", 460, 135, 510));
            lstVouchers.SetBounds(460, 165, 510, 440);
            lstVouchers.Font = new Font("Consolas", 10F);
            lstVouchers.BorderStyle = BorderStyle.FixedSingle;
            lstVouchers.HorizontalScrollbar = true;

            AcceptButton = button1;
        }

        private void MostrarComprobante(int posicion, int total, Transaccion t)
        {
            lblActual.Text = $"Registro {posicion} de {total}\r\n\r\n{t}";
        }

        private void Navegar(int paso)
        {
            List<Transaccion> historial = listaVouchers.ObtenerRecorridoAdelante();
            if (historial.Count == 0)
            {
                lblActual.Text = "(ninguno)";
                return;
            }

            indiceActual = Math.Max(0, Math.Min(historial.Count - 1, indiceActual + paso));
            MostrarComprobante(indiceActual + 1, historial.Count, historial[indiceActual]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string codigo = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Por favor, ingresa un identificador para el voucher.",
                    "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            try
            {
                Transaccion nueva = new Transaccion(codigo, "Voucher Digital", 15.50m, "Completado");
                listaVouchers.Insertar(nueva);

                int total = listaVouchers.ObtenerRecorridoAdelante().Count;
                indiceActual = total - 1;
                MostrarComprobante(total, total, nueva);

                lstVouchers.Items.Add($"{total:000} | {DateTime.Now:HH:mm:ss} | {nueva}");
                lstVouchers.TopIndex = lstVouchers.Items.Count - 1;

                textBox1.Clear();
                textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
        }

        private void lstVouchers_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}