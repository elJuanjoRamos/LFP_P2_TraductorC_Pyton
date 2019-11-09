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
        int ambitoSwitch = 0;
        int ambitoFor = 0;
        int ambitoIf = 0;
        int ambitoWhile = 0;
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
        public void obtenerLista(ArrayList listaTokens)
        {
            this.listaTokens = listaTokens;
            indice = 0;
            iteracionesSwitch = 0;
            ambitoSwitch= 0;
            ambitoFor = 0;
            ambitoIf = 0;
            tabs = "";
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
            tokenInicio =  tokenInicio + ListaId() + " " + OpcAsignacion();
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
                valorVariable = preAnalisis.Lexema + "\n";
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
            return contenidoDeArreglo + "\n";
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

            ambitoSwitch += 1;
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
                        cuerpoSwitch = "\nif " + variableSwitch + " == " + preAnalisis.Lexema +":";
                    }
                    else 
                    {
                        cuerpoSwitch = "\nelse if " + variableSwitch + " == " + preAnalisis.Lexema + ":";
                    }
                    Parea(preAnalisis.Descripcion);
                    Parea(preAnalisis.Descripcion);
                   
                    tokenInicio = tokenInicio + cuerpoSwitch;
                    string tabs = "";
                    for (int j = 0; j <= ambitoSwitch; j++)
                    {
                        tabs = tabs + " " + "\x020" + "\x020" + "\x020";
                    }

                    tokenInicio = tokenInicio + "\n" + tabs;
                    iteracionesSwitch = 1;
                    ListaDeclaracion();
                }
                else if (((Token)listaTokens[i]).Descripcion.Equals("PR_default"))
                {
                    tokenInicio = tokenInicio + "\n else: \n\t";
                    ListaDeclaracion();
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
        string tabs = "";
        string tabs1 = "";

        public void InicioFor()
        {
            ambitoFor += 1;
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
                for (int i = posicionActual; i < posicionActual+3; i++)
                {
                    tokenInicio = tokenInicio + preAnalisis.Lexema;
                    Parea(preAnalisis.Descripcion);                    
                }
                //envia punto y coma
                Parea(preAnalisis.Descripcion);

                //concatena al texto la condicion del for ==>  (x > 45)

                tokenInicio = tokenInicio + "\n" + "while" + CondicionFor();
                //trae el aumento o decremento del valor de la variable ( y + +)
                aumentoDecremento = AumentoDecremento();

                //agrega tabl al interior del for segun el ambito, es decir, para la primera llamada, un tab, segunda llamada dos tabs, etc
                for (int j = 0; j <= ambitoFor; j++)
                {
                    tabs = tabs + " " + "\x020" + "\x020" + "\x020";
                }


                tokenInicio = tokenInicio + "\n" + tabs;
                ListaDeclaracion();

                tokenInicio = tokenInicio + "\n" + tabs + aumentoDecremento;
                
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
                        valorRetorno =  preAnalisis.Lexema;
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
            //MANDA A PAREA LOS TOKENS +){ que no son necesarios
            for (int j = 0; j < 3; j++)
            {
                Parea(preAnalisis.Descripcion);

            }
            Console.WriteLine(preAnalisis.Lexema);

            return valorRetorno;
        }

        #endregion
        /**
        * COMENTARIO
        */

        #region TRADUCCION COMETARIO
        public void InicioComentario()
        {
            Console.WriteLine("llamo a comentario");
            if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
            {
                string comment = preAnalisis.Lexema;
                comment = comment.Replace("//", "#");
                Parea(preAnalisis.Descripcion);
                tokenInicio = tokenInicio + "\n" + comment + "\n";
            }
            else if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
                string comment = preAnalisis.Lexema;
                comment = comment.Replace("/*", " ' ' ' ");
                comment = comment.Replace("*/", " ' ' ' ");
                Parea(preAnalisis.Descripcion);
                tokenInicio = tokenInicio + "\n" + comment + "\n";
            }
            ListaDeclaracion();
        }
        #endregion
        /**
        * CONSOLE
        */

        #region TRADUCTOR CONSOLE
        public void InicioConsole()
        {
           
            Parea(preAnalisis.Descripcion);
            if (preAnalisis.Lexema.Equals("."))
            {
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Lexema.Equals("WriteLine"))
                {
                    Parea(preAnalisis.Descripcion);
                    Parea(preAnalisis.Descripcion);
                    tokenInicio = tokenInicio + "\n" + "print( ";
                    int posicionActual = listaTokens.IndexOf(preAnalisis);
                    for (int i = posicionActual; i < listaTokens.Count; i++)
                    {
                        if (preAnalisis.Lexema.Equals(")"))
                        {
                            tokenInicio = tokenInicio + " )";
                            Parea(preAnalisis.Descripcion);
                            Parea(preAnalisis.Descripcion);
                            Console.WriteLine(preAnalisis.Descripcion);
                            //ListaDeclaracion();
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
            ambitoWhile++;
            tokenInicio = tokenInicio + "\n" + preAnalisis.Lexema;
            Parea(preAnalisis.Descripcion);
            Parea(preAnalisis.Descripcion);
            tokenInicio = tokenInicio + " " + CondicionWhile();
            for (int i = 0; i <= ambitoWhile; i++)
            {
                tabs = tabs + "\x020" + "\x020" + "\x020";
            }
            tokenInicio = tokenInicio + "\n" + tabs;
            ListaDeclaracion();

            if (preAnalisis.Lexema.Equals("}"))
            {
                tokenInicio = tokenInicio + "\n";
                Parea(preAnalisis.Descripcion);
                ListaDeclaracion();
            }

        }
        public string CondicionWhile()
        {
            string condicion = "";
            int posicionActual = listaTokens.IndexOf(preAnalisis);
            for (int i = posicionActual; i < listaTokens.Count; i++)
            {
                if (preAnalisis.Lexema.Equals(")"))
                {
                    Parea(preAnalisis.Descripcion);
                    Parea(preAnalisis.Descripcion); 
                    break;
                }
                else
                {
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

        public void InicioIf2() {
            ambitoIf++;
            //Envia la palabra if
            Parea(preAnalisis.Descripcion);
            tokenInicio = tokenInicio + "\n if ";
            //envia el (
            Parea(preAnalisis.Descripcion);
            //obtiene las condiciones
            tokenInicio = tokenInicio + CondicionIf();
            string tabs = "";
            for (int i = 0; i <= ambitoIf; i++)
            {
                tabs = tabs + " " + "\x020" + "\x020" + "\x020";
            }
            //MANDA LA LLAVE {
            Parea(preAnalisis.Descripcion);
            tokenInicio = tokenInicio + "\n" + tabs;
            ListaDeclaracion();


            if (preAnalisis.Lexema.Equals("}"))
            {
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Descripcion.Equals("PR_else"))
                {
                    tokenInicio = tokenInicio + "\n" + "else: \n";
                    Parea(preAnalisis.Descripcion);
                    Parea(preAnalisis.Descripcion);
                    tokenInicio = tokenInicio + "\n" + tabs;
                    ListaDeclaracion();
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
            if (preAnalisis.Descripcion.Equals("Identificador") || preAnalisis.Descripcion.Equals("Digito") || preAnalisis.Descripcion.Equals("Cadena"))
            {
                simboloEvaluar = preAnalisis.Lexema;
                Parea(preAnalisis.Descripcion);
                /**
                 * SIMBOLOS DE INCREMENTO 
                 */
                simboloEvaluar = simboloEvaluar + " " + SimbolosIf();
                if ((preAnalisis.Descripcion.Equals("Identificador"))|| (preAnalisis.Descripcion.Equals("Digito"))
                    || (preAnalisis.Descripcion.Equals("Cadena") ))
                {
                    simboloEvaluar = simboloEvaluar + " " + preAnalisis.Lexema + ":"; 
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
            Parea(preAnalisis.Descripcion);
            Parea(preAnalisis.Descripcion);
            Parea(preAnalisis.Descripcion);
            tokenInicio = tokenInicio + "\x020" + "\x020" + "\x020";
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
            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                tokenInicio = tokenInicio + preAnalisis.Lexema;
                Parea(preAnalisis.Descripcion);
                if (preAnalisis.Lexema.Equals("="))
                {
                    tokenInicio = tokenInicio + " " + preAnalisis.Lexema;
                    Parea(preAnalisis.Descripcion);
                    tokenInicio = tokenInicio + preAnalisis.Lexema;
                    Parea(preAnalisis.Descripcion);
                    if (preAnalisis.Lexema.Equals(";"))
                    {
                        Parea(preAnalisis.Descripcion);
                        ListaDeclaracion();
                    }
                }
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
