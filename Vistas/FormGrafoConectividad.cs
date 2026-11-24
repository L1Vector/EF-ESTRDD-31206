using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using semana7Stack.ModuloGrafos.Estructura;
using semana7Stack.ModuloGrafos.Algoritmo;
using semana7Stack.CoordinacionModulos;

namespace semana7Stack.Vistas
{
    public partial class FormGrafoConectividad : Form
    {
        // ========================================================
        // MÓDULO 1: CONFIGURACIÓN Y ESTADO DEL SIMULADOR
        // ========================================================

        private readonly Registro_de_Ventas___Pagos registro;

        public event EventHandler RecuperacionFinalizada;

        private const string NombrePasarelaRespaldo = "RESPALDO";

        private enum RutaPago
        {
            Ninguna,
            PasarelaA,
            PasarelaB
        }

        private bool redDisponible = true;
        private bool servidorDisponible = true;
        private bool pagoPendiente = false;
        private bool pagoExitoso = false;
        private bool pagoProcesando = false;
        private bool pasarelaAFallo = false;
        private int tiempoQR = 10;

        private string mensajeOperacion = "Esperando una operación de pago.";
        private string codigoOperacion = "";
        private RutaPago rutaActual = RutaPago.Ninguna;

        private readonly HashSet<string> pasarelasDesconectadas =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Inicializa el formulario, el grafo base y los controles de selección.
        public FormGrafoConectividad(Registro_de_Ventas___Pagos formularioRegistro)
        {
            InitializeComponent();

            registro = formularioRegistro;
            btnCerrar.Click += btnCerrar_Click;
            panelGrafo.Paint -= panelGrafo_Paint;
            panelGrafo.Paint += panelGrafo_Paint;
            lblEstadoGeneral.Text = "Estado: esperando operación";
            lblDetalleOperacion.Text = mensajeOperacion;

            InicializarGrafoBase();
            ActualizarComboBoxes();
            panelGrafo.SizeChanged += (s, e) => ActualizarPosicionesGrafo();
            ActualizarEstadoPasarelas();

            CalcularRutaOptima();
        }

        // ========================================================
        // MÓDULO 2: INICIALIZACIÓN Y ORGANIZACIÓN DEL GRAFO
        // ========================================================

        // Reinicia el estado del simulador y reconstruye los vértices originales.
        public void ReiniciarGrafo()
        {
            redDisponible = true;
            servidorDisponible = true;
            pagoPendiente = false;
            pagoExitoso = false;
            pagoProcesando = false;
            pasarelaAFallo = false;
            tiempoQR = 10;
            mensajeOperacion = "Esperando una operación de pago.";
            codigoOperacion = "";
            rutaActual = RutaPago.Ninguna;
            pasarelasDesconectadas.Clear();

            // Permite volver a utilizar el botón Cerrar.
            btnCerrar.Enabled = true;

            // Quita cualquier ruta resaltada y reconstruye el grafo base.
            GestorCentral.rutaResaltada.Clear();

            foreach (Vertice vertice in GestorCentral.grafo.Vertices.ToList())
                GestorCentral.grafo.EliminarVertice(vertice);

            InicializarGrafoBase();
            ActualizarComboBoxes();
            ActualizarEstadoPasarelas();
            lblEstadoGeneral.Text = "Conectividad: DISPONIBLE";
            lblDetalleOperacion.Text = mensajeOperacion;
            panelGrafo.Invalidate();
        }

        // Crea los cuatro vértices originales y sus conexiones iniciales.
        private void InicializarGrafoBase()
        {
            Vertice pago = ObtenerOCrearVertice("Pago", new Point(80, panelGrafo.Height / 2));
            Vertice banco = ObtenerOCrearVertice("Banco", new Point(panelGrafo.Width - 80, panelGrafo.Height / 2));
            Vertice conekta = ObtenerOCrearVertice("Conekta", new Point(panelGrafo.Width / 2, panelGrafo.Height / 3));
            Vertice stripe = ObtenerOCrearVertice("Stripe", new Point(panelGrafo.Width / 2, (panelGrafo.Height * 2) / 3));

            if (pago != null && banco != null && conekta != null && stripe != null)
            {
                GestorCentral.grafo.AgregarArista(pago, conekta, 50);
                GestorCentral.grafo.AgregarArista(pago, stripe, 90);
                GestorCentral.grafo.AgregarArista(conekta, banco, 120);
                GestorCentral.grafo.AgregarArista(stripe, banco, 100);
            }

            ActualizarPosicionesGrafo();
        }

