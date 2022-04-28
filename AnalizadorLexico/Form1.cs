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
        int contNum = 1;
        int contIde = 1;

        public String analizar(String txt) {
            //letra.ToString();
            //Char.IsLetter(letra.ToString()[0]);
            // if (txtTextoAnalizar.Text.All(Char.IsLetter)) {
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
                                    return "Error Lexico";
                                if (Char.IsDigit(txt[i]))
                                    //lblResultado.Text = txt.Text + " = Identificador";
                                    Ide = "Identificador " + contIde;
                                contIde += 1;
                                return Ide;

                            }
                        }
                        if (i > 0)
                            if (txt[i - 1] == '_' && Char.IsDigit(txt[i]))
                            {
                                //blResultado.Text = txt.Text + " = Identificador";
                                Ide = "Identificador " + contIde;
                                contIde += 1;
                                return Ide;
                            }
                    }
                    if (txt.Length > 0 && txt[txt.Length - 1] == '_' || txt.Contains("."))
                        //lblResultado.Text = txt.Text + " = Error Lexico";
                        return "Error Lexico";

                    else
                        //lblResultado.Text = txt.Text + " = Identificador";
                        Ide = "Identificador " + contIde;

                    contIde += 1;
                    return Ide;
                }
                else
                    //lblResultado.Text = txt.Text + " = Error Lexico";
                    return "Error Lexico";
            }
            // }
            else if (txt.All(Char.IsDigit) || (txt.Contains(".") && Char.IsDigit(txt[txt.IndexOf(".") + 1]) && Char.IsDigit(txt[txt.IndexOf(".") - 1])))
            {
                int n = (from c in txt where c == '.' select c).Count();
                for (int i = 0; i < txt.Length; i++)
                {
                    if (Char.IsLetter(txt[i]) || n > 1)
                    {
                        return "Error Lexico";
                    }
                }


                Ins.Numeros(txt);
                String Numero = "Numero " + contNum;
                contNum += 1;
                return Numero;
            }
            else if (Char.IsUpper(txt[0]))
            {
                return DevolverPalReservada(txt);
            }
            /* else if (Char.IsUpper(txtTextoAnalizar.Text[0]))
             {
                 if (txtTextoAnalizar.Text == "Mientras();")
                 {
                     PReser = true;
                     return Ins.Mientras(lblprueba);
                 }


             }*/
            /*for (int i = 1; i < txtTextoAnalizar.Text.Length; i++) {
                 if (!Char.IsLetter(txtTextoAnalizar.Text[i]))
                 {
                     lblResultado.Text = "Error lexico";
                     return;
                 }
             }
             lblResultado.Text = txtTextoAnalizar.Text + " = Palabra reservada";
         }*/

            /*    else if (txt == "Mientras();")
                {
                    //Ins.Mientras(lblResultado);
                    return "Palabra Reservada";
                }*/

            else
            {
                //lblResultado.Text = txt.Text + " = Error Lexico";
                return "Error Lexico";
            }
            return "Error";
        }
        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            contNum=1;
            contIde = 1;
            int contPal=0;

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
                prueba = regex.Replace(Separar, @" "); // "words with multiple spaces"

                List<String> lista = prueba.Split(Convert.ToChar(" ")).ToList<String>();

                List<String> Aux = new List<string>();

                if (Char.IsUpper(txtTextoAnalizar.Text[0]))
                {
                    lblprueba.Text = DevolverPalReservada(txtTextoAnalizar.Text);
                }
                else
                    foreach (String item in lista)
                    {
                        String Componente = item;


                        //Componente = item.Trim();
                        // String Componente = Regex.Replace(item, @"\s", "");


                        if (Aux.Contains(Componente))
                        {
                            lblprueba.Text = lblprueba.Text + Componente + " = Error, identificador repetido.\n";
                        }
                        else if (Char.IsUpper(Componente[0]))
                        {
                            lblprueba.Text = lblprueba.Text + DevolverPalReservada(Componente) + "\n";
                            Aux.Add(Componente);
                        }
                        else
                        {

                            //lblprueba.Text = lblprueba.Text + "\n" + item;
                            lblprueba.Text = lblprueba.Text + item + " = " + analizar(Componente) + "\n";
                            Aux.Add(Componente);
                            contPal++;
                        }
                    }
            }
            //analizar(txtTextoAnalizar);
        }

        private String DevolverPalReservada(string txt) { 
        
        //if (txt.Contains("Mientras") == "Mientras();")

            if (txt.Contains("Mientras(") && txt.Contains(')') && txt.Contains(';'))
            {
                int inicio = txt.IndexOf("(")+1;
                int fin = txt.IndexOf(")",inicio);
                string Contenido = txt.Substring(inicio, fin - inicio);
                // Contenido = analizar(Contenido);
                List<String> lista = Contenido.Split(Convert.ToChar(" ")).ToList<String>();
                String aux = "";
                foreach (String item in lista) {
                    if (BuscOpRel(item))
                    {
                        aux = aux + item + " = " + IdenOpRel(item) + "\n";
                    }
                    else
                    {
                        aux = aux + item + " = " + analizar(item) + "\n";
                    }

                    //aux = aux + analizar(item);
                }

                return Ins.Mientras(aux);
            }
            return "Error Lexico";
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
                case "!=":
                    return "Diferente de";
                default:
                    return "Error Lexico";
            }
        }
        private bool IdenMayusculas(String Texto) {
            bool cont = false;
            for(int i = 0; i < Texto.Length; i++)
            {
                if (Char.IsUpper(Texto[i])) {
                    cont = true;
                    //return true;
                }
            }
            if (cont)
                return true;
            else
                return false;
        }


        private void txtTextoAnalizar_TextChanged_1(object sender, EventArgs e)
        {
            /*if (txtTextoAnalizar.Text.Length >= 1 && txtTextoAnalizar.Text[txtTextoAnalizar.Text.Length - 1] == ';')
                txtTextoAnalizar.MaxLength = txtTextoAnalizar.Text.Length;
            else
                txtTextoAnalizar.MaxLength = 100;*/
        }
    }
}
