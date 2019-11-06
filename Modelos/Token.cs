using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_P2_TraductorC_Pyton.Modelos
{
    class Token
    {
        private int idToken;
        private String lexema;
        private String descripcion;
        private int columna;
        private int fila;

        public Token(int idToken, string lexema, string descripcion, int columna, int fila)
        {
            this.IdToken = idToken;
            this.Lexema = lexema;
            this.Descripcion = descripcion;
            this.Columna = columna;
            this.Fila = fila;
        }

        public Token()
        {
        }

        //Setters and Getters
        public int IdToken { get => idToken; set => idToken = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Columna { get => columna; set => columna = value; }
        public int Fila { get => fila; set => fila = value; }
    }
}
