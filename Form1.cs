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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Analizar_Click(object sender, EventArgs e)
        {
            if (nombreArc == "")
            {
                tempNombreArc = "escrito";
            }
            TokenControlador.Instancia.clearListaTokens();
            AnalizadorLexico.Instancia.analizador_Lexico(textAnalizar.Text);
            TraductorControlador.Instancia.clearTokensTraducidos();

            ArrayList a = TokenControlador.Instancia.getArrayListTokens();


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
            TokenControlador.Instancia.clearListaTokens();
            TokenControlador.Instancia.clearListaTokensError();
            TraductorControlador.Instancia.clearTokensTraducidos();
            this.consolaTexto.Text = "";
            this.richTraduccion.Text = "";
        }


        #endregion



    }
}
