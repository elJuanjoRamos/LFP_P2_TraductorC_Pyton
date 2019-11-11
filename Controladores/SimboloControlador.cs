using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFP_P2_TraductorC_Pyton.Modelos;
using LFP_P2_TraductorC_Pyton.Controladores;
using System.Windows.Forms;

namespace LFP_P2_TraductorC_Pyton.Controladores
{
    class SimboloControlador
    {
        private readonly static SimboloControlador instancia = new SimboloControlador();
        private ArrayList arrayListSimbolo = new ArrayList();
        private int idSimbolo = 1;

        private SimboloControlador()
        {
        }

        public static SimboloControlador Instancia
        {
            get
            {
                return instancia;
            }
        }

        public ArrayList ArrayListSimbolo { get => arrayListSimbolo; set => arrayListSimbolo = value; }

        public void agregarSimbolo(string nombre, string valor, string tipo)
        {
            Simbolo simbolo = new Simbolo(idSimbolo, nombre, valor, tipo);
            ArrayListSimbolo.Add(simbolo);
            idSimbolo++;
        }

        public Simbolo buscarSimbolo(string nombre)
        {
            foreach (Simbolo s in ArrayListSimbolo)
            {
                if (s.Nombre.Equals(nombre))
                {
                    return s;
                }
            }
            return null;
        }

        public Boolean buscar(string nombre)
        {
            foreach (Simbolo s in ArrayListSimbolo)
            {
                if (s.Nombre.Equals(nombre))
                {
                    return true;
                }
            }
            return false;
        }

        public void editarSimbolo(string nombre, string valor)
        {
            foreach (Simbolo s in ArrayListSimbolo)
            {
                if (s.Nombre.Equals(nombre))
                {
                    s.Valor = valor;
                }
            }
        }

        public void alertMessage(String mensaje)
        {
            MessageBox.Show(mensaje, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void recorrerTokens(ArrayList arrayList)
        {

        }

        public void clearArrayListSimbolo()
        {
            this.idSimbolo = 1;
            this.arrayListSimbolo.Clear();
        }
    }
}
