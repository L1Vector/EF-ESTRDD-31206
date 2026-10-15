using System.Collections.Generic;

namespace Examen_final_parte_que_me_toco
{
    public class NodoDoble
    {
        public Transaccion Dato { get; set; }
        public NodoDoble Siguiente { get; set; }
        public NodoDoble Anterior { get; set; }

        public NodoDoble(Transaccion dato) => Dato = dato;
    }

    public class ListaEnlazadaDoble
    {
        private NodoDoble cabeza;
        private NodoDoble cola;

        public void Insertar(Transaccion t)
        {
            NodoDoble nuevo = new NodoDoble(t);
            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                cola.Siguiente = nuevo;
                nuevo.Anterior = cola;
                cola = nuevo;
            }
        }

        public List<Transaccion> ObtenerRecorridoAdelante()
        {
            List<Transaccion> lista = new List<Transaccion>();
            NodoDoble actual = cabeza;
            while (actual != null)
            {
                lista.Add(actual.Dato);
                actual = actual.Siguiente;
            }
            return lista;
        }
    }
}