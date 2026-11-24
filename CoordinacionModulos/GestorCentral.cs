using semana7Stack.ModuloGrafos.Estructura;
using semana7Stack.ModuloHistorialPagos.Estructura;
using semana7Stack.ModuloSimulacionConectivad.Estructura;
using semana7Stack.ModuloVentas.Estructura;
using semana7Stack.Vistas;

namespace semana7Stack.CoordinacionModulos
{
    internal class GestorCentral
    {
        // Módulo de ventas
        public static ListaDobleVentas ventas = new ListaDobleVentas();

        // Módulo de historial de pagos
        public static ListaDobleHistorial historial = new ListaDobleHistorial();

        // Módulo de simulación de conectividad
        public static ColaPrioridad cola = new ColaPrioridad();

        // Módulo de grafos
        public static Grafo grafo = new Grafo();
        public static List<Vertice> rutaResaltada = new List<Vertice>();
    }
}
