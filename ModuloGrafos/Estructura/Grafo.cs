using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.ModuloGrafos.Estructura
{
    internal class Grafo
    {
        public List<Vertice> Vertices { get; set; }
        public List<Arista> Aristas { get; set; }

        public Grafo()
        {
            Vertices = new List<Vertice>();
            Aristas = new List<Arista>();
        }

        public bool AgregarVertice(string nombre, Point posicion, string estado)
        {
            if (Vertices.Any(v => v.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))) // Verificar si el nombre del vértice ya existe
            {
                return false;
            }
            else
            {
                Vertices.Add(new Vertice(nombre, posicion, estado));
                return true;
            }
        }

        public void EliminarVertice(Vertice vertice)
        {
            Vertices.Remove(vertice);
            Aristas.RemoveAll(a => a.Origen == vertice || a.Destino == vertice);
        }

        public bool AgregarArista(Vertice origen, Vertice destino, int peso)
        {
            if (Aristas.Any(a => a.Origen == origen && a.Destino == destino)) // Verificar si la arista ya existe
            {
                return false;
            }
            else
            {
                Aristas.Add(new Arista(origen, destino, peso));
                Aristas.Add(new Arista(destino, origen, peso));
                return true;
            }
        }
    }
}
