using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexico
{
    class PalabrasReservadas
    {


        string AperPar = "( = Inicio de agrupación\n";
        string CerrPar = ") = Fin de agrupación\n";
        string puntoComa = "; = Fin de la instrucción";
        public String Equipo()
        {
            return "EQUIPO = Palabra reservada que muestra los integrantes del equipo.";
        }
        public String Mientras(String Componente)
        {
            String p1 = "Mientras = Palabra reservada que inicia un ciclo.\n" + AperPar;
            //f1.analizar();

            return p1 + Componente + CerrPar + puntoComa;
        }

        public String Numeros(String txt) { 
            return txt+" = Numero";
        }

        public void Etiquetas()
        {

        }
    }
}
