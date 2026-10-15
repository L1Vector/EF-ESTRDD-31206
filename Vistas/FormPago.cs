using semana7Stack.Estructuras;
using semana7Stack.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace semana7Stack.Vistas
{
    public partial class FormPago : Form
    {
        private ListaDobleValidacion lista = new ListaDobleValidacion();
        private NodoTransaccion nodoActual = null;

        public FormPago()
        {
            InitializeComponent();
            actualizarInterfaz();
        }
        private void actualizarInterfaz()
        {
            lstLista.Items.Clear();
            NodoTransaccion actual = lista.cabeza;
            while (actual != null)
            {
                if (nodoActual != null && actual == nodoActual)
                {
                    lstLista.Items.Add($"[ {actual.Codigo} ]");
                }

                actual = actual.Siguiente;
            }
        }
        private bool obtenerValor(out string valor)
        {
            if (txtCódigo.Text != "")
            {
                valor = txtCódigo.Text;
                return true;
            }
            MessageBox.Show("Por favor, ingrese un valor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            valor = null;
            return false;
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (obtenerValor(out string valor))
            {
                bool resp = lista.agregarAlInicio();
                if (resp == false)
                {
                    MessageBox.Show("El código ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Código insertado con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                actualizarInterfaz();
                txtCódigo.Clear();
            }

        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (obtenerValor(out string codigo))
            {
                if (lista.eliminar(codigo))
                {
                    MessageBox.Show($"Código [{codigo}] eliminado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show($"Código [{codigo}] no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                actualizarInterfaz();
                txtCódigo.Clear();
            }

        }
        private void btnAdelante_Click(object sender, EventArgs e)
        {
            if (nodoActual == null && lista.cabeza != null)
            {
                nodoActual = lista.cabeza;
            }

            else if (nodoActual != null && nodoActual.Anterior != null)
            {
                nodoActual = nodoActual.Anterior;
            }
            else
            {
                return;
            }

            actualizarInterfaz();
        }
        private void btnAtras_Click(object sender, EventArgs e)
        {
            if (nodoActual == null && lista.cabeza != null)
            {
                nodoActual = lista.cabeza;
            }

            else if (nodoActual != null && nodoActual.Siguiente != null)
            {
                nodoActual = nodoActual.Siguiente;
            }
            actualizarInterfaz();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lista.limpiar();
            actualizarInterfaz();
        }
    }
}
