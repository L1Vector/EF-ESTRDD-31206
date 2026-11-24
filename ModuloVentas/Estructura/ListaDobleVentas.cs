using System;
using System.Collections.Generic;
using semana7Stack.ConexionDatos;

namespace semana7Stack.ModuloVentas.Estructura
{
    // Administra dos listas doblemente enlazadas: una para los pagos registrados y otra para los vouchers.
    internal class ListaDobleVentas
    {
        // Primer y último nodo de la lista.
        public NodoTransaccion cabeza { get; set; }
        public NodoTransaccion cola { get; set; }

        // Correlativo para generar códigos únicos de pago.
        private static int contadorCorrelativo = 5000;

        // Inicializa la lista vacía.
        public ListaDobleVentas()
        {
            cabeza = null;
            cola = null;
        }

        // Obtiene el monto de la sesión y agrega el pago al inicio de la lista.
        public bool agregarAlInicio(string tipoPago)
        {
            int montoAPagar = SesionGlobal.CostoTotalActual;

            string codigoGenerado;

            // Genera un código que no exista previamente.
            do
            {
                codigoGenerado = "R" + contadorCorrelativo;
                contadorCorrelativo++;
            }
            while (existeCodigo(codigoGenerado));

            NodoTransaccion nuevo = new NodoTransaccion(
                codigoGenerado,
                montoAPagar,
                tipoPago,
                "Aceptado"
            );

            // Si está vacía, el nuevo nodo será cabeza y cola.
            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                // Inserta el nuevo nodo antes de la cabeza actual.
                nuevo.Siguiente = cabeza;
                cabeza.Anterior = nuevo;
                cabeza = nuevo;
            }

            return true;
        }

        // Comprueba si un código ya está registrado.
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

        // Busca un pago por código y lo elimina de la lista.
        public bool eliminar(string codigo)
        {
            NodoTransaccion actual = cabeza;

            while (actual != null)
            {
                if (actual.Codigo == codigo)
                {
                    // Caso 1: la lista contiene un único nodo.
                    if (cabeza == cola)
                    {
                        cabeza = null;
                        cola = null;
                    }
                    // Caso 2: se elimina la cabeza.
                    else if (actual == cabeza)
                    {
                        cabeza = cabeza.Siguiente;
                        cabeza.Anterior = null;
                    }
                    // Caso 3: se elimina la cola.
                    else if (actual == cola)
                    {
                        cola = cola.Anterior;
                        cola.Siguiente = null;
                    }
                    // Caso 4: se elimina un nodo intermedio.
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

        // Elimina todos los registros de pagos.
        public void limpiar()
        {
            cabeza = null;
            cola = null;
        }

    }
}