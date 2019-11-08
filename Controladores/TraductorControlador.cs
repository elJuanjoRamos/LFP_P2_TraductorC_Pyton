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
        int ambito = 0;


        //-------TRADUCCIONES ---------

        //Variables variable
        string lexemaAuxiliar = "";
        string variableDeclaracion = "";

        //Variables arreglo
        string nombreArregloDeclarado = "";
        string nombreArregloVacio = "";

        //importante
        string contenidoDeclarado = "";

        string contenidoVacio = "";
        //Variables for
        string cadenaFor = "";
        string cadenaFor2 = "";
        string aumentoDism = "";
        string variableDeclarada = "";

        //Variables console
        string print = "";

        //Variables if
        string condicionIf = "";


        //Variables switch
        string tipoInicio = "";
        string variableSwitch = "";
        string valorVariableSwitch = "";
        string cuerpoSwitch = "";
        string cuerpoCase = "";
        string tipoInicioAux = "";
        int iteracionesSwitch = 0;
        //guarda el token actual para evitar cosas como console.writeline( + algo ) o  console.writeline(  algo + ) lo que daria error sintactico
        String tokenPrevio = "";

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
        public void obtenerLista(ArrayList listaTokens)
        {
            this.listaTokens = listaTokens;
            indice = 0;
            preAnalisis = (Token)listaTokens[indice];
            this.tokenInicio = "";
            Inicio();
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
               // InicioFor();
            }
            else if (preAnalisis.Descripcion.Equals("PR_Console"))
            {
               // InicioConsole();
            }
            else if (preAnalisis.Descripcion.Equals("PR_switch"))
            {
                InicioSwitch();
            }
            else if (preAnalisis.Descripcion.Equals("PR_class"))
            {
               // Clase();
            }
            else if (preAnalisis.Descripcion.Equals("PR_if"))
            {
               //InicioIf2();
            }
            else if (preAnalisis.Descripcion.Equals("PR_while"))
            {
               // InicioWhile();
            }
            else if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
            {
               // ComentarioLinea();
            }
            else if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
               // ComentarioMultiLinea();
            }
            else
            {
                //this.lex = "Error Sintactico: Se esperaba palabra reservada en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                //errorSintactico = true;
                //Epsilon
            }
        }

        /**
         *  DECLARACION Y ASIGNACION
         */
        #region DECLARACION Y ASIGNACION DE VARIABLES 

        public void InicioVariable()
        {
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
            tokenInicio = " " + tokenInicio + "\n" + ListaId() + " " + OpcAsignacion();
            ListaDeclaracion();
            //PuntoComa();
        }

        string listaIds = "";
        public String ListaId()
        {
            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                if (!listaIds.Contains(",")) { listaIds = preAnalisis.Lexema; variableSwitch = preAnalisis.Lexema; } else
                { listaIds = listaIds + " "+ preAnalisis.Lexema; }
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
                return "= " + Expresion();
            }
            else
            {
                //Epsilon
            }
            return "";
        }
        string valorVariable = "";
        public String Expresion()
        {
            if (preAnalisis.Descripcion.Equals("Digito") || preAnalisis.Descripcion.Equals("Cadena") || preAnalisis.Descripcion.Equals("Identificador"))
            {
                valorVariable = preAnalisis.Lexema;
                valorVariableSwitch = preAnalisis.Lexema;
                Parea(preAnalisis.Descripcion);
                Parea(preAnalisis.Descripcion);
                return valorVariable;
            }
            return "";
        }
        #endregion


        #region DECLARACION DE ARREGLO
        // viene de la declaracion de variable
        public void InicioArreglo()
        {
            //Corchete manda a parea el corrchete izquierdo
            Parea(preAnalisis.Descripcion);
            //Corchete manda a parea el corrchete derecho
            Parea(preAnalisis.Descripcion);
            string nombreArreglo = "";
            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                nombreArreglo = preAnalisis.Lexema;
                Parea("Identificador");
                tokenInicio = tokenInicio + "\n" + nombreArreglo + OpcAsignacionArreglo();
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
                Parea(preAnalisis.Descripcion);
                Parea(preAnalisis.Descripcion);
                contenidoDeArreglo = "[]";
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
        #region DECLARACION SWITCH 
        public void InicioSwitch()
        {
            Parea(preAnalisis.Descripcion);
            if (preAnalisis.Descripcion.Equals("S_Parentesis_Izquierdo"))
            {
                
                Parea(preAnalisis.Descripcion);
                tokenInicio = tokenInicio + AsignacionSwitch();
                
                if (errorSintactico == false)
                {
                    if (preAnalisis.Descripcion.Equals("S_Parentesis_Derecho"))
                    {
                        
                        Parea(preAnalisis.Descripcion);
                        if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
                        {
                            Parea(preAnalisis.Descripcion);
                            tokenInicio = tokenInicio  + "\n" + CuerpoSwitch();
                            if (errorSintactico == false)
                            {
                                if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                                {
                                    Parea(preAnalisis.Descripcion);
                                    ListaDeclaracion();
                                }
                                else
                                {
                                    this.lex = ">>Error sintactico: Se esperaba llave de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                    this.tok = "";
                                    errorSintactico = true;
                                }
                            }
                        }
                        else
                        {
                            this.lex = ">>Error sintactico: Se esperaba llave de apertura en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            this.tok = "";
                            errorSintactico = true;
                        }
                    }
                    else
                    {
                        this.lex = ">>Error sintactico: Se esperaba parentesis de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        this.tok = "";
                        errorSintactico = true;
                    }
                }
            }
            else
            {
                this.lex = ">>Error sintactico: Se esperaba parentesis de apertura en lugar de [ " + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + " ]";
                this.tok = "";
                errorSintactico = true;
            }
        }
        public string AsignacionSwitch()
        {
            if (preAnalisis.Lexema.Equals(variableSwitch))
            {
                //Envia a la traduccion
                Parea(preAnalisis.Descripcion);
                return "";
            }
            else
            {
                this.lex = ">>Error sintactico: La variable [" + preAnalisis.Lexema + "] no esta declarada";
                this.tok = "";
                errorSintactico = true;
                return "";
            }
        }

        public String CuerpoSwitch()
        {
            if (preAnalisis.Descripcion.Equals("PR_case"))
            {
                //va armando la traduccion del switch
                if (iteracionesSwitch == 0) {
                    cuerpoSwitch = cuerpoCase + " if " + variableSwitch;
                    iteracionesSwitch = 1;
                }
                else {
                    cuerpoSwitch = cuerpoCase + "else if " + variableSwitch;
                }


                Parea(preAnalisis.Descripcion);
                //verifica si concuerdan los tipos, es decir que el caso a analizar concuerde con el tipo de variable
                //
                //  String texto = ""; ----> como la variable declarada es string
                //
                //  switch(texto){
                //      case "":  ---> la variable del case debe ser igual a string   
                //
                if ((preAnalisis.Descripcion.Equals("Cadena") && (tipoInicio.Equals("PR_string") || tipoInicio.Equals("PR_char"))) ||
                    (preAnalisis.Descripcion.Equals("Digito") && (tipoInicio.Equals("PR_int") || tipoInicio.Equals("PR_float"))))
                {
                    cuerpoSwitch = cuerpoSwitch + " == " + preAnalisis.Lexema;
                    Parea(preAnalisis.Descripcion);
                    if (preAnalisis.Descripcion.Equals("S_Dos_puntos"))
                    {
                        Parea(preAnalisis.Descripcion);
                        cuerpoSwitch = cuerpoSwitch + ":";
                        CuerpoCase();
                        return cuerpoSwitch;

                        //envia a la traduccion
                        //  traduccion(cuerpoSwitch  + "\n" + cuerpoCase);
                        if (errorSintactico == false)
                        {
                            if (preAnalisis.Descripcion.Equals("PR_break"))
                            {
                                Parea(preAnalisis.Descripcion);
                                if (preAnalisis.Descripcion.Equals("S_Punto_y_Coma"))
                                {
                                    Parea(preAnalisis.Descripcion);
                                    CuerpoSwitch();
                                }
                                else
                                {
                                    this.lex = ">>Error Sintactico: Se esperaban punto y coma en lugar de [" + preAnalisis.Descripcion + " ]";
                                    this.tok = "";
                                    errorSintactico = true;
                                }
                            }
                            else
                            {
                                this.lex = ">>Error Sintactico: Se esperaban palabra reservada BREAK en lugar de [" + preAnalisis.Descripcion + " ]";
                                this.tok = "";
                                errorSintactico = true;
                            }
                        }
                        return cuerpoSwitch;
                    }
                    else
                    {
                        this.lex = ">>Error Sintactico: Se esperaban dos puntos el lugar de [" + preAnalisis.Descripcion + " ]";
                        this.tok = "";
                        errorSintactico = true;
                    }
                }
                else
                {
                    this.lex = ">>Error Sintactico: El tipo de variable [ " + tipoInicio + "] no concuerda con el tipo de evaluacion [ " + preAnalisis.Descripcion + " ] del case";
                    this.tok = "";
                    errorSintactico = true;
                }
            }
            else if (preAnalisis.Descripcion.Equals("PR_default"))
            {
                Parea(preAnalisis.Descripcion);

                if (preAnalisis.Descripcion.Equals("S_Dos_puntos"))
                {
                    cuerpoSwitch = "else" + ":";
                    Parea(preAnalisis.Descripcion);
                    ListaDeclaracion();
                    //envia a la traduccion
                    //traduccion(cuerpoSwitch + "\n" + cuerpoCase);
                    if (errorSintactico == false)
                    {
                        if (preAnalisis.Descripcion.Equals("PR_break"))
                        {
                            Parea(preAnalisis.Descripcion);
                            if (preAnalisis.Descripcion.Equals("S_Punto_y_Coma"))
                            {
                                Parea(preAnalisis.Descripcion);
                                CuerpoSwitch();
                            }
                            else
                            {
                                this.lex = ">>Error Sintactico: Se esperaban punto y coma en lugar de [" + preAnalisis.Descripcion + " ]";
                                this.tok = "";
                                errorSintactico = true;
                            }
                        }
                        else
                        {
                            this.lex = ">>Error Sintactico: Se esperaban palabra reservada BREAK en lugar de [" + preAnalisis.Descripcion + " ]";
                            this.tok = "";
                            errorSintactico = true;
                        }
                    }
                }
                else
                {
                    this.lex = ">>Error Sintactico: Se esperaban dos puntos el lugar de [" + preAnalisis.Descripcion + " ]";
                    this.tok = "";
                    errorSintactico = true;
                }

            }
            else if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
            {

            }
            else
            { 
                this.lex = ">>Error Sintactico: Se esperaba palabra reservada CASE en lugar de [ " + preAnalisis.Lexema + " ]";
                this.tok = "";
                errorSintactico = true;
                return "";
            }
            return "";
        }

        public void CuerpoCase()
        {
            ListaDeclaracion();

        }
        #endregion


        /**
    *   Parea
    *  Este metodo lo que hace es comparar si el token de preanalisis tiene le tipo que le indicamos, 
    *  en caso no sean iguales maraca error.
    * */
        string tok = " ";
        string lex = " ";
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
                tok = tok + " " + tipoToken;

                if (indice < listaTokens.Count - 1)
                {
                    if (preAnalisis.Descripcion.Equals(tipoToken))
                    {
                        indice++;
                        preAnalisis = (Token)listaTokens[indice];
                        lex = lex + " " + preAnalisis.Lexema;
                    }
                    else
                    {
                        //Se genera un error sintactico y se agrega a la lista de errores sintacitos
                        this.lex = ">>Error sintactico se esperaba [" + tipoToken + "] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        errorSintactico = true;
                    }
                }
            }

        }

        public string returnT()
        {
            if (errorSintactico == true)
            {
                return this.lex;
            }
            else
            {
                return this.lex + "\n" + this.tok + "\n";
            }
        }


        public void traduccionComentario(string cadena, string tipo)
        {
            if (tipo.Equals("ComentarioLinea"))
            {
                cadena = cadena.Replace("//", "#");
            }
            else if (tipo.Equals("ComentarioMultiLinea"))
            {
                cadena = cadena.Replace("/*", "' ' '");
                cadena = cadena.Replace("*/", "' ' '" + "\n");
            }
        }

        ArrayList tokensTraducidos = new ArrayList();
        int iteracionesFor = 0;
        string finalFor = "";
        public void traduccion(string cadena)
        {
            Console.WriteLine("la cadena es " + cadena);
            tokensTraducidos.Add(cadena);
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
