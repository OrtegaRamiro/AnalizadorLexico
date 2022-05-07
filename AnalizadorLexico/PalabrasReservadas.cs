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


        public string AperPar = "( = Inicio de agrupación\n";
        public string CerrPar = ") = Fin de agrupación\n";
        public string puntoComa = "; = Fin de la instrucción\n";
        public string coma = ", = Separador de parametros   \n";
        public string llaveApe = "{ = Inicio de bloque de instrucciones\n";
        public string llaveCerr = " } = Fin de bloque de instrucciones\n";
        public string comillasdobles = "\" Comillas para texto\n";
        public String Equipo()
        {
            return "EQUIPO = Palabra reservada que muestra los integrantes del equipo.";
        }

        public String Escribir()
        {
            String p1 = "Escribir = Palabra reservada que imprime texto en pantalla.\n";
            return p1;
        }

        public String Para()
        {
            String p1 = "Para = Palabra reservada que inicia un ciclo en base a una condicion y se incrementa o decrementa segun se necesite.\n";
            return p1;
        }

        public String Encuentra()
        {
            String p1 = "Encuentra = Palabra reservada que sirve para buscar el valor de una variable dentro de un vector.\n";
            return p1;
        }

        public String Limpiar()
        {
            String p1 = "Limpiar = Palabra reservada para limpiar la consola\n";
            return p1;
        }
        public String Entero()
        {
            String p1 = "Entero = Palabra reservada para indicar un tipo de dato entero.\n";
            return p1;
        }
        public String Flotante()
        {
            String p1 = "Flotante = Palabra reservada para indicar un tipo de dato con punto flotante.\n";
            return p1;
        }
        public String Cadena()
        {
            String p1 = "Cadena = Palabra reservada para indicar tipo de dato Cadena\n";
            return p1;
        }
        public String SegunSea()
        {
            String p1 = "SegunSea = Palabra reservada que evalua una expresion que puede tomar n valores de casos.\n";
            return p1;
        }
        public String Leer()
        {
            String p1 = "Leer = Palabra reservada que lee y asigna el dato por teclado.\n";
            return p1;
        }
        public String Aleatorio()
        {
            String p1 = "Aleatorio = Palabra reservada que genera un numero pseudoaleatorio en base a un rango minimo y un rango maximo\n";
            return p1;
        }
        public String Estructura()
        {
            String p1 = "Estructura = Palabra reservada que indica el inicio de una estructura de variables para formar un registro\n";
            return p1;
        }
        public String INICIO()
        {
            String p1 = "INICIO = Palabra reservadaque abre e inicia un programa\n";
            return p1;
        }
        public String FIN()
        {
            String p1 = "FIN = Palabra reservada que cierra o termina el programa.\n";
            return p1;
        }
        public String Raiz()
        {
            String p1 = "Raiz = Palabra reservada para la operacion de raiz cuadrada.\n";
            return p1;
        }
        public String Exponente()
        {
            String p1 = "Exponente = Palabra reservada que obtiene el exponente de un numero.\n";
            return p1;
        }
        public String Crear()
        {
            String p1 = "Crear = Palabra reservada para crear un archivo.\n";
            return p1;
        }
        public String Modifica()
        {
            String p1 = "Modifica = Palabra reservada para modificar un archivo creado anteriormente\n";
            return p1;
        }
        public String Elimina()
        {
            String p1 = "Elimina = Palabra reservada para eliminar un archivo ya creado anteriormente.\n";
            return p1;
        }
        public String Compara(String Componente)
        {
            String p1 = "Compara = Palabra reservada que permite compara el contenido de dos variables o numeros\n" + AperPar;
            return p1 + Componente;
        }
        public String Mientras(String Componente)
        {
            String p1 = "Mientras = Palabra reservada que inicia un ciclo.\n" + AperPar;
            //f1.analizar();

            return p1 + Componente;
        }

        public String Numeros(String txt)
        {
            return txt + " = Numero";
        }

        public void Etiquetas()
        {

        }
    }
}
