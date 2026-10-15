using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.Modelos
{
    internal class Transaccion
    {
        public int Id;
        public decimal Importe;
        public string Tipo;
        public string Estado;
        public int Intentos;

        public Transaccion(int id, decimal importe, string tipo)
        {
            Id = id;
            Importe = importe;
            Tipo = tipo;
            Estado = "Pendiente";
            Intentos = 0;
        }

    }
}
