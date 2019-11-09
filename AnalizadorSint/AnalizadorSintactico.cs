using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFP_P2_TraductorC_Pyton.Modelos;
using LFP_P2_TraductorC_Pyton.Controladores;

namespace LFP_P2_TraductorC_Pyton.AnalizadorSint
{
    class AnalizadorSintactico
    {
        private readonly static AnalizadorSintactico instancia = new AnalizadorSintactico();
        ArrayList listaTokens = new ArrayList();
        int indice = 0;
        Token preAnalisis = null;
        Boolean errorSintactico = false;
        String tokenInicio = "";
        string identificadorFor = "";



        //-------TRADUCCIONES ---------

        //Variables variable
        string lexemaAuxiliar = "";
        string variableDeclaracion = "";

        //Variables arreglo
        string nombreArregloDeclarado = "";
        string nombreArregloVacio = "";
        string contenidoDeclarado = "";
        string contenidoVacio = "";
        
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

        private AnalizadorSintactico()
        {

        }

        public static AnalizadorSintactico Instancia
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
            this.tok = ""; this.lex = "";
            this.tokenInicio = preAnalisis.Descripcion;
            this.errorSintactico = false;
            Inicio();
        }

        public void Inicio()
        {
            ListaDeclaracion();
        }

        /**En casos donde se mande a llamar algun metodo al inicio se debe de averiguar si el token actual es parte de los 
        *   primeros del metodo que se esta llamando de lo contrario es error (a menos que se acepte epsilon)
         * En este caso este metodo llama a DeclaracionVariable y DeclaracionVariable llama a tipo y los primeros serian los no terminales int, double, class, etc
        **/
        public void ListaDeclaracion()
        {
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };

