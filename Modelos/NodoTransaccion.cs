using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.Modelos
{
    internal class NodoTransaccion
    {
        public NodoTransaccion(string valor)
        {
            Valor = valor;
            Siguiente = null;
            Anterior = null;
        }
        public string Valor { get; set; }
        public NodoTransaccion Siguiente { get; set; }
        public NodoTransaccion Anterior { get; set; }

    }
}
