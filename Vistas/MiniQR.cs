
using System;
using System.Windows.Forms;

namespace semana7Stack.Vistas
{
    public partial class MiniQR : Form
    {
        // Estados posibles:
        // ESPERANDO, EXITO, EXPIRADO y PENDIENTE_RED.
        //
        // X = EXITO
        // 10 segundos = EXPIRADO
        // Caída de red = PENDIENTE_RED

        public string ResultadoOperacion { get; private set; } = "ESPERANDO";

        private System.Windows.Forms.Timer timerExpiracion;
        private int tiempoRestante = 10;

        public MiniQR(int monto)
        {
            InitializeComponent();

            ConfigurarVentana(monto);

            // Detectar el cierre mediante la X.
            FormClosing += FormQR_FormClosing;

            // Conectar el botón del diseñador.
            btnSimularFalloRed.Click -= btnSimularFalloRed_Click;
            btnSimularFalloRed.Click += btnSimularFalloRed_Click;

            lblTiempo.Text = $"Tiempo restante: {tiempoRestante}s";

            IniciarTimer();
        }

        // ==========================================
        // CONFIGURACIÓN DE VENTANA
        // ==========================================

        private void ConfigurarVentana(int monto)
        {
            Text = "Escanee el código QR";

            // Se respeta el tamaño configurado en el diseñador.
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            lblMonto.Text = $"Monto a pagar: S/{monto}";
        }

        // ==========================================
        // BOTÓN SIMULAR CAÍDA DE RED
        // ==========================================

        private void btnSimularFalloRed_Click(object sender, EventArgs e)
        {
            timerExpiracion?.Stop();

            ResultadoOperacion = "PENDIENTE_RED";

            DialogResult = DialogResult.Abort;

            Close();
        }

        // ==========================================
        // TEMPORIZADOR QR
        // ==========================================

        private void IniciarTimer()
        {
            timerExpiracion = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };

            timerExpiracion.Tick += TimerExpiracion_Tick;
            timerExpiracion.Start();
        }

        private void TimerExpiracion_Tick(object sender, EventArgs e)
        {
            tiempoRestante--;

            lblTiempo.Text = $"Tiempo restante: {tiempoRestante}s";

            if (tiempoRestante <= 0)
            {
                timerExpiracion.Stop();

                // El QR expiró: cancelar la operación.
                ResultadoOperacion = "EXPIRADO";

                DialogResult = DialogResult.Cancel;

                Close();
            }
        }

        // ==========================================
        // CIERRE DE VENTANA
        // ==========================================

        private void FormQR_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            timerExpiracion?.Stop();

            // Cerrar mediante la X antes de expirar = EXITO.
            if (ResultadoOperacion == "ESPERANDO")
            {
                ResultadoOperacion = "EXITO";

                DialogResult = DialogResult.OK;
            }
        }

    }
}
