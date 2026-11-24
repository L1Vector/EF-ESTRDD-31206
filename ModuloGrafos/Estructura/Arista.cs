using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.ModuloGrafos.Estructura
{
    internal class Arista
    {
        public Vertice Origen { get; set; }
        public Vertice Destino { get; set; }
        public int Peso { get; set; } // Latencia de la conexión en milisegundos

        public Arista(Vertice origen, Vertice destino, int peso)
        {
            Origen = origen;
            Destino = destino;
            Peso = peso;
        }

    }
}
