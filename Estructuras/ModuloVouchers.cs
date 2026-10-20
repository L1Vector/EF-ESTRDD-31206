using semana7Stack.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semana7Stack.Estructuras
{
    internal class ModuloVouchers
    {
        public NodoVoucher Cabeza { get; private set; }
        public NodoVoucher Cola { get; private set; }
        public NodoVoucher Actual { get; private set; }

        public ModuloVouchers()
        {
            Cabeza = null;
            Cola = null;
            Actual = null;
        }

        public void Agregar(Voucher voucher)
        {
            NodoVoucher nuevo = new NodoVoucher(voucher);

            if (Cabeza == null)
            {
                Cabeza = nuevo;
                Cola = nuevo;
            }
            else
            {
                Cola.Siguiente = nuevo;
                nuevo.Anterior = Cola;
                Cola = nuevo;
            }

            Actual = nuevo;
        }

        public bool MoverSiguiente()
        {
            if (Actual != null && Actual.Siguiente != null)
            {
                Actual = Actual.Siguiente;
                return true;
            }
            return false;
        }

        public bool MoverAnterior()
        {
            if (Actual != null && Actual.Anterior != null)
            {
                Actual = Actual.Anterior;
                return true;
            }
            return false;
        }

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

        public int ObtenerPosicionActual()
        {
            if (Actual == null) return 0;

            int pos = 1;
            NodoVoucher temp = Cabeza;
            while (temp != null && temp != Actual)
            {
                pos++;
                temp = temp.Siguiente;
            }
            return pos;
        }
    }
}
