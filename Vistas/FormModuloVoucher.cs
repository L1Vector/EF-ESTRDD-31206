using semana7Stack.Estructuras;
using semana7Stack.Modelos;
using semana7Stack.Vistas;
using System;
using System.Windows.Forms; // Asegúrate de incluir System.Windows.Forms

namespace semana7Stack
{
    public partial class FormModuloVouchers : Form
    {
        private ModuloVouchers gestor = new ModuloVouchers();

        public FormModuloVouchers()
        {
            InitializeComponent();

            // Suscribirse al evento Load para cargar la transacción global al iniciar
            this.Load += FormModuloVouchers_Load;
        }

        private void FormModuloVouchers_Load(object sender, EventArgs e)
        {
            CargarTransaccionGlobal();
            ActualizarVista();
        }

        // Método para leer la variable global y registrarla como Voucher
        private void CargarTransaccionGlobal()
        {
            // Verificamos si la lista global existe y contiene elementos
            if (SesionGlobal.HistorialTransaccionesGlobal != null && SesionGlobal.HistorialTransaccionesGlobal.Count > 0)
            {
                // Recorremos CADA nodo de transacción dentro de la lista global
                foreach (var trans in SesionGlobal.HistorialTransaccionesGlobal)
                {
                    // Verificamos si ya existe en la lista del gestor local de Vouchers
                    bool yaExiste = false;
                    foreach (var v in gestor.ObtenerTodos())
                    {
                        if (v.IdOperacion == trans.Codigo)
                        {
                            yaExiste = true;
                            break;
                        }
                    }

                    // Si no existe, lo agregamos como un nuevo Voucher
                    if (!yaExiste)
                    {
                        Voucher nuevoVoucher = new Voucher(
                            trans.Codigo,
                            trans.Tipo,
                            trans.Pago,
                            trans.Estado
                        );

                        gestor.Agregar(nuevoVoucher);
                    }
                }
            }
        }


        private void ActualizarVista()
        {
            // Visualizar todos los pagos registrados en el ListBox de historial
            lstHistorial.Items.Clear();
            foreach (var v in gestor.ObtenerTodos())
            {
                lstHistorial.Items.Add(v); // ToString() de Voucher formateará la línea automáticamente
            }

            // Visualizar el detalle del voucher actual seleccionado con los punteros
            lstVoucher.Items.Clear();
            if (gestor.Actual != null)
            {
                int posicion = gestor.ObtenerPosicionActual();
                int total = gestor.ObtenerTotalRegistros();

                lblActual.Text = $"Registro {posicion} de {total}\r\n\r\n{gestor.Actual.Dato}";
            }
            else
            {
                lblActual.Text = "(ninguno)";
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim();

            if (!string.IsNullOrEmpty(codigo))
            {
                // Registro manual opcional desde el textbox
                Voucher nuevoVoucher = new Voucher(codigo, "Voucher Digital", SesionGlobal.CostoTotalActual, "Completado");
                gestor.Agregar(nuevoVoucher);

                ActualizarVista();

                txtCodigo.Clear();
                txtCodigo.Focus();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un código de voucher válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (gestor.MoverSiguiente())
            {
                ActualizarVista();
            }
            else
            {
                MessageBox.Show("Estás en el último voucher del historial.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (gestor.MoverAnterior())
            {
                ActualizarVista();
            }
            else
            {
                MessageBox.Show("Estás en el primer voucher del historial.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAnteriorPag_Click(object sender, EventArgs e)
        {
            FormPago formPago = new FormPago();
            this.Hide();
            formPago.ShowDialog();
            this.Show();
        }
    }
}