        // Obtiene un vértice existente o lo agrega al grafo.
        private Vertice ObtenerOCrearVertice(string nombre, Point posicion)
        {
            Vertice vertice = GestorCentral.grafo.Vertices.FirstOrDefault(v => v.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (vertice == null)
            {
                GestorCentral.grafo.AgregarVertice(nombre, posicion, "Activo");
                vertice = GestorCentral.grafo.Vertices.FirstOrDefault(v => v.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            }

            return vertice;
        }

        // Actualiza la ubicación de Pago, Banco y las pasarelas.
        private void ActualizarPosicionesGrafo()
        {
            int ancho = Math.Max(panelGrafo.Width, 200);
            int alto = Math.Max(panelGrafo.Height, 120);
            int centroY = alto / 2;

            Vertice pago = GestorCentral.grafo.Vertices.FirstOrDefault(v => v.Nombre.Equals("Pago", StringComparison.OrdinalIgnoreCase));
            Vertice banco = GestorCentral.grafo.Vertices.FirstOrDefault(v => v.Nombre.Equals("Banco", StringComparison.OrdinalIgnoreCase));

            if (pago != null)
                pago.Posicion = new Point(80, centroY);

            if (banco != null)
                banco.Posicion = new Point(ancho - 80, centroY);

            ReorganizarPasarelasVerticalmente();
            panelGrafo.Invalidate();
        }

        // Distribuye las pasarelas en una columna dentro del panel.
        private void ReorganizarPasarelasVerticalmente()
        {
            var pasarelas = GestorCentral.grafo.Vertices
                .Where(v => !EsVerticeTerminal(v))
                .OrderBy(v => v.Nombre, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (pasarelas.Count == 0)
                return;

            int margenVertical = 42;
            int altoUtil = Math.Max(0, panelGrafo.Height - (margenVertical * 2));
            int separacion = pasarelas.Count == 1 ? 0 : altoUtil / (pasarelas.Count - 1);
            int inicioY = pasarelas.Count == 1 ? panelGrafo.Height / 2 : margenVertical;
            int centroX = panelGrafo.Width / 2;

            for (int i = 0; i < pasarelas.Count; i++)
                pasarelas[i].Posicion = new Point(centroX, inicioY + (i * separacion));
        }

        // Actualiza las opciones de vértices y pasarelas en los ComboBoxes.
        private void ActualizarComboBoxes()
        {
            var vertices = GestorCentral.grafo.Vertices.ToArray();

            cboOrigenArista.Items.Clear();
            cboDestinoArista.Items.Clear();
            cboOrigenArista.Items.AddRange(vertices);
            cboDestinoArista.Items.AddRange(vertices);

            if (vertices.Length > 0)
            {
                cboOrigenArista.SelectedIndex = 0;
                cboDestinoArista.SelectedIndex = vertices.Length > 1 ? 1 : 0;
            }

            var pasarelas = vertices
                .Where(v => !EsVerticeTerminal(v))
                .Select(v => v.Nombre)
                .ToArray();

            cmbPasarela.Items.Clear();
            cmbPasarela.Items.AddRange(pasarelas);

            if (cmbPasarela.Items.Count > 0)
                cmbPasarela.SelectedIndex = 0;
        }

        // Indica si el vértice corresponde al origen Pago o al destino Banco.
        private bool EsVerticeTerminal(Vertice vertice)
        {
            return vertice.Nombre.Equals("Pago", StringComparison.OrdinalIgnoreCase)
                   || vertice.Nombre.Equals("Banco", StringComparison.OrdinalIgnoreCase);
        }

        // Obtiene la pasarela elegida actualmente en cmbPasarela.
        private Vertice ObtenerPasarelaSeleccionada()
        {
            if (cmbPasarela.SelectedItem == null)
                return null;

            string nombre = cmbPasarela.SelectedItem.ToString();
            return GestorCentral.grafo.Vertices.FirstOrDefault(v =>v.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) && !EsVerticeTerminal(v));
        }

        // ========================================================
        // MÓDULO 3: VISUALIZACIÓN DEL GRAFO Y LAS OPERACIONES
        // ========================================================

        // Dibuja las conexiones, sus latencias y los vértices del grafo.
        // Se conserva la lógica original de Paint.
        private void panelGrafo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var conexionesDibujadas = new List<(Vertice, Vertice)>();

            foreach (var arista in GestorCentral.grafo.Aristas)
            {
                bool yaDibujada = conexionesDibujadas.Any(par =>
                    (par.Item1 == arista.Origen && par.Item2 == arista.Destino) ||
                    (par.Item1 == arista.Destino && par.Item2 == arista.Origen));

                if (yaDibujada)
                    continue;

                conexionesDibujadas.Add((arista.Origen, arista.Destino));

                int indiceOrigen = GestorCentral.rutaResaltada.IndexOf(arista.Origen);
                int indiceDestino = GestorCentral.rutaResaltada.IndexOf(arista.Destino);
                bool esParteDeRuta = indiceOrigen >= 0 && indiceDestino >= 0 && Math.Abs(indiceOrigen - indiceDestino) == 1;

                using (Pen lapiz = new Pen(esParteDeRuta ? Color.DarkOrange : Color.LightGray, esParteDeRuta ? 4 : 2))
                {
                    g.DrawLine(lapiz, arista.Origen.Posicion, arista.Destino.Posicion);
                }

                int midX = (arista.Origen.Posicion.X + arista.Destino.Posicion.X) / 2;
                int midY = (arista.Origen.Posicion.Y + arista.Destino.Posicion.Y) / 2;

                g.DrawString(
                    $"{arista.Peso} ms",
                    Font,
                    esParteDeRuta ? Brushes.DarkOrange : Brushes.DarkBlue,
                    midX,
                    midY);
            }

            const int rectAncho = 90;
            const int rectAlto = 36;

            foreach (var vertice in GestorCentral.grafo.Vertices)
            {
                bool esDeRuta = GestorCentral.rutaResaltada.Contains(vertice);
                bool esActivo = string.Equals(vertice.Estado, "Activo", StringComparison.OrdinalIgnoreCase);
                Brush relleno = !esActivo ? Brushes.Gray : esDeRuta ? Brushes.Orange : Brushes.DeepSkyBlue;

                Rectangle rect = new Rectangle(
                    vertice.Posicion.X - rectAncho / 2,
                    vertice.Posicion.Y - rectAlto / 2,
                    rectAncho,
                    rectAlto);

                g.FillRectangle(relleno, rect);
                g.DrawRectangle(Pens.Black, rect);

                using (Font fuenteNodo = new Font("Arial", 9, FontStyle.Bold))
                {
                    SizeF size = g.MeasureString(vertice.Nombre, fuenteNodo);
                    g.DrawString(
                        vertice.Nombre,
                        fuenteNodo,
                        Brushes.Black,
                        vertice.Posicion.X - size.Width / 2,
                        vertice.Posicion.Y - size.Height / 2);
                }
            }
        }

        // Actualiza la información visual de la última operación mostrada.
        public void MostrarResultado(string codigo, string estado, string pasarela, string mensaje)
        {
            codigoOperacion = codigo;
            mensajeOperacion = mensaje;

            string estadoNormalizado = (estado ?? "").Trim().ToUpperInvariant();
            string pasarelaNormalizada = (pasarela ?? "").Trim();

            pagoPendiente = estadoNormalizado == "PENDIENTE";
            pagoExitoso = estadoNormalizado == "APROBADO";
            pagoProcesando = estadoNormalizado == "PROCESANDO";

            // Mantiene la compatibilidad con los códigos A y B y admite nombres de pasarela.
            if (pasarelaNormalizada.Equals("A", StringComparison.OrdinalIgnoreCase))
            {
                rutaActual = RutaPago.PasarelaA;
            }
            else if (pasarelaNormalizada.Equals("B", StringComparison.OrdinalIgnoreCase))
            {
                rutaActual = RutaPago.PasarelaB;
            }
            else
            {
                Vertice seleccionada = GestorCentral.grafo.Vertices.FirstOrDefault(v =>
                    v.Nombre.Equals(pasarelaNormalizada, StringComparison.OrdinalIgnoreCase));

                if (seleccionada != null)
                {
                    rutaActual = RutaPago.PasarelaB;
                    pasarelaAFallo = false;
                }
            }

            if (pagoPendiente && (!redDisponible || !servidorDisponible))
                pasarelaAFallo = true;

            lblDetalleOperacion.Text = "Operación: " + codigo + " | " + mensaje;
            panelGrafo.Invalidate();
        }

        // ========================================================
        // MÓDULO 4: ADMINISTRACIÓN DE PASARELAS Y CONEXIONES
        // ========================================================

        // Crea la pasarela RESPALDO y la conecta entre Pago y Banco.
        private void CrearPasarelaRespaldo()
        {
            Vertice pago = GestorCentral.grafo.Vertices.FirstOrDefault(v =>
                v.Nombre.Equals("Pago", StringComparison.OrdinalIgnoreCase));
            Vertice banco = GestorCentral.grafo.Vertices.FirstOrDefault(v =>
                v.Nombre.Equals("Banco", StringComparison.OrdinalIgnoreCase));

            if (pago == null || banco == null)
                return;

            Vertice respaldo = GestorCentral.grafo.Vertices.FirstOrDefault(v =>
                v.Nombre.Equals(NombrePasarelaRespaldo, StringComparison.OrdinalIgnoreCase));

            if (respaldo == null)
            {
                Point posicion = new Point(panelGrafo.Width / 2, panelGrafo.Height / 2);
                bool agregado = GestorCentral.grafo.AgregarVertice(NombrePasarelaRespaldo, posicion, "Activo");

                if (!agregado)
                    return;

                respaldo = GestorCentral.grafo.Vertices.FirstOrDefault(v =>
                    v.Nombre.Equals(NombrePasarelaRespaldo, StringComparison.OrdinalIgnoreCase));
            }

            if (respaldo == null)
                return;

            GestorCentral.grafo.AgregarArista(pago, respaldo, 45);
            GestorCentral.grafo.AgregarArista(respaldo, banco, 50);
            ActualizarComboBoxes();
            ReorganizarPasarelasVerticalmente();
            panelGrafo.Invalidate();
        }

        // Agrega una conexión entre los dos vértices seleccionados.
        private void btnAgregarConexion_Click(object sender, EventArgs e)
        {
            Vertice v1 = (Vertice)cboOrigenArista.SelectedItem;
            Vertice v2 = (Vertice)cboDestinoArista.SelectedItem;

            if (v1 != null && v2 != null && v1 != v2)
            {
                bool aristaAgregada = GestorCentral.grafo.AgregarArista(v1, v2, (int)numLatencia.Value);

                if (aristaAgregada)
                {
                    panelGrafo.Invalidate();
                }
                else
                {
                    MessageBox.Show(
                        "La conexión ya existe entre estos vértices.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(
                    "Seleccione dos vértices diferentes para conectar.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Registra una nueva pasarela en el grafo.
        private void btnAgregarPasarela_Click(object sender, EventArgs e)
        {
            string nombre = txtPasarela.Text.Trim().ToUpper();
            string estado = "Activo";

            if (!string.IsNullOrEmpty(nombre))
            {
                if (nombre.Equals(NombrePasarelaRespaldo, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("La pasarela RESPALDO se crea automáticamente cuando los servidores fallan.","Nombre reservado",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                Point pos = new Point(panelGrafo.Width / 2, panelGrafo.Height / 2);
                bool verticeAgregado = GestorCentral.grafo.AgregarVertice(nombre, pos, estado);

                if (verticeAgregado)
                {
                    ReorganizarPasarelasVerticalmente();
                    ActualizarEstadoPasarelas();
                    ActualizarComboBoxes();
                    txtPasarela.Clear();
                    panelGrafo.Invalidate();
                }
                else
                {
                    MessageBox.Show($"La pasarela '{nombre}' ya existe.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese un nombre válido para la pasarela.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        // Elimina una pasarela y las conexiones asociadas a ella.
        private void btnEliminarPasarela_Click(object sender, EventArgs e)
        {
            string nombre = txtPasarela.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(nombre))
            {
                var verticeAEliminar = GestorCentral.grafo.Vertices.FirstOrDefault(v =>
                    v.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

                if (verticeAEliminar != null)
                {
                    if (EsVerticeTerminal(verticeAEliminar))
                    {
                        MessageBox.Show(
                            "Los vértices Pago y Banco no se pueden eliminar.",
                            "Operación no permitida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (verticeAEliminar.Nombre.Equals(NombrePasarelaRespaldo, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show(
                            "La pasarela RESPALDO es administrada automáticamente y no se puede eliminar.",
                            "Operación no permitida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    // Limpia la ruta resaltada antes de eliminar el vértice.
                    GestorCentral.rutaResaltada.Clear();
                    GestorCentral.grafo.EliminarVertice(verticeAEliminar);
                    pasarelasDesconectadas.Remove(verticeAEliminar.Nombre);
                    ReorganizarPasarelasVerticalmente();
                    ActualizarComboBoxes();
                    panelGrafo.Invalidate();

                    MessageBox.Show(
                        $"Se eliminó la pasarela '{nombre}' y sus conexiones.",
                        "Pasarela eliminada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        $"No se encontró la pasarela '{nombre}'.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(
                    "Ingrese un nombre válido para la pasarela.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Actualiza las pasarelas según el estado de red, servidor y desconexiones manuales.
        private void ActualizarEstadoPasarelas()
        {
            foreach (var vertice in GestorCentral.grafo.Vertices)
            {
                if (EsVerticeTerminal(vertice))
                {
                    vertice.Estado = "Activo";
                    continue;
                }

                if (vertice.Nombre.Equals(NombrePasarelaRespaldo, StringComparison.OrdinalIgnoreCase))
                {
                    vertice.Estado = servidorDisponible ? "Inactivo" : "Activo";
                    continue;
                }

                bool desconectadaManualmente = pasarelasDesconectadas.Contains(vertice.Nombre);
                bool esConekta = vertice.Nombre.Equals("Conekta", StringComparison.OrdinalIgnoreCase);
                bool debeDesconectarse = !servidorDisponible || desconectadaManualmente || (esConekta && !redDisponible);
                vertice.Estado = debeDesconectarse ? "Inactivo" : "Activo";
            }

            panelGrafo.Invalidate();
        }

        // Desconecta manualmente la pasarela seleccionada.
        private void btnDesconectarRed_Click(object sender, EventArgs e)
        {
            Vertice pasarela = ObtenerPasarelaSeleccionada();

            if (pasarela == null)
            {
                MessageBox.Show("Seleccione una pasarela para desconectar.","Atención",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            if (pasarela.Nombre.Equals(NombrePasarelaRespaldo, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("La pasarela RESPALDO se administra automáticamente.","Operación no permitida",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            if (pasarelasDesconectadas.Contains(pasarela.Nombre))
            {
                MessageBox.Show($"La pasarela {pasarela.Nombre} ya está desconectada manualmente.","Sin cambios",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            pasarelasDesconectadas.Add(pasarela.Nombre);
            ActualizarEstadoPasarelas();
            panelGrafo.Invalidate();
            CalcularRutaOptima();

            MessageBox.Show($"La pasarela {pasarela.Nombre} fue desconectada correctamente.","Pasarela desconectada",MessageBoxButtons.OK,MessageBoxIcon.Information);

            // Carga a Registro de pagos errores de simulación
            registro?.SimularFalloRed();
        }

        // ========================================================
        // MÓDULO 5: SIMULACIÓN Y RECUPERACIÓN DE CONECTIVIDAD
        // ========================================================

        // Oculta la ventana, espera dos segundos y recupera la conectividad.
        private async void btnCerrar_Click(object sender, EventArgs e)
        {
            btnCerrar.Enabled = false;
            Hide();

            if (IsDisposed)
                return;

            redDisponible = true;
            servidorDisponible = true;
            ActualizarEstadoPasarelas();
            lblEstadoGeneral.Text = "Conectividad: DISPONIBLE";
            lblDetalleOperacion.Text = "La red y los servidores fueron recuperados correctamente.";
            panelGrafo.Invalidate();
            RecuperacionFinalizada?.Invoke(this, EventArgs.Empty);
        }

        // Recupera la red, el servidor y todas las pasarelas.
        private void btnRecuperarRed_Click(object sender, EventArgs e)
        {
            pasarelasDesconectadas.Clear();
            redDisponible = true;
            servidorDisponible = true;
            ActualizarEstadoPasarelas();
            lblEstadoGeneral.Text = "Conectividad: DISPONIBLE";
            lblDetalleOperacion.Text = "Toda la red y sus pasarelas fueron recuperadas correctamente.";
            panelGrafo.Invalidate();

            // Sincroniza la recuperación con el formulario principal.
            registro.SimularRecuperacionRed();

            MessageBox.Show(
                "Toda la red fue recuperada correctamente.\n\n" +
                "Ahora puedes pulsar Procesar para atender los pagos pendientes.",
                "Red recuperada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Carga de simulacion de fallo de red al sistema
            registro?.SimularRecuperacionRed();
        }

        // Verifica si la pasarela RESPALDO está activa en el grafo.
        private bool HayRespaldoActivo()
        {
            if (string.IsNullOrWhiteSpace(NombrePasarelaRespaldo) || GestorCentral.grafo?.Vertices == null)
                return false;

            return GestorCentral.grafo.Vertices.Any(v => v.Nombre.Equals(NombrePasarelaRespaldo, StringComparison.OrdinalIgnoreCase) && string.Equals(v.Estado, "Activo", StringComparison.OrdinalIgnoreCase));
        }

        // Actualiza el estado de conectividad y el mensaje general del simulador.
        public void ActualizarEstado(bool red, bool servidor, string mensaje)
        {
            bool servidorCambio = servidorDisponible != servidor;

            redDisponible = red;
            servidorDisponible = servidor;
            mensajeOperacion = mensaje;

            if (!servidorDisponible)
                CrearPasarelaRespaldo();

            // Solo actualiza estados de pasarelas si cambió la disponibilidad del servidor,
            // no en cada tick repetitivo de temporizadorAtencion
            if (servidorCambio && !HayRespaldoActivo())
            {
                ActualizarEstadoPasarelas();
            }

            if (redDisponible && servidorDisponible)
            {
                lblEstadoGeneral.Text = "Conectividad: DISPONIBLE";

                if (pagoPendiente)
                {
                    pasarelaAFallo = false;
                    rutaActual = RutaPago.PasarelaB;
                }
            }
            else
            {
                lblEstadoGeneral.Text = !servidorDisponible
                    ? "Servidores: CAÍDOS | RESPALDO ACTIVO"
                    : "Conectividad: CON FALLAS";

                if (pagoPendiente)
                {
                    pasarelaAFallo = true;
                    rutaActual = RutaPago.PasarelaA;
                }
            }

            if (string.IsNullOrWhiteSpace(codigoOperacion))
                lblDetalleOperacion.Text = mensajeOperacion;

            panelGrafo.Invalidate();
        }

        // Simula la caída general de los servidores y activa RESPALDO.
        public void AplicarFalloServidores()
        {
            servidorDisponible = false;
            CrearPasarelaRespaldo();
            ActualizarEstadoPasarelas();
            lblEstadoGeneral.Text = "Servidores: CAÍDOS | RESPALDO ACTIVO";
            lblDetalleOperacion.Text = "Pasarelas principales inactivas. RESPALDO disponible.";

            MessageBox.Show(
                "Los servidores están caídos.\n\n"
                + "Las pasarelas principales fueron desconectadas.\n"
                + "Se activó automáticamente la pasarela RESPALDO.",
                "Pasarela de respaldo activada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            panelGrafo.Invalidate();
        }

        // Restaura los servidores y actualiza la disponibilidad de las pasarelas.
        public void RestaurarServidores()
        {
            servidorDisponible = true;
            ActualizarEstadoPasarelas();
            lblEstadoGeneral.Text = redDisponible ? "Conectividad: DISPONIBLE" : "Conectividad: CON FALLAS";
            lblDetalleOperacion.Text = "Servidores recuperados. RESPALDO permanece inactiva.";

            MessageBox.Show(
                "Los servidores fueron recuperados.\n\n"
                + "Las pasarelas principales están disponibles "
                + "si no tienen desconexiones manuales.\n"
                + "RESPALDO permanece en el grafo, pero está inactiva.",
                "Servidores recuperados",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            panelGrafo.Invalidate();
        }

        // ========================================================
        // MÓDULO 6: CÁLCULO Y VISUALIZACIÓN DE RUTAS
        // ========================================================

        // Calcula con Dijkstra la ruta activa de menor latencia entre Pago y Banco.
        public void CalcularRutaOptima()
        {
            Vertice inicio = GestorCentral.grafo.Vertices.FirstOrDefault(v => v.Nombre.Equals("Pago", StringComparison.OrdinalIgnoreCase));
            Vertice fin = GestorCentral.grafo.Vertices.FirstOrDefault(v => v.Nombre.Equals("Banco", StringComparison.OrdinalIgnoreCase));
            GestorCentral.rutaResaltada.Clear();
            if (inicio == null || fin == null)
            {
                lblDetalleOperacion.Text = "No se encontraron los vértices Pago y Banco.";
                panelGrafo.Invalidate();
                return;
            }
            var resultado = Dijkstra.CalcularRutaCorta(GestorCentral.grafo, inicio, fin);
            if (resultado.Recorrido.Count == 0 || resultado.LatenciaTotal == int.MaxValue)
            {
                lblDetalleOperacion.Text = "No existe una ruta activa entre Pago y Banco.";
                MessageBox.Show(
                    "No existe una ruta disponible. Comprueba el estado de la red, los servidores y las pasarelas.",
                    "Ruta no encontrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                panelGrafo.Invalidate();
                return;
            }
            GestorCentral.rutaResaltada.AddRange(resultado.Recorrido);
            string recorrido = string.Join(" -> ", resultado.Recorrido.Select(v => v.Nombre));
            lblDetalleOperacion.Text = $"Ruta óptima: {recorrido} | Latencia total: {resultado.LatenciaTotal} ms";
            panelGrafo.Invalidate();
        }
        private void btnRutaOptima_Click(object sender, EventArgs e)
        {
            Vertice inicio = GestorCentral.grafo.Vertices.FirstOrDefault(v =>v.Nombre.Equals("Pago", StringComparison.OrdinalIgnoreCase));
            Vertice fin = GestorCentral.grafo.Vertices.FirstOrDefault(v => v.Nombre.Equals("Banco", StringComparison.OrdinalIgnoreCase));

            GestorCentral.rutaResaltada.Clear();

            if (inicio == null || fin == null)
            {
                lblDetalleOperacion.Text = "No se encontraron los vértices Pago y Banco.";
                panelGrafo.Invalidate();
                return;
            }

            var resultado = Dijkstra.CalcularRutaCorta(GestorCentral.grafo, inicio, fin);

            if (resultado.Recorrido.Count == 0 || resultado.LatenciaTotal == int.MaxValue)
            {
                lblDetalleOperacion.Text = "No existe una ruta activa entre Pago y Banco.";
                MessageBox.Show(
                    "No existe una ruta disponible. Comprueba el estado de la red, los servidores y las pasarelas.",
                    "Ruta no encontrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                panelGrafo.Invalidate();
                return;
            }

            GestorCentral.rutaResaltada.AddRange(resultado.Recorrido);
            string recorrido = string.Join(" -> ", resultado.Recorrido.Select(v => v.Nombre));
            lblDetalleOperacion.Text = $"Ruta óptima: {recorrido} | Latencia total: {resultado.LatenciaTotal} ms";
            panelGrafo.Invalidate();
        }
    }
}
