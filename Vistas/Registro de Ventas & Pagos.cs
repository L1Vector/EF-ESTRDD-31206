using semana7Stack.ModuloCarrito;
using semana7Stack.ConexionDatos;
using semana7Stack.CoordinacionModulos;
using semana7Stack.ModuloGrafos.Estructura;
using semana7Stack.ModuloHistorialPagos.Modelo;
using semana7Stack.ModuloSimulacionConectivad.Modelo;
using semana7Stack.ModuloVentas.Estructura;
using semana7Stack.ModuloGrafos.Algoritmo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace semana7Stack.Vistas
{
    public partial class Registro_de_Ventas___Pagos : Form
    {
        // ========================================================
        // CONFIGURACIÓN Y ESTADO GENERAL DEL FORMULARIO
        // ========================================================
        private NodoTransaccion nodoActual;
        private List<Voucher> vouchersEncontrados = new List<Voucher>();
        private int indiceVoucher;

        private System.Windows.Forms.Timer temporizadorAtencion = new System.Windows.Forms.Timer();

        private bool procesamientoIniciado;
        private bool redSimulada = true;
        private bool servidorSimulado = true;
        private int siguienteId = 1;

        private FormGrafoConectividad ventanaGrafo;
        private Transaccion transaccionEnProceso;

        private string ultimoCodigoOperacion = string.Empty;
        private string ultimoEstadoOperacion = string.Empty;
        private string ultimaPasarela = string.Empty;

        // Inicializa los controles, gestores, temporizador y estados iniciales.
        public Registro_de_Ventas___Pagos()
        {
            InitializeComponent();

            if (cmbTipo.Items.Count > 0 && cmbTipo.SelectedIndex < 0)
                cmbTipo.SelectedIndex = 0;

            temporizadorAtencion.Interval = 2000;
            temporizadorAtencion.Tick += temporizadorAtencion_Tick;

            numCantidadProducto.Minimum = 1;
            numCantidadProducto.Maximum = 100;
            numCantidadProducto.Value = 1;
            tabPagos.Enabled = false;
            lstPendientes.HorizontalScrollbar = true;
            lstHistorial.HorizontalScrollbar = true;
            lstHistorial1.HorizontalScrollbar = true;
            lstVoucher.HorizontalScrollbar = true;
            lstLista.HorizontalScrollbar = true;
            lstListaCarrito.HorizontalScrollbar = true;

            if (cmbPago.Items.Count == 0)
            {
                cmbPago.Items.Add("Pago con QR");
                cmbPago.Items.Add("Tarjeta de Crédito/Débito");
                cmbPago.SelectedIndex = 0;
            }

            lblEstadoProceso.Text = "Estado: en espera";
            this.Load += Registro_de_Ventas___Pagos_Load;

            actualizarInterfaz();
            actualizarEstado();
            actualizarPendientes();
            actualizarInterfazPago();
        }

        // Carga el historial global y actualiza las listas iniciales del formulario.
        private void Registro_de_Ventas___Pagos_Load(object sender, EventArgs e)
        {
            GestorCentral.cola.CargarDesdeHistorialGlobal(ref siguienteId);
            CargarTransaccionGlobal();

            lblActual2.Text = "0 de 0 vouchers";
            btnAnterior.Enabled = false;
            btnSiguiente.Enabled = false;

            actualizarPendientes();
            ActualizarListaHistorial1();
        }

        // ========================================================
        // MÓDULO 1: CARRITO DE COMPRAS MEDIANTE PILAS
        // ========================================================

        // Muestra los productos del carrito y calcula su costo total.
        private void actualizarInterfaz()
        {
            lstListaCarrito.Items.Clear();

            if (PilaCarritoStack.miPila.Count == 0)
            {
                lstListaCarrito.Items.Add("Carrito vacío");
                lblInfoCarrito.Text = "Productos: 0 | Último agregado: Ninguno | Costo total: S/0";
                return;
            }

            int[] elementos = PilaCarritoStack.miPila.ToArray();
            int[] cantidades = PilaCarritoStack.Cantidades.ToArray();
            int[] costos = PilaCarritoStack.Costo.ToArray();

            for (int i = 0; i < elementos.Length; i++)
            {
                int precioUnitario = cantidades[i] == 0 ? 0 : costos[i] / cantidades[i];
                lstListaCarrito.Items.Add(
                    $"[{elementos[i]}] - x{cantidades[i]} un. (S/{precioUnitario} c/u) - Subtotal: S/{costos[i]}");
            }

            lblInfoCarrito.Text =
                $"Productos: {PilaCarritoStack.miPila.Count} | Último agregado: [{PilaCarritoStack.miPila.Peek()}] | Costo total: S/{PilaCarritoStack.Costo.Sum()}";
        }

        // Valida los datos ingresados y obtiene el código, cantidad y subtotal.
        private bool obtenerValor(out int codigo, out int cantidad, out int costoSubtotal)
        {
            codigo = 0;
            cantidad = (int)numCantidadProducto.Value;
            costoSubtotal = 0;

            if (string.IsNullOrWhiteSpace(txtCodigoProducto.Text) || string.IsNullOrWhiteSpace(txtCostoProducto.Text))
            {
                MessageBox.Show(
                    "Por favor, ingrese el código de producto y el precio unitario.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(txtCodigoProducto.Text.Trim(), out codigo))
            {
                MessageBox.Show("Código de producto inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(txtCostoProducto.Text.Trim(), out int precioUnitario) || precioUnitario < 0)
            {
                MessageBox.Show("Precio unitario inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            costoSubtotal = precioUnitario * cantidad;
            return true;
        }

        // Valida y agrega un producto nuevo a las pilas del carrito.
        private void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            if (!obtenerValor(out int codigo, out int cantidad, out int costoSubtotal))
                return;

            if (PilaCarritoStack.miPila.Contains(codigo))
            {
                MessageBox.Show("El código de producto ya existe en el carrito.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PilaCarritoStack.miPila.Push(codigo);
            PilaCarritoStack.Cantidades.Push(cantidad);
            PilaCarritoStack.Costo.Push(costoSubtotal);
            actualizarInterfaz();

            txtCodigoProducto.Clear();
            txtCostoProducto.Clear();
            numCantidadProducto.Value = 1;
            txtCodigoProducto.Focus();
        }

        // Retira el último producto agregado al carrito mediante Pop.
        private void btnDeshacerCarrito_Click(object sender, EventArgs e)
        {
            if (PilaCarritoStack.miPila.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int codigo = PilaCarritoStack.miPila.Pop();
            int cantidad = PilaCarritoStack.Cantidades.Pop();
            int costoSubtotal = PilaCarritoStack.Costo.Pop();

            MessageBox.Show(
                $"Se eliminó el código: {codigo} | Cantidad: {cantidad} | Subtotal: S/{costoSubtotal}",
                "Deshacer (Pop)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            actualizarInterfaz();
        }

        // Muestra el último producto agregado sin retirarlo de la pila.
        private void btnUltimoCarrito_Click(object sender, EventArgs e)
        {
            if (PilaCarritoStack.miPila.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                $"Último agregado -> Código: {PilaCarritoStack.miPila.Peek()} | Cantidad: {PilaCarritoStack.Cantidades.Peek()} | Subtotal: S/{PilaCarritoStack.Costo.Peek()}",
                "Cima de la Pila (Peek)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Vacía las pilas del carrito y actualiza su presentación.
        private void btnVaciarCarrito_Click(object sender, EventArgs e)
        {
            PilaCarritoStack.miPila.Clear();
            PilaCarritoStack.Cantidades.Clear();
            PilaCarritoStack.Costo.Clear();

            MessageBox.Show("Carrito vaciado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            actualizarInterfaz();
        }

        // Calcula el total del carrito y habilita la sección de pagos.
        private void btnPagarCarrito_Click(object sender, EventArgs e)
        {
            if (PilaCarritoStack.miPila.Count == 0)
            {
                MessageBox.Show(
                    "El carrito está vacío. Agregue productos antes de pagar.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int costoTotal = PilaCarritoStack.Costo.Sum();
            SesionGlobal.CostoTotalActual = costoTotal;

            MessageBox.Show(
                $"Monto total a pagar: S/{costoTotal}.",
                "Proceso de pago",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            tabPagos.Enabled = true;
            tabControl1.SelectedTab = tabPagos;
        }

        // ========================================================
        // MÓDULO 2: REGISTRO Y VALIDACIÓN DE PAGOS
        // ========================================================

        // Muestra las transacciones registradas en la lista doble de ventas.
        private void actualizarInterfazPago()
        {
            lstLista.Items.Clear();
            lblCosto.Text = $"Costo: S/ {SesionGlobal.CostoTotalActual:N2}";
            NodoTransaccion actual = GestorCentral.ventas.cabeza;

            if (actual == null)
            {
                lstLista.Items.Add("No hay registros de pago.");
                nodoActual = null;
                return;
            }

            while (actual != null)
            {
                string textoNodo = $"[{actual.Codigo}] - {actual.Tipo} - S/{actual.Pago} ({actual.Estado})";
                lstLista.Items.Add(actual == nodoActual ? $"-> {textoNodo}" : $"   {textoNodo}");
                actual = actual.Siguiente;
            }
        }

        // Avanza al siguiente registro de la lista doble.
        private void btnAdelantePagos_Click(object sender, EventArgs e)
        {
            if (nodoActual == null && GestorCentral.ventas.cabeza != null)
                nodoActual = GestorCentral.ventas.cabeza;
            else if (nodoActual != null && nodoActual.Siguiente != null)
                nodoActual = nodoActual.Siguiente;

            actualizarInterfazPago();
        }

        // Retrocede al registro anterior de la lista doble.
        private void btnAtrasPAgos_Click(object sender, EventArgs e)
        {
            if (nodoActual == null && GestorCentral.ventas.cabeza != null)
                nodoActual = GestorCentral.ventas.cabeza;
            else if (nodoActual != null && nodoActual.Anterior != null)
                nodoActual = nodoActual.Anterior;

            actualizarInterfazPago();
        }


        // Elimina todos los registros de la lista de ventas.
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            GestorCentral.ventas.limpiar();
            nodoActual = null;
            actualizarInterfazPago();
        }


        // ========================================================
        // MÓDULO 2: GENERACIÓN Y VALIDACIÓN DE PAGOS (QR Y TARJETA)
        // ========================================================

        // Registra el pago seleccionado y procesa la respuesta del QR o la tarjeta.
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (cmbPago.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un tipo de transacción.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tipoPago = cmbPago.SelectedItem.ToString().Trim();
            bool esTarjeta = tipoPago.IndexOf("Tarjeta", StringComparison.OrdinalIgnoreCase) >= 0 || cmbPago.SelectedIndex == 1;
            bool registrado = GestorCentral.ventas.agregarAlInicio(tipoPago);

            if (!registrado)
                return;

            nodoActual = GestorCentral.ventas.cabeza;

            if (nodoActual == null)
                return;

            if (esTarjeta)
            {
                MessageBox.Show("Inserte su tarjeta.", "Terminal POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (redSimulada && servidorSimulado)
                {
                    nodoActual.Estado = "APROBADO";
                    ActualizarGraficoOperacion(nodoActual.Codigo, "APROBADO", "A", "Pago aprobado inicialmente por Pasarela A.");
                    GuardarTransaccionGlobal(nodoActual);

                    MessageBox.Show(
                        $"Transacción {nodoActual.Codigo} aprobada por S/{nodoActual.Pago}.",
                        "Pago exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    AgregarPagoPendiente(nodoActual);
                    GuardarTransaccionGlobal(nodoActual);

                    MessageBox.Show(
                        $"Transacción {nodoActual.Codigo} quedó pendiente por fallo de conexión.",
                        "Pago pendiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else if (!redSimulada || !servidorSimulado)
            {
                AgregarPagoPendiente(nodoActual);
                GuardarTransaccionGlobal(nodoActual);

                MessageBox.Show(
                    $"No se pudo validar el QR. La operación {nodoActual.Codigo} quedó pendiente.",
                    "Pago pendiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                using (MiniQR formularioQR = new MiniQR(SesionGlobal.CostoTotalActual))
                {
                    formularioQR.ShowDialog(this);
                    string respuesta = formularioQR.ResultadoOperacion;

                    if (respuesta == "EXITO")
                    {
                        nodoActual.Estado = "APROBADO";
                        GuardarTransaccionGlobal(nodoActual);
                        ActualizarGraficoOperacion(nodoActual.Codigo, "APROBADO", "A", "Pago aprobado correctamente.");
                        tabControl1.SelectedIndex = 3;
                    }
                    else if (respuesta == "EXPIRADO")
                    {
                        nodoActual.Estado = "CANCELADO";
                        ActualizarGraficoOperacion(
                            nodoActual.Codigo,
                            "CANCELADO",
                            "A",
                            "El tiempo de 10 segundos del QR terminó. La venta fue cancelada.");
                        GuardarTransaccionGlobal(nodoActual);

                        MessageBox.Show(
                            $"El tiempo del QR terminó. La operación {nodoActual.Codigo} fue cancelada.",
                            "Pago cancelado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else if (respuesta == "PENDIENTE_RED")
                    {
                        SimularFalloRed();
                        AgregarPagoPendiente(nodoActual);
                        GuardarTransaccionGlobal(nodoActual);
                        ActualizarGraficoOperacion(nodoActual.Codigo, "PENDIENTE", "A", "Pago pendiente por caída de red.");
                        tabControl1.SelectedIndex = 4;

                        MessageBox.Show(
                            $"La operación {nodoActual.Codigo} quedó pendiente por caída de red.",
                            "Pago pendiente",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        nodoActual.Estado = "CANCELADO";
                        ActualizarGraficoOperacion(nodoActual.Codigo, "CANCELADO", "A", "La operación fue cancelada.");
                        GuardarTransaccionGlobal(nodoActual);

                        MessageBox.Show("El pago fue cancelado.", "Pago cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            RegistrarVoucher(nodoActual);
            actualizarInterfazPago();
            actualizarPendientes();
        }

        // Guarda una transacción en el historial global si todavía no existe.
        private void GuardarTransaccionGlobal(NodoTransaccion nodo)
        {
            if (nodo == null || SesionGlobal.HistorialTransaccionesGlobal == null)
                return;

            bool existe = SesionGlobal.HistorialTransaccionesGlobal.Any(x =>
                x != null && string.Equals(x.Codigo, nodo.Codigo, StringComparison.OrdinalIgnoreCase));

            if (!existe)
                SesionGlobal.HistorialTransaccionesGlobal.Add(nodo);
        }


        // ========================================================
        // MÓDULO 3: HISTORIAL Y BÚSQUEDA DE VOUCHERS
        // ========================================================

        // Incorpora al historial los vouchers de las transacciones globales que falten.
        private void CargarTransaccionGlobal()
        {
            if (SesionGlobal.HistorialTransaccionesGlobal == null)
                return;

            List<Voucher> vouchersActuales = GestorCentral.historial.ObtenerTodos().ToList();

            foreach (NodoTransaccion trans in SesionGlobal.HistorialTransaccionesGlobal)
            {
                if (trans == null)
                    continue;

                bool yaExiste = vouchersActuales.Any(v =>
                    string.Equals(Convert.ToString(v.IdOperacion), trans.Codigo, StringComparison.OrdinalIgnoreCase));

                if (!yaExiste)
                {
                    Voucher nuevoVoucher = new Voucher(trans.Codigo, trans.Tipo, trans.Pago, trans.Estado);
                    GestorCentral.historial.Agregar(nuevoVoucher);
                    vouchersActuales.Add(nuevoVoucher);
                }
            }
        }

        // Busca vouchers cuyo código coincida con el texto ingresado.
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Ingresa el código del voucher.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return;
            }

            vouchersEncontrados = GestorCentral.historial.ObtenerTodos()
                .Where(v => Convert.ToString(v.IdOperacion).IndexOf(codigo, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            if (vouchersEncontrados.Count == 0)
            {
                lstVoucher.Items.Clear();
                lblActual2.Text = "0 de 0 vouchers";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;

                MessageBox.Show(
                    "No se encontró ningún voucher con ese código.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            indiceVoucher = 0;
            MostrarVoucherEncontrado();
        }

        // Muestra en pantalla el voucher correspondiente al índice actual.
        private void MostrarVoucherEncontrado()
        {
            lstVoucher.Items.Clear();

            if (vouchersEncontrados == null || vouchersEncontrados.Count == 0)
            {
                lblActual2.Text = "0 de 0 vouchers";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
                return;
            }

            if (indiceVoucher < 0)
                indiceVoucher = 0;

            if (indiceVoucher >= vouchersEncontrados.Count)
                indiceVoucher = vouchersEncontrados.Count - 1;

            Voucher voucher = vouchersEncontrados[indiceVoucher];
            lstVoucher.Items.Add("Código: " + voucher.IdOperacion);
            lstVoucher.Items.Add("Tipo: " + voucher.TipoOperacion);
            lstVoucher.Items.Add("Importe: S/ " + voucher.Monto.ToString("N2"));
            lstVoucher.Items.Add("Fecha: " + voucher.Timestamp);
            lstVoucher.Items.Add("Estado: " + voucher.Estado);

            lblActual2.Text = $"{indiceVoucher + 1} de {vouchersEncontrados.Count} vouchers";
            btnAnterior.Enabled = indiceVoucher > 0;
            btnSiguiente.Enabled = indiceVoucher < vouchersEncontrados.Count - 1;
        }

        // Avanza al siguiente voucher encontrado.
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (indiceVoucher < vouchersEncontrados.Count - 1)
            {
                indiceVoucher++;
                MostrarVoucherEncontrado();
            }
        }

        // Retrocede al voucher anterior encontrado.
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (indiceVoucher > 0)
            {
                indiceVoucher--;
                MostrarVoucherEncontrado();
            }
        }

        // Agrega un voucher nuevo o actualiza el estado de uno existente.
        private void RegistrarVoucher(NodoTransaccion nodo)
        {
            if (nodo == null)
                return;

            Voucher existente = GestorCentral.historial.ObtenerTodos().FirstOrDefault(v =>
                string.Equals(Convert.ToString(v.IdOperacion), nodo.Codigo, StringComparison.OrdinalIgnoreCase));

            if (existente != null)
            {
                existente.Estado = nodo.Estado;
            }
            else
            {
                Voucher nuevo = new Voucher(nodo.Codigo, nodo.Tipo, nodo.Pago, nodo.Estado);
                GestorCentral.historial.Agregar(nuevo);
            }

            ActualizarHistorialVouchers();
        }

        // Actualiza la lista principal con todos los vouchers almacenados.
        private void ActualizarListaHistorial1()
        {
            lstHistorial1.Items.Clear();

            foreach (Voucher voucher in GestorCentral.historial.ObtenerTodos())
            {
                lstHistorial1.Items.Add(
                    $"{voucher.IdOperacion} | {voucher.TipoOperacion} | S/. {voucher.Monto:N2} | {voucher.Estado} | {voucher.Timestamp:dd/MM/yyyy HH:mm:ss}");
            }
        }

        // Refresca las listas y reinicia la navegación del historial de vouchers.
        private void ActualizarHistorialVouchers()
        {
            ActualizarListaHistorial1();
            vouchersEncontrados = GestorCentral.historial.ObtenerTodos().ToList();

            if (vouchersEncontrados.Count == 0)
            {
                lstVoucher.Items.Clear();
                lblActual2.Text = "0 de 0 vouchers";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
                return;
            }

            indiceVoucher = 0;
            MostrarVoucherEncontrado();
        }

        // ========================================================
        // MÓDULO 4: COLA DE PRIORIDAD Y OPERACIONES PENDIENTES
        // ========================================================

        // Muestra las operaciones pendientes y su prioridad en la cola.
        private void actualizarPendientes()
        {
            lstPendientes.Items.Clear();
            List<Transaccion> pendientes = GestorCentral.cola.ObtenerPendientesOrdenados();

            foreach (Transaccion item in pendientes)
            {
                string estadoMostrar = item.Estado;

                if (item.Estado == "PROCESANDO")
                    estadoMostrar = "PROCESANDO POR " + item.Pasarela;

                lstPendientes.Items.Add(
                    $"ID: {item.Id} | {item.Tipo} | Prioridad: {obtenerPrioridad(item)} | S/ {item.Importe:0.00} | {estadoMostrar}");
            }

            if (pendientes.Count == 0)
                lstPendientes.Items.Add("No hay operaciones pendientes.");

            lblTotalPendientes.Text = "Elementos en la cola: " + GestorCentral.cola.CantidadPendientes();
        }

        // Devuelve el nivel de prioridad según el tipo de transacción.
        private string obtenerPrioridad(Transaccion transaccion)
        {
            if (transaccion.Tipo == "Extorno")
                return "Alta";

            if (transaccion.Tipo == "Reversion" || transaccion.Tipo == "Reversión")
                return "Media";

            return "Normal";
        }

        // Agrega una transacción a la cola cuando no hay conectividad disponible.
        private void AgregarPagoPendiente(NodoTransaccion nodo)
        {
            if (nodo == null)
                return;

            nodo.Estado = "PENDIENTE";
            Transaccion nueva = new Transaccion(siguienteId, Convert.ToDecimal(nodo.Pago), "Cobro", nodo.Codigo);
            siguienteId++;
            GestorCentral.cola.Agregar(nueva);

            ultimoCodigoOperacion = nodo.Codigo;
            ultimoEstadoOperacion = "PENDIENTE";
            ultimaPasarela = "A";

            lstHistorial.Items.Add(
                DateTime.Now.ToString("HH:mm:ss") +
                " | Operación " + nodo.Codigo +
                " | Conectividad no disponible" +
                " | Enviada a la cola de pagos pendientes.");

            if (ventanaGrafo != null && !ventanaGrafo.IsDisposed)
            {
                ventanaGrafo.MostrarResultado(
                    nodo.Codigo,
                    "PENDIENTE",
                    "A",
                    "Pago pendiente. Esperando recuperación y procesamiento manual.");
            }

            actualizarPendientes();
            actualizarInterfazPago();
        }

        // ========================================================
        // MÓDULO 4: PROCESAMIENTO DE OPERACIONES PENDIENTES
        // ========================================================

        // Atiende cada operación pendiente y comprueba la disponibilidad de su pasarela.
        private void temporizadorAtencion_Tick(object sender, EventArgs e)
        {
            actualizarEstado();

            if (!GestorCentral.cola.RedDisponible || !GestorCentral.cola.ServidorDisponible)
            {
                detenerProcesamiento("Conexión perdida. Las operaciones siguen pendientes.");
                return;
            }

            // PRIMER TICK: selecciona una operación y comienza su procesamiento.
            if (transaccionEnProceso == null)
            {
                List<Transaccion> pendientes = GestorCentral.cola.ObtenerPendientesOrdenados();

                if (pendientes.Count == 0)
                {
                    FinalizarProcesamiento();
                    return;
                }

                // Selecciona la primera pasarela disponible de la ruta óptima.
                string nombrePasarela = ObtenerPasarelaDisponible();

                if (string.IsNullOrWhiteSpace(nombrePasarela))
                {
                    detenerProcesamiento(
                        "No existe una ruta disponible hacia el banco. " +
                        "Los pagos siguen pendientes.");
                    return;
                }

                transaccionEnProceso = pendientes[0];
                transaccionEnProceso.Pasarela = nombrePasarela;
                transaccionEnProceso.Estado = "PROCESANDO";
                ActualizarOperacionOriginal(transaccionEnProceso.CodigoOperacion, "PROCESANDO");

                ActualizarGraficoOperacion(transaccionEnProceso.CodigoOperacion,"PROCESANDO",nombrePasarela,nombrePasarela + " está procesando el pago.");

                lblEstadoProceso.Text ="Estado: " + nombrePasarela + " procesando " + transaccionEnProceso.CodigoOperacion;

                lstHistorial.Items.Add(
                    DateTime.Now.ToString("HH:mm:ss") +
                    " | Operación " + transaccionEnProceso.CodigoOperacion +
                    " | Procesando por " + nombrePasarela);

                actualizarPendientes();
                return;
            }

            // SEGUNDO TICK: verifica la pasarela y confirma la operación.
            Transaccion actual = transaccionEnProceso;
            Vertice pasarelaActual = GestorCentral.grafo.Vertices.FirstOrDefault(v =>v.Nombre.Equals(actual.Pasarela, StringComparison.OrdinalIgnoreCase));

            if (pasarelaActual == null || !pasarelaActual.Estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
            {
                detenerProcesamiento(
                    "La pasarela " + actual.Pasarela +
                    " dejó de estar disponible. El pago sigue pendiente.");
                return;
            }

            if (!GestorCentral.cola.IntentarObtenerSiguiente(out Transaccion retirada))
            {
                detenerProcesamiento("No se pudo retirar la operación de la cola.");
                return;
            }

            if (!string.Equals(retirada.CodigoOperacion, actual.CodigoOperacion, StringComparison.OrdinalIgnoreCase))
            {
                GestorCentral.cola.Agregar(retirada);
                transaccionEnProceso = null;
                detenerProcesamiento("El orden de la cola cambió. La operación se conservó como pendiente.");
                return;
            }

            actual.Intentos++;
            actual.Estado = "APROBADO";
            ActualizarOperacionOriginal(actual.CodigoOperacion, "APROBADO");

            ActualizarGraficoOperacion(
                actual.CodigoOperacion,
                "APROBADO",
                actual.Pasarela,
                "Pago aprobado mediante " + actual.Pasarela + ".");

            lstHistorial.Items.Add(
                DateTime.Now.ToString("HH:mm:ss") +
                " | Operación " + actual.CodigoOperacion +
                " | " + actual.Pasarela +
                " | Intento: " + actual.Intentos +
                " | S/ " + actual.Importe.ToString("0.00") +
                " | APROBADO");

            transaccionEnProceso = null;
            actualizarPendientes();

            if (GestorCentral.cola.CantidadPendientes() == 0)
            {
                FinalizarProcesamiento();
            }
            else
            {
                lblEstadoProceso.Text =
                    "Estado: operación aprobada por " + actual.Pasarela + ". Continuando con la cola.";
            }
        }

        // Detiene el temporizador y devuelve a pendiente la operación en curso.
        private void detenerProcesamiento(string mensaje)
        {
            temporizadorAtencion.Stop();

            if (transaccionEnProceso != null)
            {
                transaccionEnProceso.Estado = "PENDIENTE";
                ActualizarOperacionOriginal(transaccionEnProceso.CodigoOperacion, "PENDIENTE");
                transaccionEnProceso = null;
            }

            procesamientoIniciado = false;
            btnProcesar.Enabled = true;
            lblEstadoProceso.Text = "Estado: " + mensaje;
            actualizarPendientes();
        }

        // Inicia manualmente la atención de las operaciones pendientes.
        private void btnProcesar_Click(object sender, EventArgs e)
        {
            servidorSimulado = true;
            if (!redSimulada && !servidorSimulado)
            {
                lblEstadoProceso.Text = "Estado: primero debe recuperar la red y el servidor.";
                return;
            }

            if (procesamientoIniciado)
                return;

            if (GestorCentral.cola.CantidadPendientes() == 0)
            {
                FinalizarProcesamiento();
                return;
            }

            procesamientoIniciado = false;
            btnProcesar.Enabled = false;
            lblEstadoProceso.Text = "Estado: procesando pagos pendientes.";

            lstHistorial.Items.Add(DateTime.Now.ToString("HH:mm:ss") +" | Procesamiento manual iniciado por el usuario.");

            temporizadorAtencion.Start();
        }

        // Finaliza la atención cuando ya no quedan operaciones en la cola.
        private void FinalizarProcesamiento()
        {
            temporizadorAtencion.Stop();
            procesamientoIniciado = false;
            transaccionEnProceso = null;
            actualizarPendientes();
            lblEstadoProceso.Text = "Estado: todos los pagos fueron procesados correctamente.";
            btnProcesar.Enabled = true;

            lstHistorial.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " | Todos los pagos fueron procesados. Reiniciando grafo.");

            if (ventanaGrafo != null && !ventanaGrafo.IsDisposed)
                ventanaGrafo.ReiniciarGrafo();
        }

        // ========================================================
        // MÓDULO 4: SIMULACIÓN DE CONECTIVIDAD
        // ========================================================

        // Sincroniza el estado de red y servidor con el gestor y el formulario del grafo.
        private void actualizarEstado()
        {
            GestorCentral.cola.ActualizarConexion(redSimulada, servidorSimulado);

            lblEstadoRed.Text = redSimulada ? "Red: DISPONIBLE" : "Red: DESCONECTADA";
            lblEstadoRed.ForeColor = redSimulada ? Color.Green : Color.Red;
            lblEstadoServidor.Text = servidorSimulado ? "Servidor: DISPONIBLE" : "Servidor: SIN RESPUESTA";
            lblEstadoServidor.ForeColor = servidorSimulado ? Color.Green : Color.Red;

            if (ventanaGrafo != null && !ventanaGrafo.IsDisposed)
            {
                ventanaGrafo.ActualizarEstado(redSimulada,servidorSimulado,"Se actualizó el estado de la conectividad.");
            }
        }

        // Restablece la conectividad y habilita el procesamiento si hay pendientes.
        public void SimularRecuperacionRed()
        {
            redSimulada = true;
            servidorSimulado = true;
            actualizarEstado();

            lblEstadoRed.Text = "Red: Conectada";
            lblEstadoRed.ForeColor = Color.Green;
            lblEstadoServidor.Text = "Servidor: Conectado";
            lblEstadoServidor.ForeColor = Color.Green;
            lblEstadoProceso.Text = "Estado: conectividad recuperada. Presione Procesar para continuar.";

            lstHistorial.Items.Add(
                DateTime.Now.ToString("HH:mm:ss") +
                " | Red y servidor recuperados correctamente.");

            if (ventanaGrafo != null && !ventanaGrafo.IsDisposed)
                ventanaGrafo.ActualizarEstado(true, true, "Red y servidor recuperados correctamente.");

            actualizarPendientes();
            btnProcesar.Enabled = GestorCentral.cola.CantidadPendientes() > 0;
        }

        // Desconecta la red y detiene el procesamiento cuando está en curso.
        public void SimularFalloRed()
        {
            redSimulada = false;
            actualizarEstado();

            lstHistorial.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " | Fallo simulado de red");

            if (procesamientoIniciado)
                detenerProcesamiento("Red desconectada; quedan operaciones pendientes.");
            else
                lblEstadoProceso.Text = "Estado: Sin red";
        }

        // Simula la recuperación del servidor y actualiza el grafo.
        private void btnRecuperarServidor_Click(object sender, EventArgs e)
        {
            servidorSimulado = true;
            actualizarEstado();
            lstHistorial.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " | Servidor recuperado");

            lblEstadoProceso.Text = redSimulada
                ? "Estado: servidor y red disponibles; pulsa Procesar pendientes."
                : "Estado: servidor recuperado; esperando recuperación de red";

            ventanaGrafo?.RestaurarServidores();
        }

        // Simula la caída del servidor y detiene el procesamiento si corresponde.
        private void btnFallarServidor_Click(object sender, EventArgs e)
        {
            servidorSimulado = false;
            actualizarEstado();
            lstHistorial.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " | Fallo simulado del servidor");

            if (procesamientoIniciado)
                detenerProcesamiento("Servidor sin respuesta; quedan operaciones pendientes.");
            else
                lblEstadoProceso.Text = "Estado: Servidor caído";

            ventanaGrafo?.AplicarFalloServidores();
        }

        // ========================================================
        // MÓDULO 4: SIMULACIÓN DE CONECTIVIDAD
        // ========================================================

        // Registra una operación y la envía a la cola si no hay conectividad.
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            actualizarEstado();

            if (cmbTipo.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de operación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tipo = cmbTipo.SelectedItem.ToString();
            decimal importe = numImporte.Value;

            if (importe <= 0)
            {
                MessageBox.Show("El importe debe ser mayor que cero.", "Importe inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numImporte.Focus();
                return;
            }

            Transaccion nueva = new Transaccion(siguienteId, importe, tipo);
            siguienteId++;

            if (GestorCentral.cola.RedDisponible && GestorCentral.cola.ServidorDisponible)
            {
                nueva.Estado = "APROBADO";
                lstHistorial.Items.Add(
                    DateTime.Now.ToString("HH:mm:ss") +
                    " | ID " + nueva.Id +
                    " | " + tipo +
                    " | S/ " + importe.ToString("0.00") +
                    " | Confirmada");

                MessageBox.Show("Operación realizada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                nueva.Estado = "PENDIENTE";
                GestorCentral.cola.Agregar(nueva);
                lstHistorial.Items.Add(
                    DateTime.Now.ToString("HH:mm:ss") +
                    " | ID " + nueva.Id +
                    " | " + tipo +
                    " | S/ " + importe.ToString("0.00") +
                    " | Pendiente por fallo");
            }

            actualizarPendientes();
            lblTotalPendientes.Text = "Elementos en la cola: " + GestorCentral.cola.CantidadPendientes();
        }

        // ========================================================
        // MÓDULO 5: VISUALIZACIÓN Y ADMINISTRACIÓN DEL GRAFO
        // ========================================================

        // Abre la ventana del grafo o activa la instancia que ya está abierta.
        private void btnVerGrafo_Click(object sender, EventArgs e)
        {
            if (ventanaGrafo == null || ventanaGrafo.IsDisposed)
            {
                ventanaGrafo = new FormGrafoConectividad(this);
                ventanaGrafo.RecuperacionFinalizada += FormularioGrafo_RecuperacionFinalizada;

                ventanaGrafo.FormClosed += (s, args) =>
                {
                    ventanaGrafo = null;
                    ultimoCodigoOperacion = string.Empty;
                    ultimoEstadoOperacion = string.Empty;
                    ultimaPasarela = string.Empty;
                };

                ventanaGrafo.Show(this);
            }
            else
            {
                if (!ventanaGrafo.Visible)
                    ventanaGrafo.Show(this);

                ventanaGrafo.Activate();
            }

            ventanaGrafo.ActualizarEstado(redSimulada, servidorSimulado, "Estado actual de la conectividad.");

            if (!string.IsNullOrWhiteSpace(ultimoCodigoOperacion))
            {
                ventanaGrafo.MostrarResultado(
                    ultimoCodigoOperacion,
                    ultimoEstadoOperacion,
                    ultimaPasarela,
                    "Última operación registrada.");
            }
        }

        // Sincroniza el formulario principal después de recuperar el grafo.
        private void FormularioGrafo_RecuperacionFinalizada(object sender, EventArgs e)
        {
            redSimulada = true;
            servidorSimulado = true;
            actualizarEstado();
            actualizarPendientes();

            lblEstadoRed.Text = "Red: Conectada";
            lblEstadoRed.ForeColor = Color.Green;
            lblEstadoServidor.Text = "Servidor: CONECTADO";
            lblEstadoServidor.ForeColor = Color.Green;
            lblEstadoProceso.Text = GestorCentral.cola.CantidadPendientes() > 0
                ? "Estado: red recuperada. Presione Procesar para continuar."
                : "Estado: red recuperada. No hay pagos pendientes.";

            btnProcesar.Enabled = GestorCentral.cola.CantidadPendientes() > 0;
        }

        // Envía al grafo el estado y la pasarela de la operación indicada.
        private void ActualizarGraficoOperacion(string codigo, string estado, string pasarela, string mensaje)
        {
            ultimoCodigoOperacion = codigo;
            ultimoEstadoOperacion = estado;
            ultimaPasarela = pasarela;

            if (ventanaGrafo != null && !ventanaGrafo.IsDisposed)
                ventanaGrafo.MostrarResultado(codigo, estado, pasarela, mensaje);
        }

        // Obtiene la primera pasarela activa de la ruta óptima entre Pago y Banco.
        private string ObtenerPasarelaDisponible()
        {
            Vertice inicio = GestorCentral.grafo.Vertices.FirstOrDefault(v => v.Nombre.Equals("Pago", StringComparison.OrdinalIgnoreCase));
            Vertice fin = GestorCentral.grafo.Vertices.FirstOrDefault(v => v.Nombre.Equals("Banco", StringComparison.OrdinalIgnoreCase));

            if (inicio == null || fin == null)
                return null;

            var resultado = Dijkstra.CalcularRutaCorta(GestorCentral.grafo, inicio, fin);

            if (resultado.Recorrido.Count < 3 || resultado.LatenciaTotal == int.MaxValue)
                return null;

            // Obtiene la primera pasarela intermedia de la ruta calculada.
            Vertice pasarela = resultado.Recorrido.FirstOrDefault(v =>!v.Nombre.Equals("Pago", StringComparison.OrdinalIgnoreCase) && !v.Nombre.Equals("Banco", StringComparison.OrdinalIgnoreCase));

            if (pasarela == null || !pasarela.Estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                return null;

            return pasarela.Nombre;
        }

        // ========================================================
        // NAVEGACIÓN ENTRE PESTAÑAS Y EVENTOS AUXILIARES
        // ========================================================

        // Abre la pestaña del carrito.
        private void btnCarrito_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabCarrito;
        }

        // Abre la pestaña del simulador de conectividad.
        private void btnsimulador_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabConectividad;
        }

        // Abre la pestaña del historial y actualiza sus datos.
        private void btnhistorial_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabHistorial;
            ActualizarHistorialVouchers();
        }

        // Cambia a la pestaña correspondiente al índice configurado en el diseñador.
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        // Cambia a la cuarta pestaña del formulario.
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        // Cambia a la quinta pestaña del formulario.
        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        // ========================================================
        // ACTUALIZACIÓN Y SINCRONIZACIÓN DE TRANSACCIONES
        // ========================================================

        // Actualiza el estado de una operación en las listas y el historial global.
        private void ActualizarOperacionOriginal(string codigo, string nuevoEstado)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return;

            NodoTransaccion actual = GestorCentral.ventas.cabeza;

            while (actual != null)
            {
                if (string.Equals(actual.Codigo, codigo, StringComparison.OrdinalIgnoreCase))
                {
                    actual.Estado = nuevoEstado;
                    break;
                }

                actual = actual.Siguiente;
            }

            if (SesionGlobal.HistorialTransaccionesGlobal != null)
            {
                foreach (NodoTransaccion item in SesionGlobal.HistorialTransaccionesGlobal)
                {
                    if (item != null && string.Equals(item.Codigo, codigo, StringComparison.OrdinalIgnoreCase))
                        item.Estado = nuevoEstado;
                }
            }

            foreach (Voucher voucher in GestorCentral.historial.ObtenerTodos())
            {
                if (string.Equals(Convert.ToString(voucher.IdOperacion), codigo, StringComparison.OrdinalIgnoreCase))
                    voucher.Estado = nuevoEstado;
            }

            actualizarInterfazPago();
            ActualizarHistorialVouchers();
        }
    }
}
