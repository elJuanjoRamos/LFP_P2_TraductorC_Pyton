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
            TraductorControlador.Instancia.clearTokensTraducidos();

            ArrayList a = TokenControlador.Instancia.getArrayListTokens();


            Console.WriteLine(a.Count);
            ArrayList b = TokenControlador.Instancia.getArrayListErrors();

            if (b.Count == 0)
            {

                AnalizadorSintactico.Instancia.obtenerLista(a);


                TraductorControlador.Instancia.obtenerLista(a);
                this.consolaTexto.Text = "";
                this.consolaTexto.AppendText(AnalizadorSintactico.Instancia.returnT());
                this.richTraduccion.Text = "";
                richTraduccion.AppendText(TraductorControlador.Instancia.getTokensTraducidos());
            }
            else
            {
                this.consolaTexto.AppendText("Exiten errores lexicos");
            }
        }
        public string textoMostrar = "";
        private void MaterialFlatButton1_Click(object sender, EventArgs e)
        {
            /*ArrayList ar = TraductorControlador.Instancia.getTokensTraducidos();

            var cadena = "";
            for (int i = 0; i < ar.Count; i++)
            {
                richTraduccion.AppendText(ar[i].ToString());
            }*/
        }

    }
}
