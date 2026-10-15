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
        int montoAPagar = SesionGlobal.CostoTotalActual;
        public bool agregarAlInicio()
        {
            int contador = 5000;
            string codigo = "R" + contador.ToString();
            NodoTransaccion duplicado = cabeza;
            NodoTransaccion nuevo = new NodoTransaccion(codigo, montoAPagar);
            while (duplicado != null)
            {
                if (duplicado.Codigo == nuevo.Codigo)
                {
                    return false;
                }
                duplicado = duplicado.Siguiente;
                contador++;
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
        public bool eliminar(string codigo)
        {
            NodoTransaccion actual = cabeza;
            while (actual != null)
            {
                if (actual.Codigo == codigo)
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
                elementos.Add(actual.Codigo);
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
                elementos.Add(actual.Codigo);
                actual = actual.Siguiente;
            }
            return elementos;
        }
    }
}
