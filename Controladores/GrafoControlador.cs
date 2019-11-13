using LFP_P2_TraductorC_Pyton.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_P2_TraductorC_Pyton.Controladores
{
    class GrafoControlador
    {
        private readonly static GrafoControlador instancia = new GrafoControlador();
        public static GrafoControlador Instancia
        {
            get
            {
                return instancia;
            }
        }
        int nodoCont = 0;
        public void generarTexto(string path)
        {
            ArrayList listaSimbolos = SimboloControlador.Instancia.ArrayListSimbolo;
            string cuerpoNodos = "";
            string ordenNodos = "";

            for (int i = 0; i < listaSimbolos.Count; i++)
            {
                Simbolo simbolo = (Simbolo)listaSimbolos[i];
                if (simbolo.Tipo.Equals("Array"))
                {
                    if (!simbolo.Valor.Equals("[]"))
                    {
                        string valores = simbolo.Valor;
                        valores = valores.Replace('[', ' ');
                        valores = valores.Replace(']', ' ');

                        string[] contenido = valores.Trim().Split(',');

                        for (int j = 0; j < contenido.Length; j++)
                        {
                            cuerpoNodos = cuerpoNodos + contenido[j] + "[shape = box, color = blue] " + "\n";
                            ordenNodos = ordenNodos + contenido[j] + "->";
                        }
                        generarImagen(cuerpoNodos, ordenNodos, simbolo.Nombre, path);
                        cuerpoNodos = ordenNodos = "";
                        
                    }
                }
            }
         
        }
        public void generarImagen(string cuerpo, string orden, string nombre, string path)
        {
            string texto =
            "digraph G {" +
              "rankdir = LR " + "\n" +
              "Inicio[shape = box, color = blue] "+ "\n" +
              cuerpo + "\n" +
              "Fin[shape = box, color = blue] " +
              "Inicio->" + orden + "Fin"+

             "}";
            System.IO.File.WriteAllText(path + "\\" + nombre + ".dot", texto);
            var command = "dot -Tpng \"" + path + "\\" + nombre + ".dot\"  -o \"" + path + "\\" + nombre + ".png\"   ";
            var procStarInfo = new ProcessStartInfo("cmd", "/C" + command);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStarInfo;
            proc.Start();
            proc.WaitForExit();
            
        }
    }
}
