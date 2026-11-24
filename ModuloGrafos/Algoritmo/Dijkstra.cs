
using semana7Stack.ModuloGrafos.Estructura;
using System;
using System.Collections.Generic;
using System.Linq;

namespace semana7Stack.ModuloGrafos.Algoritmo
{
    internal class Dijkstra
    {
        /// <summary>
        /// Calcula el camino de menor latencia entre dos vértices activos.
        /// Ignora vértices inactivos y pesos negativos.
        /// </summary>
        public static (List<Vertice> Recorrido, int LatenciaTotal) CalcularRutaCorta(Grafo grafo, Vertice inicio, Vertice fin)
        {
            if (grafo == null || inicio == null || fin == null)
                return (new List<Vertice>(), int.MaxValue);

            // Obtener únicamente los vértices activos.
            List<Vertice> verticesActivos = grafo.Vertices
                .Where(v => string.Equals(v.Estado,"Activo",StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Verificar que el inicio y el destino estén activos.
            if (!verticesActivos.Contains(inicio) || !verticesActivos.Contains(fin))
            {
                return (new List<Vertice>(), int.MaxValue);
            }

            var latencias = new Dictionary<Vertice, int>();
            var previos = new Dictionary<Vertice, Vertice>();
            var noVisitados = new HashSet<Vertice>(verticesActivos);

            foreach (Vertice vertice in verticesActivos)
            {
                latencias[vertice] = int.MaxValue;
                previos[vertice] = null;
            }

            latencias[inicio] = 0;

            while (noVisitados.Count > 0)
            {
                // Seleccionar el vértice con menor latencia.
                Vertice actual = noVisitados
                    .OrderBy(v => latencias[v])
                    .First();

                if (latencias[actual] == int.MaxValue)
                    break;

                noVisitados.Remove(actual);

                if (actual == fin)
                    break;

                // Revisar las conexiones del vértice actual.
                foreach (var arista in grafo.Aristas.Where(a => a.Origen == actual))
                {
                    Vertice vecino = arista.Destino;

                    // Ignorar vértices inactivos y pesos negativos.
                    if (!noVisitados.Contains(vecino) ||  arista.Peso < 0)
                    {
                        continue;
                    }

                    // Evitar desbordamiento.
                    if (latencias[actual] >
                        int.MaxValue - arista.Peso)
                    {
                        continue;
                    }

                    int latenciaAlternativa = latencias[actual] + arista.Peso;

                    if (latenciaAlternativa < latencias[vecino])
                    {
                        latencias[vecino] = latenciaAlternativa;
                        previos[vecino] = actual;
                    }
                }
            }

            // Comprobar si existe una ruta.
            if (latencias[fin] == int.MaxValue)
                return (new List<Vertice>(), int.MaxValue);

            // Reconstruir el recorrido.
            var recorrido = new List<Vertice>();
            Vertice paso = fin;

            while (paso != null)
            {
                recorrido.Insert(0, paso);

                if (paso == inicio)
                    break;

                if (!previos.TryGetValue(paso,out Vertice anterior))
                {
                    return (new List<Vertice>(), int.MaxValue);
                }

                paso = anterior;
            }

            if (recorrido.Count == 0 || recorrido[0] != inicio)
            {
                return (new List<Vertice>(), int.MaxValue);
            }

            return (recorrido, latencias[fin]);
        }
    }
}
