using System;
using System.Windows.Forms;
using semana7Stack.Estructuras;
using semana7Stack.Modelos;

namespace semana7Stack.Vistas
{
    public partial class FormPago : Form
    {
        private ListaDobleValidacion lista = new ListaDobleValidacion();
        private NodoTransaccion nodoActual = null;

        public FormPago()
        {
            InitializeComponent();

            // Cargar tipos de pago
            if (cbxTipo.Items.Count == 0)
            {
                cbxTipo.Items.Add("Pago con QR");
                cbxTipo.Items.Add("Tarjeta de Crédito/Débito");
                cbxTipo.SelectedIndex = 0;
            }

            actualizarInterfaz();
        }

        private void actualizarInterfaz()
        {
            lstLista.Items.Clear();
            NodoTransaccion actual = lista.cabeza;

            if (actual == null)
            {
                lstLista.Items.Add("No hay registros de pago.");
                label3.Text = $"Monto a procesar: S/{SesionGlobal.CostoTotalActual} | Registros: 0";
                nodoActual = null;
                return;
            }

            // Recorremos la lista doble completa
            while (actual != null)
            {
                string textoNodo = $"[{actual.Codigo}] - {actual.Tipo} - S/{actual.Pago} ({actual.Estado})";

                if (nodoActual != null && actual == nodoActual)
                {
                    lstLista.Items.Add($"-> {textoNodo}");
                }
                else
                {
                    lstLista.Items.Add($"   {textoNodo}");
                }

                actual = actual.Siguiente;
            }

            // Información en el Label inferior
            if (nodoActual != null)
            {
                label3.Text = $"Seleccionado: {nodoActual.Codigo} | Método: {nodoActual.Tipo} | Monto: S/{nodoActual.Pago} ";
            }
            else
            {
                label3.Text = $"Monto global del carrito: S/{SesionGlobal.CostoTotalActual}";
            }
        }


        // Botón "Validar Pago"
        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (nodoActual != null)
            {
                nodoActual.Estado = "APROBADO";
                MessageBox.Show($"La transacción [{nodoActual.Codigo}] de tipo [{nodoActual.Tipo}] por S/{nodoActual.Pago} ha sido VALIDADA con éxito.", "Validación de Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                actualizarInterfaz();
            }
            else
            {
                MessageBox.Show("Seleccione o registre una transacción para validar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Navegación bidireccional (Punteros Siguiente / Anterior)
        private void btnAdelante_Click(object sender, EventArgs e)
        {
            if (nodoActual == null && lista.cabeza != null)
            {
                nodoActual = lista.cabeza;
            }
            else if (nodoActual != null && nodoActual.Siguiente != null)
            {
                nodoActual = nodoActual.Siguiente;
            }

            actualizarInterfaz();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            if (nodoActual == null && lista.cabeza != null)
            {
                nodoActual = lista.cabeza;
            }
            else if (nodoActual != null && nodoActual.Anterior != null)
            {
                nodoActual = nodoActual.Anterior;
            }

            actualizarInterfaz();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lista.limpiar();
            nodoActual = null;
            actualizarInterfaz();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (cbxTipo.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un tipo de transacción (QR o Tarjeta).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tipoPago = cbxTipo.SelectedItem.ToString();

            // Insertar en la lista doble pasando el tipo de transacción seleccionado
            bool resp = lista.agregarAlInicio(tipoPago);

            if (resp)
            {
                nodoActual = lista.cabeza; // Establece el puntero visual en el nuevo nodo
    

                // Si estás usando la opción B (una lista dinámica global en C# tipo List<T>):
                SesionGlobal.HistorialTransaccionesGlobal.Add(nodoActual);
                MessageBox.Show($"Transacción {nodoActual.Codigo} registrada correctamente por S/{SesionGlobal.CostoTotalActual}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            actualizarInterfaz();
        }

        private void btnCarrito_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsimulador_Click(object sender, EventArgs e)
        {
            FormGestionFalloReconexion formGestion = new FormGestionFalloReconexion();
            this.Hide();
            formGestion.ShowDialog();
            this.Show();
        }

        private void btnhistorial_Click(object sender, EventArgs e)
        {
            FormModuloVouchers formModuloVouchers = new FormModuloVouchers();
            this.Hide();
            formModuloVouchers.ShowDialog();
            this.Show();

        }
    }
}