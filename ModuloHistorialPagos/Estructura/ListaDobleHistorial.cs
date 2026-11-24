using semana7Stack.ModuloHistorialPagos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.ModuloHistorialPagos.Estructura
{
    internal class ListaDobleHistorial
    {

        // Primer nodo, último nodo y nodo actualmente seleccionado.
        public NodoVoucher Cabeza { get; private set; }
        public NodoVoucher Cola { get; private set; }
        public NodoVoucher Actual { get; private set; }

        // Inicializa la lista de vouchers vacía.
        public ListaDobleHistorial()
        {
            Cabeza = null;
            Cola = null;
            Actual = null;
        }

        // Agrega un voucher al final de la lista.
        public void Agregar(Voucher voucher)
        {
            if (voucher == null)
                return;

            NodoVoucher nuevo = new NodoVoucher(voucher);

            // Si es el primer voucher, será cabeza y cola.
            if (Cabeza == null)
            {
                Cabeza = nuevo;
                Cola = nuevo;
            }
            else
            {
                // Enlaza el nuevo nodo después de la cola actual.
                Cola.Siguiente = nuevo;
                nuevo.Anterior = Cola;
                Cola = nuevo;
            }

            // Selecciona el voucher recién agregado.
            Actual = nuevo;
        }

        // Avanza al siguiente voucher si existe.
        public bool MoverSiguiente()
        {
            if (Actual != null && Actual.Siguiente != null)
            {
                Actual = Actual.Siguiente;
                return true;
            }

            return false;
        }

        // Retrocede al voucher anterior si existe.
        public bool MoverAnterior()
        {
            if (Actual != null && Actual.Anterior != null)
            {
                Actual = Actual.Anterior;
                return true;
            }

            return false;
        }

        // Obtiene todos los vouchers desde la cabeza hasta la cola.
        public List<Voucher> ObtenerTodos()
        {
            List<Voucher> lista = new List<Voucher>();
            NodoVoucher temp = Cabeza;

            while (temp != null)
            {
                lista.Add(temp.Dato);
                temp = temp.Siguiente;
            }

            return lista;
        }

        // Cuenta la cantidad de vouchers registrados.
        public int ObtenerTotalRegistros()
        {
            int total = 0;
            NodoVoucher temp = Cabeza;

            while (temp != null)
            {
                total++;
                temp = temp.Siguiente;
            }

            return total;
        }

        // Obtiene la posición del voucher actual empezando desde 1.
        public int ObtenerPosicionActual()
        {
            if (Actual == null)
                return 0;

            int pos = 1;
            NodoVoucher temp = Cabeza;

            while (temp != null && temp != Actual)
            {
                pos++;
                temp = temp.Siguiente;
            }

            return temp == null ? 0 : pos;
        }
    }
}
