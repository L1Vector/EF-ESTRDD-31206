using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.ModuloGrafos.Estructura
{
    internal class Vertice
    {
        public string Nombre { get; set; }
        public Point Posicion { get; set; }
        public string Estado { get; set; } // Guarda condicion del vertice

        public Vertice(string nombre, Point posicion, string estado)
        {
            Nombre = nombre;
            Posicion = posicion;
            Estado = estado;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
