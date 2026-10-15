using System;
using System.Collections.Generic;

namespace Examen_final_parte_que_me_toco
{
    public class ColaTransacciones
    {
        private Queue<Transaccion> cola = new Queue<Transaccion>();

        public void Encolar(Transaccion t) => cola.Enqueue(t);

        public Transaccion Desencolar()
        {
            if (cola.Count > 0) return cola.Dequeue();
            throw new InvalidOperationException("La cola está vacía.");
        }

        public List<Transaccion> ListarElementos() => new List<Transaccion>(cola);
    }
}