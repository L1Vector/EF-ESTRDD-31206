using semana7Stack.ConexionDatos;
using semana7Stack.ModuloSimulacionConectivad.Modelo;
using semana7Stack.ModuloVentas.Estructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.ModuloSimulacionConectivad.Estructura
{
    internal class ColaPrioridad
    {

        private PriorityQueue<Transaccion, (int Prioridad, int Orden)> cola =
           new PriorityQueue<Transaccion, (int Prioridad, int Orden)>();

        private int siguienteOrden = 0;

        private HashSet<string> codigosEnCola =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public bool RedDisponible = false;
        public bool ServidorDisponible = true;

        public void ActualizarConexion(bool red, bool servidor)
        {
            RedDisponible = red;
            ServidorDisponible = servidor;
        }

        private int ObtenerPrioridad(Transaccion t)
        {
            string tipo = (t.Tipo ?? "").Trim();

            if (tipo.Equals("Extorno", StringComparison.OrdinalIgnoreCase))
                return 0;

            if (tipo.Equals("Reversion", StringComparison.OrdinalIgnoreCase) ||
                tipo.Equals("Reversión", StringComparison.OrdinalIgnoreCase))
                return 1;

            return 2;
        }

        public void Agregar(Transaccion t)
        {
            if (t == null)
                return;

            if (!string.IsNullOrWhiteSpace(t.CodigoOperacion))
            {
                if (codigosEnCola.Contains(t.CodigoOperacion))
                    return;

                codigosEnCola.Add(t.CodigoOperacion);
            }

            cola.Enqueue(t, (ObtenerPrioridad(t), siguienteOrden));
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
            bool encontrado = cola.TryDequeue(out t, out _);

            if (encontrado &&
                !string.IsNullOrWhiteSpace(t.CodigoOperacion))
            {
                codigosEnCola.Remove(t.CodigoOperacion);
            }

            return encontrado;
        }

        public int CantidadPendientes()
        {
            return cola.Count;
        }

        public void CargarDesdeHistorialGlobal(ref int idActual)
        {
            if (SesionGlobal.HistorialTransaccionesGlobal == null)
                return;

            foreach (NodoTransaccion item in SesionGlobal.HistorialTransaccionesGlobal)
            {
                if (item == null)
                    continue;

                string estado = (item.Estado ?? "").Trim();

                bool estaPendiente =
                    estado.Equals("PENDIENTE", StringComparison.OrdinalIgnoreCase) ||
                    estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase) ||
                    estado.Equals("PROCESANDO", StringComparison.OrdinalIgnoreCase);

                if (!estaPendiente)
                    continue;

                Transaccion t = new Transaccion(
                    idActual,
                    Convert.ToDecimal(item.Pago),
                    "Cobro",
                    item.Codigo
                );

                t.Estado = "Pendiente";

                int cantidadAntes = cola.Count;

                Agregar(t);

                // Solo incrementar el ID si realmente se agregó.
                if (cola.Count > cantidadAntes)
                {
                    idActual++;
                }
            }

        }
    }
}
