using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFP_P2_TraductorC_Pyton.Controladores;
using LFP_P2_TraductorC_Pyton.Modelos;
namespace LFP_P2_TraductorC_Pyton.Controladores
{
    class ReporteControlador
    {
        private readonly static ReporteControlador instancia = new ReporteControlador();
        private ReporteControlador()
        {
        }

        public static ReporteControlador Instancia
        {
            get
            {
                return instancia;
            }
        }

        public void getReportTokens()
        {
            string tbody = "";
            string content = "";
            foreach (Token t in TokenControlador.Instancia.ArrayListTokens)
            {
                content = "<tr>\n" +
                    "     <th scope=\"row\">" + t.IdToken + "</th>\n" +
                    "     <td>" + t.Lexema + "</td>\n" +
                    "     <td>" + t.Descripcion + "</td>\n" +
                    "     <td>" + t.Fila + "</td>\n" +
                    "     <td>" + t.Columna + "</td>\n" +
                    "</tr>";
                tbody = tbody + content;
            }
            string thead = "<th scope=\"col\">ID Token</th>\n" +
            "<th scope=\"col\">Token</th>\n" +
            "<th scope=\"col\">Descripcion</th>" +
            "<th scope=\"col\">Fila</th>\n" +
            "<th scope=\"col\">Columna</th>";
            getHTML("Tokens", thead, tbody);
        }

        public void ImprimirVacia()
        {
            string cadena = "";

            string cadena2 = "<th scope =\"col\">No</th>\n" +
             "          <th scope=\"col\">Lexema</th>\n" +
             "          <th scope=\"col\">Token</th>\n" +
             "          <th scope=\"col\">Fila</th>\n" +
             "          <th scope=\"col\">Columna</th>\n";
            getHTML(cadena, cadena2, "Tokens ");
        }

        public void getReportTokensError()
        {
            string tbody = "";
            string content = "";
            foreach (Token t in TokenControlador.Instancia.ArrayListErrors)
            {
                content = "<tr>\n" +
                    "     <th scope=\"row\">" + t.IdToken + "</th>\n" +
                    "     <td>" + t.Lexema + "</td>\n" +
                    "     <td>" + t.Descripcion + "</td>\n" +
                    "     <td>" + t.Fila + "</td>\n" +
                    "     <td>" + t.Columna + "</td>\n" +
                    "</tr>";
                tbody = tbody + content;
            }
            string thead = "<th scope=\"col\">ID Token</th>\n" +
            "<th scope=\"col\">Token</th>\n" +
            "<th scope=\"col\">Descripcion</th>\n" +
            "<th scope=\"col\">Fila</th>\n" +
            "<th scope=\"col\">Columna</th>";
            getHTML("Tokens Error", thead, tbody);
        }

        public void getReporteError()
        {
            string tbody = "";
            string content = "";
            foreach (Sintactico t in SintacticoControlador.Instancia.ArrayListSintactico)
            {
                content = "<tr>\n" +
                    "     <th scope=\"row\">" + t.IdSintactico + "</th>\n" +
                    "     <td>" + t.Lexema + "</td>\n" +
                    "     <td>" + t.Descripcion + "</td>\n" +
                    "     <td>" + t.Fila + "</td>\n" +
                    "     <td>" + t.Columna + "</td>\n" +
                    "</tr>";
                tbody = tbody + content;
            }
            string thead = "<th scope=\"col\">ID Token</th>\n" +
            "<th scope=\"col\">Token</th>\n" +
            "<th scope=\"col\">Descripcion</th>\n" +
            "<th scope=\"col\">Fila</th>\n" +
            "<th scope=\"col\">Columna</th>";
            getHTML("Errores Sintacticos", thead, tbody);
        }

        public void getTablaSimbolos()
        {
            string tbody = "";
            string content = "";
            foreach (Simbolo s in SimboloControlador.Instancia.ArrayListSimbolo)
            {
                content = "<tr>\n" +
                    "     <th scope=\"row\">" + s.IdSimbolo + "</th>\n" +
                    "     <td>" + s.Nombre + "</td>\n" +
                    "     <td>" + s.Tipo + "</td>\n" +
                    "     <td>" + s.Valor + "</td>\n" +
                    "</tr>";
                tbody = tbody + content;
            }
            string thead = "<th scope=\"col\">ID Simbolo</th>\n" +
            "<th scope=\"col\">Variable</th>\n" +
            "<th scope=\"col\">Tipo</th>\n" +
            "<th scope=\"col\">Valor</th>\n";
            getHTML("Tabla de Simbolos", thead, tbody);
        }

        public void getHTML(string title, string thead, string tbody)
        {
            string html = "<html lang=\"en\">\n" +
            "<head>\n" +
            "    <meta charset=\"UTF-8\">\n" +
            "    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n" +
            "    <meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\">\n" +
            "    <link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css\" integrity=\"sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T\" crossorigin=\"anonymous\">\n" +
            "    <title>" + title + "</title>\n" +
            "</head>\n" +
            "<body>\n" +
            "    <nav class=\"navbar navbar-light bg-light\">\n" +
            "    <a class=\"navbar-brand\" href=\"#\">\n" +
            "        Lenguajes Formales y de Programación\n" +
            "    </a>\n" +
            "    </nav>\n" +
            "    <div class=\"container\">\n" +//DateTime.Now.ToString("G");
            "    <div class=\"jumbotron jumbotron-fluid\">\n" +
            "    <div class=\"container\">\n" +
            "        <h1 class=\"display-2\">" + title + "</h1>\n" +
            "        <p class=\"lead\">Listado de " + title + " detectados por el analizador</p>\n" +
            "        <p class=\"lead\"><strong>" + DateTime.Now.ToString("G") + "</strong></p>\n" +
            "    </div>\n" +
            "    </div>\n" +
            "    <table id=\"example\" class=\"table table-striped table - bordered\" style=\"width: 100 % \"\n" +
            "    <thead\n" +
            "        <tr>\n" +
            thead +
            "        </tr>\n" +
            "    </thead>\n" +
            "    <tbody>\n" +
            tbody +
            "    </tbody>\n" +
            "    </table>\n" +
            "    </div>\n" +
            "\n" +
            "    <script src=\"https://code.jquery.com/jquery-3.3.1.slim.min.js\" integrity=\"sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo\" crossorigin=\"anonymous\"></script>\n" +
            "    <script>\n" +
            "    // Write on keyup event of keyword input element\n" +
            "    $(document).ready(function() {\n"+
            "       $('#example').DataTable();\n"+
            "      } );\n" +
            "    </script>\n" +
            "    <script src=\"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js\" integrity=\"sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1\" crossorigin=\"anonymous\"></script>\n" +
            "    <script src=\"https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js\" integrity=\"sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM\" crossorigin=\"anonymous\"></script>\n" +
            "</body>\n" +
            "</html>";
            File.WriteAllText("Reporte de " + title + ".html", html);
            System.Diagnostics.Process.Start("Reporte de " + title + ".html");
        }
    }
}
