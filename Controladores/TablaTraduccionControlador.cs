using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using LFP_P2_TraductorC_Pyton.Modelos;

namespace LFP_P2_TraductorC_Pyton.Controladores
{
    class TablaTraduccionControlador
    {
        private readonly static TablaTraduccionControlador instancia = new TablaTraduccionControlador();
        private ArrayList tablaSimbolos = new ArrayList();
        private int idToken = 1;
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

        public void agregar(string lexema, string valor, string tipo)
        {
            TablaTraduccion t = new TablaTraduccion(idToken, lexema, valor, tipo);
            tablaSimbolos.Add(t);
            idToken++;
        }


        public ArrayList getTabla()
        {
            return this.tablaSimbolos;
        }

    }
}
