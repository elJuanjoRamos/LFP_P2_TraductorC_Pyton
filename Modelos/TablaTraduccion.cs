using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_P2_TraductorC_Pyton.Modelos
{
    class TablaTraduccion
    {
        private int idToken;
        private String lexema;
        private String valor;
        private String tipo;

        public TablaTraduccion(int idToken, string lexema, string valor, string tipo)
        {
            this.IdToken = idToken;
            this.Lexema = lexema;
            this.valor= valor;
            this.tipo = tipo;
        }

        public TablaTraduccion()
        {
        }

        //Setters and Getters
        public int IdToken { get => idToken; set => idToken = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public string Valor { get => valor; set => valor= value; }
        public string Tipo { get => tipo; set => tipo = value; }
    }
}
