using System;
using semana7Stack.Modelos;

namespace semana7Stack.Estructuras
{
    internal class ListaDobleValidacion
    {
        public NodoTransaccion cabeza { get; set; }
        public NodoTransaccion cola { get; set; }

        private static int contadorCorrelativo = 5000;

        public ListaDobleValidacion()
        {
            cabeza = null;
            cola = null;
        }

        // Se recibe el tipo de pago seleccionado en la interfaz
        public bool agregarAlInicio(string tipoPago)
        {
            int montoAPagar = SesionGlobal.CostoTotalActual;

            string codigoGenerado;
            do
            {
                codigoGenerado = "R" + contadorCorrelativo;
                contadorCorrelativo++;
            }
            while (existeCodigo(codigoGenerado));

            NodoTransaccion nuevo = new NodoTransaccion(codigoGenerado, montoAPagar, tipoPago, "PENDIENTE");

            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                nuevo.Siguiente = cabeza;
                cabeza.Anterior = nuevo;
                cabeza = nuevo;
            }
            return true;
        }

        private bool existeCodigo(string codigo)
        {
            NodoTransaccion actual = cabeza;
            while (actual != null)
            {
                if (actual.Codigo == codigo)
                    return true;
                actual = actual.Siguiente;
            }
            return false;
        }

        public bool eliminar(string codigo)
        {
            NodoTransaccion actual = cabeza;
            while (actual != null)
            {
                if (actual.Codigo == codigo)
                {
                    if (cabeza == cola)
                    {
                        cabeza = null;
                        cola = null;
                    }
                    else if (actual == cabeza)
                    {
                        cabeza = cabeza.Siguiente;
                        cabeza.Anterior = null;
                    }
                    else if (actual == cola)
                    {
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
            cabeza = null;
            cola = null;
        }
    }
}