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

        //Variables arreglo
        string nombreArreglo = "";
        string contenido = "";


        //Variables console


        //Variables switch
        string tipoInicio = "";
        string identInicio = "";
        string tipoInicioAux = "";

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
            } else if (preAnalisis.Descripcion.Equals("PR_switch"))
            {
                InicioSwitch();
            } else if (preAnalisis.Descripcion.Equals("PR_class"))
            {
                Clase();
            } else if (preAnalisis.Descripcion.Equals("PR_if"))
            {
                InicioIf();
            }
            else
            {
                //this.lex = "Error Sintactico: Se esperaba palabra reservada en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                //errorSintactico = true;
                //Epsilon
            }

        }


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
                this.identInicio = preAnalisis.Lexema;
                this.lexemaAuxiliar = preAnalisis.Lexema;
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
                LlenarTabla(this.lexemaAuxiliar, preAnalisis.Lexema, "variable");
                Parea("Digito");
            }
            else if (preAnalisis.Descripcion.Equals("Cadena") && (tokenInicio.Equals("PR_char") || tokenInicio.Equals("PR_string")))
            {
                LlenarTabla(this.lexemaAuxiliar, preAnalisis.Lexema, "variable");
                Parea("Cadena");
            }
            else if (preAnalisis.Descripcion.Equals("Identificador") && (tokenInicio.Equals("PR_bool") || tokenInicio.Equals("PR_boolean")))
            {
                LlenarTabla(this.lexemaAuxiliar, preAnalisis.Lexema, "variable");
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
                Parea("S_Punto_y_Coma");
                if (preAnalisis.Descripcion.Equals("PR_switch"))
                {
                    InicioSwitch();
                } else
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
                this.nombreArreglo = preAnalisis.Lexema;
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
                LlenarTabla(this.nombreArreglo, this.contenido , "arreglo");
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
                this.contenido = "[" + preAnalisis.Lexema; 
                Parea(preAnalisis.Descripcion);
                ListaValor1();
                this.contenido = this.contenido + "]";
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
                    this.contenido = this.contenido + "," + preAnalisis.Lexema;
                    Parea("Digito");
                    ListaValor1();
                }
                else if (preAnalisis.Descripcion.Equals("Cadena") && ((tokenInicio.Equals("PR_char") || tokenInicio.Equals("PR_string"))))
                {
                    this.contenido = this.contenido + "," + preAnalisis.Lexema;
                    Parea("Cadena");
                    ListaValor1();
                }
                else if (preAnalisis.Descripcion.Equals("Identificador") && ((tokenInicio.Equals("PR_bool") || tokenInicio.Equals("PR_boolean"))))
                {
                    this.contenido = this.contenido + "," + preAnalisis.Lexema;
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
                        this.contenido = "[]";
                        LlenarTabla(this.nombreArreglo, this.contenido, "arreglo");

                        PuntoComa();

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
                } else if (preAnalisis.Descripcion.Equals("Digito") || preAnalisis.Descripcion.Equals("Identificador") || preAnalisis.Descripcion.Equals("Cadena"))
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



        #region DECLARACION SWTICH 

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
                            if (errorSintactico == false )
                            {
                                if (preAnalisis.Descripcion.Equals("S_Llave_Derecha"))
                                {
                                    Parea(preAnalisis.Descripcion);
                                    ListaDeclaracion();
                                } else
                                {
                                    this.lex = ">>Error sintactico: Se esperaba llave de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                                    this.tok = "";
                                    errorSintactico = true;
                                }
                            } 
                        } else
                        {
                            this.lex = ">>Error sintactico: Se esperaba llave de apertura en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                            this.tok = "";
                            errorSintactico = true;
                        }
                    } else
                    {
                        this.lex = ">>Error sintactico: Se esperaba parentesis de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        this.tok = "";
                        errorSintactico = true;
                    }
                }
            } else
            {
                this.lex = ">>Error sintactico: Se esperaba parentesis de apertura en lugar de [ " + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + " ]";
                this.tok = "";
                errorSintactico = true;
            }
            

        }
        public void AsignacionSwitch()
        {
            if (preAnalisis.Lexema.Equals(identInicio))
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
                Parea(preAnalisis.Descripcion);
                //verifica si concuerdan los tipos, es decir que el caso a analizar concuerde con el tipo de variable
                //
                //  String texto = ""; ----> como la variable declarada es string
                //
                //  switch(texto){
                //      case "":  ---> la variable del case debe ser igual a string   
                //
                if ( (preAnalisis.Descripcion.Equals("Cadena") && (tipoInicio.Equals("PR_string") || tipoInicio.Equals("PR_char"))) ||
                    (preAnalisis.Descripcion.Equals("Digito") && (tipoInicio.Equals("PR_int") || tipoInicio.Equals("PR_float"))))
                {
                    Parea(preAnalisis.Descripcion);
                    if (preAnalisis.Descripcion.Equals("S_Dos_puntos"))
                    {
                        Parea(preAnalisis.Descripcion);
                        CuerpoCase();
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
                    } else
                    {
                        this.lex = ">>Error Sintactico: Se esperaban dos puntos el lugar de [" + preAnalisis.Descripcion + " ]";
                        this.tok = "";
                        errorSintactico = true;
                    }
                } else
                {
                    this.lex = ">>Error Sintactico: El tipo de variable [ " + tipoInicio+ "] no concuerda con el tipo de evaluacion [ " + preAnalisis.Descripcion + " ] del case";
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
            if (preAnalisis.Descripcion.Equals("PR_for"))
            {
                InicioFor();
            } else if (preAnalisis.Descripcion.Equals("PR_int") || preAnalisis.Descripcion.Equals("PR_string") 
                || preAnalisis.Descripcion.Equals("PR_float") || preAnalisis.Descripcion.Equals("PR_bool")
                || preAnalisis.Descripcion.Equals("PR_boolean") || preAnalisis.Descripcion.Equals("PR_char"))
            {
                InicioVariable();
                tipoInicio = tipoInicioAux;
            }
            // esto sirve por si la variable ya fue declarada anteriormente
            // y se quiere modificar su valor o algo asi

            // int variable;


            // switch(condicion)
            // case 0:
            //       variable = 0;
            // case 1: 
            //       variable  = 1; 
            // etc
            else if (preAnalisis.Descripcion.Equals("Identificador"))
            {

            }
            else
            {
                //Epsilon
            }

        }
        #endregion



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
                        MetodoPrincipal();
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

        #region METODO IF-ELSE
        /**
         * METODO IF
         **/
        public void InicioIf()
        {
            if (preAnalisis.Descripcion.Equals("PR_if"))
            {
                Parea("PR_if");
                this.Condicion();
                this.Entonces();
            }
            else
            {

            }
        }

        public void Condicion()
        {
            if (preAnalisis.Descripcion.Equals("S_Parentesis_Izquierdo"))
            {
                Parea("S_Parentesis_Izquierdo");
                if (preAnalisis.Descripcion.Equals("Identificador"))
                {
                    Parea("Identificador");
                    /**
                     * COMPARAR SIMBOLO DE IF 
                     */
                    this.SimboloIf();
                    if (preAnalisis.Descripcion.Equals("Identificador"))
                    {
                        Parea("Identificador");
                        if (preAnalisis.Descripcion.Equals("S_Parentesis_Derecho"))
                        {
                            Parea("S_Parentesis_Derecho");
                        }
                        else
                        {
                            if (!errorSintactico)
                            {
                                Console.WriteLine("error");
                                // viene epsilon
                                this.lex = ">>Error sintactico: Se esperaba un parentesis de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
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
                            this.lex = ">>Error sintactico: Se esperaba un identificador en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
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
                        this.lex = ">>Error sintactico: Se esperaba un identificador en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
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
                    this.lex = ">>Error sintactico: Se esperaba parentesis de apertura en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                    errorSintactico = true;
                }
            }
        }

        public void SimboloIf()
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

        public void Entonces()
        {
            if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
            {
                Parea("S_Llave_Izquierda");
                /**
                 * CUERPO DE IF 
                 */
                //this.CuerpoIf();
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
                        this.lex = ">>Error sintactico: Se esperaba llave de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
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
                    this.lex = ">>Error sintactico: Se esperaba llave de apertural en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                    errorSintactico = true;
                }
            }
        }

        public void OtroModoIf()
        {
            if (preAnalisis.Descripcion.Equals("S_Division"))
            {
                Parea("S_Division");
            }
            else
            {

            }
        }

        public void CuerpoIf()
        {
            this.InstruccionIf();
            this.OtraInstruccionIf();
        }

        public void InstruccionIf()
        {
            Console.WriteLine("Instruccion");
        }

        public void OtraInstruccionIf()
        {
            Console.WriteLine("Otra Instruccion");
        }
        #endregion


        /**
         * COMENTARIOS  
         */
        #region COMENTARIOS
        public void Comentario()
        {
            if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
            {
                ComentarioLinea();
            }
            else if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
                ComentarioMultilinea();
            }
            else
            {

            }
        }

        public void OtroComentario()
        {
            if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
            {
                Comentario();
            }
            else if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
                Comentario();
            }
            else
            {

            }
        }

        public void ComentarioLinea()
        {
            if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
            {
                Parea("ComentarioLinea");
                OtroComentario();
            }
            {
                //EPSILON
            }
        }

        public void ComentarioMultilinea()
        {
            if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
                Parea("ComentarioMultiLinea");
                OtroComentario();
            }
            {
                //EPSILON
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
        string temp = "";
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

        public void LlenarTabla(string lexAux, string lexema, string tipo)
        {
            TablaTraduccionControlador.Instancia.agregar(lexAux, lexema, tipo);
        }
    }

}
