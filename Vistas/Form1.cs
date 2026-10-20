using System;
using System.Drawing;
using System.Windows.Forms;

namespace proyectofinal
{
    public partial class Form1 : Form
    {
        private ListaDobleVouchers historialVouchers = new ListaDobleVouchers();
        private PilaExtornoSaldo pilaExtornos = new PilaExtornoSaldo();
        private int contadorNotificaciones = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            RegistrarLog("=== SISTEMA INICIALIZADO ===");
            RegistrarLog("Estructuras activas: Lista Doblemente Enlazada + Pila de Extornos.");
        }

        private void RegistrarLog(string texto)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {texto}\r\n");
        }

        private void btnRegistrarDebitoPOS_Click(object sender, EventArgs e)
        {
            TransaccionPendiente tx = new TransaccionPendiente(
                101 + pilaExtornos.Contador,
                28.90m,
                "POS PAX A35",
                "Cobro_Sin_Boleta"
            );

            pilaExtornos.Push(tx);
            RegistrarLog($"[PUSH PILA] Débito pendiente registrado en POS. Tx #{tx.IdTransaccion} por S/.{tx.Monto:F2}");
        }

        private void btnExtornoAutomatico_Click(object sender, EventArgs e)
        {
            TransaccionPendiente txExtornada = pilaExtornos.EjecutarRollbackExtorno();

            if (txExtornada != null)
            {
                NotificacionVoucher notiExtorno = new NotificacionVoucher(
                    contadorNotificaciones++,
                    "SIN-BOLETA",
                    txExtornada.Monto,
                    "Tarjeta Crédito",
                    "Extornado",
                    $"Se aplicó el extorno automático de S/.{txExtornada.Monto:F2} por falla/cancelación en POS."
                );

                historialVouchers.AgregarVoucher(notiExtorno);
                RegistrarLog($"[POP ROLLBACK] EXTORNO APLICADO: S/.{txExtornada.Monto:F2} de Tx #{txExtornada.IdTransaccion}");
                RegistrarLog($"[SMS SENT] {notiExtorno.MensajeSMS}");
            }
            else
            {
                RegistrarLog("[ALERTA] La Pila de extornos está vacía. No hay transacciones pendientes.");
            }
        }

        private void btnEmitirBoleta_Click(object sender, EventArgs e)
        {
            if (pilaExtornos.Contador > 0)
            {
                pilaExtornos.ConfirmarYVaciarBoleta();

                NotificacionVoucher notiExito = new NotificacionVoucher(
                    contadorNotificaciones++,
                    "B155-00459851",
                    28.90m,
                    "Tarjeta Crédito",
                    "Confirmado",
                    "OXXO: Su comprobante B155-00459851 por S/.28.90 fue emitido exitosamente."
                );

                historialVouchers.AgregarVoucher(notiExito);
                RegistrarLog("[FLUSH PILA] Boleta B155-00459851 emitida con éxito. Se confirmó la transacción.");
            }
            else
            {
                RegistrarLog("[ALERTA] No hay débitos pendientes en POS para confirmar e imprimir boleta.");
            }
        }

        private void btnVoucherAnterior_Click(object sender, EventArgs e)
        {
            NotificacionVoucher v = historialVouchers.IrAnterior();
            if (v != null)
            {
                RegistrarLog($"[NAVEGACIÓN PREV] {v}");
            }
            else
            {
                RegistrarLog("[HISTORIAL] No hay comprobantes en el historial.");
            }
        }

        private void btnVoucherSiguiente_Click(object sender, EventArgs e)
        {
            NotificacionVoucher v = historialVouchers.IrSiguiente();
            if (v != null)
            {
                RegistrarLog($"[NAVEGACIÓN NEXT] {v}");
            }
            else
            {
                RegistrarLog("[HISTORIAL] No hay comprobantes en el historial.");
            }
        }

   
    }


    public class NotificacionVoucher
    {
        public int IdNotificacion { get; set; }
        public string NumBoleta { get; set; }
        public decimal Monto { get; set; }
        public string TipoMedio { get; set; }
        public string Estado { get; set; }
        public string MensajeSMS { get; set; }
        public DateTime FechaHora { get; set; }

        public NotificacionVoucher(int id, string boleta, decimal monto, string medio, string estado, string sms)
        {
            IdNotificacion = id;
            NumBoleta = boleta;
            Monto = monto;
            TipoMedio = medio;
            Estado = estado;
            MensajeSMS = sms;
            FechaHora = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Boleta: {NumBoleta} | S/.{Monto:F2} | Estado: {Estado}";
        }
    }

    public class TransaccionPendiente
    {
        public int IdTransaccion { get; set; }
        public decimal Monto { get; set; }
        public string CanalPOS { get; set; }
        public string EstadoPOS { get; set; }

        public TransaccionPendiente(int id, decimal monto, string canal, string estado)
        {
            IdTransaccion = id;
            Monto = monto;
            CanalPOS = canal;
            EstadoPOS = estado;
        }
    }

    public class NodoDoble
    {
        public NotificacionVoucher Dato;
        public NodoDoble Siguiente;
        public NodoDoble Anterior;

        public NodoDoble(NotificacionVoucher voucher)
        {
            Dato = voucher;
            Siguiente = null;
            Anterior = null;
        }
    }

    public class ListaDobleVouchers
    {
        private NodoDoble cabeza;
        private NodoDoble cola;
        private NodoDoble actual;

        public void AgregarVoucher(NotificacionVoucher voucher)
        {
            NodoDoble nuevo = new NodoDoble(voucher);
            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                cola.Siguiente = nuevo;
                nuevo.Anterior = cola;
                cola = nuevo;
            }
            actual = cola;
        }

        public NotificacionVoucher IrAnterior()
        {
            if (actual != null && actual.Anterior != null)
            {
                actual = actual.Anterior;
            }
            return actual?.Dato;
        }

        public NotificacionVoucher IrSiguiente()
        {
            if (actual != null && actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            return actual?.Dato;
        }
    }

    public class NodoPila
    {
        public TransaccionPendiente Dato;
        public NodoPila Siguiente;

        public NodoPila(TransaccionPendiente tx)
        {
            Dato = tx;
            Siguiente = null;
        }
    }

    public class PilaExtornoSaldo
    {
        private NodoPila tope;
        public int Contador { get; private set; }

        public void Push(TransaccionPendiente tx)
        {
            NodoPila nuevo = new NodoPila(tx);
            nuevo.Siguiente = tope;
            tope = nuevo;
            Contador++;
        }

        public TransaccionPendiente EjecutarRollbackExtorno()
        {
            if (tope == null) return null;

            TransaccionPendiente txExtornada = tope.Dato;
            tope = tope.Siguiente;
            Contador--;

            return txExtornada;
        }

        public void ConfirmarYVaciarBoleta()
        {
            tope = null;
            Contador = 0;
        }
    }
}


