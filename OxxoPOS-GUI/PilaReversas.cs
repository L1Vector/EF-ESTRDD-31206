using System;
using System.Collections.Generic;

namespace Examen_final_parte_que_me_toco
{
    public class PilaReversas
    {
        private Stack<Transaccion> pila = new Stack<Transaccion>();

        public void ApilarReversa(Transaccion t) => pila.Push(t);

        public Transaccion DesapilarReversa()
        {
            if (pila.Count > 0) return pila.Pop();
            throw new InvalidOperationException("No hay reversas.");
        }

        public List<Transaccion> ListarElementos() => new List<Transaccion>(pila);
    }
}