using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using LFP_P2_TraductorC_Pyton.Controladores;
using LFP_P2_TraductorC_Pyton.AnalizadorLex;
using LFP_P2_TraductorC_Pyton.AnalizadorSint;
using LFP_P2_TraductorC_Pyton.Modelos;
namespace LFP_P2_TraductorC_Pyton
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Analizar_Click(object sender, EventArgs e)
        {
            TokenControlador.Instancia.clearListaTokens();

            AnalizadorLexico.Instancia.analizador_Lexico(textAnalizar.Text);
            ArrayList a = TokenControlador.Instancia.getArrayListTokens();


            ArrayList b = TokenControlador.Instancia.getArrayListErrors();

            if (b.Count == 0)
            {

                AnalizadorSintactico.Instancia.obtenerLista(a);
                this.consolaTexto.Text = "";
                this.consolaTexto.AppendText(AnalizadorSintactico.Instancia.returnT());

            }
            else
            {
                this.consolaTexto.AppendText("Exiten errores lexicos");
            }
        }

        private void MaterialFlatButton1_Click(object sender, EventArgs e)
        {
            ArrayList ar = TablaTraduccionControlador.Instancia.getTabla();

            Console.WriteLine("----------------------------------");
            Console.WriteLine("ID  l      Lexema     l   Valor  ");
            Console.WriteLine("----------------------------------");
            var cadena = "";
            for (int i = 0; i < ar.Count; i++)
            {
                TablaTraduccion t = (TablaTraduccion)ar[i];
                if (t.Tipo.Equals("variable") || t.Tipo.Equals("arreglo"))
                {
                    cadena = t.Lexema + " = " + t.Valor + "\n";
                }                
                richTraduccion.AppendText(cadena);

                Console.WriteLine( t.IdToken + "   l    " + t.Lexema + "             l   " + t.Valor);

            }
        }
    }
}
