using semana7Stack.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.Estructuras
{
    internal class GestorReintentos
    {
        private PriorityQueue<Transaccion, (int Prioridad, int Orden)> cola =
           new PriorityQueue<Transaccion, (int Prioridad, int Orden)>();

        private int siguienteOrden = 0;

        public bool RedDisponible = false;
        public bool ServidorDisponible = true;

        public void ActualizarConexion(bool red, bool servidor)
        {
            RedDisponible = red;
            ServidorDisponible = servidor;
        }

        private int ObtenerPrioridad(Transaccion t)
        {
            if (t.Tipo == "Extorno")
                return 0;

            if (t.Tipo == "Reversion")
                return 1;

            return 2;
        }

        public void Agregar(Transaccion t)
        {
            int prioridad = ObtenerPrioridad(t);

            cola.Enqueue(t, (prioridad, siguienteOrden));
            siguienteOrden++;
        }

        public List<Transaccion> ObtenerPendientesOrdenados()
        {
            return cola.UnorderedItems
                .OrderBy(item => item.Priority.Prioridad)
                .ThenBy(item => item.Priority.Orden)
                .Select(item => item.Element)
                .ToList();
        }

        public bool IntentarObtenerSiguiente(out Transaccion t)
        {
            return cola.TryDequeue(out t, out _);
        }

        public int CantidadPendientes()
        {
            return cola.Count;
        }

        public void CargarDesdeHistorialGlobal(ref int idActual)
        {
            if (SesionGlobal.HistorialTransaccionesGlobal != null)
            {
                foreach (var item in SesionGlobal.HistorialTransaccionesGlobal)
                {
                    // Usamos el idActual recibido y luego lo incrementamos
                    Transaccion t = new Transaccion(idActual, (decimal)item.Pago, item.Tipo = "Reversión");
                    idActual++; // Incrementa para la siguiente transacción

                    t.Estado = item.Estado;

                    cola.Enqueue(t, (ObtenerPrioridad(t), siguienteOrden));
                    siguienteOrden++;
                }

                SesionGlobal.HistorialTransaccionesGlobal.Clear();
            }
        }
    }
}
