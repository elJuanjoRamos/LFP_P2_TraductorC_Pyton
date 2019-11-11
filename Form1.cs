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
using System.IO;

namespace LFP_P2_TraductorC_Pyton
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        //variables globales
        public string nombreArc = "";
        public string tempNombreArc = "";
        public string cadenaInicio = "";
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        int ambito = 0;
        string tab = "";

        private void Analizar_Click(object sender, EventArgs e)
        {
            TokenControlador.Instancia.clearListaTokensError();
            TokenControlador.Instancia.clearListaTokens();
            TraductorControlador.Instancia.clearTokensTraducidos();
            TablaTraduccionControlador.Instancia.clearTabla();

            if (nombreArc == "")
            {
                tempNombreArc = "escrito";
            }
            if (textAnalizar.Text != "")
            {

                AnalizadorLexico.Instancia.analizador_Lexico(textAnalizar.Text);
                ArrayList a = TokenControlador.Instancia.getArrayListTokens();
                ArrayList b = TokenControlador.Instancia.getArrayListErrors();
                ArrayList arrayTraduccion = TablaTraduccionControlador.Instancia.getTabla();



                if (b.Count == 0)
                {
                    AnalizadorSintactico.Instancia.obtenerLista(a);
                    TraductorControlador.Instancia.obtenerLista(a);
                    this.consolaTexto.Text = "";
                    this.consolaTexto.AppendText(AnalizadorSintactico.Instancia.returnT());
                    this.richTraduccion.Text = "";
                    Console.WriteLine("el arreglo traduccion es " + arrayTraduccion.Count);
                    for (int i = 0; i < arrayTraduccion.Count; i++)
                    {

                        CadenaTraducida texto = (CadenaTraducida)arrayTraduccion[i];


                        richTraduccion.AppendText(texto.Cadena + "\n");


                        /*if (texto.Tipo.Equals("for") || texto.Tipo.Equals("if") || 
                            texto.Tipo.Equals("switch") || texto.Tipo.Equals("while") || 
                            texto.Tipo.Equals("else") || texto.Tipo.Equals("variable")
                            || texto.Tipo.Equals("array") || texto.Tipo.Equals("case"))
                        {

                        }

                        if (texto.Tipo.Equals("#"))
                        {
                            ambito++;
                            richTraduccion.AppendText(texto.Tipo + "\n");

                            for (int j = 0; j <= ambito; j++)
                            {
                                tab = tab + "\x020" + "\x020" + "\x020";
                            }
                            string textoInterior = "";
                            for (int k = i+1; k < arrayTraduccion.Count; k++)
                            {
                                texto = ((CadenaTraducida)arrayTraduccion[k]);
                                if (texto.Tipo.Equals("#"))
                                {
                                    i = k-1;
                                    tab="";
                                    richTraduccion.AppendText(textoInterior + "\n");
                                    break;
                                }
                                else
                                {
                                    textoInterior = textoInterior + "\n" + tab + texto.Cadena;
                                }
                            }
                            Console.WriteLine();
                            }  
                        else
                        {
                            
                        }*/
                    }
                    


            }
            else
            {
                this.consolaTexto.AppendText("Exiten errores lexicos");
            }

            }
            else
            {
                alertMessage("No se ha detectado ningun texto"); 
            }

        }
        public void Tabular1(String texto)
        {
            
        }
        public int index = 0;
        public void Tabular2(string texto)
        {
            ArrayList arrayTraduccion = TablaTraduccionControlador.Instancia.getTabla();
            string txt = "";
           
            if (texto.Equals("cuerpoSwitch"))
            {

                for (int m = index+1; m < arrayTraduccion.Count; m++)
                {
                    if (((CadenaTraducida)arrayTraduccion[m]).Tipo == "cuerpoSwitch")
                    {
                        richTraduccion.AppendText(cadenaInicio + "\n");
                        Console.WriteLine("la cadena de inicio es " + cadenaInicio);
                        index = m;
                        break;
                    }
                    else
                    {
                        cadenaInicio = cadenaInicio + "\n" + tab + ((CadenaTraducida)arrayTraduccion[m]).Cadena;
                    }
                }
                /*string tabs = "";

                for (int j = 0; j <= ambito; j++)
                {
                    tabs = tabs + "\x020" + "\x020" + "\x020" + "\x020";
                }*/
                //string cuerpoExpresion = "";

            }
            Tabular2(txt);

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

        public void alertMessage(String mensaje)
        {
            MessageBox.Show(mensaje, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #region MENU ARCHIVO
        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "cs files (*.cs)|*.cs";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                nombreArc = Path.GetFileName(filePath);
                tempNombreArc = nombreArc;
            }

            if (File.Exists(filePath))
            {
                StreamReader streamReader = new StreamReader(filePath);
                string line;
                try
                    {
                        line = streamReader.ReadLine();
                        while (line != null)
                        {
                            textAnalizar.AppendText(line + "\n");
                            line = streamReader.ReadLine();
                        }
                    }
                    catch (Exception)
                    {
                        alertMessage("Ha ocurrido un error.");
                    }
                
                streamReader.Close();
            }
        }
        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(nombreArc))
            {
                String dir = nombreArc;
                StreamWriter streamWriter = new StreamWriter(@dir);
                try
                {
                    
                        try
                        {
                            streamWriter.WriteLine(textAnalizar.Text);
                            streamWriter.WriteLine("\n");
                        }
                        catch (Exception)
                        {
                            alertMessage("Ha ocurrido un error D:");
                        }
                    
                }
                catch (Exception) { }
                streamWriter.Close();
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save Org Files";
                saveFileDialog.DefaultExt = "cs";
                saveFileDialog.Filter = "Cs files (*.cs)|*.cs";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = nombreArc;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String dir = saveFileDialog.FileName;
                    StreamWriter streamWriter = new StreamWriter(@dir);
                    nombreArc = dir;
                    try
                    {
                            try
                            {
                                streamWriter.WriteLine(textAnalizar.Text);
                                streamWriter.WriteLine("\n");
                            }
                            catch (Exception)
                            {
                                alertMessage("Ha ocurrido un error D:");

                            }
                        
                    }
                    catch
                    {
                        alertMessage("Ha ocurrido un error D:");
                    }
                    streamWriter.Close();
                }
            }
        }

        #endregion

        #region MENU DOCUMENTO

        private void ReporteTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nombreArc == "" && tempNombreArc == "")
            {
                TokenControlador.Instancia.ImprimirVacia("Archivo");
            }
            else if (!(nombreArc == "") && (nombreArc == tempNombreArc))
            {
                TokenControlador.Instancia.ImprimirTokens(nombreArc);
            }
            else
            {
                TokenControlador.Instancia.ImprimirTokens("NoArchivo");
            }
        }

        private void ReporteErroresLexicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nombreArc == "" && tempNombreArc == "")
            {
                TokenControlador.Instancia.ImprimirVacia("Archivo");
            }
            else if (!(nombreArc == "") && (nombreArc == tempNombreArc))
            {
                TokenControlador.Instancia.ImprimirErrores(nombreArc);
            }
            else
            {
                TokenControlador.Instancia.ImprimirErrores("NoArchivo");
            }
        }
        private void LimpiaDocumentosRecientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nombreArc = "";
            tempNombreArc = "";
            ambito = 0;
            tab = "";
            TokenControlador.Instancia.clearListaTokensError();
            TokenControlador.Instancia.clearListaTokens();
            TraductorControlador.Instancia.clearTokensTraducidos();
            TablaTraduccionControlador.Instancia.clearTabla();
            this.consolaTexto.Text = "";
            this.richTraduccion.Text = "";
            this.textAnalizar.Text = "";
        }


        #endregion



    }
}
