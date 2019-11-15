using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFP_P2_TraductorC_Pyton.Modelos;
using LFP_P2_TraductorC_Pyton.Controladores;
using System.Collections;

namespace LFP_P2_TraductorC_Pyton.Controladores
{
    class TraductorControlador
    {
        private readonly static TraductorControlador instancia = new TraductorControlador();
        ArrayList listaTokens = new ArrayList();
        int indice = 0;
        Token preAnalisis = null;
        Boolean errorSintactico = false;
        String tokenInicio = "";
        string tipoVariable = "";
        //VARIABLES QUE SIRVEN PARA PONER TABS
        int ambito= 0;

        //Variables switch
        string variableSwitch = "";
        int iteracionesSwitch = 0;

        private TraductorControlador()
        {

        }

        public static TraductorControlador Instancia
        {
            get
            {
                return instancia;
            }
        }



        /**
        * Sintactico 
        * Este meteodo es el constructor de la clase, recibe la lista de tokens, lo hace publico y comienza el analisis
        * sintactico llamando a la produccion inicial.
        * La variable indice funcionara como iterador para recorrer la lista de tokens
        * La variable preanalisis es el token actual segun el indice.
        * */

        
        /// <summary>
        /// NECESARIAS
        /// </summary>
        string cadenaVariable = "";
        string bandera= "";
        string tabs = "";
        string declFor = "";
        string condFor = "";
        string finalFor = "";
        string tokenAnterior = "";
        string inicioDeclaracion = "";
        int interacionSwitch = 0;

         Boolean vieneFor = false;

        Token flagToken = null;

        public void obtenerLista(ArrayList listaTokens)
        {
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_double",  "PR_string"};
            tipoVariable = "";
            for (int i = 13; i < listaTokens.Count-2; i++)
            {
                flagToken = (Token)listaTokens[i];

                #region TRADUCCION FOR
                if (flagToken.Descripcion.Equals("PR_for") )
                {
                    inicioDeclaracion = flagToken.Descripcion;
                    declFor = "";
                    condFor = "";
                    finalFor = "";


                    declFor = "for ";

                    
                    //llenarBandera("for");
                    for (int j = i + 3; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Lexema.Equals(";"))
                        {
                            tokenInicio = "";
                            break;
                        }
                        else
                        {
                            declFor = declFor + flagToken.Lexema;
                        }
                    }

                    for (int j = i + 7; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Descripcion.Equals("Digito"))
                        {
                            condFor = condFor +  flagToken.Lexema;
                            break;
                        }
                    }

                    for (int j = i + 11; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Lexema.Equals(")"))
                        {
                            i = i + 11;
                            break;
                        }
                        else
                        {
                            finalFor = finalFor + flagToken.Lexema;
                        }
                    }

                    string[] cont = declFor.Split('=');

                    TablaTraduccionControlador.Instancia.agregar(tabs + cont[0] + " in range(" + cont[1] + "," + condFor + ")", "for");
                    /*
                    //llenarBandera("for");
                    inicioDeclaracion = flagToken.Descripcion;
                    Parea(flagToken.Descripcion);
                    declFor = "";
                    condFor = "";
                    finalFor = "";
                    vieneFor = true;
                    Parea(flagToken.Descripcion);
                    for (int j = i + 3; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Lexema.Equals(";"))
                        {
                            Parea(flagToken.Descripcion);
                            TablaTraduccionControlador.Instancia.agregar(tabs + declFor, "for");
                            tokenInicio = "";
                            break;
                        }
                        else
                        {
                            declFor = declFor + flagToken.Lexema;
                            Parea(flagToken.Descripcion);
                        }
                    }

                    for (int j = i + 7; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Lexema.Equals(";"))
                        {
                            Parea(flagToken.Descripcion);
                            TablaTraduccionControlador.Instancia.agregar(tabs + "while " + condFor + ":", "for");
                            break;
                        }
                        else
                        {
                            condFor = condFor + flagToken.Lexema;
                            Parea(flagToken.Descripcion);
                        }
                    }

                    for (int j = i + 11; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Lexema.Equals(")"))
                        {
                            Parea(flagToken.Descripcion);
                            i = i + 11;
                            break;
                        }
                        else
                        {
                            finalFor = finalFor + flagToken.Lexema;
                            Parea(flagToken.Descripcion);
                        }
                    }
                    */
                }
                #endregion

