using semana7Stack.Modelos;
using semana7Stack.Estructuras;

namespace OXXO_Voucher

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ModuloVouchers gestor = new ModuloVouchers();

        private void ActualizarVista()
        {
            lstHistorial.Items.Clear();
            foreach (var v in gestor.ObtenerTodos())
            {
                lstHistorial.Items.Add(v);
            }

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
                // Se crea el objeto Voucher con los datos de la transacción
                Voucher nuevoVoucher = new Voucher(codigo, "Voucher Digital", 15.50m, "Completado");
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
    }
}
