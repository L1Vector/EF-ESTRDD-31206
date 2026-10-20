using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.Modelos
{
    internal class NodoTransaccion
    {
        public string Codigo { get; set; }
        public double Pago { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public NodoTransaccion Siguiente { get; set; }
        public NodoTransaccion Anterior { get; set; }

        public NodoTransaccion(string codigo, double pago, string tipo, string estado)
        {
            this.Codigo = codigo;
            this.Pago = pago;
            this.Tipo = tipo;
            this.Estado = estado;
            this.Siguiente = null;
            this.Anterior = null;
        }
    }
}