                #region TRADUCCION DE VARIABLES Y ARREGLOS
                else if (Array.Exists(reservadasVariable, element => element == flagToken.Descripcion) || bandera.Equals("variable"))
                {

                    Parea(flagToken.Descripcion);
                    //llenarBandera("variable");
                    if (tipoVariable.Equals("")) { tipoVariable = flagToken.Lexema; }

                    //SE VA A VARIABLE

                    if (flagToken.Descripcion.Equals("Identificador"))
                    {
                        Parea(flagToken.Descripcion);
                        for (int j = i; j < listaTokens.Count; j++)
                        {
                            Token temp = (Token)listaTokens[j];
                            if (temp.Lexema.Equals(";"))
                            {
                                Parea(temp.Descripcion);
                                if (((Token)listaTokens[j - 1]).Descripcion.Equals("Identificador") && ((Token)listaTokens[j - 2]).Descripcion.Contains("PR_"))
                                {
                                    EnviarTablaSimbolo(tabs + ((Token)listaTokens[j - 1]).Lexema, tipoVariable, "");
                                }
                                else if (cadenaVariable.Contains(","))
                                {
                                    string[] words = cadenaVariable.Split(',');
                                    for (int n = 0; n < words.Length; n++)
                                    {
                                        if (!words[n].Contains("="))
                                        {
                                            EnviarTablaSimbolo(tabs + words[n], tipoVariable, "");

                                        }
                                        else
                                        {
                                            TablaTraduccionControlador.Instancia.agregar(tabs + words[n], "variable");
                                        }
                                    }
                                    tipoVariable = "";
                                }
                                else
                                {
                                    TablaTraduccionControlador.Instancia.agregar(tabs + cadenaVariable, "variable");
                                }
                                i = j;
                                bandera = "";
                                cadenaVariable = "";
                                break;
                            }
                            else
                            {
                                cadenaVariable = cadenaVariable + temp.Lexema;
                                Parea(temp.Descripcion);

                            }
                        }

                    }
                    //SE VA A ARREGLO
                    else if (flagToken.Lexema.Equals("["))
                    {
                        Parea(flagToken.Descripcion);
                        Token temp = (Token)listaTokens[i + 2];
                        for (int j = i + 2; j < listaTokens.Count; j++)
                        {
                            temp = (Token)listaTokens[j];
                            if (temp.Lexema.Equals(";"))
                            {
                                Parea(temp.Descripcion);
                                cadenaVariable = cadenaVariable.Replace("{", "[");
                                cadenaVariable = cadenaVariable.Replace("}", "]");

                                Console.WriteLine(cadenaVariable);
                                TablaTraduccionControlador.Instancia.agregar(tabs + cadenaVariable, "array");
                                string[] partesArreglo = cadenaVariable.Split('=');
                                EnviarTablaSimbolo(partesArreglo[0], "array", partesArreglo[1]);
                                i = j;
                                bandera = "";
                                cadenaVariable = "";
                                break;
                            }
                            else
                            {
                                if (temp.Descripcion.Contains("PR_"))
                                {
                                    Parea(temp.Descripcion);
                                }
                                else
                                {
                                    cadenaVariable = cadenaVariable + temp.Lexema;
                                    Console.WriteLine(cadenaVariable);
                                    Parea(temp.Descripcion);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region TRADUCCION IF
                #region CONDICION IF
                else if (flagToken.Descripcion.Equals("PR_if"))
                {
                    tokenInicio = "if ";
                    vieneFor = false;
                    Parea(flagToken.Descripcion);
                    for (int j = i + 2; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Lexema.Equals(")"))
                        {
                            Parea(flagToken.Descripcion);
                            TablaTraduccionControlador.Instancia.agregar(tabs + tokenInicio, " if");
                            tokenInicio = "";
                            i = j;
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio + flagToken.Lexema;
                            Parea(flagToken.Descripcion);
                        }
                    }
                }
                #endregion

                #region TRADUCCION DE ELSE
                else if (flagToken.Descripcion.Equals("PR_else"))
                {
                    Parea(flagToken.Descripcion);
                    vieneFor = false;
                    TablaTraduccionControlador.Instancia.agregar(tabs + " else:", "else");
                    vieneFor = true;

                }
                #endregion
                #endregion

                #region TRADUCCION WHILE
                else if (flagToken.Descripcion.Equals("PR_while"))
                {
                    tokenInicio = " while";
                    vieneFor = false;
                    Parea(flagToken.Descripcion);
                    for (int j = i + 2; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Lexema.Equals(")"))
                        {
                            Parea(flagToken.Descripcion);
                            TablaTraduccionControlador.Instancia.agregar(tabs + tokenInicio + ":", "while");
                            tokenInicio = "";
                            i = j;
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio + " " + flagToken.Lexema;
                            Parea(flagToken.Descripcion);
                        }
                    }
                }
                #endregion

                #region TRADUCCION COMENTARIO

                //Comentario de Linea
                else if (flagToken.Descripcion.Equals("ComentarioLinea"))
                {
                    string comment = flagToken.Lexema;
                    comment = comment.Replace("//", "#");
                    Parea(flagToken.Descripcion);
                    TablaTraduccionControlador.Instancia.agregar(tabs + comment, "comentario");
                }
                //Comentario Multi linea
                else if (flagToken.Descripcion.Equals("ComentarioMultiLinea"))
                {
                    string comment = flagToken.Lexema;
                    comment = comment.Replace("/*", "' ' ' ");
                    comment = comment.Replace("*/", " ' ' '");
                    Parea(flagToken.Descripcion);
                    TablaTraduccionControlador.Instancia.agregar(tabs + comment, "comentario");
                }

                #endregion

                #region TRADUCCION CONSOLE WRITELINE
                if (flagToken.Descripcion.Equals("PR_Console"))
                {
                    tokenInicio = "print(";
                    vieneFor = false;
                    Parea(flagToken.Descripcion);
                    for (int j = i + 4; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Lexema.Equals(")"))
                        {
                            Parea(flagToken.Descripcion);
                            TablaTraduccionControlador.Instancia.agregar(tabs + tokenInicio + ")", "console");
                            tokenInicio = "";
                            i = j;
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio + flagToken.Lexema;
                            Parea(flagToken.Descripcion);
                        }
                    }
                }
                #endregion

                #region TRADUCCION SWITCH
                if (flagToken.Descripcion.Equals("PR_switch"))
                {
                    iteracionesSwitch = 0;
                    ambito--;
                    agregarTabulaciones(ambito);
                    Parea(flagToken.Descripcion);
                    variableSwitch = ((Token)listaTokens[i + 2]).Lexema;
                }
                else if (flagToken.Descripcion.Equals("PR_case"))
                {
                    tokenInicio = "";
                    Parea(flagToken.Descripcion);
                    for (int m = i + 1; m < listaTokens.Count; m++)
                    {
                        flagToken = (Token)listaTokens[m];
                        if (flagToken.Lexema.Equals(":"))
                        {
                            Parea(flagToken.Descripcion);
                            if (iteracionesSwitch == 0)
                            {
                                TablaTraduccionControlador.Instancia.agregar(tabs + "if " + variableSwitch + "==" + tokenInicio + ":", "switch");
                            }
                            else
                            {
                                TablaTraduccionControlador.Instancia.agregar(tabs + "elif " + variableSwitch + "==" + tokenInicio + ":", "switch");
                            }
                            ambito++;
                            i = m;
                            agregarTabulaciones(ambito);
                            tokenInicio = "";
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio + " " + flagToken.Lexema;
                        }
                    }
                    iteracionesSwitch++;
                }
                else if (flagToken.Descripcion.Equals("PR_break"))
                {
                    ambito--;
                    agregarTabulaciones(ambito);
                }
                else if (flagToken.Descripcion.Equals("PR_default"))
                {
                    TablaTraduccionControlador.Instancia.agregar(tabs + "else: ", "switch");
                    ambito++;
                    agregarTabulaciones(ambito);
                }

                #endregion

                #region TRADUCCION SIN TIPO
                else if (flagToken.Descripcion.Equals("Identificador") && tokenAnterior.Equals("{"))
                {
                    tokenInicio = "";
                    Parea(flagToken.Descripcion);
                    for (int m = i; m < listaTokens.Count; m++)
                    {
                        flagToken = (Token)listaTokens[m];
                        if (flagToken.Lexema.Equals(";"))
                        {
                            Parea(flagToken.Descripcion);
                            TablaTraduccionControlador.Instancia.agregar(tabs + tokenInicio, "declaracion");
                            i = m;
                            tokenInicio = "";
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio + " " + flagToken.Lexema;
                            Parea(flagToken.Descripcion);
                        }
                    }
                }
                #endregion

                #region DELIMITADORES DE DECLARACION
                else if (flagToken.Descripcion.Equals("S_Llave_Izquierda"))
                {
                    ambito++;
                    tokenAnterior = flagToken.Lexema;
                    agregarTabulaciones(ambito);

                }
                else if (flagToken.Descripcion.Equals("S_Llave_Derecha"))
                {
                    //Envia hasta el final el aumento del for
                    /*if (inicioDeclaracion.Equals("PR_for") )
                    {
                        if (finalFor.Contains("++"))
                        {

                            finalFor = finalFor.Replace("++", "+=1");
                        }
                        else
                        {
                            finalFor = finalFor.Replace("--", "-= 1");
                        }
                        TablaTraduccionControlador.Instancia.agregar(tabs + " "+finalFor, "for");
                    }*/
                    variableSwitch = "";
                    tokenAnterior = "";
                    ambito--;
                    agregarTabulaciones(ambito);

                }
                #endregion                
            }
        }


