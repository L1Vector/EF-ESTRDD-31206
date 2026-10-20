using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.Modelos
{
    public class Voucher
    {
        public string IdOperacion { get; set; }
        public string TipoOperacion { get; set; }
        public double Monto { get; set; }
        public DateTime Timestamp { get; set; }
        public string Estado { get; set; }

        public Voucher(string id, string tipo, double monto, string estado)
        {
            IdOperacion = id;
            TipoOperacion = tipo;
            Monto = monto;
            Timestamp = DateTime.Now;
            Estado = "COMPLETADO";
        }

        public override string ToString()
        {
            return $"[{IdOperacion}] ({Timestamp:HH:mm:ss}) | {TipoOperacion} - S/. {Monto:N2} | Estado: {Estado} ";
        }
    }

    public class NodoVoucher
    {
        public Voucher Dato { get; set; }
        public NodoVoucher Siguiente { get; set; }
        public NodoVoucher Anterior { get; set; }

        public NodoVoucher(Voucher voucher)
        {
            Dato = voucher;
            Siguiente = null;
            Anterior = null;
        }
    }
}
