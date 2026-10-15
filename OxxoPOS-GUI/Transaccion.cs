using System;

namespace Examen_final_parte_que_me_toco
{
    public class Transaccion
    {
        public string IdOperacion { get; set; }
        public string TipoOperacion { get; set; }
        public decimal Monto { get; set; }
        public DateTime Timestamp { get; set; }
        public string Estado { get; set; }

        public Transaccion(string id, string tipo, decimal monto, string estado)
        {
            IdOperacion = id;
            TipoOperacion = tipo;
            Monto = monto;
            Timestamp = DateTime.Now;
            Estado = estado;
        }

        public override string ToString()
        {
            return $"[{IdOperacion}] {TipoOperacion} - S/. {Monto:N2} | Estado: {Estado} ({Timestamp:HH:mm:ss})";
        }
    }
}