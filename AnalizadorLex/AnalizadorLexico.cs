using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using LFP_P2_TraductorC_Pyton.Controladores;

namespace LFP_P2_TraductorC_Pyton.AnalizadorLex
{
    class AnalizadorLexico
    {
        private readonly static AnalizadorLexico instancia = new AnalizadorLexico();

        //VARIABLES GLOBALES
        string auxiliar = "";

        private AnalizadorLexico()
        {
        }

        public static AnalizadorLexico Instancia
        {
            get
            {
                return instancia;
            }
        }


        // metodo para analizar

        public async void analizador_Lexico(String totalTexto)
        {
            ////
            int opcion = 0;
            int columna = -1;
            int fila = 1;
            totalTexto = totalTexto + " ";

            char[] charsRead = new char[totalTexto.Length];
            using (StringReader reader = new StringReader(totalTexto))
            {
                await reader.ReadAsync(charsRead, 0, totalTexto.Length);
            }

            StringBuilder reformattedText = new StringBuilder();
            using (StringWriter writer = new StringWriter(reformattedText))
            {
                for (int i = 0; i < charsRead.Length; i++)
                {
                    columna++;
                    Char c = totalTexto[i];
                    switch (opcion)
                    {
                        case 0:
                            //VERIFICA SI LO QUE VIENE ES LETRA
                            if (char.IsLetter(c))
                            {
                                opcion = 1;
                                auxiliar += c;
                            }

                            //VERIFICA SI ES ESPACIO EN BLANCO O SALTO DE LINEA
                            else if (c.Equals('\n'))
                            {
                                opcion = 0;
                                columna = 0;//COLUMNA 0
                                fila++; //FILA INCREMENTA

                            }
                            //VERIFICA SI ES ESPACIO EN BLANCO O SALTO DE LINEA
                            else if (char.IsWhiteSpace(c))
                            {
                                columna++;
                                opcion = 0;
                            }
                            //VERIFICA SI LO QUE VIENE ES DIGITO
                            else if (char.IsDigit(c))
                            {
                                opcion = 2;
                                auxiliar += c;
                            }
                            //VERIFICA SI ES SIMBOLO
                            else if (char.IsSymbol(c))
                            {
                                if (c.Equals('<'))
                                {
                                    columna++;
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Menor_Que");
                                }
                                else if (c.Equals('>'))
                                {
                                    columna++;
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Mayor_Que");

                                }
                                else if (c.Equals('='))
                                {
                                    columna++;
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Igual");
                                }
                                else if (c.Equals('+'))
                                {
                                    columna++;
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Suma");
                                }

                                else
                                {
                                    //Console.WriteLine("esta entrando al ultimo else");
                                    columna++;
                                    TokenControlador.Instancia.agregarError(fila, (columna - 1), c.ToString(), "Simb_Desconocido_" + c);
                                    opcion = 10;
                                    i--;
                                }
                            }
                            //VERIFICA SI LO QUE VIENE ES SIGNO DE PUNTUACION
                            else if (char.IsPunctuation(c))
                            {
                                //Console.WriteLine("esta entrando a puntuacion");
                                if (c.Equals('"'))
                                {
                                    columna++;
                                    opcion = 3;
                                    i--;
                                    columna--;
                                }
                                /*if (c.Equals("'"))
                                {
                                    Console.WriteLine("entro");
                                    columna++;
                                    opcion = 11;
                                    i--;
                                    columna--;
                                }*/
                                else if (c.Equals(','))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Coma");
                                    /*opcion = 5;
                                    i--;
                                    columna--;*/
                                }
                                else if (c.Equals('{'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Llave_Izquierda");
                                }
                                else if (c.Equals('}'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Llave_Derecha");
                                }
                                else if (c.Equals(';'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Punto_y_Coma");

                                }
                                else if (c.Equals(':'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Dos_puntos");
                                }
                                else if (c.Equals('.'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Punto");
                                }
                                else if (c.Equals('_'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Guion_Bajo");
                                }
                                else if (c.Equals('('))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, columna, c.ToString(), "S_Parentesis_Izquierdo");
                                }
                                else if (c.Equals(')'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, columna, c.ToString(), "S_Parentesis_Derecho");
                                }
                                else if (c.Equals('['))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, columna, c.ToString(), "S_Corchete_Izquierdo");
                                }
                                else if (c.Equals(']'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, columna, c.ToString(), "S_Corchete_Derecho");
                                }
                                else if (c.Equals('%'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Porcentaje");
                                }
                                else if (c.Equals('-'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Resta");
                                }
                                else if (c.Equals('/'))
                                {
                                    //TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Division");
                                    columna++;
                                    opcion = 14;
                                    i--;
                                    columna--;
                                }
                                else if (c.Equals('*'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Mult");
                                }
                                else if (c.Equals('!'))
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Excl");
                                }
                                else
                                {
                                    //Console.WriteLine("ULTIMO ELSE PUNTUACION");
                                    columna++;
                                    TokenControlador.Instancia.agregarError(fila, (columna - 1), c.ToString(), "Simb_Desconocido_" + c);
                                    alertMessage("Se detectó un error en la fila " + fila + ", columna " + (columna - 1));
                                    opcion = 10;
                                    i--;
                                    columna--;
                                }

                            }
                            //LO MANDA A SIGNOS DESCONOCIDOS
                            else
                            {
                                columna++;
                                //Console.WriteLine("esta entrando al ultimo else");
                                TokenControlador.Instancia.agregarError(fila, (columna - 1), c.ToString(), "Simb_Desconocido_" + c);
                                alertMessage("Se detectó un error en la fila " + fila + ", columna " + (columna - 1));
                                opcion = 10;
                                i--;
                                columna--;
                            }
                            break;
                        case 1:
                            if (Char.IsLetterOrDigit(c) || c == '_')
                            {
                                auxiliar += c;
                                opcion = 1;
                            }
                            else
                            {
                                string[] reservadasC = { "class", "static", "void", "string", "args",
                                    "int", "new", "float", "char", "bool", "boolean", "if", "else",
                                    "switch", "case", "break", "for",  "while", "null"};

                                string[] reservadasMayus = { "Main", "Console", "WriteLine", "Count", "Length" };

                                if (Array.Exists(reservadasC, element => element.Equals(auxiliar.ToLower())))
                                {
                                    int lex = Array.IndexOf(reservadasC, auxiliar.ToLower());
                                    TokenControlador.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar.ToLower(), "PR_" + reservadasC[lex]);

                                }
                                else if (Array.Exists(reservadasMayus, element => element.Equals(auxiliar)))
                                {
                                    int lex = Array.IndexOf(reservadasMayus, auxiliar);
                                    TokenControlador.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "PR_" + reservadasMayus[lex]);
                                }
                                else
                                {
                                    TokenControlador.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "Identificador");
                                    //alertMessage("Se detecto un error, Linea" + fila + " , columna " + columna);
                                }


                                auxiliar = "";
                                i--;
                                columna--;
                                opcion = 0;
                            }
                            break;
                        case 2:
                            if (Char.IsDigit(c))
                            {
                                auxiliar += c;
                                opcion = 2;
                            }
                            else if (c == '.')
                            {
                                opcion = 8;
                                auxiliar += c;
                            }
                            else
                            {
                                TokenControlador.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "Digito");
                                auxiliar = "";
                                i--;
                                columna--;
                                opcion = 0;
                            }
                            break;
                        case 3:
                            if (c == '"')
                            {
                                auxiliar += c;
                                opcion = 4;
                            }
                            break;
                        case 4:
                            if (c != '"')
                            {
                                if (c.Equals('\n')) { fila++; columna = 0; }
                                auxiliar += c;
                                opcion = 4;
                            }
                            else
                            {
                                opcion = 5;
                                i--;
                                columna--;
                            }
                            break;
                        case 5:
                            if (c == '"')
                            {
                                auxiliar += c;
                                TokenControlador.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "Cadena");
                                opcion = 0;
                                auxiliar = "";
                            }
                            break;
                        case 8:
                            if (char.IsDigit(c))
                            {
                                opcion = 9;
                                auxiliar += c;
                            }
                            else
                            {
                                opcion = 0;
                                auxiliar = "";
                            }
                            break;
                        case 9:
                            if (Char.IsDigit(c))
                            {
                                opcion = 9;
                                auxiliar += c;
                            }
                            else
                            {
                                TokenControlador.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "Digito");
                                auxiliar = "";
                                i--;
                                columna--;
                                opcion = 0;
                            }

                            break;
                        case 10:
                            auxiliar += c;
                            //TokenControlador.Instancia.error(auxiliar, "Desconocido");
                            opcion = 0;
                            auxiliar = "";
                            break;
                        case 11:
                            if (c.Equals("'"))
                            {
                                auxiliar += c;
                                opcion = 12;
                            }
                            break;
                        case 12:
                            if (!c.Equals("'"))
                            {
                                if (c.Equals('\n')) { fila++; columna = 0; }
                                auxiliar += c;
                                opcion = 12;
                            }
                            else
                            {
                                opcion = 13;
                                i--;
                                columna--;
                            }
                            break;
                        case 13:
                            if (c.Equals("'"))
                            {
                                auxiliar += c;
                                TokenControlador.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "Cadena");
                                opcion = 0;
                                auxiliar = "";
                            }
                            break;
                        //COMENTARIO
                        case 14:
                            if (c == '/')
                            {
                                auxiliar += c;
                                opcion = 15;
                            }
                            break;
                        case 15:
                            if (c == '/')
                            {
                                auxiliar += c;
                                opcion = 16;
                            }
                            else if (c == '*')
                            {
                                auxiliar += c;
                                opcion = 17;
                            }
                            else
                            {
                                TokenControlador.Instancia.agregarToken(fila, (columna - 1), auxiliar.ToString(), "S_Division");
                                auxiliar = "";
                                columna--;
                                i--;
                                opcion = 0;
                            }
                            break;
                        case 16:
                            if (!c.Equals('\n'))
                            {
                                auxiliar += c;
                                opcion = 16;
                            }
                            else
                            {
                                fila++; columna = 0;
                                TokenControlador.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "ComentarioLinea");
                                opcion = 0;
                                auxiliar = "";
                            }
                            break;
                        case 17:
                            if (c != '*')
                            {
                                if (c.Equals('\n')) { fila++; columna = 0; }
                                auxiliar += c;
                                opcion = 17;
                            }
                            else
                            {
                                auxiliar += c;
                                opcion = 18;
                            }
                            break;
                        case 18:
                            if (c == '/')
                            {
                                auxiliar += c;
                                opcion = 18;
                                TokenControlador.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "ComentarioMultiLinea");
                                opcion = 0;
                                auxiliar = "";
                            }
                            break;
                    }
                }
            }

        }
        public void alertMessage(String mensaje)
        {
            MessageBox.Show(mensaje, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
