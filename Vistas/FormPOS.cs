using semana7Stack.Modelos;
using semana7Stack.Vistas;

namespace semana7Stack
{
    public partial class FormPOS : Form
    {
        private Stack<int> miPila = new Stack<int>();
        private Stack<int> Costo = new Stack<int>();
        public FormPOS()
        {
            InitializeComponent();
            actualizarInterfaz();
        }

        private void actualizarInterfaz()
        {
            lstVisualizacion.Items.Clear();
            if (miPila.Count == 0)
            {
                lstVisualizacion.Items.Add("Carrito vacío");
                lblInfo.Text = "Productos: 0 | Ultimo agregado: Ninguno | Costo total: S/0";
                return;
            }

            // Convertir las pilas a arreglos para poder iterar sobre ellos
            int[] elementos = miPila.ToArray();
            int[] costos = Costo.ToArray();

            // Mostrar los elementos de la pila y sus costos en el ListBox
            for (int i = 0; i < elementos.Length; i++)
            {
                lstVisualizacion.Items.Add($"[{elementos[i]}]  -  S/{costos[i]}");
            }

            // Actualizar la etiqueta con la información del carrito
            lblInfo.Text = $"Productos: {miPila.Count} | Último agregado: {miPila.Peek()} | Costo total: S/{Costo.Sum()}";
        }
        private bool obtenerValor(out int código, out int costo)
        {
            if (string.IsNullOrWhiteSpace(txtValor.Text) || string.IsNullOrWhiteSpace(txtCosto.Text))
            {
                MessageBox.Show("Por favor, ingrese un código de producto y un costo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                código = 0;
                costo = 0;
                return false;
            }
            else if (!int.TryParse(txtValor.Text, out código))
            {
                MessageBox.Show("Código de producto inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                costo = 0;
                return false;
            }
            else if (!int.TryParse(txtCosto.Text, out costo))
            {
                MessageBox.Show("Costo de producto inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                código = 0;
                return false;
            }
            return true;
        }
        private void btnPush_Click(object sender, EventArgs e)
        {
            if (obtenerValor(out int valor, out int costo))
            {
                // Verificar si el código de producto ya existe en la pila
                if (miPila.Contains(valor))
                {
                    MessageBox.Show("El código de producto ya existe en el carrito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Agregar el código de producto y su costo a las pilas
                miPila.Push(valor);
                Costo.Push(costo);

                // Actualizar la interfaz y limpiar los campos de texto
                actualizarInterfaz();
                txtValor.Clear();
                txtValor.Focus();
                txtCosto.Clear();
                txtCosto.Focus();
            }
        }

        private void btnPop_Click(object sender, EventArgs e)
        {
            if (miPila.Count > 0)
            {
                int codigo = miPila.Pop();
                int costoProducto = Costo.Pop();
                MessageBox.Show($"Se eliminó el código de producto: {codigo} con costo: S/{costoProducto}", "Pop", MessageBoxButtons.OK, MessageBoxIcon.Information);
                actualizarInterfaz();
            }
            else
            {
                MessageBox.Show("El carrito está vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPeek_Click(object sender, EventArgs e)
        {
            if (miPila.Count > 0)
            {
                int codigo = miPila.Peek();
                int costoProducto = Costo.Peek();
                MessageBox.Show($"El código de producto en la cima es: {codigo} con costo: S/{costoProducto}", "Peek", MessageBoxButtons.OK, MessageBoxIcon.Information);
                actualizarInterfaz();
            }
            else
            {
                MessageBox.Show("El carrito está vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            miPila.Clear();
            Costo.Clear();
            MessageBox.Show("Carrito vaciado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            actualizarInterfaz();
        }

        private void btnContains_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtValor.Text, out int codigo))
            {
                if (miPila.Contains(codigo))
                {
                    MessageBox.Show($"El código de producto {codigo} está en el carrito", "Contains", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"El código de producto {codigo} NO está en el carrito", "Contains", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if(string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Por favor, ingrese un código de producto para verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Código de producto inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (miPila.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Agregue productos antes de pagar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Calcular el costo total sumando los elementos de la pila de costos
            int costoTotal = Costo.Sum();

            // 3. Guardar el dato en la variable global para que la siguiente fase lo lea
            SesionGlobal.CostoTotalActual = costoTotal;

            MessageBox.Show($"Monto total a pagar: S/{costoTotal}.",
                    "Proceso de Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 4. Abrir la siguiente fase (FormPago) y ocultar el carrito actual
            FormPago formPago = new FormPago();
            this.Hide();
            formPago.ShowDialog();
            this.Close();
        }
    }
}
