using semana7Stack.Modelos;
using semana7Stack.Estructuras;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace semana7Stack
{
    public partial class FormGestionFalloReconexion : Form
    {
        private GestorReintentos gestor = new GestorReintentos();

        private System.Windows.Forms.Timer temporizadorAtencion = new System.Windows.Forms.Timer();

        private bool procesamientoIniciado = false;
        private bool redSimulada = true;
        private bool servidorSimulado = true;
        private int siguienteId = 1;

        private void FormGestionFalloReconexion_Load(object sender, EventArgs e)
        {
            // Agrega esta línea dentro de tu Load existente:
            gestor.CargarDesdeHistorialGlobal(ref siguienteId);

            // Lo que ya tuviera tu Load...
            actualizarPendientes();
        }
        public FormGestionFalloReconexion()
        {
            InitializeComponent();

            // Seleccionar el primer tipo disponible.
            if (cmbTipo.Items.Count > 0 && cmbTipo.SelectedIndex < 0)
            {
                cmbTipo.SelectedIndex = 0;
            }

            // Configurar el temporizador.
            temporizadorAtencion.Interval = 2000;
            temporizadorAtencion.Tick += temporizadorAtencion_Tick;

            actualizarEstado();
            actualizarPendientes();

            lblEstadoProceso.Text = "Estado: en espera";
        }

        // Actualizar lista de operaciones pendientes
        private void actualizarPendientes()
        {
            lstPendientes.Items.Clear();

            List<Transaccion> pendientes = gestor.ObtenerPendientesOrdenados();

            foreach (Transaccion item in pendientes)
            {
                lstPendientes.Items.Add(
                    "ID: " + item.Id +
                    " | " + item.Tipo +
                    " | Prioridad: " + obtenerPrioridad(item) +
                    " | S/ " + item.Importe.ToString("0.00") +
                    " | " + item.Estado
                );
            }

            if (pendientes.Count == 0)
            {
                lstPendientes.Items.Add("No hay operaciones pendientes.");
            }

            lblTotalPendientes.Text = "Elementos en la cola: " + gestor.CantidadPendientes();

        }

        // Atención de operaciones cada 2 segundos
        private void temporizadorAtencion_Tick(object sender, EventArgs e)
        {
            actualizarEstado();

            if (!gestor.RedDisponible || !gestor.ServidorDisponible)
            {
                detenerProcesamiento("Conexión perdida; quedan operaciones pendientes.");

                lstHistorial.Items.Add(
                    DateTime.Now.ToString("HH:mm:ss") + " | Atención detenida por fallo de conexión"
                );

                return;
            }

            Transaccion actual;

            if (!gestor.IntentarObtenerSiguiente(out actual))
            {
                detenerProcesamiento("No hay operaciones pendientes.");
                return;
            }

            actual.Intentos++;
            actual.Estado = "Confirmada (simulación)";

            lstHistorial.Items.Add(
                DateTime.Now.ToString("HH:mm:ss") +
                " | Atendida ID " + actual.Id +
                " | " + actual.Tipo +
                " | Prioridad: " + obtenerPrioridad(actual) +
                " | S/ " + actual.Importe.ToString("0.00") +
                " | Confirmada"
            );

            actualizarPendientes();

            if (gestor.CantidadPendientes() == 0)
            {
                detenerProcesamiento("Todas las operaciones fueron atendidas.");
            }
            else
            {
                lblEstadoProceso.Text = "Estado: operación atendida; siguiente en 2 segundos";
            }
        }

        // Actualización de estados de conexión
        private void actualizarEstado()
        {
            gestor.ActualizarConexion(
                redSimulada,
                servidorSimulado
            );

            if (redSimulada)
            {
                lblEstadoRed.Text = "Red: DISPONIBLE";
                lblEstadoRed.ForeColor = Color.Green;
            }
            else
            {
                lblEstadoRed.Text = "Red: DESCONECTADA";
                lblEstadoRed.ForeColor = Color.Red;
            }

            if (servidorSimulado)
            {
                lblEstadoServidor.Text = "Servidor: DISPONIBLE";
                lblEstadoServidor.ForeColor = Color.Green;
            }
            else
            {
                lblEstadoServidor.Text = "Servidor: SIN RESPUESTA";
                lblEstadoServidor.ForeColor = Color.Red;
            }
        }

        // Obtener prioridad en cola
        private string obtenerPrioridad(Transaccion t)
        {
            if (t.Tipo == "Extorno")
                return "Alta";

            if (t.Tipo == "Reversion")
                return "Media";

            return "Normal";
        }

        // Detener el proceso
        private void detenerProcesamiento(string mensaje)
        {
            temporizadorAtencion.Stop();

            procesamientoIniciado = false;
            btnProcesar.Enabled = true;

            lblEstadoProceso.Text = "Estado: " + mensaje;

            actualizarPendientes();
        }

        // Liberar el temporizador al cerrar
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            temporizadorAtencion.Stop();
            temporizadorAtencion.Dispose();

            base.OnFormClosed(e);
        }

        // Registrar una operación
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            actualizarEstado();

            if (cmbTipo.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de operación.");
                return;
            }

            string tipo = cmbTipo.SelectedItem.ToString();
            decimal importe = numImporte.Value;

            if (importe <= 0)
            {
                MessageBox.Show("El importe debe ser mayor que cero",
                "Importe inválido",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );

                numImporte.Focus();
                return;
            }

            Transaccion nueva = new Transaccion(siguienteId, importe, tipo);

            siguienteId++;

            if (gestor.RedDisponible && gestor.ServidorDisponible)
            {
                nueva.Estado = "Confirmada (simulación)";

                lstHistorial.Items.Add(
                    DateTime.Now.ToString("HH:mm:ss") +
                    " | ID " + nueva.Id +
                    " | " + tipo +
                    " | S/ " + importe.ToString("0.00") +
                    " | Confirmada"
                );

                MessageBox.Show(
                    "Operación realizada correctamente",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                nueva.Estado = "Pendiente";
                gestor.Agregar(nueva);

                lstHistorial.Items.Add(
                    DateTime.Now.ToString("HH:mm:ss") +
                    " | ID " + nueva.Id +
                    " | " + tipo +
                    " | S/ " + importe.ToString("0.00") +
                    " | Pendiente por fallo"
                );
            }

            actualizarPendientes();
            lblTotalPendientes.Text = "Elementos en la cola: " + gestor.CantidadPendientes();
        }


        // Simulación de fallar servidor
        private void btnFallarServidor_Click(object sender, EventArgs e)
        {
            servidorSimulado = false;
            actualizarEstado();

            lstHistorial.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " | Fallo simulado del servidor");

            if (procesamientoIniciado)
            {
                detenerProcesamiento("Servidor sin respuesta; quedan operaciones pendientes.");
            }
            else
            {
                lblEstadoProceso.Text = "Estado: Servidor caído";
            }
        }


        // Recuperar servidor
        private void btnRecuperarServidor_Click(object sender, EventArgs e)
        {
            servidorSimulado = true;
            actualizarEstado();

            lstHistorial.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " | Servidor recuperado");
            if (servidorSimulado)
            {
                lblEstadoProceso.Text = "Estado: servidor y red disponibles; pulsa Procesar pendientes.";
            }
            else
            {
                lblEstadoProceso.Text = "Estado: servidor recuperado; esperando recuperación de red";
            }

        }

        // Simulación de desconexión de red
        private void btnFallarRed_Click(object sender, EventArgs e)
        {
            redSimulada = false;
            actualizarEstado();

            lstHistorial.Items.Add(
                DateTime.Now.ToString("HH:mm:ss") + " | Fallo simulado de red");

            if (procesamientoIniciado)
            {
                detenerProcesamiento("Red desconectada; quedan operaciones pendientes.");
            }
            else
            {
                lblEstadoProceso.Text = "Estado: Sin red";
            }
        }

        // Simulación de recuperar red
        private void btnRecuperarRed_Click(object sender, EventArgs e)
        {
            redSimulada = true;
            actualizarEstado();

            lstHistorial.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " | Red recuperada");
            if (servidorSimulado)
            {
                lblEstadoProceso.Text = "Estado: red y servidor disponibles; pulsa Procesar pendientes.";
            }
            else
            {
                lblEstadoProceso.Text = "Estado: red recuperada; esperando recuperación de servidor";
            }


        }

        // Procesar operaciones pendientes por pérdida de red o sistema
        private void btnProcesar_Click(object sender, EventArgs e)
        {
            actualizarEstado();

            if (!gestor.RedDisponible || !gestor.ServidorDisponible)
            {
                MessageBox.Show(
                    "No se puede procesar: la red o el servidor no están disponibles.",
                    "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }

            if (gestor.CantidadPendientes() == 0)
            {
                MessageBox.Show("No hay operaciones pendientes.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }

            if (procesamientoIniciado)
            {
                MessageBox.Show("El procesamiento ya está iniciado.");
                return;
            }

            procesamientoIniciado = true;
            btnProcesar.Enabled = false;

            lblEstadoProceso.Text = "Estado: procesando pendientes";

            lstHistorial.Items.Add(
                DateTime.Now.ToString("HH:mm:ss") + " | Inicio del procesamiento manual");

            temporizadorAtencion.Start();
        }
    }
}
