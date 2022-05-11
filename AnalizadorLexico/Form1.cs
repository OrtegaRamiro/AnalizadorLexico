using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexico
{
    public partial class Form1 : Form
    {
        PalabrasReservadas Ins = new PalabrasReservadas();
        public Form1()
        {
            InitializeComponent();
        }
        int contIde = 1;
        int contIns = 1;
        int contCad = 1;
        int contVec = 1;

        public String analizar(String txt)
        {
            String Ide = "";

            if (Char.IsLetter(txt[0]) && Char.IsLower(txt[0]))
            {
                if (IdenMayusculas(txt) == false)
                {

                    for (int i = 0; i < txt.Length; i++)
                    {
                        if (Char.IsDigit(txt[i]))
                        {
                            if (i == txt.Length - 1)
                            {
                                if (txt.Contains("."))
                                    return Ins.errorLexico;
                                if (Char.IsDigit(txt[i]))
                                    Ide = "Identificador " + contIde;
                                contIde += 1;
                                return Ide;

                            }
                        }
                        if (txt.Contains("[") && txt.Contains("]"))
                        {
                            Ide = "Identificador Vector " + contVec;
                            contVec++;
                            return Ide;
                        }
                        if (i > 0)
                            if (txt[i - 1] == '_' && Char.IsDigit(txt[i]))
                            {
                                Ide = "Identificador " + contIde;
                                contIde += 1;
                                return Ide;
                            }
                    }
                    if (txt.Length > 0 && txt[txt.Length - 1] == '_' || txt.Contains("."))
                        return Ins.errorLexico;

                    else
                        Ide = "Identificador " + contIde;

                    contIde += 1;
                    return Ide;
                }
                else
                    return Ins.errorLexico;
            }
            else if (txt.All(Char.IsDigit) || (txt.Contains(".") && Char.IsDigit(txt[txt.IndexOf(".") + 1]) && Char.IsDigit(txt[txt.IndexOf(".") - 1])))
            {
                int n = (from c in txt where c == '.' select c).Count();
                for (int i = 0; i < txt.Length; i++)
                {
                    if (Char.IsLetter(txt[i]) || n > 1)
                    {
                        return Ins.errorLexico;
                    }
                }


                Ins.Numeros(txt);
                String Numero = "Número " + txt;
                return Numero;
            }
            else if (Char.IsUpper(txt[0]))
            {
                return DevolverPalReservada(txt);
            }
            else if (BuscaOpAr(txt))
            {
                return IdenOpAr(txt);
            }
            else
            {
                return Ins.errorLexico;
            }
            return "Error";
        }
        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            contIde = 1;
            contIns = 1;
            contCad = 1;
            contVec = 1;
            int contPal = 0;

            if (txtTextoAnalizar.Text == "")
            {
                lblprueba.Text = "";
            }
            else
            {

                lblprueba.Text = "";
                String Separar = txtTextoAnalizar.Text.Trim(' ');

                string prueba;
                Regex regex = new Regex(@"[ ]{2,}", RegexOptions.None);
                prueba = regex.Replace(Separar, @" ");

                List<String> lista = prueba.Split(Convert.ToChar(" ")).ToList<String>();
                List<String> ide = new List<String>();

                List<String> Aux = new List<string>();

                if (Char.IsUpper(txtTextoAnalizar.Text[0]))
                {
                    lblprueba.Text = DevolverPalReservada(txtTextoAnalizar.Text);
                }
                else
                {
                    foreach (String item in lista)
                    {
                        String Componente = item;

                        if (Aux.Contains(Componente))
                        {
                            int Ind = Aux.IndexOf(Componente);
                            lblprueba.Text = lblprueba.Text + Componente + " = " + ide[Ind] + "\n";
                        }
                        else if (Char.IsUpper(Componente[0]))
                        {
                            lblprueba.Text = lblprueba.Text + DevolverPalReservada(Componente) + "\n";
                        }
                        else if (item[item.Length - 1] == ';')
                        {
                            string auxCom = item.Substring(0, item.Length - 1);
                            if (auxCom.All(char.IsDigit))
                            {
                                lblprueba.Text = lblprueba.Text + auxCom + " = " + analizar(auxCom) + "\n" + Ins.puntoComa;
                            }
                            else if (auxCom.Contains("."))
                            {
                                lblprueba.Text = lblprueba.Text + auxCom + " = " + analizar(auxCom) + "\n" + Ins.puntoComa;
                            }
                            else
                            {
                                if (Aux.Contains(auxCom))
                                {
                                    int Ind = Aux.IndexOf(auxCom);
                                    lblprueba.Text = lblprueba.Text + auxCom + " = " + ide[Ind] + "\n" + Ins.puntoComa;
                                }
                                else
                                {
                                    ide.Add(analizar(auxCom));
                                    Aux.Add(auxCom);
                                    lblprueba.Text = lblprueba.Text + auxCom + " = " + ide[contPal] + "\n" + Ins.puntoComa;
                                    contPal++;
                                }

                            }
                        }
                        else
                        {
                            ide.Add(analizar(Componente));
                            Aux.Add(Componente);
                            lblprueba.Text = lblprueba.Text + item + " = " + ide[contPal] + "\n";
                            contPal++;
                        }
                    }
                }
            }
        }

        private String DevolverPalReservada(string txt)
        {
            try
            {
                if (txt == "EQUIPO();")
                {
                    String aux = "";

                    aux = aux + Ins.Equipo();
                    if (txt.Contains("(") && txt.Contains(")"))
                    {
                        aux = aux + Ins.AperPar;
                        aux = aux + Ins.CerrPar;
                    }
                    if (txt.Contains(";"))
                        aux = aux + Ins.puntoComa;

                    return aux;
                }
                if (txt.Contains("Escribir"))
                {
                    String aux = "";
                    aux = aux + Ins.Escribir();
                    if (txt.Contains('('))
                    {
                        aux = aux + Ins.AperPar;
                        if (txt.Contains('\"') && txt.Contains('\"'))
                        {
                            if (txt.Contains(','))
                            {
                                int Ini = txt.IndexOf("\"");
                                int Fin = txt.IndexOf(",") - 1;

                                string Contenido = txt.Substring(Ini, (Fin + 1) - (Ini));
                                aux = aux + Contenido + " = Cadena " + contCad + "\n";
                                contCad++;

                                string ConVariable = txt.Substring(txt.IndexOf(',') + 1, txt.IndexOf(')') - (txt.IndexOf(',') + 1));

                                aux = aux + ", = Separador de parametros\n" + ConVariable + " = " + analizar(ConVariable) + "\n";

                            }
                            else
                            {
                                int Ini = txt.IndexOf("\"");
                                int Fin = txt.IndexOf(")") - 1;

                                string Contenido = txt.Substring(Ini, (Fin + 1) - (Ini));
                                aux = aux + Contenido + " = Cadena " + contCad + "\n";
                                contCad++;

                            }
                        }
                        else
                        {
                            int indComp = txt.IndexOf("(") + 1;
                            int finComp = txt.IndexOf(")");
                            string Comp = txt.Substring(indComp, finComp - indComp);
                            int ContIdeInterno = contIde;
                            string compAux = analizar(Comp);
                            if (compAux.Contains("Identificador"))
                            {
                                aux = aux + Comp + " = " + "Identificador " + ContIdeInterno + "\n";
                            }
                        }
                        if (txt.Contains(')'))
                        {
                            aux = aux + Ins.CerrPar;
                            if (txt.Contains(';'))
                            {
                                aux = aux + Ins.puntoComa;
                            }
                        }
                    }
                    return aux;
                }
                if (txt.Contains("Mientras(") && txt.Contains(')'))
                {
                    int posParCerr = txt.IndexOf(")", 0);
                    List<String> lista = devCont(txt, "(", ")");
                    String aux = "";
                    foreach (String item in lista)
                    {
                        if (BuscOpRel(item))
                        {
                            aux = aux + item + " = " + IdenOpRel(item) + "\n";
                        }
                        else
                        {
                            aux = aux + item + " = " + analizar(item) + "\n";
                        }
                    }
                    if (txt[posParCerr] == ')')
                    {
                        aux = aux + Ins.CerrPar;
                    }
                    if (txt.Contains("{"))
                    {
                        if (txt[posParCerr + 1] == '{' && txt[txt.Length - 1] == '}')
                        {
                            aux = aux + Ins.llaveApe;
                            List<String> listaCorchetes = devCont(txt, "{", "}");

                            foreach (String item in listaCorchetes)
                            {
                                if (item == "INS")
                                {
                                    aux = aux + "INS = " + Ins.instruccion + contIns + "\n";
                                    contIns++;
                                }
                            }
                            aux = aux + Ins.llaveCerr;
                        }
                    }
                    else if (txt.Contains("INS"))
                    {
                        aux = aux + "INS = " + Ins.instruccion + contIns + "\n";
                    }

                    return Ins.Mientras(aux);
                }
                if (txt.Contains("Compara(") && txt.Contains(')'))
                {
                    txt = txt.Trim();
                    int posParCerr = txt.IndexOf(")", 0);
                    List<String> lista = devCont(txt, "(", ")");
                    String aux = "";
                    foreach (String item in lista)
                    {
                        if (BuscOpRel(item))
                        {
                            aux = aux + item + " = " + IdenOpRel(item) + "\n";
                        }
                        else
                        {
                            aux = aux + item + " = " + analizar(item) + "\n";
                        }
                    }
                    if (txt[posParCerr] == ')')
                    {
                        aux = aux + Ins.CerrPar;
                    }
                    aux = contLlaves(txt, posParCerr, aux);

                    if (txt.Contains("Sino"))
                    {
                        aux = aux + "Sino = Palabra reservada que indica un caso contrario a la comparacion previa.\n";

                        String texto = txt.Trim();
                        int IndiceLlaveSino = txt.IndexOf("Sino") + 3;
                        char llaveape = txt[IndiceLlaveSino + 1];
                        if (txt[txt.IndexOf("Sino") + 4] == '{' && txt[txt.Length - 1] == '}')
                        {
                            if (txt.Contains("{") && txt.Contains("}"))
                            {
                                if (txt[IndiceLlaveSino + 1] == '{')
                                {
                                    aux = aux + Ins.llaveApe;
                                    string Contenido = txt.Substring(IndiceLlaveSino + 2, (txt.Length - 1) - (IndiceLlaveSino + 2));
                                    List<String> listaCorchetes = Contenido.Split(Convert.ToChar(" ")).ToList<String>();
                                    foreach (String item in listaCorchetes)
                                    {
                                        if (item == "INS")
                                        {
                                            aux = aux + "INS = " + Ins.instruccion + contIns + "\n";
                                            contIns++;
                                        }
                                    }
                                    aux = aux + Ins.llaveCerr;
                                }
                            }
                        }
                        else if (txt.Contains("INS"))
                        {
                            aux = aux + "INS = " + Ins.instruccion + contIns + "\n";
                        }

                        else if (texto[texto.Length - 1] == 'S' && texto[texto.Length - 2] == 'N' && texto[texto.Length - 3] == 'I')
                        {
                            aux = aux + "INS = " + Ins.instruccion + contIns + "\n";
                        }

                    }
                    return Ins.Compara(aux);
                }
                if (txt.Contains("Para"))
                {
                    String variable = "";
                    String aux = "";
                    aux = aux + Ins.Para();
                    if (txt.Contains("(") && txt.Contains(')') && txt.Contains(';'))
                    {
                        int indIni = txt.IndexOf('(') + 1;
                        int indFin = txt.IndexOf(';');

                        string comp = txt.Substring(indIni, indFin - indIni);
                        List<String> lista = comp.Split(Convert.ToChar(" ")).ToList<String>();

                        aux = aux + Ins.AperPar;
                        foreach (string item in lista)
                        {
                            contIde = 1;
                            if (BuscOpRel(item))
                                aux = aux + item + " = " + IdenOpRel(item) + "\n";
                            else if (item == ";")
                                aux = aux + item + " = " + "Separador de parametros.\n";
                            else if (item == lista[0] + "++")
                                aux = aux + item + " = " + " Incremento.\n";
                            else if (item == lista[0] + "--")
                                aux = aux + item + " = " + " Decremento\n";
                            else
                            {
                                String auxVar = analizar(item);
                                aux = aux + item + " = " + auxVar + "\n";
                                if (auxVar.Contains("Identificador"))
                                    variable = item;
                            }
                        }
                        aux = aux + Ins.puntoComa;
                        indIni = indFin + 1;
                        indFin = txt.LastIndexOf(";");
                        comp = txt.Substring(indIni, indFin - indIni);
                        lista = comp.Split(Convert.ToChar(" ")).ToList<String>();

                        foreach (string item in lista)
                        {
                            if (BuscOpRel(item))
                                aux = aux + item + " = " + IdenOpRel(item) + "\n";
                            else if (item == ";")
                                aux = aux + item + " = " + "Separador de parametros.\n";
                            else if (item == lista[0] + "++")
                                aux = aux + item + " = " + " Incremento.\n";
                            else if (item == lista[0] + "--")
                                aux = aux + item + " = " + " Decremento\n";
                            else
                                aux = aux + item + " = " + analizar(item) + "\n";
                        }
                        aux = aux + Ins.puntoComa;
                        indIni = indFin + 1;
                        indFin = txt.IndexOf(")");
                        comp = txt.Substring(indIni, indFin - indIni);
                        lista = comp.Split(Convert.ToChar(" ")).ToList<String>();

                        foreach (string item in lista)
                        {
                            if (item.Contains("++"))
                            {
                                aux = aux + item + " = incremento de " + variable + " en uno.\n";
                            }
                            else if (item.Contains("--"))
                            {
                                aux = aux + item + " = decremento de " + variable + " en uno.\n";
                            }
                        }

                        aux = aux + Ins.CerrPar;

                        if (txt[txt.Length - 1] == 'S' && txt[txt.Length - 2] == 'N' && txt[txt.Length - 3] == 'I')
                        {
                            aux = aux + "INS = " + Ins.instruccion + contIns;
                            contIns++;
                        }

                        else if (txt.Contains('{') && txt.Contains('}'))
                        {
                            aux = aux + Ins.llaveApe;
                            int indLlavesAper = txt.IndexOf('{');
                            int indLlavesCerr = txt.IndexOf('}');

                            string compLlaves = txt.Substring(indLlavesAper + 1, indLlavesCerr - (indLlavesAper + 1));
                            List<String> listaLlaves = compLlaves.Split(Convert.ToChar(" ")).ToList<String>();

                            foreach (String item in listaLlaves)
                            {
                                if (item == "INS")
                                {
                                    aux = aux + "INS = " + Ins.instruccion + contIns + "\n";
                                    contIns++;
                                }
                            }
                            aux = aux + Ins.llaveCerr;
                        }
                        return aux;
                    }
                    else
                    {
                        return "Error";
                    }
                }
                if (txt.Contains("Encuentra"))
                {
                    List<String> ListaAuxLlaves = new List<string>();
                    String aux = "";
                    aux = aux + Ins.Encuentra();

                    if (txt.Contains('(') && txt.Contains(')'))
                    {
                        if (txt.Contains('[') && txt.Contains(']'))
                        {
                            ListaAuxLlaves.Add("[ = Inicio de bloque de instrucciones de posicion.\n");
                            int indLlavesAper = txt.IndexOf('[');
                            int indDosPuntos = txt.IndexOf(':');

                            string compLlaves1 = txt.Substring(indLlavesAper + 1, indDosPuntos - (indLlavesAper + 1));

                            List<String> listaLlaves = compLlaves1.Split(Convert.ToChar(" ")).ToList<String>();

                            listaLlaves.Add(":");
                            int indLlavesCerr = txt.IndexOf(']');
                            string compLlaves2 = txt.Substring(indDosPuntos + 1, indLlavesCerr - (indDosPuntos + 1));
                            compLlaves2 = compLlaves2.Replace(" ", String.Empty);
                            //listaLlaves = compLlaves.Split(Convert.ToChar(" ")).ToList<String>();
                            listaLlaves.Add(compLlaves2);

                            if (compLlaves1.Length == 0 && compLlaves2.Length == 0)
                            {
                                if (txt.Contains(":"))
                                {
                                    listaLlaves.Clear();
                                    listaLlaves.Add(":");
                                }
                            }

                            foreach (string item in listaLlaves)
                            {
                                if (item == ":")
                                {
                                    ListaAuxLlaves.Add(item + " = Simbolo para indicar todas las posiciones del vector o hasta un punto especifico.\n");
                                }
                                else if (item == ",")
                                {
                                    ListaAuxLlaves.Add(item + " = Separador de parametros.\n");
                                }
                                else if (item == "INI")
                                {
                                    ListaAuxLlaves.Add(item + " = Palabra reservada para indicar primera posicion de vector.\n");
                                }
                                else if (item == "FIN")
                                {
                                    ListaAuxLlaves.Add(item + " = Palabra reservada para indicar la ultima posicion del vector.\n");
                                }
                                else
                                {
                                    ListaAuxLlaves.Add(item + " = " + analizar(item) + "\n");
                                }

                            }
                            ListaAuxLlaves.Add("] = Fin de bloque de instrucciones de posicion\n");
                        }

                        int indParAper = txt.IndexOf('(');
                        int indComa = txt.IndexOf(',');

                        string comp = txt.Substring(indParAper + 1, indComa - (indParAper + 1));
                        List<String> listaParentesis = comp.Split(Convert.ToChar(" ")).ToList<String>();

                        listaParentesis.Add(",");
                        indComa = txt.IndexOf(',') + 1;
                        int indCorCerr = txt.IndexOf('[');
                        comp = txt.Substring(indComa, indCorCerr - indComa);
                        comp = comp.Replace(" ", String.Empty);
                        listaParentesis.Add(comp);

                        aux = aux + Ins.AperPar;



                        //var vec = new List<string>();
                        //string[] vector = vec.ToArray();

                        int contadorItem = 0;
                        foreach (string item in listaParentesis)
                        {
                            if (item == ",")
                            {
                                aux = aux + Ins.coma;
                            }
                            else
                                aux = aux + item + " = " + analizar(item) + "\n";

                            if (contadorItem == 2)
                            {
                                foreach (string itemCorch in ListaAuxLlaves)
                                    aux = aux + itemCorch;
                                break;
                            }
                            contadorItem++;
                        }
                    }
                    if (txt.Contains(')'))
                        aux = aux + Ins.CerrPar;

                    if (txt.Contains(';'))
                        aux = aux + Ins.puntoComa;

                    return aux;
                }
                if (txt.Contains("Limpiar"))
                {
                    String aux = "";
                    aux = aux + Ins.Limpiar();
                    if (txt.Contains("("))
                        aux = aux + Ins.AperPar;

                    if (txt.Contains(")"))
                        aux = aux + Ins.CerrPar;

                    if (txt.Contains(";"))
                        aux = aux + Ins.puntoComa;

                    return aux;
                }
                if (txt.Contains("Entero") || txt.Contains("Flotante"))
                {
                    if (txt.Contains("Estructura"))
                    {

                    }
                    else
                    {
                        String aux = "";

                        List<String> ListaComponentes = txt.Split(Convert.ToChar(" ")).ToList<String>();


                        foreach (string item in ListaComponentes)
                        {
                            if (item == "Entero")
                            {
                                aux = aux + Ins.Entero();
                            }
                            else if (item == "Flotante")
                            {
                                aux = aux + Ins.Flotante();
                            }
                            else if (item == "Cadena")
                            {
                                aux = aux + Ins.Cadena();
                            }
                            else if (item == "=")
                            {
                                aux = aux + item + "=" + " Simbolo de igual a.\n";
                            }
                            else if (item.Contains(";"))
                            {
                                int inicio = item.IndexOf(item[0]);
                                int fin = item.IndexOf(item[item.Length - 1]);
                                string componente = item.Substring(inicio, fin - inicio);
                                aux = aux + componente + " = " + analizar(componente) + "\n";
                                aux = aux + Ins.puntoComa;
                            }
                            else
                            {
                                aux = aux + item + " = " + analizar(item) + "\n";
                            }
                        }
                        return aux;
                    }
                }
                if (txt.Contains("Cadena"))
                {
                    if (txt.Contains("Estructura"))
                    {

                    }
                    else
                    {
                        String aux = "";
                        List<String> ListaComponentes = txt.Split(Convert.ToChar(" ")).ToList<String>();

                        foreach (string item in ListaComponentes)
                        {
                            if (item == "Cadena")
                            {
                                aux = aux + Ins.Cadena();
                            }
                            else if (item == "=")
                            {
                                aux = aux + item + "=" + " Simbolo de igual a.\n";
                            }
                            else if (item.Contains("\""))
                            {
                                int inicio = item.IndexOf("\"");
                                int fin = item.IndexOf((";"));
                                string componente = item.Substring(inicio, fin - inicio);

                                aux = aux + componente + " = " + "Cadena " + contCad + "\n";
                                aux = aux + Ins.puntoComa + "\n";
                                contCad++;
                            }
                            else
                            {
                                string itemaux;
                                itemaux = analizar(item);
                                if (itemaux == "Error lexico")
                                {
                                    aux = aux + item + " = " + "Error lexico\n";
                                }
                                else
                                {
                                    aux = aux + item + " = " + itemaux + "\n";
                                }
                            }
                        }
                        return aux;
                    }
                }
                if (txt.Contains("Segunsea"))
                {
                    String aux = "";
                    aux = aux + Ins.SegunSea();
                    string caso = "";
                    int numCaso = 1;

                    if (txt.Contains('('))
                    {
                        aux = aux + Ins.AperPar;
                        int inicio = txt.IndexOf("(") + 1;
                        int fin = txt.IndexOf(")");
                        string componente = txt.Substring(inicio, fin - inicio);


                        string ide = analizar(componente);
                        if (ide.Contains("Identificador"))
                        {
                            aux = aux + componente + " = " + ide + "\n";
                            aux = aux + Ins.CerrPar;
                            if (txt.Contains("{"))
                            {
                                aux = aux + Ins.llaveApe;
                                int inicioLlaves = txt.IndexOf("{") + 1;
                                int finLlaves = txt.IndexOf("}") + 1;
                                string componenteLlaves = txt.Substring(inicioLlaves, finLlaves - inicioLlaves);
                                List<String> ListaComponentes = componenteLlaves.Split(Convert.ToChar(" ")).ToList<String>();

                                foreach (string item in ListaComponentes)
                                {
                                    if (item == "Caso")
                                    {
                                        aux = aux + item + " = " + " Caso a utilizar ";
                                    }
                                    else if (item.Contains(":"))
                                    {
                                        int ini = item.IndexOf(item[0]);
                                        int PuntosDobles = item.IndexOf(":");
                                        string dividir = item.Substring(ini, PuntosDobles - ini);
                                        if (dividir.All(Char.IsDigit))
                                        {
                                            caso = dividir;
                                            aux = aux + dividir + "\n";
                                            aux = aux + item[PuntosDobles] + " = Inicio de instrucciones\n";
                                        }
                                    }
                                    else if (item == "INS")
                                    {
                                        aux = aux + item + " = " + Ins.instruccion + contIns + "\n";
                                        contIns++;

                                    }
                                    else if (item.Contains("Salir"))
                                    {
                                        if (item.Contains(";"))
                                        {
                                            int iniSalir = item.IndexOf(item[0]);
                                            int puntoComa = item.IndexOf(";");
                                            string dividir = item.Substring(iniSalir, puntoComa - iniSalir);
                                            aux = aux + dividir + " = Salida de caso " + numCaso + "\n";
                                            numCaso++;
                                            aux = aux + " ; = Fin del caso " + caso + "\n";
                                            if (item.Contains("}"))
                                                aux = aux + Ins.llaveCerr;
                                        }

                                    }
                                    else if (item == ";")
                                    {
                                        aux = aux + " ; " + " = Fin del caso \n";
                                    }
                                    else if (item.Contains("}"))
                                    {
                                        aux = aux + Ins.llaveCerr;
                                    }
                                }
                            }
                        }

                    }
                    return aux;
                }
                if (txt.Contains("Leer"))
                {
                    String aux = "";
                    aux = aux + Ins.Leer();

                    if (txt.Contains("("))
                    {
                        if (txt.Contains(")"))
                        {
                            aux = aux + Ins.AperPar;
                            int inicio = txt.IndexOf("(") + 1;
                            int fin = txt.IndexOf(")");
                            string variable = txt.Substring(inicio, fin - inicio);
                            string variableaux = analizar(variable);
                            if (variableaux.Contains("Identificador"))
                            {
                                aux = aux + variable + " = " + variableaux + "\n";
                                aux = aux + Ins.CerrPar;
                            }
                            if (txt[txt.Length - 1] == ';')
                            {
                                aux = aux + Ins.puntoComa;
                            }

                        }
                        else
                        {
                            aux = aux + " Erro lexico\n";
                        }
                    }
                    else
                    {
                        aux = aux + " Erro lexico\n";
                    }
                    return aux;
                }
                if (txt.Contains("Aleatorio"))
                {
                    String aux = "";
                    aux = aux + Ins.Aleatorio();
                    if (txt.Contains("("))
                    {
                        if (txt.Contains(","))
                        {
                            aux = aux + Ins.AperPar;

                            int IniComp = txt.IndexOf("(") + 1;
                            int FinComp = txt.IndexOf(",");
                            string comp = txt.Substring(IniComp, FinComp - IniComp);
                            aux = aux + comp + " = " + analizar(comp) + "\n";


                            IniComp = txt.IndexOf(",") + 1;
                            aux = aux + Ins.coma;
                            FinComp = txt.IndexOf(")");
                            comp = txt.Substring(IniComp, FinComp - IniComp);
                            aux = aux + comp + " = " + analizar(comp) + "\n";
                        }
                        aux = aux + Ins.CerrPar;
                    }
                    if (txt[txt.Length - 1].ToString() == ";")
                    {
                        aux = aux + Ins.puntoComa;
                    }
                    return aux;
                }
                if (txt.Contains("Estructura"))
                {
                    Console.WriteLine(txt);
                    String aux = "";
                    aux = aux + Ins.Estructura();
                    if (txt.Contains("("))
                    {
                        aux = aux + Ins.AperPar;
                        int IniComp = txt.IndexOf("(") + 1;
                        int FinComp = txt.IndexOf(")");
                        string comp = txt.Substring(IniComp, FinComp - IniComp);
                        string compaux = analizar(comp);

                        if (compaux.Contains("Identificador"))
                        {
                            aux = aux + comp + " = " + compaux + "\n";
                        }
                        else
                        {
                            aux = aux + compaux + " = Error lexico\n";
                        }

                        if (txt[FinComp].ToString() == ")")
                        {
                            aux = aux + Ins.CerrPar;

                            if (txt[FinComp + 1].ToString() == "{")
                            {
                                aux = aux + Ins.llaveApe;
                                IniComp = txt.IndexOf("{");
                                FinComp = txt.IndexOf("}");
                                comp = txt.Substring(IniComp + 1, FinComp - (IniComp + 1));
                                List<String> ListaComponentes = comp.Split(Convert.ToChar(" ")).ToList<String>();
                                string itemaux = "";
                                foreach (string item in ListaComponentes)
                                {

                                    if (item == "Entero")
                                    {
                                        aux = aux + Ins.Entero();
                                    }
                                    else if (item == "Flotante")
                                    {
                                        aux = aux + Ins.Flotante();
                                    }
                                    else if (item == "Cadena")
                                    {
                                        aux = aux + Ins.Cadena();
                                    }

                                    else if (item[item.Length - 1].ToString() == ";")
                                    {
                                        IniComp = 0;
                                        FinComp = item.IndexOf(";");
                                        itemaux = item.Substring(IniComp, FinComp - IniComp);
                                        aux = aux + itemaux;
                                        itemaux = analizar(itemaux);
                                        if (itemaux.Contains("Identificador"))
                                        {
                                            aux = aux + " = " + itemaux + "\n";
                                        }
                                        aux = aux + Ins.puntoComa + Ins.llaveCerr;

                                    }
                                }
                            }
                        }
                    }
                    return aux;
                }
                if (txt.Contains("INICIO"))
                {
                    String aux = "";
                    aux = aux + Ins.INICIO();
                    if (txt.Contains("{") && txt.Contains("}"))
                    {
                        aux = aux + Ins.llaveApe;
                        int IniComp = txt.IndexOf("{") + 1;
                        int FinComp = txt.IndexOf("}");
                        string Comp = txt.Substring(IniComp, FinComp - IniComp);
                        List<String> ListaComponentes = Comp.Split(Convert.ToChar(" ")).ToList<String>();
                        foreach (string item in ListaComponentes)
                        {
                            if (item == "INS")
                            {
                                aux = aux + item + "  = " + Ins.instruccion + contIns + "\n";
                            }
                        }
                        aux = aux + Ins.llaveCerr;
                    }
                    if (txt.Contains("FIN"))
                        aux = aux + Ins.FIN();
                    return aux;
                }
                if (txt.Contains("Raiz"))
                {
                    String aux = "";
                    aux = aux + Ins.Raiz();
                    if (txt.Contains("(") && txt.Contains(")"))
                    {
                        aux = aux + Ins.AperPar;

                        int IniComp = txt.IndexOf("(") + 1;
                        int FinComp = txt.IndexOf(",");
                        string Comp = txt.Substring(IniComp, FinComp - IniComp);
                        List<String> ListaComponentes = Comp.Split(Convert.ToChar(" ")).ToList<String>();

                        foreach (string item in ListaComponentes)
                        {
                            string itemAux = item;
                            itemAux = analizar(item);
                            if (itemAux.Contains("Identificador"))
                            {
                                aux = aux + item + " = " + itemAux + "\n";
                            }
                            else if (itemAux.Contains("Numero"))
                            {
                                aux = aux + item + " = " + itemAux + "\n";
                            }
                        }

                        aux = aux + Ins.coma;
                        ListaComponentes.Clear();
                        IniComp = txt.IndexOf(",") + 1;
                        FinComp = txt.IndexOf(")");
                        Comp = txt.Substring(IniComp, FinComp - IniComp);
                        ListaComponentes = Comp.Split(Convert.ToChar(" ")).ToList<String>();
                        foreach (string item in ListaComponentes)
                        {
                            string itemAux = item;
                            itemAux = analizar(item);
                            if (itemAux.Contains("Identificador"))
                            {
                                aux = aux + item + " = " + itemAux + "\n";
                            }
                            else if (itemAux.Contains("Numero"))
                            {
                                aux = aux + item + " = " + itemAux + "\n";
                            }
                        }
                        aux = aux + Ins.CerrPar;
                    }
                    if (txt.Contains(";"))
                    {
                        aux = aux + Ins.puntoComa;
                    }
                    return aux;
                }
                if (txt.Contains("Exponente"))
                {
                    String aux = "";
                    aux = aux + Ins.Exponente();
                    if (txt.Contains("(") && txt.Contains(")"))
                    {
                        aux = aux + Ins.AperPar;

                        int IniComp = txt.IndexOf("(") + 1;
                        int FinComp = txt.IndexOf(",");
                        string Comp = txt.Substring(IniComp, FinComp - IniComp);
                        List<String> ListaComponentes = Comp.Split(Convert.ToChar(" ")).ToList<String>();

                        foreach (string item in ListaComponentes)
                        {
                            string itemAux = item;
                            itemAux = analizar(item);
                            if (itemAux.Contains("Identificador"))
                            {
                                aux = aux + item + " = " + itemAux + "\n";
                            }
                            else if (itemAux.Contains("Numero"))
                            {
                                aux = aux + item + " = " + itemAux + "\n";
                            }
                        }

                        aux = aux + Ins.coma;
                        ListaComponentes.Clear();
                        IniComp = txt.IndexOf(",") + 1;
                        FinComp = txt.IndexOf(")");
                        Comp = txt.Substring(IniComp, FinComp - IniComp);
                        ListaComponentes = Comp.Split(Convert.ToChar(" ")).ToList<String>();
                        foreach (string item in ListaComponentes)
                        {
                            string itemAux = item;
                            itemAux = analizar(item);
                            if (itemAux.Contains("Identificador"))
                            {
                                aux = aux + item + " = " + itemAux + "\n";
                            }
                            else if (itemAux.Contains("Numero"))
                            {
                                aux = aux + item + " = " + itemAux + "\n";
                            }
                        }
                        aux = aux + Ins.CerrPar;
                    }
                    if (txt.Contains(";"))
                    {
                        aux = aux + Ins.puntoComa;
                    }
                    return aux;
                }
                if (txt.Contains("Crear"))
                {
                    String aux = "";
                    aux = Ins.Crear();

                    if (txt.Contains("(") && txt.Contains(")"))
                    {
                        aux = aux + Ins.AperPar;
                        int IniComp = txt.IndexOf("(") + 1;
                        int FinComp = txt.IndexOf(",");
                        string Comp = txt.Substring(IniComp, FinComp - IniComp);

                        if (Comp[0].ToString() == "\"" && Comp[Comp.Length - 1].ToString() == "\"")
                        {
                            aux = aux + Comp + Ins.cadContArchivo;
                            //contCad++;
                        }
                        aux = aux + Ins.coma;

                        IniComp = txt.IndexOf(",") + 1;
                        FinComp = txt.IndexOf(")");
                        Comp = txt.Substring(IniComp, FinComp - IniComp);

                        if (Comp[0].ToString() == "\"" && Comp[Comp.Length - 1].ToString() == "\"")
                        {
                            aux = aux + Comp + Ins.cadRutaArchivo;
                            //contCad++;
                        }

                        aux = aux + Ins.CerrPar;

                    }
                    if (txt.Contains(";"))
                        aux = aux + Ins.puntoComa;

                    return aux;
                }
                if (txt.Contains("Modifica"))
                {
                    String aux = "";
                    aux = aux + Ins.Modifica();

                    if (txt.Contains("(") && txt.Contains(")"))
                    {
                        aux = aux + Ins.AperPar;
                        int IniComp = txt.IndexOf("(") + 1;
                        int FinComp = txt.IndexOf(",");
                        string Comp = txt.Substring(IniComp, FinComp - IniComp);

                        if (Comp[0].ToString() == "\"" && Comp[Comp.Length - 1].ToString() == "\"")
                        {
                            aux = aux + Comp + Ins.cadNombreArchivo;
                            //contCad++;
                        }

                        aux = aux + Ins.coma;

                        IniComp = txt.IndexOf(",") + 1;
                        FinComp = txt.LastIndexOf(",");
                        Comp = txt.Substring(IniComp, FinComp - IniComp);

                        if (Comp[0].ToString() == "\"" && Comp[Comp.Length - 1].ToString() == "\"")
                        {
                            aux = aux + Comp + Ins.cadRutaArchivo;
                            //contCad++;
                        }

                        aux = aux + Ins.coma;

                        IniComp = txt.LastIndexOf(",") + 1;
                        FinComp = txt.IndexOf(")");
                        Comp = txt.Substring(IniComp, FinComp - IniComp);
                        if (Comp[0].ToString() == "\"" && Comp[Comp.Length - 1].ToString() == "\"")
                        {
                            aux = aux + Comp + Ins.cadContArchivo;
                            contCad++;
                        }
                        aux = aux + Ins.CerrPar;
                    }
                    if (txt.Contains(";"))
                    {
                        aux = aux + Ins.puntoComa;
                    }
                    return aux;
                }
                if (txt.Contains("Elimina"))
                {
                    String aux = "";
                    aux = Ins.Elimina();

                    if (txt.Contains("(") && txt.Contains(")"))
                    {
                        aux = aux + Ins.AperPar;
                        int IniComp = txt.IndexOf("(") + 1;
                        int FinComp = txt.IndexOf(",");
                        string Comp = txt.Substring(IniComp, FinComp - IniComp);

                        if (Comp[0].ToString() == "\"" && Comp[Comp.Length - 1].ToString() == "\"")
                        {
                            aux = aux + Comp + Ins.cadNombreArchivo;
                            //contCad++;
                        }
                        aux = aux + Ins.coma;

                        IniComp = txt.IndexOf(",") + 1;
                        FinComp = txt.IndexOf(")");
                        Comp = txt.Substring(IniComp, FinComp - IniComp);

                        if (Comp[0].ToString() == "\"" && Comp[Comp.Length - 1].ToString() == "\"")
                        {
                            aux = aux + Comp + Ins.cadRutaArchivo;
                            //contCad++;
                        }

                        aux = aux + Ins.CerrPar;

                    }
                    if (txt.Contains(";"))
                        aux = aux + Ins.puntoComa;

                    return aux;
                }

                return "Error Lexico";
            }
            catch (Exception ex)
            {
                return "Error Lexico " + ex.Message;
            }
        }



        private string contLlaves(string txt, int posParAb, String aux)
        {
            if (txt.Contains("{") && txt.Contains("}"))
            {
                if (txt[posParAb + 1] == '{')
                {
                    aux = aux + Ins.llaveApe;
                    List<String> listaCorchetes = devCont(txt, "{", "}");

                    foreach (String item in listaCorchetes)
                    {
                        if (item == "INS")
                        {
                            aux = aux + "INS = " + Ins.instruccion + contIns + "\n";
                            contIns++;
                        }
                    }
                    aux = aux + Ins.llaveCerr;
                }
            }
            else if (txt.Contains("INS"))
            {
                aux = aux + "INS = " + Ins.instruccion + contIns + "\n";
                contIns++;
            }

            return aux;
        }
        private List<String> devCont(string txt, string ComponenteCom, string Componentefinal)
        {
            int inicio = txt.IndexOf(ComponenteCom) + 1;
            int fin = txt.IndexOf(Componentefinal, inicio);
            string Contenido = txt.Substring(inicio, fin - inicio);
            List<String> lista = Contenido.Split(Convert.ToChar(" ")).ToList<String>();
            return lista;
        }

        private bool BuscaOpAr(String txt)
        {
            if (txt == "+" || txt == "-" || txt == "*" || txt == "/" || txt == "=")
                return true;
            return false;
        }
        private String IdenOpAr(String txt)
        {
            switch (txt)
            {
                case "+":
                    return "Suma";
                case "-":
                    return "Resta";
                case "*":
                    return "Multiplicacion";
                case "/":
                    return "Division";
                case "=":
                    return "Simbolo igual";
                case "==":
                    return "Simbolo de comparacion";
                default:
                    return "Error en Operador aritemetico";
            }
        }
        private bool BuscOpRel(String txt)
        {
            if (txt == "<" || txt == "<=" || txt == ">" || txt == ">=" || txt == "=" || txt == "!=")
            {
                return true;
            }
            return false;
        }
        private String IdenOpRel(String txt)
        {
            switch (txt)
            {
                case ">":
                    return "Mayor que";
                case ">=":
                    return "Mayor o igual que";
                case "<":
                    return "Menor que";
                case "<=":
                    return "Menor o igual que";
                case "=":
                    return "Igual que";
                case "==":
                    return "Signo de comparacion";
                case "!=":
                    return "Diferente de";
                default:
                    return "Error Lexico";
            }
        }
        private bool IdenMayusculas(String Texto)
        {
            bool cont = false;
            for (int i = 0; i < Texto.Length; i++)
            {
                if (Char.IsUpper(Texto[i]))
                {
                    cont = true;
                }
            }
            if (cont)
                return true;
            else
                return false;
        }


        private void txtTextoAnalizar_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            contIde = 1;
            contIns = 1;
            contCad = 1;
            contVec = 1;
            txtTextoAnalizar.Text = "";
            txtTextoAnalizar.Focus();
            lblprueba.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}