        /**
    *   Parea
    *  Este metodo lo que hace es comparar si el token de preanalisis tiene le tipo que le indicamos, 
    *  en caso no sean iguales maraca error.
    * */
        public void Parea(String tipoToken)
        {
            if (errorSintactico)
            {
                if (indice < listaTokens.Count - 1)
                {
                    indice++;
                    preAnalisis = (Token)listaTokens[indice];
                    if (preAnalisis.Descripcion.Equals("S_Punto_y_Coma"))
                    {
                        errorSintactico = false;
                    }
                }
            }
            else
            {
                if (indice < listaTokens.Count - 1)
                {
                    if (preAnalisis.Descripcion.Equals(tipoToken))
                    {
                        indice++;
                        preAnalisis = (Token)listaTokens[indice];
                    }
                    else
                    {
                        errorSintactico = true;
                    }
                }
            }
        }

        public void agregarTabulaciones(int contador)
        {
            tabs = "";
            for (int i = 0; i < contador; i++)
            {
                tabs = tabs + "\t"; 
            }
        }

        public void llenarBandera(string cadena)
        {
            this.bandera = cadena;
        }

        public string getTokensTraducidos()
        {
            return this.tokenInicio;
        }

        public void clearTokensTraducidos()
        {
            this.tokenInicio = "";
        }

