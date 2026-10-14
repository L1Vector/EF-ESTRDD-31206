using semana7Stack.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.Estructuras
{
    internal class ListaDobleValidacion
    {
        public NodoTransaccion cabeza { get; set; }
        public NodoTransaccion cola { get; set; }

        public ListaDobleValidacion()
        {
            cabeza = null;
            cola = null;
        }

        public bool agregarAlInicio(string valor)
        {
            NodoTransaccion duplicado = cabeza;
            NodoTransaccion nuevo = new NodoTransaccion(valor);
            while (duplicado != null)
            {
                if (duplicado.Valor == nuevo.Valor)
                {
                    return false;
                }
                duplicado = duplicado.Siguiente;
            }

            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                // Si hay un nodo con el mismo código, no se permite agregarlo y se devuelve 2
                nuevo.Siguiente = cabeza;
                cabeza.Anterior = nuevo;
                cabeza = nuevo;
            }
            return true;

        }
        public bool agregarAlFinal(string valor)
        {
            NodoTransaccion nuevo = new NodoTransaccion(valor);
            NodoTransaccion duplicado = cabeza;
            while (duplicado != null)
            {
                if (duplicado.Valor == nuevo.Valor)
                {
                    return false;
                }
                duplicado = duplicado.Siguiente;
            }

            if (cola == null)
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
            return true;

        }
        public bool eliminar(string valor)
        {
            NodoTransaccion actual = cabeza;
            while (actual != null)
            {
                if (actual.Valor == valor)
                {
                    //1. Cuando existe un solo nodo
                    if (cabeza == cola)
                    {
                        cabeza = null;
                        cola = null;
                    }
                    else if (actual == cabeza)
                    {
                        // 2. Cuando se elimina la cabeza
                        cabeza = cabeza.Siguiente;
                        cabeza.Anterior = null;
                    }
                    else if (actual == cola)
                    {
                        //3. Cuando se elimina la cola
                        cola = cola.Anterior;
                        cola.Siguiente = null;
                    }
                    else
                    {
                        actual.Anterior.Siguiente = actual.Siguiente;
                        actual.Siguiente.Anterior = actual.Anterior;
                    }
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }
        public bool buscar(string valor)
        {
            NodoTransaccion busca = cabeza;
            while (busca != null)
            {
                if (busca.Valor == valor)
                { return true; }
                busca = busca.Siguiente;
            }
            return false;
        }
        public void limpiar()
        {
            cola = null;
            cabeza = cola;
        }
        public List<string> imprimirAtras()
        {
            List<string> elementos = new List<string>();
            NodoTransaccion actual = cola;
            while (actual != null)
            {
                elementos.Add(actual.Valor);
                actual = actual.Anterior;
            }
            return elementos;
        }
        public List<string> imprimirAdelante()
        {
            List<string> elementos = new List<string>();
            NodoTransaccion actual = cabeza;
            while (actual != null)
            {
                elementos.Add(actual.Valor);
                actual = actual.Siguiente;
            }
            return elementos;
        }
    }
}
