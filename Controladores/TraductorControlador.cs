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

        //VARIABLES QUE SIRVEN PARA PONER TABS
        int ambito= 0;
        //-------TRADUCCIONES ---------



        //importante
        string contenidoDeclarado = "";

        //Variables for
        string condicion = "";
        string aumentoDecremento = "";



        //Variables switch
        string variableSwitch = "";
        string cuerpoSwitch = "";
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
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string"};
            for (int i = 0; i < listaTokens.Count; i++)
            {
                flagToken = (Token)listaTokens[i];
                #region TRADUCCION FOR
                if (flagToken.Descripcion.Equals("PR_for"))
                {
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
                            declFor = declFor + " " + flagToken.Lexema;
                            Parea(flagToken.Descripcion);
                        }
                    }

                    for (int j = i+3; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Lexema.Equals(";"))
                        {
                            Parea(flagToken.Descripcion);
                            TablaTraduccionControlador.Instancia.agregar(tabs + "while "+condFor + ":", "for");
                            break;
                        }
                        else
                        {
                            condFor = condFor + " " + flagToken.Lexema;
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
                }
                #endregion

                #region TRADUCCION DE VARIABLES Y ARREGLOS
                else if (Array.Exists(reservadasVariable, element => element == flagToken.Descripcion) || bandera.Equals("variable"))
                {
                    Parea(flagToken.Descripcion);
                    llenarBandera("variable");
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
                                TablaTraduccionControlador.Instancia.agregar(tabs + cadenaVariable, "variable");
                                i = j;
                                bandera = "";
                                cadenaVariable = "";
                                break;
                            }
                            else
                            {
                                cadenaVariable = cadenaVariable + " " + temp.Lexema;
                                Parea(temp.Descripcion);

                            }
                        }

                    }
                    //SE VA A ARREGLO
                    else if (flagToken.Lexema.Equals("["))
                    {
                        Parea(flagToken.Descripcion);
                        for (int j = i+2; j < listaTokens.Count; j++)
                        {
                            Token temp = (Token)listaTokens[j];
                            if (temp.Lexema.Equals(";"))
                            {
                                Parea(temp.Descripcion);
                                cadenaVariable = cadenaVariable.Replace("{", "[");
                                cadenaVariable = cadenaVariable.Replace("}", "]");
                                TablaTraduccionControlador.Instancia.agregar(tabs + cadenaVariable, "array");
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
                                    cadenaVariable = cadenaVariable + " " + temp.Lexema;
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
                    tokenInicio = " if";
                    vieneFor = false;
                    Parea(flagToken.Descripcion);
                    for (int j = i+2; j < listaTokens.Count; j++)
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
                            tokenInicio = tokenInicio + " " + flagToken.Lexema;
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
                            TablaTraduccionControlador.Instancia.agregar(tabs + tokenInicio, "while");
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
                    tokenInicio = " print[";
                    vieneFor = false;
                    Parea(flagToken.Descripcion);
                    for (int j = i + 4; j < listaTokens.Count; j++)
                    {
                        flagToken = (Token)listaTokens[j];
                        if (flagToken.Lexema.Equals(")"))
                        {
                            Parea(flagToken.Descripcion);
                            TablaTraduccionControlador.Instancia.agregar(tabs + tokenInicio + "]", "console");
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

                #region TRADUCCION SWITCH
                if (flagToken.Descripcion.Equals("PR_switch"))
                {
                    iteracionesSwitch = 0;
                    ambito--;
                    agregarTabulaciones(ambito);
                    Parea(flagToken.Descripcion);
                }
                else if (flagToken.Descripcion.Equals("PR_case"))
                {
                    tokenInicio = "";
                    Parea(flagToken.Descripcion);
                    for (int m = i+1; m < listaTokens.Count; m++)
                    {
                        flagToken = (Token)listaTokens[m];
                        if (flagToken.Lexema.Equals(":"))
                        {
                            Parea(flagToken.Descripcion);
                            if (iteracionesSwitch == 0)
                            {
                                TablaTraduccionControlador.Instancia.agregar(tabs + "if ==" + tokenInicio+":", "switch");
                            }
                            else
                            {
                                TablaTraduccionControlador.Instancia.agregar(tabs + "elif ==" + tokenInicio + ":", "switch");
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
                    /*if (inicioDeclaracion.Equals("PR_for") && vieneFor)
                    {
                        Console.WriteLine(finalFor);
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
                    tokenAnterior = "";
                    ambito--;
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
                //tokens innecesarios
                else if (flagToken.Descripcion.Equals("S_Parentesis_Izquierdo")|| flagToken.Descripcion.Equals("S_Parentesis_Derecho"))
                {
                    Parea("S_Parentesis_Izquierdo");
                }
                
            }


            /*this.listaTokens = listaTokens;
            indice = 0;
            iteracionesSwitch = 0;
            preAnalisis = (Token)listaTokens[indice];
            this.tokenInicio = "";
            Inicio();*/
        }

        public void Inicio()
        {
            ListaDeclaracion();
        }
        public void ListaDeclaracion()
        {
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };

            //Manda a llamar a metodo variable
            if (Array.Exists(reservadasVariable, element => element == preAnalisis.Descripcion))
            {
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Descripcion.Equals("Identificador"))
                {
                    InicioVariable();
                }
                else if (preAnalisis.Descripcion.Equals("S_Corchete_Izquierdo"))
                {
                    Console.WriteLine("entro");
                    InicioArreglo();
                }
            }
            else if (preAnalisis.Descripcion.Equals("PR_for"))
            {
                InicioFor();
            }
            else if (preAnalisis.Descripcion.Equals("PR_class"))
            {
                InicioClase();
            }
            else if (preAnalisis.Descripcion.Equals("PR_Console"))
            {
                InicioConsole();
            }
            else if (preAnalisis.Descripcion.Equals("PR_switch"))
            {
                InicioSwitch();
            }
            else if (preAnalisis.Descripcion.Equals("PR_if"))
            {
                InicioIf2();
            }
            else if (preAnalisis.Descripcion.Equals("PR_while"))
            {
                InicioWhile();
            }
            else if (preAnalisis.Descripcion.Equals("ComentarioLinea") || preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
                InicioComentario();
            }
            else if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                AsignacionSinTipo();
            }
            else
            {
                //this.lex = "Error Sintactico: Se esperaba palabra reservada en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                //errorSintactico = true;
                //Epsilon
            }
        }

        /**
         *  VARIABLES
         */
        #region TRADUCCION DE VARIABLES 

        public void InicioVariable()
        {
            tokenInicio = "";
            DeclaracionVariable();
            //por si viene mas de una declaracion
            ListaDeclaracionVariable();
        }
        public void ListaDeclaracionVariable()
        {
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };

            if (Array.Exists(reservadasVariable, element => element == preAnalisis.Descripcion))
            {
                DeclaracionVariable();
                ListaDeclaracionVariable();
            }
            else
            {
                //Epsilon
            }

        }
        public void DeclaracionVariable()
        {
            tokenInicio = tokenInicio + ListaId();
            string valores = OpcAsignacion();
            if (valores == "")
            {
                Parea("S_Punto_y_Coma");
            }
            else
            {
                tokenInicio = tokenInicio + " " + valores;
            }
            TablaTraduccionControlador.Instancia.agregar(tokenInicio,  "variable");
            ListaDeclaracion();
            
        }

        string listaIds = "";
        public String ListaId()
        {
            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                if (!listaIds.Contains(",")) { listaIds = preAnalisis.Lexema; variableSwitch = preAnalisis.Lexema; }
                else
                { listaIds = listaIds + " " + preAnalisis.Lexema; }
                Parea("Identificador");
                ListaId1();
            }

            return listaIds;
        }
        public void ListaId1()
        {
            if (preAnalisis.Descripcion.Equals("S_Coma"))
            {

                listaIds = listaIds + ",";
                Parea("S_Coma");
                ListaId();
            }
            else
            {
                //Epsilon
            }
        }
        public String OpcAsignacion()
        {
            if (preAnalisis.Lexema.Equals("="))
            {
                Parea(preAnalisis.Descripcion);
                return "= " + ExpresionVariable();
            }
            else
            {
                //Epsilon
            }
            return "";
        }
        string valorVariable = "";
        public String ExpresionVariable()
        {
            if (preAnalisis.Descripcion.Equals("Digito") || preAnalisis.Descripcion.Equals("Cadena") || preAnalisis.Descripcion.Equals("Identificador"))
            {
                valorVariable = preAnalisis.Lexema;
                Parea(preAnalisis.Descripcion);
                Parea(preAnalisis.Descripcion);
                return valorVariable;
            }
            return "";
        }
        #endregion


        #region TRADUCCION DE ARREGLO
        // viene de la declaracion de variable
        public void InicioArreglo()
        {
            tokenInicio = "";
            //Corchete manda a parea el corrchete izquierdo
            Parea(preAnalisis.Descripcion);
            //Corchete manda a parea el corrchete derecho
            Parea(preAnalisis.Descripcion);
            string nombreArreglo = "";
            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                nombreArreglo = preAnalisis.Lexema;
                Parea("Identificador");
                tokenInicio = tokenInicio + nombreArreglo + OpcAsignacionArreglo();
                TablaTraduccionControlador.Instancia.agregar(tokenInicio, "array");
                //Manda a parea el simbolo de punto y coma
                Parea(preAnalisis.Descripcion);
            }
            ListaDeclaracion();
        }


        public String OpcAsignacionArreglo()
        {
            if (preAnalisis.Lexema.Equals("="))
            {
                Parea("S_Igual");
                return " = " + ExpresionArreglo();
            }
            else
            {
                return "";
                //Epsilon
            }

        }


        // Estos metodos sirven por si el arreglo se declara de una vez, es decir arreglo = {a, b, c}
        public String ExpresionArreglo()
        {
            string contenidoDeArreglo = "";
            if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
            {

                Parea("S_Llave_Izquierda");

                contenidoDeArreglo = ListaValor();

                if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                {
                    Parea("S_Llave_Derecha");
                }
            }
            else if (preAnalisis.Descripcion.Equals("PR_new"))
            {
                Parea("PR_new");

                Parea(preAnalisis.Descripcion);
                contenidoDeArreglo = preAnalisis.Lexema;
                Parea(preAnalisis.Descripcion);

                int posicionActual = listaTokens.IndexOf(preAnalisis);
                for (int i = posicionActual; i < listaTokens.Count; i++)
                {
                    if (preAnalisis.Lexema.Equals("]"))
                    {
                        contenidoDeArreglo = contenidoDeArreglo + "]";
                        Parea(preAnalisis.Descripcion);
                        break;
                    }
                    else
                    {
                        contenidoDeArreglo = contenidoDeArreglo + preAnalisis.Lexema;
                        Parea(preAnalisis.Descripcion);
                    }
                }
                //Parea(preAnalisis.Descripcion);
                //Parea(preAnalisis.Descripcion);

            }
            return contenidoDeArreglo;
        }
        public String ListaValor()
        {

            contenidoDeclarado = "[" + preAnalisis.Lexema;
            Parea(preAnalisis.Descripcion);
            ListaValor1();
            contenidoDeclarado = contenidoDeclarado + "]";
            return contenidoDeclarado;

        }
        public void ListaValor1()
        {
            if (preAnalisis.Descripcion.Equals("S_Coma"))
            {
                Parea("S_Coma");
                contenidoDeclarado = contenidoDeclarado + "," + preAnalisis.Lexema;
                Parea(preAnalisis.Descripcion);
                ListaValor1();
            }
            else
            {
                //Epsilon
            }
        }

        // estos metodos sirven por si el arreglo se declara como new, es decir new tipo[]


        #endregion


        /**
        * METODO SWITCH
        */
        #region TRADUCCION SWITCH 
        public void InicioSwitch()
        {
            tokenInicio = "";
            int posicionActual = listaTokens.IndexOf(preAnalisis);
            for (int i = posicionActual; i < listaTokens.Count; i++)
            {
                if (preAnalisis.Descripcion.Equals("Identificador"))
                {
                    variableSwitch = preAnalisis.Lexema;
                    Parea(preAnalisis.Descripcion);
                    CaseSwitch();
                    break;
                }
                else
                {
                    Parea(preAnalisis.Descripcion);
                }
            }
        }

        public void CaseSwitch()
        {
            int posicionActual = listaTokens.IndexOf(preAnalisis);
            for (int i = posicionActual; i < listaTokens.Count; i++)
            {
                if (preAnalisis.Descripcion.Equals("PR_case"))
                {
                    Parea("PR_case");

                    if (iteracionesSwitch == 0)
                    {
                        cuerpoSwitch = "if " + variableSwitch + " == " + preAnalisis.Lexema + ":";
                    }
                    else
                    {
                        cuerpoSwitch = "elif " + variableSwitch + " == " + preAnalisis.Lexema + ":";
                    }
                    Parea(preAnalisis.Descripcion);
                    Parea(preAnalisis.Descripcion);

                    //Evia cada caso del case ya traducido al a la tabla 
                    TablaTraduccionControlador.Instancia.agregar(cuerpoSwitch, "switch");

                    //Envia el delimitador 
                    TablaTraduccionControlador.Instancia.agregar("", "#");

                    ListaDeclaracion();
                    //Envia el delimitador 
                    TablaTraduccionControlador.Instancia.agregar("", "#");

                }
                else if (((Token)listaTokens[i]).Descripcion.Equals("PR_default"))
                {
                    
                    TablaTraduccionControlador.Instancia.agregar("else:", "else");
                    //Envia el delimitador 
                    TablaTraduccionControlador.Instancia.agregar("",  "#");
                    ListaDeclaracion();
                    //Envia el delimitador 
                    TablaTraduccionControlador.Instancia.agregar("", "#");

                }
                else
                {
                    Parea(preAnalisis.Descripcion);
                }

            }
        }



        #endregion

        /**
        * METODO FOR
        */

        #region TRADUCCION FOR

        public void InicioFor()
        {
            tokenInicio = "";
            //Envia a parea los simbolos inicesarios
            Parea(preAnalisis.Descripcion);
            Parea(preAnalisis.Descripcion);
            Parea(preAnalisis.Descripcion);
            int posicionActual = 0;

            posicionActual = listaTokens.IndexOf(preAnalisis);
            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                /*
                 for ( int y = 5 ; y > 45 ; y + + ) { }
                 */

                //declaracion de variable y = 5; 
                for (int i = posicionActual; i < posicionActual + 3; i++)
                {
                    tokenInicio = tokenInicio + preAnalisis.Lexema;
                    Parea(preAnalisis.Descripcion);
                }
                //envia punto y coma
                Parea(preAnalisis.Descripcion);


                //Envia la variable traducida a la tabla
                TablaTraduccionControlador.Instancia.agregar(tokenInicio, "for");



                //concatena al texto la condicion del for ==>  (x > 45)
                tokenInicio = "while" + CondicionFor();

                //Envia la variable traducida a la tabla
                TablaTraduccionControlador.Instancia.agregar(tokenInicio, "for");


                //trae el aumento o decremento del valor de la variable ( y + +)
                aumentoDecremento = AumentoDecremento();
                Parea(preAnalisis.Descripcion);
                Parea(preAnalisis.Descripcion);

                if (preAnalisis.Lexema.Equals("{"))
                {
                    ambito++;
                    agregarTabulaciones(ambito);
                    Parea(preAnalisis.Descripcion);

                }


                //Agrego delimitadores
                TablaTraduccionControlador.Instancia.agregar("", "#");

                ListaDeclaracion();

                //Envia la variable traducida a la tabla
                TablaTraduccionControlador.Instancia.agregar(aumentoDecremento, "for");

                //Agrego delimitadores
                TablaTraduccionControlador.Instancia.agregar("", "#");

 
                if (preAnalisis.Lexema.Equals("}"))
                {
                    Parea(preAnalisis.Descripcion);
                    ListaDeclaracion();
                }
            }

        }
        public String CondicionFor()
        {
            condicion = "";
            int posicionActual = listaTokens.IndexOf(preAnalisis);
            for (int i = posicionActual; i < posicionActual + 4; i++)
            {
                //Se detiene cuando ecuentra un punto y coma
                if (preAnalisis.Lexema.Equals(";"))
                {
                    break;
                }
                else
                {
                    //concatena al texto de la condicion el token actual
                    condicion = condicion + " " + preAnalisis.Lexema;
                    Parea(preAnalisis.Descripcion);
                }
            }

            return condicion;
        }
        public String AumentoDecremento()
        {
            Parea(preAnalisis.Descripcion);
            String valorRetorno = "";
            int posicionActual = listaTokens.IndexOf(preAnalisis);
            for (int i = posicionActual; i < posicionActual + 4; i++)
            {
                if (preAnalisis.Lexema.Equals(")"))
                {
                    break;
                }
                else
                {
                    if (preAnalisis.Descripcion.Equals("Identificador"))
                    {
                        valorRetorno = preAnalisis.Lexema;
                        Parea(preAnalisis.Descripcion);
                    }
                    else if (preAnalisis.Descripcion.Equals("S_Resta"))
                    {
                        Parea(preAnalisis.Descripcion);
                        if (preAnalisis.Descripcion.Equals("S_Resta"))
                        {
                            valorRetorno = valorRetorno + "-= 1";
                            Parea(preAnalisis.Descripcion);
                            break;
                        }
                    }
                    else if (preAnalisis.Descripcion.Equals("S_Suma"))
                    {
                        Parea(preAnalisis.Descripcion);
                        if (preAnalisis.Descripcion.Equals("S_Suma"))
                        {
                            valorRetorno = valorRetorno + "+= 1";
                            break;
                        }
                    }
                }
            }
            
            return valorRetorno;
        }

        #endregion
        /**
        * COMENTARIO
        */

        #region TRADUCCION COMETARIO
        public void InicioComentario()
        {
            tokenInicio = "";

            //VERIFICA QUE TIPO DE COMENTARIO ES Y REEMPLAZA LOS CARACTERES POR LOS DE TRADUCCION
            if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
            {
                tokenInicio = preAnalisis.Lexema;
                tokenInicio = tokenInicio.Replace("//", "#");
                Parea(preAnalisis.Descripcion);

            }
            else if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
                tokenInicio = preAnalisis.Lexema;
                tokenInicio = tokenInicio.Replace("/*", " ' ' ' ");
                tokenInicio = tokenInicio.Replace("*/", " ' ' ' ");
                Parea(preAnalisis.Descripcion);
            }
            //Envia la variable traducida a la tabla
            TablaTraduccionControlador.Instancia.agregar(tokenInicio, "comentario");
            ListaDeclaracion();
        }
        #endregion
        /**
        * CONSOLE
        */

        #region TRADUCTOR CONSOLE
        public void InicioConsole()
        {
            tokenInicio = "";
            //Envia a parea la palbra Console
            Parea(preAnalisis.Descripcion);
            if (preAnalisis.Lexema.Equals("."))
            {
                //Evia a parea el punto
                Parea(preAnalisis.Descripcion);
                //verifica si viene WriteLine
                if (preAnalisis.Lexema.Equals("WriteLine"))
                {
                    //Envia a parea los simbolos innecesarios
                    Parea(preAnalisis.Descripcion);
                    Parea(preAnalisis.Descripcion);
                    tokenInicio = "print(";

                    //encuentra la posicion actual
                    int posicionActual = listaTokens.IndexOf(preAnalisis);

                    //Hace un for para armar el contenido de la cadena
                    for (int i = posicionActual; i < listaTokens.Count; i++)
                    {
                        if (preAnalisis.Lexema.Equals(";"))
                        {
                            TablaTraduccionControlador.Instancia.agregar(tokenInicio, "print");
                            Parea(preAnalisis.Descripcion);
                            ListaDeclaracion();
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio + " " + preAnalisis.Lexema;
                            Parea(preAnalisis.Descripcion);
                        }
                    }
                }
            }
        }
        #endregion
        /**
        * METODO WHILE
        */

        #region TRADUCTOR WHILE

        public void InicioWhile()
        {

            //agrega al token de inicio la palbara while
            tokenInicio = preAnalisis.Lexema;

            //Envia a parea los simbolos inceearios
            Parea(preAnalisis.Descripcion);
            Parea(preAnalisis.Descripcion);

            //Trae la condicion del while
            tokenInicio = tokenInicio + " " + CondicionWhile();

            //Envia la traduccion a la tabla
            TablaTraduccionControlador.Instancia.agregar(tokenInicio, "while");

            //Agrega delimitador
            TablaTraduccionControlador.Instancia.agregar("", "#");

            ListaDeclaracion();
            //Agrega delimitador
            TablaTraduccionControlador.Instancia.agregar("", "#");

            if (preAnalisis.Lexema.Equals("}"))
            {
                //Envia a parea la llave de cierre
                Parea(preAnalisis.Descripcion);
                ListaDeclaracion();
            }

        }
        public string CondicionWhile()
        {
            string condicion = "";
            //verifica en que posicion se esta
            int posicionActual = listaTokens.IndexOf(preAnalisis);
            //hace un for recorriendo los tokens para formar la condicion
            for (int i = posicionActual; i < listaTokens.Count; i++)
            {
                //cuando encuentra parentesis se detiene
                if (preAnalisis.Lexema.Equals(")"))
                {
                    //envia a parea los simbolos innecesarios
                    Parea(preAnalisis.Descripcion);
                    Parea(preAnalisis.Descripcion);
                    break;
                }
                else
                {
                    //va formando la condicion
                    condicion = condicion + preAnalisis.Lexema;
                    Parea(preAnalisis.Descripcion);
                }
            }
            return condicion + ":";
        }


        #endregion
        /**
        * METODO IF
        */
        #region TRADUCTOR IF

        public void InicioIf2()
        {
            tokenInicio = "";
            //Envia la palabra if
            Parea(preAnalisis.Descripcion);
            //envia el (
            Parea(preAnalisis.Descripcion);
            //obtiene las condiciones
            tokenInicio = "if " + CondicionIf();
            //Envia el if traducido a la tabla
            TablaTraduccionControlador.Instancia.agregar(tokenInicio, "if");


            //MANDA LA LLAVE {
            Parea(preAnalisis.Descripcion);

            //Envia el if traducido a la tabla
            TablaTraduccionControlador.Instancia.agregar("", "#");

            ListaDeclaracion();
            
            //Envia el if traducido a la tabla
            TablaTraduccionControlador.Instancia.agregar("", "#");


            if (preAnalisis.Lexema.Equals("}"))
            {
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Descripcion.Equals("PR_else"))
                {
                    //Envia el else traducido a la tabla
                    TablaTraduccionControlador.Instancia.agregar("else:", "else");
                    //Envia a parea  los simbolos inecesarios
                    Parea(preAnalisis.Descripcion);
                    Parea(preAnalisis.Descripcion);
                    
                    //Envia delimitadores
                    TablaTraduccionControlador.Instancia.agregar("", "#");

                    ListaDeclaracion();
                    //Envia delimitadores
                    TablaTraduccionControlador.Instancia.agregar("", "#");


                    if (preAnalisis.Lexema.Equals("}"))
                    {
                        Parea(preAnalisis.Descripcion);
                        ListaDeclaracion();
                    }
                }
                else
                {
                    ListaDeclaracion();
                }
            }



        }


        public String CondicionIf()
        {
            string simboloEvaluar = "";
            //verifica que simbolo es el que viene
            if (preAnalisis.Descripcion.Equals("Identificador") || preAnalisis.Descripcion.Equals("Digito") || preAnalisis.Descripcion.Equals("Cadena"))
            {
                simboloEvaluar = preAnalisis.Lexema;
                Parea(preAnalisis.Descripcion);
                /**
                 * SIMBOLOS DE INCREMENTO 
                 */
                 //trae los simbolos condicionales
                simboloEvaluar = simboloEvaluar + " " + SimbolosIf();

                //verifica el token siguiente, si cumple con lo requerido
                if ((preAnalisis.Descripcion.Equals("Identificador")) || (preAnalisis.Descripcion.Equals("Digito"))
                    || (preAnalisis.Descripcion.Equals("Cadena")))
                {
                    //crea la sentencia if
                    simboloEvaluar = simboloEvaluar + " " + preAnalisis.Lexema + ":";
                    //envia a parea los simbolos inecesarios
                    Parea(preAnalisis.Descripcion);
                    Parea(preAnalisis.Descripcion);
                }

            }
            return simboloEvaluar;

        }

        public String SimbolosIf()
        {
            string condicionIf = "";
            switch (preAnalisis.Descripcion)
            {
                case "S_Igual":
                    if (preAnalisis.Descripcion.Equals("S_Igual"))
                    {
                        Parea("S_Igual");
                        if (preAnalisis.Descripcion.Equals("S_Igual"))
                        {
                            condicionIf = "==";
                            Parea("S_Igual");
                        }
                    }
                    break;
                case "S_Mayor_Que":
                    if (preAnalisis.Descripcion.Equals("S_Mayor_Que"))
                    {
                        condicionIf = preAnalisis.Lexema;
                        Parea("S_Mayor_Que");
                        if (preAnalisis.Lexema.Equals("="))
                        {
                            condicionIf = condicionIf + preAnalisis.Lexema;
                            Parea(preAnalisis.Descripcion);
                        }
                    }
                    break;
                case "S_Menor_Que":
                    if (preAnalisis.Descripcion.Equals("S_Menor_Que"))
                    {
                        condicionIf = condicionIf + preAnalisis.Lexema;
                        Parea("S_Menor_Que");
                        if (preAnalisis.Lexema.Equals("="))
                        {
                            condicionIf = condicionIf + preAnalisis.Lexema;
                            Parea(preAnalisis.Descripcion);
                        }
                    }
                    break;
                case "S_Excl":
                    if (preAnalisis.Descripcion.Equals("S_Excl"))
                    {
                        Parea("S_Excl");
                        if (preAnalisis.Descripcion.Equals("S_Igual"))
                        {
                            condicionIf = condicionIf + "!=";
                            Parea("S_Igual");
                        }
                    }

                    break;
                default:
                    break;
            }
            return condicionIf;
        }
        #endregion



        #region CLASE
        public void InicioClase()
        {
            //Envia a parea los simbolos innecesarios
            Parea(preAnalisis.Descripcion);
            Parea(preAnalisis.Descripcion);
            Parea(preAnalisis.Descripcion);

            ListaDeclaracion();
            if (preAnalisis.Lexema.Equals("}"))
            {
                Parea(preAnalisis.Descripcion);
                ListaDeclaracion();
            }

        }

        #endregion

        #region ASIGNACION SIN TIPO
        public void AsignacionSinTipo()
        {
            tokenInicio = "";


            if (preAnalisis.Descripcion.Equals("Identificador"))
            {

                tokenInicio = preAnalisis.Lexema + " = ";
                Parea("Identificador");
                Parea("S_Igual");

                Expresion();
                TablaTraduccionControlador.Instancia.agregar(tokenInicio, "asignacion");
                Parea("S_Punto_y_Coma");
                ListaDeclaracion();
            }
        }
        #endregion


        public void Expresion()
        {
            Termino();
            ExpresionPrima();
            /*if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                Parea("Identificador");
                Expresion();

            }
            else if (preAnalisis.Descripcion.Equals("Cadena"))
            {
                Parea("Cadena");
                Expresion();
            }
            else if (preAnalisis.Descripcion.Equals("Digito"))
            {
                Parea("Digito");
                Expresion();
            }
            else
            {
                //EPSILON
                //error dado que anterior venia signo igual
                //this.lex = ">> Error sintactico se esperaba [ Cadena Digito o Identificador ]";
                ///SintacticoControlador.Instancia.agregarError(preAnalisis.Descripcion, this.lex, preAnalisis.Fila, preAnalisis.Columna);
                //this.errorSintactico = true;
            }*/
        }
        #region Posibles Metodos Innecesarios
        public void ExpresionPrima()
        {
            tokenInicio = tokenInicio + preAnalisis.Lexema;
            if (preAnalisis.Lexema.Equals("+"))
            {
                Parea(preAnalisis.Descripcion);
                EvaluarSiguiente1(preAnalisis.Descripcion);
            }
            else if (preAnalisis.Lexema.Equals("-"))
            {
                Parea(preAnalisis.Descripcion);
                EvaluarSiguiente1(preAnalisis.Descripcion);
            }

            else
            {
                //Epsilon
            }
        }
        public void EvaluarSiguiente1(string texto)
        {
            if (!preAnalisis.Lexema.Equals(texto))
            {
                Termino();
                ExpresionPrima();
            }
            else
            {
            }
        }
        public void Termino()
        {
            Factor();
            TerminoPrima();
        }

        public void TerminoPrima()
        {
            tokenInicio = tokenInicio + preAnalisis.Lexema;
            if (preAnalisis.Lexema.Equals("*"))
            {
                Parea(preAnalisis.Descripcion);
                EvaluaSigiente(preAnalisis.Descripcion);
            }
            else if (preAnalisis.Lexema.Equals("/"))
            {
                Parea(preAnalisis.Descripcion);
                EvaluaSigiente(preAnalisis.Descripcion);
            }
            else
            {
                //Epsilon
            }
        }
        public void EvaluaSigiente(string texto)
        {
            if (!preAnalisis.Lexema.Equals(texto))
            {
                Factor();
                TerminoPrima();
            }
            else
            {
            }
        }
        public void Factor()
        {
            tokenInicio = tokenInicio + preAnalisis.Lexema;
            if (preAnalisis.Lexema.Equals("("))
            {
                Parea(preAnalisis.Descripcion);
                Expresion();
                Parea("S_Parentesis_Derecho");

            }
            else if (preAnalisis.Descripcion.Equals("Digito") || preAnalisis.Descripcion.Equals("Cadena"))
            {
                Parea(preAnalisis.Descripcion);
            }
            else if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                Parea(preAnalisis.Descripcion);

                if (preAnalisis.Descripcion.Equals("S_Punto"))
                {
                    Parea("S_Punto");
                    if (preAnalisis.Descripcion.Equals("Identificador"))
                    {
                        Parea("Identificador");
                    }
                    else
                    {
                    }
                }
                else
                {

                    //Epsiolon
                }
            }
            else
            {
            }
        }
        #endregion

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


    }

}
