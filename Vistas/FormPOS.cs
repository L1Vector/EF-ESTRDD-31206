namespace semana7Stack
{
    public partial class FormPOS : Form
    {
        private Stack<int> miPila = new Stack<int>();
        private Stack<int> costo = new Stack<int>();
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
            bool esTope = true;
            foreach (int elemento in miPila)
            {
                if (esTope)
                {
                    lstVisualizacion.Items.Add($"[{elemento}]  -  S/{costo.Peek()}");
                    esTope = false;
                }
                else
                {
                    lstVisualizacion.Items.Add($"[{elemento}]  -  S/{costo.Peek()}");
                }
            }
            lblInfo.Text = $"Productos: {miPila.Count} | Último agregado: {miPila.Peek()} | Costo total: S/{costo.Sum()}";
        }
        private bool obtenerValor(out int código, out int costo)
        {
            if(string.IsNullOrWhiteSpace(txtValor.Text) || string.IsNullOrWhiteSpace(txtCosto.Text))
            {
                MessageBox.Show("Por favor, ingrese un código de producto y un costo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                código = 0;
                costo = 0;
                return false;
            }
            else if(!int.TryParse(txtValor.Text, out código))
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
                miPila.Push(valor);
                this.costo.Push(costo);
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
                int costoProducto = this.costo.Pop();
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
                int costoProducto = this.costo.Peek();
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
            costo.Clear();
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
        }
    }
}
