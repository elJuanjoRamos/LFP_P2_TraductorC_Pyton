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

    class TokenControlador
    {
        private readonly static TokenControlador instancia = new TokenControlador();
        private ArrayList arrayListTokens = new ArrayList();
        private ArrayList arrayListErrors = new ArrayList();
        private int idToken = 1;
        private int idTokenError = 1;
        private TokenControlador()
        {
        }

        public static TokenControlador Instancia
        {
            get
            {
                return instancia;
            }
        }

        public void agregarToken(int fila, int columna, string lexema, string descripcion)
        {
            Token token = new Token(idToken, lexema, descripcion, columna, fila);
            ArrayListTokens.Add(token);
            idToken++;
        }

        public void agregarError(int fila, int columna, string lexema, string descripcion)
        {
            Token token = new Token(idTokenError, lexema, descripcion, columna, fila);
            ArrayListErrors.Add(token);
            idTokenError++;
        }

        public void resetClass()
        {
            ArrayListErrors.Clear();
            ArrayListTokens.Clear();
            idToken = 1;
            idTokenError = 1;
        }

        public ArrayList ArrayListTokens { get => arrayListTokens; set => arrayListTokens = value; }
        public ArrayList ArrayListErrors { get => arrayListErrors; set => arrayListErrors = value; }

    }

}
