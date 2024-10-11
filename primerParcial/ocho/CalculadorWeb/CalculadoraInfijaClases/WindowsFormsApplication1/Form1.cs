using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
            
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void button14_Click(object sender, EventArgs e)
        {

            if (!EsUltimoCaracterOperador(textBox1.Text))
            {
                textBox1.Text += "+";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!EsUltimoCaracterOperador(textBox1.Text))
            {
                textBox1.Text += "-";
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (!EsUltimoCaracterOperador(textBox1.Text))
            {
                textBox1.Text += "*";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (!EsUltimoCaracterOperador(textBox1.Text))
            {
                textBox1.Text += "/";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (SonParentesisValidos(textBox1.Text))
            {
                string expresion = textBox1.Text;

                try
                {
                    ServiceReference1.WebServiceSoapClient sc1 = new ServiceReference1.WebServiceSoapClient();
                    var postfija = sc1.ConvertirAPostfijo(expresion);
                    double resultado = sc1.EvaluarPostfijo(postfija);
                    textBox2.Text = resultado.ToString();
                }
                catch (Exception ex)
                {
                    textBox2.Text = "Error: " + ex.Message;
                }
            }
            else
            {
                MessageBox.Show("Expresión inválida. Revisa los operadores y paréntesis.", "Error de Sintaxis", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int tam = textBox1.Text.Length;
            if (tam > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, tam - 1);
            }
        }

        private bool EsUltimoCaracterOperador(string texto)
        {
            if (texto.Length == 0) return false;

            char ultimoCaracter = texto[texto.Length - 1];
            return "+-*/".Contains(ultimoCaracter);
        }

        private bool BalanceParentesisCorrecto(string texto)
        {
            int balance = 0;

            foreach (char c in texto)
            {
                if (c == '(') balance++;
                if (c == ')') balance--;
                if (balance < 0) return false;
            }

            return balance == 0;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text += "(";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text += ")";
        }

        private bool EsCaracterValido(char c)
        {
            return char.IsDigit(c) || "+-*/() ".Contains(c);
        }

        private bool SonParentesisValidos(string texto)
        {
            int balance = 0;

            for (int i = 0; i < texto.Length; i++)
            {
                char c = texto[i];

                if (!EsCaracterValido(c))
                    return false;

                if (c == '(') balance++;
                if (c == ')') balance--;
                if (i > 0 && c == '(' && texto[i - 1] == '(')
                    return false;

                if (i > 0 && c == ')' && texto[i - 1] == ')')
                    return false;

                if (c == '(' && i < texto.Length - 1 && EsUltimoCaracterOperador(texto.Substring(0, i)))
                    return false;

                if (i == texto.Length - 1 && EsUltimoCaracterOperador(texto))
                    return false;
            }

            return balance == 0;
        }
    }
}
