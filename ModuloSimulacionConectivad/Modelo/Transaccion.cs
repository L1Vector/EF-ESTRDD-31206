
using System;

namespace semana7Stack.ModuloSimulacionConectivad.Modelo
{
    internal class Transaccion
    {
        public int Id;
        public decimal Importe;
        public string Tipo;
        public string Estado;
        public int Intentos;

        // Código de la operación original: R5000, R5001...
        public string CodigoOperacion;

        // Nombre real de la pasarela que procesa el pago.
        public string Pasarela;

        public Transaccion(
            int id,
            decimal importe,
            string tipo,
            string codigoOperacion = "")
        {
            Id = id;
            Importe = importe;
            Tipo = tipo;
            CodigoOperacion = codigoOperacion;

            Estado = "Pendiente";
            Intentos = 0;
            Pasarela = "";
        }
    }
}
