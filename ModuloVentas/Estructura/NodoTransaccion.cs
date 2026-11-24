using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.ModuloVentas.Estructura
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
            Codigo = codigo;
            Pago = pago;
            Tipo = tipo;
            Estado = estado;
            Siguiente = null;
            Anterior = null;
        }
    }
}
