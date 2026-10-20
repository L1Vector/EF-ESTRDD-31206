using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using semana7Stack.Modelos;
using semana7Stack.Vistas;

namespace semana7Stack
{
    public partial class FormPOS : Form
    {
        // Pilas para almacenar los datos de cada elemento del carrito
        private Stack<int> miPila = new Stack<int>();     // Códigos de producto
        private Stack<int> Cantidades = new Stack<int>(); // Cantidades seleccionadas
        private Stack<int> Costo = new Stack<int>();      // Subtotales (Precio Unitario * Cantidad)

        public FormPOS()
        {
            InitializeComponent();

            // Configuración inicial del NumericUpDown (numCantidad)
            numCantidad.Minimum = 1;
            numCantidad.Maximum = 100;
            numCantidad.Value = 1;

            actualizarInterfaz();
        }

        // Función para actualizar la interfaz de usuario
        private void actualizarInterfaz()
        {
            lstVisualizacion.Items.Clear();
            if (miPila.Count == 0)
            {
                lstVisualizacion.Items.Add("Carrito vacío");
                lblInfo.Text = "Productos: 0 | Último agregado: Ninguno | Costo total: S/0";
                return;
            }

            // Convertir las pilas a arreglos para iterar en el orden de inserción
            int[] elementos = miPila.ToArray();
            int[] cantidades = Cantidades.ToArray();
            int[] costos = Costo.ToArray();

            // Mostrar el detalle en el ListBox: [Código] - xCantidad - S/Subtotal
            for (int i = 0; i < elementos.Length; i++)
            {
                int precioUnitario = costos[i] / cantidades[i];
                lstVisualizacion.Items.Add($"[{elementos[i]}]  -  x{cantidades[i]} un. (S/{precioUnitario} c/u)  -  Subtotal: S/{costos[i]}");
            }

            // Actualizar la etiqueta con la información del carrito
            lblInfo.Text = $"Productos: {miPila.Count} | Último agregado: [{miPila.Peek()}] | Costo total: S/{Costo.Sum()}";
        }

        // Función para obtener código, precio unitario y calcular el costo total
        private bool obtenerValor(out int código, out int cantidad, out int costoSubtotal)
        {
            código = 0;
            cantidad = (int)numCantidad.Value;
            costoSubtotal = 0;

            if (string.IsNullOrWhiteSpace(txtValor.Text) || string.IsNullOrWhiteSpace(txtCosto.Text))
            {
                MessageBox.Show("Por favor, ingrese el código de producto y el precio unitario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!int.TryParse(txtValor.Text, out código))
            {
                MessageBox.Show("Código de producto inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!int.TryParse(txtCosto.Text, out int precioUnitario))
            {
                MessageBox.Show("Precio unitario inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Cálculo: Precio Unitario x Cantidad
            costoSubtotal = precioUnitario * cantidad;
            return true;
        }

        // Evento del botón "Agregar Productos" (Push)
        private void btnPush_Click(object sender, EventArgs e)
        {
            if (obtenerValor(out int valor, out int cantidad, out int costoSubtotal))
            {
                // Verificar si el código de producto ya existe en la pila
                if (miPila.Contains(valor))
                {
                    MessageBox.Show("El código de producto ya existe en el carrito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Push en las tres pilas
                miPila.Push(valor);
                Cantidades.Push(cantidad);
                Costo.Push(costoSubtotal);

                // Actualizar la interfaz y resetear los campos
                actualizarInterfaz();
                txtValor.Clear();
                txtCosto.Clear();
                numCantidad.Value = 1;
                txtValor.Focus();
            }
        }

        // Evento del botón "Deshacer" (Pop)
        private void btnPop_Click(object sender, EventArgs e)
        {
            if (miPila.Count > 0)
            {
                int codigo = miPila.Pop();
                int cantidad = Cantidades.Pop();
                int costoSubtotal = Costo.Pop();

                MessageBox.Show($"Se eliminó el código: {codigo} | Cantidad: {cantidad} | Subtotal: S/{costoSubtotal}", "Deshacer (Pop)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                actualizarInterfaz();
            }
            else
            {
                MessageBox.Show("El carrito está vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón "Ver último agregado" (Peek)
        private void btnPeek_Click(object sender, EventArgs e)
        {
            if (miPila.Count > 0)
            {
                int codigo = miPila.Peek();
                int cantidad = Cantidades.Peek();
                int costoSubtotal = Costo.Peek();

                MessageBox.Show($"Último agregado -> Código: {codigo} | Cantidad: {cantidad} | Subtotal: S/{costoSubtotal}", "Cima de la Pila (Peek)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                actualizarInterfaz();
            }
            else
            {
                MessageBox.Show("El carrito está vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón "Vaciar Carrito" (Limpiar)
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            miPila.Clear();
            Cantidades.Clear();
            Costo.Clear();
            MessageBox.Show("Carrito vaciado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            actualizarInterfaz();
        }

        // Evento del botón "Buscar Artículo" (Contains)
        private void btnContains_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtValor.Text, out int codigo))
            {
                if (miPila.Contains(codigo))
                {
                    MessageBox.Show($"El código de producto {codigo} se encuentra en el carrito", "Buscar Artículo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"El código de producto {codigo} NO se encuentra en el carrito", "Buscar Artículo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Por favor, ingrese un código de producto para verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Código de producto inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón "Pagar"
        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (miPila.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Agregue productos antes de pagar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calcular el costo total y almacenarlo en la clase SesionGlobal
            int costoTotal = Costo.Sum();
            SesionGlobal.CostoTotalActual = costoTotal;

            MessageBox.Show($"Monto total a pagar: S/{costoTotal}.", "Proceso de Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);

            FormPago formPago = new FormPago();
            this.Hide();
            formPago.ShowDialog();
            this.Show();
        }
    }
}