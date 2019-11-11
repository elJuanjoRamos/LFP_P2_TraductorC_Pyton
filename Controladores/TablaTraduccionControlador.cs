using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFP_P2_TraductorC_Pyton.Modelos;

namespace LFP_P2_TraductorC_Pyton.Controladores
{
    class TablaTraduccionControlador
    {
        private readonly static TablaTraduccionControlador instancia = new TablaTraduccionControlador();
        private ArrayList tablaTraducciones = new ArrayList();
        private int id = 1;
        private TablaTraduccionControlador()
        {
        }

        public static TablaTraduccionControlador Instancia
        {
            get
            {
                return instancia;
            }
        }

        public void agregar(string texto, string tipo)
        {
            CadenaTraducida c = new CadenaTraducida(id, texto, tipo);
            tablaTraducciones.Add(c);
            id++;
        }


        public ArrayList getTabla()
        {
            return this.tablaTraducciones;
        }
        public void clearTabla()
        {
            tablaTraducciones.Clear();
        }
    }
}
