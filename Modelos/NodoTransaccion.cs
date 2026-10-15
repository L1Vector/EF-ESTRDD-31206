using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.Modelos
{
    internal class NodoTransaccion
    {
        public NodoTransaccion(string codigo, double pago)
        {
            string Codigo = codigo;
            double Pago = pago;
            Siguiente = null;
            Anterior = null;
        }
        public string Codigo { get; set; }
        public double Pago { get; set; }
        public NodoTransaccion Siguiente { get; set; }
        public NodoTransaccion Anterior { get; set; }

    }
}