            //Manda a llamar a metodo variable
            if (Array.Exists(reservadasVariable, element => element == preAnalisis.Descripcion))
            {
                InicioVariable();
            }
            else if (preAnalisis.Descripcion.Equals("PR_for"))
            {
                InicioFor();
            }
            else if (preAnalisis.Descripcion.Equals("PR_Console"))
            {
                InicioConsole();
            }
            else if (preAnalisis.Descripcion.Equals("PR_switch"))
            {
                InicioSwitch();
            }
            else if (preAnalisis.Descripcion.Equals("PR_class"))
            {
                Clase();
            }
            else if (preAnalisis.Descripcion.Equals("PR_if"))
            {
                InicioIf2();
            }
            else if (preAnalisis.Descripcion.Equals("PR_while"))
            {
                InicioWhile();
            }
            else if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
            {
                ComentarioLinea();
            }
            else if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
                ComentarioMultiLinea();
            }
            else if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                AsignacionSinTipo();
            }
            else {

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
            tokenInicio = "";
            tokenInicio = preAnalisis.Descripcion;

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
            tokenInicio = "";
            tokenInicio = preAnalisis.Descripcion;
            Tipo();
            //ListaId();
            //OpcAsignacion();
            //PuntoComa();
        }

        public void Tipo()
        {

            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };

            if (Array.Exists(reservadasVariable, element => element == preAnalisis.Descripcion))
            {
                this.tipoInicio = preAnalisis.Descripcion;
                Parea(preAnalisis.Descripcion);
                //Por si se esta declarando un arreglo;
                if (preAnalisis.Descripcion.Equals("S_Corchete_Izquierdo"))
                {
                    Parea("S_Corchete_Izquierdo");
                    DeclararArreglo();
                }
                else
                {
                    ListaId();
                    OpcAsignacion();
                    PuntoComa();
                }
            }
        }

        public void ListaId()
        {

            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                this.lexemaAuxiliar = preAnalisis.Lexema;
                variableDeclaracion = lexemaAuxiliar;
                Parea("Identificador");
                ListaId1();
            }
            else
            {
                // viene epsilon
                this.lex = ">> Error sintactico se esperaba [ identificador ] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                this.tok = "";
                errorSintactico = true;
            }
        }
        public void ListaId1()
        {
            if (preAnalisis.Descripcion.Equals("S_Coma"))
            {
                Parea("S_Coma");
                ListaId();
            }
            else
            {
                //Epsilon
            }
        }
        public void OpcAsignacion()
        {
            if (preAnalisis.Lexema.Equals("="))
            {

                Parea("S_Igual");
                Expresion();
            }
            else
            {
                //Epsilon
            }
        }
        public void Expresion()
        {
            if (preAnalisis.Descripcion.Equals("Digito") && (tokenInicio.Equals("PR_int") || tokenInicio.Equals("PR_float")))
            {
                Parea("Digito");
            }
            else if (preAnalisis.Descripcion.Equals("Cadena") && (tokenInicio.Equals("PR_char") || tokenInicio.Equals("PR_string")))
            {
                Parea("Cadena");
            }
            else if (preAnalisis.Descripcion.Equals("Identificador") && (tokenInicio.Equals("PR_bool") || tokenInicio.Equals("PR_boolean")))
            {
                Parea("Identificador");
            }
            else
            {
                errorSintactico = true;
                this.tok = "";
                this.lex = ">>Error sintactico: el tipo de variable " + this.tokenInicio + " no coincide con el valor de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                Parea(preAnalisis.Descripcion);
            }
        }
        public void PuntoComa()
        {
            if (preAnalisis.Descripcion.Equals("S_Punto_y_Coma"))
            {
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Descripcion.Equals("PR_switch"))
                {
                    this.variableSwitch = this.lexemaAuxiliar;
                    InicioSwitch();
                }
                else
                {
                    ListaDeclaracion();
                }
            }
            else if (preAnalisis.Descripcion.Equals("S_Coma"))
            {
                Parea("S_Coma");
                ListaId();
                OpcAsignacion();
                PuntoComa();
            }
            else
            {
                this.lex = ">>Error sintactico: Se esperaba [ punto y coma  ] al final de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                this.tok = "";
                errorSintactico = true;
            }
        }
        #endregion

        /**
         *  DECLARACION ARREGLO
         */
        #region DECLARACION DE ARREGLO
        // viene de la declaracion de variable
        public void DeclararArreglo()
        {
            if (preAnalisis.Descripcion.Equals("S_Corchete_Derecho"))
            {
                Parea("S_Corchete_Derecho");
                NombreArreglo();
                OpcAsignacionArreglo();
            }
            else
            {
                this.lex = ">>Error sintactico: Se esperaba corchete de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                this.tok = "";
                errorSintactico = true;
            }
        }
        public void NombreArreglo()
        {
            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                this.nombreArregloDeclarado = preAnalisis.Lexema;
                Parea("Identificador");
            }
            else
            {
                // viene epsilon
                this.lex = ">> Error sintactico se esperaba [ identificador ] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                this.tok = "";
                errorSintactico = true;
            }
        }

        public void OpcAsignacionArreglo()
        {
            if (preAnalisis.Lexema.Equals("="))
            {
                Parea("S_Igual");
                ExpresionArreglo();
            }
            else
            {
                //Epsilon
            }
        }


        // Estos metodos sirven por si el arreglo se declara de una vez, es decir arreglo = {a, b, c}
        public void ExpresionArreglo()
        {
            if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
            {
                Parea("S_Llave_Izquierda");
                ListaValor();

                if (errorSintactico == false)
                {
                    if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                    {
                        Parea("S_Llave_Derecha");
                        PuntoComa();
                        ListaDeclaracion();
                    }
                    else
                    {
                        this.lex = ">>Error sintactico: Se esperaba Llave de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        this.tok = "";
                        errorSintactico = true;
                    }
                }
            }
            else if (preAnalisis.Descripcion.Equals("PR_new"))
            {
                Parea("PR_new");
                ExpresionArreglo2();
            }
            else
            {
                this.lex = ">>Error sintactico: Se esperaba Llave apertura en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                this.tok = "";
                errorSintactico = true;
            }
        }
        public void ListaValor()
        {

            if ((preAnalisis.Descripcion.Equals("Digito") && (tokenInicio.Equals("PR_int") || tokenInicio.Equals("PR_float")))
                ||
                (preAnalisis.Descripcion.Equals("Cadena") && (tokenInicio.Equals("PR_char") || tokenInicio.Equals("PR_string")))
                ||
                (preAnalisis.Descripcion.Equals("Identificador") && (tokenInicio.Equals("PR_bool") || tokenInicio.Equals("PR_boolean"))))
            {
                this.contenidoDeclarado = "[" + preAnalisis.Lexema;
                Parea(preAnalisis.Descripcion);
                ListaValor1();
                this.contenidoDeclarado = this.contenidoDeclarado + "]";

            }
            else
            {
                this.lex = ">>Error Sintactico: para un arreglo tipo " + tokenInicio + " se esperan valores numericos en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                this.tok = "";
                errorSintactico = true;
            }

        }
        public void ListaValor1()
        {
            if (preAnalisis.Descripcion.Equals("S_Coma"))
            {
                Parea("S_Coma");

                if (preAnalisis.Descripcion.Equals("Digito") && (tokenInicio.Equals("PR_int") || tokenInicio.Equals("PR_float")))
                {
                    this.contenidoDeclarado = this.contenidoDeclarado + "," + preAnalisis.Lexema;
                    Parea("Digito");
                    ListaValor1();
                }
                else if (preAnalisis.Descripcion.Equals("Cadena") && ((tokenInicio.Equals("PR_char") || tokenInicio.Equals("PR_string"))))
                {
                    this.contenidoDeclarado = this.contenidoDeclarado + "," + preAnalisis.Lexema;
                    Parea("Cadena");
                    ListaValor1();
                }
                else if (preAnalisis.Descripcion.Equals("Identificador") && ((tokenInicio.Equals("PR_bool") || tokenInicio.Equals("PR_boolean"))))
                {
                    this.contenidoDeclarado = this.contenidoDeclarado + "," + preAnalisis.Lexema;
                    Parea("Identificador");
                    ListaValor1();
                }
                else
                {
                    Parea(preAnalisis.Descripcion);
                    this.lex = ">>Error Sintactico: para un arreglo tipo " + tokenInicio + " se esperan valores tipo " + tokenInicio + " en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                    this.tok = "";
                    errorSintactico = true;
                }
            }
            else
            {
                //Epsilon
            }
        }

        // estos metodos sirven por si el arreglo se declara como new, es decir new tipo[]

        public void ExpresionArreglo2()
        {
            if (preAnalisis.Descripcion.Equals(tokenInicio))
            {
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Descripcion.Equals("S_Corchete_Izquierdo"))
                {
                    Parea("S_Corchete_Izquierdo");
                    if (preAnalisis.Descripcion.Equals("S_Corchete_Derecho"))
                    {
                        Parea("S_Corchete_Derecho");
                        PuntoComa();
                        ListaDeclaracion();
                    }
                    else if (preAnalisis.Descripcion.Equals("Identificador") || preAnalisis.Descripcion.Equals("Digito"))
                    {
                        Parea(preAnalisis.Descripcion);
                        if (preAnalisis.Descripcion.Equals("S_Corchete_Derecho"))
                        {
                            Parea("S_Corchete_Derecho");
                            PuntoComa();
                            ListaDeclaracion();
                        }
                        else
                        {
                            this.lex = ">>Error Sintactico: se esperaba parentesis de cierre en lugar de [" + preAnalisis.Descripcion + "]";
                            this.tok = "";
                            errorSintactico = true;
                        }
                    }
                    else
                    {
                        this.lex = ">>Error Sintactico: se esperaba parentesis de cierre en lugar de [" + preAnalisis.Descripcion + "]";
                        this.tok = "";
                        errorSintactico = true;
                    }
                }
                else
                {
                    this.lex = ">>Error Sintactico: se esperaba parentesis de apertura en lugar de [" + preAnalisis.Descripcion + "]";
                    this.tok = "";
                    errorSintactico = true;
                }
            }
            else
            {
                this.lex = ">>Error Sintactico: el tipo del arreglo debe ser el mismo que el de su asignacion, " + tokenInicio + "[] = new " + tokenInicio + "[] en lugar de " + preAnalisis.Descripcion + "[]";
                this.tok = "";
                errorSintactico = true;
            }
        }
        #endregion

        /**
         *  DECLARACION FOR
         */
        #region DECLARACION FOR 


        public void InicioFor()
        {
            Parea(preAnalisis.Descripcion);
            if (preAnalisis.Descripcion.Equals("S_Parentesis_Izquierdo"))
            {
                Parea("S_Parentesis_Izquierdo");
                Asignacion();
            }
            else
            {

                this.lex = ">>Error Sintactico: Se esperaba parentesis de apertura";
                errorSintactico = true;
            }
        }

        public void Asignacion()
        {
            if (preAnalisis.Descripcion.Equals("PR_int"))
            {
                Parea("PR_int");
                if (preAnalisis.Descripcion.Equals("Identificador"))
                {
                    identificadorFor = preAnalisis.Lexema;
                    Parea("Identificador");
                    if (preAnalisis.Descripcion.Equals("S_Igual"))
                    {
                        Parea("S_Igual");
                        if (preAnalisis.Descripcion.Equals("Digito"))
                        {
                            try
                            {
                                int.Parse(preAnalisis.Lexema);
                                Parea(preAnalisis.Descripcion);
                                if (preAnalisis.Descripcion.Equals("S_Punto_y_Coma"))
                                {
                                    Parea("S_Punto_y_Coma");
                                    ExpresionFor();
                                }
                                else
                                {
                                    this.lex = ">>Error Sintactico: Se esperaba palabra punto y coma en lugar de [" + preAnalisis.Descripcion + "]";
                                    this.tok = "";
                                    errorSintactico = true;
                                }
                            }
                            catch
                            {
                                this.lex = ">>Error Sintactico: Se esperaba valor entero en lugar de [" + preAnalisis.Descripcion + "]";
                                this.tok = "";
                                errorSintactico = true;
                            }
                        }
                        else
                        {
                            this.lex = ">>Error Sintactico: Se esperaba palabra valor numerico en lugar de [" + preAnalisis.Descripcion + "]";
                            this.tok = "";
                            errorSintactico = true;
                        }
                    }
                    else
                    {
                        this.lex = ">>Error Sintactico: Se esperaba palabra Signo igual en lugar de [" + preAnalisis.Descripcion + "]";
                        this.tok = "";
                        errorSintactico = true;
                    }
                }
                else
                {
                    this.lex = ">>Error Sintactico: Se esperaba palabra Identificador en lugar de [" + preAnalisis.Descripcion + "]";
                    this.tok = "";
                    errorSintactico = true;
                }
            }
            else
            {
                this.lex = ">>Error Sintactico: Se esperaba palabra reservada INT en lugar de [" + preAnalisis.Descripcion + "]";
                this.tok = "";
                errorSintactico = true;
            }
        }


        public void ExpresionFor()
        {
            if (identificadorFor.Equals(preAnalisis.Lexema))
            {
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Descripcion.Equals("S_Menor_Que") || preAnalisis.Descripcion.Equals("S_Mayor_Que"))
                {
                    Parea(preAnalisis.Descripcion);
                    if (preAnalisis.Lexema.Equals("="))
                    {
                        Parea("S_Igual");
                    }
                    if (preAnalisis.Descripcion.Equals("Digito"))
                    {
                        try
                        {
                            int.Parse(preAnalisis.Lexema);
                            Parea("Digito");
                            if (preAnalisis.Descripcion.Equals("S_Punto_y_Coma"))
                            {
                                Parea("S_Punto_y_Coma");
                                if (identificadorFor.Equals(preAnalisis.Lexema))
                                {
                                    Parea(preAnalisis.Descripcion);
                                    SimboloIncrementoDecremento();
                                }
                                else
                                {
                                    this.lex = ">>Error Sintactico: La variable [" + preAnalisis.Lexema + "]  no esta definida";
                                    this.tok = "";
                                    errorSintactico = true;
                                }
                            }
                            else
                            {
                                this.lex = ">>Error Sintactico: Se esperaba palabra punto y coma en lugar de [" + preAnalisis.Descripcion + "]";
                                this.tok = "";
                                errorSintactico = true;
                            }
                        }
                        catch (Exception)
                        {

                            this.lex = ">>Error Sintactico: Se esperaba valores enteros en lugar de [" + preAnalisis.Descripcion + "]";
                            this.tok = "";
                            errorSintactico = true;
                        }


                        //Aqui vendria la parte de evaluar si lo que viene es el tamaño de un array, es decir array.count
                    } /*else if (true)
                        {
                        }*/
                    else
                    {
                        this.lex = ">>Error Sintactico: Se esperaba un valor numerico en lugar de [" + preAnalisis.Lexema + "]";
                        this.tok = "";
                        errorSintactico = true;
                    }
                }
                else
                {
                    //Epsilon u otro simbolo
                    this.lex = ">>Error Sintactico: Se esperaba simbolo mayor que o menor que en lugar de [" + preAnalisis.Lexema + "]";
                    this.tok = "";
                    errorSintactico = true;
                }
            }
            else
            {
                this.lex = ">>Error Sintactico: La variable [" + preAnalisis.Lexema + "]  no esta definida";
                this.tok = "";
                errorSintactico = true;
            }
        }

        public void SimboloIncrementoDecremento()
        {
            if (preAnalisis.Descripcion.Equals("S_Suma"))
            {
                Parea("S_Suma");
                if (preAnalisis.Descripcion.Equals("S_Suma"))
                {
                    Parea("S_Suma");
                    if (preAnalisis.Descripcion.Equals("S_Parentesis_Derecho"))
                    {
                        Parea("S_Parentesis_Derecho");
                        if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
                        {
                            Parea("S_Llave_Izquierda");
                            //Esto es lo que va a contener el for
                            ListaDeclaracion();
                            
                            if (errorSintactico != true)
                            {
                                if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                                {

                                    Parea("S_Llave_Derecha");
                                    ListaDeclaracion();
                                }
                                else
                                {
                                    this.lex = ">>Error Sintactico: Se esperaban llave de cierre en lugar de [ " + preAnalisis.Lexema + "]";
                                    this.tok = "";
                                    errorSintactico = true;
                                }
                            }
                        }
                        else
                        {
                            this.lex = ">>Error Sintactico: Se esperaban llave de apertura en lugar de [ " + preAnalisis.Lexema + "]";
                            this.tok = "";
                            errorSintactico = true;
                        }
                    }
                    else
                    {
                        this.lex = ">>Error Sintactico: Se esperaban parentesis de cierre en lugar de [ " + preAnalisis.Lexema + "]";
                        this.tok = "";
                        errorSintactico = true;
                    }
                }
                else
                {
                    this.lex = ">>Error Sintactico: Se esperaban simbolo de incremento (+) en lugar de [ " + preAnalisis.Lexema + "]";
                    this.tok = "";
                    errorSintactico = true;
                }
            }
            else if (preAnalisis.Descripcion.Equals("S_Resta"))
            {
                Parea("S_Resta");
                if (preAnalisis.Descripcion.Equals("S_Resta"))
                {
                    Parea("S_Resta");
                    if (preAnalisis.Descripcion.Equals("S_Parentesis_Derecho"))
                    {
                        Parea("S_Parentesis_Derecho");
                        if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
                        {
                            Parea("S_Llave_Izquierda");
                            //Esto es lo que va a contener el for
                            ListaDeclaracion();
                            if (errorSintactico != true)
                            {
                                if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                                {
                                    Parea("S_Llave_Derecha");
                                    ListaDeclaracion();
                                }
                                else
                                {
                                    this.lex = ">>Error Sintactico: Se esperaban llave de cierre en lugar de [ " + preAnalisis.Lexema + "]";
                                    this.tok = "";
                                    errorSintactico = true;
                                }
                            }
                        }
                        else
                        {
                            this.lex = ">>Error Sintactico: Se esperaban llave de apertura en lugar de [ " + preAnalisis.Lexema + "]";
                            this.tok = "";
                            errorSintactico = true;
                        }

                    }
                    else
                    {
                        this.lex = ">>Error Sintactico: Se esperaban parentesis de cierre en lugar de [ " + preAnalisis.Lexema + "]";
                        this.tok = "";
                        errorSintactico = true;
                    }
                }
                else
                {
                    this.lex = ">>Error Sintactico: Se esperaban simbolo de decremeto (-) en lugar de [ " + preAnalisis.Lexema + "]";
                    this.tok = "";
                    errorSintactico = true;
                }
            }
            else
            {
                this.lex = ">>Error Sintactico: Se esperaban simbolos de incremento (++) o decremeto (--) en lugar de [ " + preAnalisis.Lexema + "]";
                this.tok = "";
                errorSintactico = true;
            }
        }
        #endregion

        /**
         * CONSOLE WRITELINE
         */
        #region IMPRIMIR EN PANTALLA
        public void InicioConsole()
        {
            Parea(preAnalisis.Descripcion);
            if (preAnalisis.Descripcion.Equals("S_Punto"))
            {
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Descripcion.Equals("PR_WriteLine"))
                {
                    Parea(preAnalisis.Descripcion);
                    if (preAnalisis.Descripcion.Equals("S_Parentesis_Izquierdo"))
                    {
                        Parea(preAnalisis.Descripcion);
                        this.tokenPrevio = preAnalisis.Descripcion;
                        CuerpoConsole();
                        if (errorSintactico == false)
                        {
                            if (preAnalisis.Descripcion.Equals("S_Parentesis_Derecho"))
                            {
                                Parea(preAnalisis.Descripcion);
                                PuntoComa();
                            }
                            else
                            {
                                errorSintactico = true;
                                this.lex = ">>Error Sintactico: Se esperaba parentesis de cierre en lugar de [" + preAnalisis.Descripcion + "]";
                            }
                        }
                    }
                    else
                    {
                        errorSintactico = true;
                        this.lex = ">>Error Sintactico: Se esperaba parentesis de apertura en lugar de [" + preAnalisis.Descripcion + "]";
                    }
                }
                else
                {
                    errorSintactico = true;
                    this.lex = ">>Error Sintactico: Se esperaba palabra reservada WriteLine en lugar de [" + preAnalisis.Descripcion + "]";
                }
            }
            else
            {
                errorSintactico = true;
                this.lex = ">>Error Sintactico: Se esperaba punto en lugar de [" + preAnalisis.Descripcion + "]";
            }
        }

        public void CuerpoConsole()
        {

            if (preAnalisis.Descripcion.Equals("Digito") || preAnalisis.Descripcion.Equals("Identificador") || preAnalisis.Descripcion.Equals("Cadena"))
            {
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Descripcion.Equals("S_Suma"))
                {
                    CuerpoConsole();
                }
                else if (preAnalisis.Descripcion.Equals("Digito") || preAnalisis.Descripcion.Equals("Identificador") || preAnalisis.Descripcion.Equals("Cadena"))
                {
                    this.lex = "Error Sintactico: Se esperaba signo mas enlugar de [" + preAnalisis.Descripcion + "]";
                    errorSintactico = true;
                }
                else
                {
                    CuerpoConsole();
                }

            }
            else if (preAnalisis.Descripcion.Equals("S_Suma"))
            {
                if (tokenPrevio != "S_Suma" && tokenPrevio != "")
                {
                    Parea(preAnalisis.Descripcion);
                    if (preAnalisis.Descripcion.Equals("Digito") || preAnalisis.Descripcion.Equals("Identificador") || preAnalisis.Descripcion.Equals("Cadena"))
                    {
                        Parea(preAnalisis.Descripcion);
                        CuerpoConsole();
                    }
                    else
                    {
                        this.lex = "Error Sintactico: Se esperaba Digito, Identificador o cadena en lugar de [" + preAnalisis.Descripcion + "]";
                        errorSintactico = true;
                    }
                }
                else
                {
                    this.lex = "Error Sintactico: Se esperaba Digito, Identificador o cadena antes de [" + preAnalisis.Descripcion + "]";
                    errorSintactico = true;
                }
            }
            else
            {

            }
        }
        #endregion

        /**
         * METODO SWITCH
         */
        #region DECLARACION SWITCH 
        public void InicioSwitch()
        {
            this.tipoInicioAux = tipoInicio;
            Parea(preAnalisis.Descripcion);
            if (preAnalisis.Descripcion.Equals("S_Parentesis_Izquierdo"))
            {
                Parea(preAnalisis.Descripcion);
                AsignacionSwitch();
                if (errorSintactico == false)
                {
                    if (preAnalisis.Descripcion.Equals("S_Parentesis_Derecho"))
                    {
                        Parea(preAnalisis.Descripcion);
                        if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
                        {
                            Parea(preAnalisis.Descripcion);
                            CuerpoSwitch();
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
        public void AsignacionSwitch()
        {
            if (preAnalisis.Lexema.Equals(variableSwitch))
            {
                Parea(preAnalisis.Descripcion);
            }
            else
            {
                this.lex = ">>Error sintactico: La variable [" + preAnalisis.Lexema + "] no esta declarada";
                this.tok = "";
                errorSintactico = true;
            }
        }

        public void CuerpoSwitch()
        {
            if (preAnalisis.Descripcion.Equals("PR_case"))
            {
                //va armando la traduccion del switch
                if (iteracionesSwitch == 0) { cuerpoSwitch = cuerpoCase + " if " + variableSwitch; iteracionesSwitch = 1; }
                else { cuerpoSwitch = cuerpoCase + "else if " + variableSwitch; }


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
                        cuerpoSwitch = cuerpoSwitch + ":";
                        Parea(preAnalisis.Descripcion);
                        CuerpoCase();


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
            }
        }

        public void CuerpoCase()
        {
            ListaDeclaracion();

        }
        #endregion

        /**
         * CLASE Y METODO PRINCIPAL
         */
        #region  CLASE Y METODO PRINCIPAL
        public void Clase()
        {
            if (preAnalisis.Descripcion.Equals("PR_class"))
            {
                Parea("PR_class");
                if (preAnalisis.Descripcion.Equals("Identificador"))
                {
                    Parea("Identificador");
                    if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
                    {
                        Parea("S_Llave_Izquierda");
                        /**
                         * LLAMADA AL METODO PRINCIPAL 
                         */
                        ListaDeclaracion();
                        MetodoPrincipal();
                        ListaDeclaracion();
                        if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                        {
                            Parea("S_Llave_Derecha");
                        }
                        else
                        {
                            if (!errorSintactico)
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                this.lex = ">>Error sintactico: Se esperaba Llave de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                errorSintactico = true;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("error");
                        // viene epsilon
                        this.lex = ">>Error sintactico: Se esperaba Llave de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        errorSintactico = true;
                    }
                }
                else
                {
                    Console.WriteLine("error");
                    // viene epsilon
                    this.lex = ">> Error sintactico se esperaba [ identificador ] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                    errorSintactico = true;
                }
            }
            else
            {
                //EPSILON
            }
        }

        public void MetodoPrincipal()
        {
            if (preAnalisis.Descripcion.Equals("PR_static"))
            {
                Parea("PR_static");
                if (preAnalisis.Descripcion.Equals("PR_void"))
                {
                    Parea("PR_void");
                    if (preAnalisis.Descripcion.Equals("PR_Main"))
                    {
                        Parea("PR_Main");

                        if (preAnalisis.Descripcion.Equals("S_Parentesis_Izquierdo"))
                        {
                            Parea("S_Parentesis_Izquierdo");
                            /**
                             *  PARAMETRO DEL METODO PRINCIPAL
                             */
                            ParametroMain();
                            if (preAnalisis.Descripcion.Equals("S_Parentesis_Derecho"))
                            {
                                Parea("S_Parentesis_Derecho");
                                if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
                                {
                                    Parea("S_Llave_Izquierda");
                                    /**
                                     * LLAMADA A TODO LO DEMAS DECLARIONES ETC
                                     */
                                    //MetodoPrincipal();
                                    ListaDeclaracion();
                                    if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                                    {
                                        Parea("S_Llave_Derecha");
                                    }
                                    else
                                    {
                                        if (!errorSintactico)
                                        {
                                            Console.WriteLine("error");
                                            // viene epsilon
                                            this.lex = ">>Error sintactico: Se esperaba Llave de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                            errorSintactico = true;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("error");
                                    // viene epsilon
                                    this.lex = ">>Error sintactico: Se esperaba Llave de apertura en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                    errorSintactico = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                if (!errorSintactico)
                                {
                                    this.lex = ">>Error sintactico: Se esperaba parentesis cerrado en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                    errorSintactico = true;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba parentesis abierto en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("error");
                        // viene epsilon
                        this.lex = ">>Error sintactico: Se esperaba la palabra reservada [ Main ] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        errorSintactico = true;
                    }
                }
                else
                {
                    Console.WriteLine("error");
                    // viene epsilon
                    this.lex = ">>Error sintactico: Se esperaba la palabra reservada [ void ] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                    errorSintactico = true;
                }
            }
            else
            {
                Console.WriteLine("error");
                // viene epsilon
                this.lex = ">>Error sintactico: Se esperaba la palabra reservada [ static ] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                errorSintactico = true;

            }
        }

        public void ParametroMain()
        {
            if (preAnalisis.Descripcion.Equals("PR_string"))
            {
                Parea("PR_string");
                if (preAnalisis.Descripcion.Equals("S_Corchete_Izquierdo"))
                {
                    Parea("S_Corchete_Izquierdo");
                    if (preAnalisis.Descripcion.Equals("S_Corchete_Derecho"))
                    {
                        Parea("S_Corchete_Derecho");
                        if (preAnalisis.Descripcion.Equals("PR_args"))
                        {
                            Parea(preAnalisis.Descripcion);
                        }
                        else
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba un palabra reservada args en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("error");
                        // viene epsilon
                        this.lex = ">>Error sintactico: Se esperaba corchete de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        errorSintactico = true;
                    }
                }
                else
                {
                    Console.WriteLine("error");
                    // viene epsilon
                    this.lex = ">>Error sintactico: Se esperaba corchete de apertura en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                    errorSintactico = true;
                }
            }
            else
            {
                // viene epsilon
            }
        }
        #endregion

        /**
         * DECLARACION IF  -ELSE
         */
        #region DECLARACION IF
        public void InicioIf2()
        {
            if (preAnalisis.Descripcion.Equals("PR_if"))
            {
                Parea("PR_if");
                if (preAnalisis.Descripcion.Equals("S_Parentesis_Izquierdo"))
                {
                    Parea("S_Parentesis_Izquierdo");
                    /**
                     * CONDICION
                     */
                    CondicionIf();
                    if (preAnalisis.Descripcion.Equals("S_Parentesis_Derecho"))
                    {
                        Parea("S_Parentesis_Derecho");
                        if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
                        {
                            Parea("S_Llave_Izquierda");
                            /**
                             * CUERPO DE IF 
                             */
                            ListaDeclaracion();
                            if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                            {
                                Parea("S_Llave_Derecha");
                                /**
                                 * ELSE IF 
                                 */
                                ElseIf();
                            }
                            else
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                this.lex = ">>Error sintactico: Se esperaba llave de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                errorSintactico = true;
                            }
                        }
                        else
                        {
                            if (!errorSintactico)
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                this.lex = ">>Error sintactico: Se esperaba llave de apertura en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                errorSintactico = true;
                            }
                        }
                    }
                    else
                    {
                        if (!errorSintactico)
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba parentesis de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                }
                else
                {
                    if (!errorSintactico)
                    {
                        Console.WriteLine("error");
                        // viene epsilon
                        this.lex = ">>Error sintactico: Se esperaba parentesis de apertura de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        errorSintactico = true;
                    }
                }
            }
            else
            {

            }
        }

        public void CondicionIf()
        {
            string simboloEvaluar = "";
            if (preAnalisis.Descripcion.Equals("Identificador") || preAnalisis.Descripcion.Equals("Digito") || preAnalisis.Descripcion.Equals("Cadena"))
            {
                simboloEvaluar = preAnalisis.Descripcion;
                Parea(preAnalisis.Descripcion);
                /**
                 * SIMBOLOS DE INCREMENTO 
                 */
                SimbolosIf();
                if ((preAnalisis.Descripcion.Equals("Identificador") && simboloEvaluar.Equals("Identificador"))
                    || (preAnalisis.Descripcion.Equals("Identificador") && simboloEvaluar.Equals("Digito"))
                    || (preAnalisis.Descripcion.Equals("Identificador") && simboloEvaluar.Equals("Cadena"))
                    || (preAnalisis.Descripcion.Equals("Digito") && simboloEvaluar.Equals("Identificador"))
                    || (preAnalisis.Descripcion.Equals("Digito") && simboloEvaluar.Equals("Digito"))
                    || (preAnalisis.Descripcion.Equals("Cadena") && simboloEvaluar.Equals("Identificador"))
                    || (preAnalisis.Descripcion.Equals("Cadena") && simboloEvaluar.Equals("Cadena")))
                {
                    Parea(preAnalisis.Descripcion);
                }
                else
                {
                    if (!errorSintactico)
                    {
                        Console.WriteLine("error");
                        // viene epsilon
                        this.lex = ">>Error sintactico: Se esperaba "+ simboloEvaluar +" en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        errorSintactico = true;
                    }
                }
            }
            else
            {
                if (!errorSintactico)
                {
                    Console.WriteLine("error");
                    // viene epsilon
                    this.lex = ">>Error sintactico: Se esperaba Identificador en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                    errorSintactico = true;
                }
            }
        }

        public void SimbolosIf()
        {
            switch (preAnalisis.Descripcion)
            {
                case "S_Igual":
                    if (preAnalisis.Descripcion.Equals("S_Igual"))
                    {
                        Parea("S_Igual");
                        if (preAnalisis.Descripcion.Equals("S_Igual"))
                        {
                            condicionIf = condicionIf + "==";
                            Parea("S_Igual");
                        }
                        else
                        {
                            if (!errorSintactico)
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                errorSintactico = true;
                            }
                        }
                    }
                    else
                    {
                        if (!errorSintactico)
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                    break;
                case "S_Mayor_Que":
                    if (preAnalisis.Descripcion.Equals("S_Mayor_Que"))
                    {
                        Parea("S_Mayor_Que");
                        if (preAnalisis.Lexema.Equals("="))
                        {
                            Parea(preAnalisis.Descripcion);
                        }
                    }
                    else
                    {
                        if (!errorSintactico)
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                    break;
                case "S_Menor_Que":
                    if (preAnalisis.Descripcion.Equals("S_Menor_Que"))
                    {
                        Parea("S_Menor_Que");
                        if (preAnalisis.Lexema.Equals("="))
                        {
                            Parea(preAnalisis.Descripcion);
                        }
                    }
                    else
                    {
                        if (!errorSintactico)
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
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
                        else
                        {
                            if (!errorSintactico)
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                errorSintactico = true;
                            }
                        }
                    }
                    else
                    {
                        if (!errorSintactico)
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                    break;
                default:
                    this.lex = ">>Error sintactico: Se esperaba un operador en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                    errorSintactico = true;
                    break;
            }
        }

        public void ElseIf()
        {
            if (preAnalisis.Descripcion.Equals("PR_else"))
            {
                Parea("PR_else");
                if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
                {
                    Parea("S_Llave_Izquierda");
                    /**
                     * CUERPO DE IF 
                     */
                    ListaDeclaracion();
                    if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                    {
                        Parea("S_Llave_Derecha");
                        ListaDeclaracion();
                    }
                    else
                    {
                        Console.WriteLine("error");
                        // viene epsilon
                        this.lex = ">>Error sintactico: Se esperaba llave de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        errorSintactico = true;
                    }
                }
                else
                {
                    if (!errorSintactico)
                    {
                        Console.WriteLine("error");
                        // viene epsilon
                        this.lex = ">>Error sintactico: Se esperaba llave de apertura en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        errorSintactico = true;
                    }
                }
            }
            else
            {

            }
        }
        #endregion

        /**
         * COMENTARIOS  
         */
        #region COMENTARIOS


        public void ComentarioLinea()
        {
            if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
            {
                ///traduccionComentario(preAnalisis.Lexema, preAnalisis.Descripcion);
                Parea("ComentarioLinea");
                ListaDeclaracion();
            }
            else
            {

            }
        }

        public void ComentarioMultiLinea()
        {
            if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
               // traduccionComentario(preAnalisis.Lexema, preAnalisis.Descripcion);
                Parea(preAnalisis.Descripcion);
                ListaDeclaracion();
            }
            else
            {

            }
        }
        #endregion

        /**
         * DECLARACION WHILE  
         */
        #region DECLARACION WHILE 
        public void InicioWhile()
        {
            if (preAnalisis.Descripcion.Equals("PR_while"))
            {
                Parea("PR_while");
                if (preAnalisis.Descripcion.Equals("S_Parentesis_Izquierdo"))
                {
                    Parea("S_Parentesis_Izquierdo");
                    /**
                     * CONDICION
                     */
                    CondicionWhile();
                    if (preAnalisis.Descripcion.Equals("S_Parentesis_Derecho"))
                    {
                        Parea("S_Parentesis_Derecho");
                        if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
                        {
                            Parea("S_Llave_Izquierda");
                            /**
                             * CUERPO DE WHILE 
                             */
                            ListaDeclaracion();
                            if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                            {
                                Parea("S_Llave_Derecha");
                            }
                            else
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                this.lex = ">>Error sintactico: Se esperaba llave de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                errorSintactico = true;
                            }
                        }
                        else
                        {
                            if (!errorSintactico)
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                this.lex = ">>Error sintactico: Se esperaba llave de apertura en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                errorSintactico = true;
                            }
                        }
                    }
                    else
                    {
                        if (!errorSintactico)
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba parentesis de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                }
                else
                {
                    if (!errorSintactico)
                    {
                        Console.WriteLine("error");
                        // viene epsilon
                        this.lex = ">>Error sintactico: Se esperaba parentesis de apertura de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        errorSintactico = true;
                    }
                }
            }
            else
            {
            }
        }
        public void CondicionWhile()
        {
            string simboloEvaluar = "";
            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                simboloEvaluar = preAnalisis.Descripcion;
                Parea("Identificador");
                /**
                 * SIMBOLOS DE INCREMENTO 
                 */
                SimbolosWhile();
                if ((preAnalisis.Descripcion.Equals("Identificador") && simboloEvaluar.Equals("Identificador"))
                    || (preAnalisis.Descripcion.Equals("Identificador") && simboloEvaluar.Equals("Digito"))
                    || (preAnalisis.Descripcion.Equals("Identificador") && simboloEvaluar.Equals("Cadena"))
                    || (preAnalisis.Descripcion.Equals("Digito") && simboloEvaluar.Equals("Identificador"))
                    || (preAnalisis.Descripcion.Equals("Digito") && simboloEvaluar.Equals("Digito"))
                    || (preAnalisis.Descripcion.Equals("Cadena") && simboloEvaluar.Equals("Identificador"))
                    || (preAnalisis.Descripcion.Equals("Cadena") && simboloEvaluar.Equals("Cadena")))
                {
                    Parea(preAnalisis.Descripcion);
                }
                else
                {
                    if (!errorSintactico)
                    {
                        Console.WriteLine("error");
                        // viene epsilon
                        this.lex = ">>Error sintactico: Se esperaba Identificador, Cadena o Digito en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        errorSintactico = true;
                    }
                }
            }
            else
            {
                if (!errorSintactico)
                {
                    Console.WriteLine("error");
                    // viene epsilon
                    this.lex = ">>Error sintactico: Se esperaba Identificador en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                    errorSintactico = true;
                }
            }
        }

        public void SimbolosWhile()
        {
            Console.WriteLine(preAnalisis.Descripcion);
            switch (preAnalisis.Descripcion)
            {
                case "S_Igual":
                    if (preAnalisis.Descripcion.Equals("S_Igual"))
                    {
                        Parea("S_Igual");
                        if (preAnalisis.Descripcion.Equals("S_Igual"))
                        {
                            Parea("S_Igual");
                        }
                        else
                        {
                            if (!errorSintactico)
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                errorSintactico = true;
                            }
                        }
                    }
                    else
                    {
                        if (!errorSintactico)
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                    break;
                case "S_Mayor_Que":
                    if (preAnalisis.Descripcion.Equals("S_Mayor_Que"))
                    {
                        Parea("S_Mayor_Que");
                        if (preAnalisis.Lexema.Equals("="))
                        {
                            Parea(preAnalisis.Descripcion);
                        }
                    }
                    else
                    {
                        if (!errorSintactico)
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                    break;
                case "S_Menor_Que":
                    if (preAnalisis.Descripcion.Equals("S_Menor_Que"))
                    {
                        Parea("S_Menor_Que");
                        if (preAnalisis.Lexema.Equals("="))
                        {
                            Parea(preAnalisis.Descripcion);
                        }
                    }
                    else
                    {
                        if (!errorSintactico)
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                    break;
                case "S_Excl":
                    if (preAnalisis.Descripcion.Equals("S_Excl"))
                    {
                        Parea("S_Excl");
                        if (preAnalisis.Descripcion.Equals("S_Igual"))
                        {
                            Parea("S_Igual");
                        }
                        else
                        {
                            if (!errorSintactico)
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                errorSintactico = true;
                            }
                        }
                    }
                    else
                    {
                        if (!errorSintactico)
                        {
                            Console.WriteLine("error");
                            // viene epsilon
                            this.lex = ">>Error sintactico: Se esperaba el signo igual en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            errorSintactico = true;
                        }
                    }
                    break;
                default:
                    this.lex = ">>Error sintactico: Se esperaba un operador en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                    errorSintactico = true;
                    break;
            }
        }
        #endregion

        /**
         * ASIGNACION SIN TIPO 
         */
        // cuando solo viene la variable, x = 0, cadena = "hola", expresion = 5+5;

        #region ASIGNACION SIN TIPO
        public void AsignacionSinTipo()
        {
            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Lexema.Equals("="))
                {
                    Parea(preAnalisis.Descripcion);
                    if (preAnalisis.Descripcion.Equals("Identificador") || preAnalisis.Descripcion.Equals("Cadena") || preAnalisis.Descripcion.Equals("Digito"))
                    {
                        Parea(preAnalisis.Descripcion);
                        if (preAnalisis.Lexema.Equals(";"))
                        {
                            Parea(preAnalisis.Descripcion);
                            ListaDeclaracion();
                        }
                        else
                        {
                            this.lex = ">>Error Sintactico: Se esperaba Punto y Coma en lugar de [" + preAnalisis.Descripcion + "]";
                            errorSintactico = true;
                        }
                    }
                    else
                    {
                        this.lex = ">>Error Sintactico: Se esperaba Identificador, Digito o Cadena en lugar de [" + preAnalisis.Descripcion + "]";
                        errorSintactico = true;
                    }
                }
                else
                {
                    this.lex = ">>Error Sintactico: Se esperaba Signo Igual en lugar de [" + preAnalisis.Descripcion + "]";
                    errorSintactico = true;
                }
            }
            else
            {
                this.lex = ">>Error Sintactico: Se esperaba Identificador en lugar de [" + preAnalisis.Descripcion + "]";
                errorSintactico = true;
            }

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
                indice++;
                if (indice <= listaTokens.Count -1)
                {
                    if (preAnalisis.Descripcion.Equals(tipoToken))
                    {
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

    }
}