        public void EnviarTablaSimbolo(string nombre, string tipo, string valor)
        {
            if (tipo.ToLower().Equals("int"))
            {
                TablaTraduccionControlador.Instancia.agregar(nombre + " = 0", "variable");
                SimboloControlador.Instancia.agregarSimbolo(nombre, "0", "Int");
            }
            else if (tipo.ToLower().Equals("string"))
            {
                TablaTraduccionControlador.Instancia.agregar(nombre + " = " + "\" " + "\"", "variable");
                SimboloControlador.Instancia.agregarSimbolo(nombre, "\"" + "\"", "String");
            }
            else if (tipo.ToLower().Equals("float") || tipo.ToLower().Equals("double"))
            {
                TablaTraduccionControlador.Instancia.agregar(nombre + " = 0.0", "variable");
                SimboloControlador.Instancia.agregarSimbolo(nombre, "0.0", "Float");
            }
            else if (tipo.ToLower().Equals("bool") || tipo.ToLower().Equals("boolean"))
            {
                TablaTraduccionControlador.Instancia.agregar(nombre + " = false", "variable");
                SimboloControlador.Instancia.agregarSimbolo(nombre, "fale", "Boolean");
            }
            else if (tipo.ToLower().Equals("char"))
            {
                TablaTraduccionControlador.Instancia.agregar(nombre + " = ' '", "variable");
                SimboloControlador.Instancia.agregarSimbolo(nombre, "' '", "Char");
            }
            else if (tipo.ToLower().Equals("array"))
            {
                SimboloControlador.Instancia.agregarSimbolo(nombre, valor, "Array");
            }

        }
    }

}
