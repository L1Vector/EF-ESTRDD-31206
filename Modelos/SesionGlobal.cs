using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using semana7Stack.Modelos;

namespace semana7Stack.Modelos
{
    internal class SesionGlobal
    {
        // Variable global para almacenar el costo total del carrito
        public static int CostoTotalActual { get; set; } = 0;

        // Variable global para almacenar el costo total del carrito
        public static string IdentificadorTotem { get; set; } = "TOTEM-OXXO-01";

        //Variable global para almacenar historial de voletas para acumular múltiples transacciones aprobadas/generadas
        public static List<NodoTransaccion> HistorialTransaccionesGlobal { get; set; } = new List<NodoTransaccion>();

    }
    
}


