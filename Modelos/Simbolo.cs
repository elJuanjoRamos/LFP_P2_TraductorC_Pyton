using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_P2_TraductorC_Pyton.Modelos
{
    class Simbolo
    {
        private int idSimbolo;
        private string nombre;
        private string valor;
        private string tipo;

        public Simbolo(int idSimbolo, string nombre, string valor, string tipo)
        {
            this.IdSimbolo = idSimbolo;
            this.Nombre = nombre;
            this.Valor = valor;
            this.Tipo = tipo;
        }

        public int IdSimbolo { get => idSimbolo; set => idSimbolo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Valor { get => valor; set => valor = value; }
        public string Tipo { get => tipo; set => tipo = value; }
    }
}
