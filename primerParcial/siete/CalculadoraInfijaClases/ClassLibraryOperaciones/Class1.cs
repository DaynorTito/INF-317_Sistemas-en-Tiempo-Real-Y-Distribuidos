using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClassLibraryOperaciones
{
    public class Class1
    {
        public string evaluarExpresion(string expresion)
        {
            try
            {
                var postfija = ConvertirAPostfijo(expresion);
                return EvaluarPostfijo(postfija).ToString();
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        private List<string> ConvertirAPostfijo(string expresion)
        {
            var output = new List<string>();
            var operadores = new Stack<string>();

            var tokens = Regex.Matches(expresion, @"(\d+(\.\d+)?)|[+\-*/()]")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();

            foreach (var token in tokens)
            {
                double numero;

                if (double.TryParse(token, out numero))
                {
                    output.Add(token);
                }
                else if (token == "(")
                {
                    operadores.Push(token);
                }
                else if (token == ")")
                {
                    while (operadores.Count > 0 && operadores.Peek() != "(")
                    {
                        output.Add(operadores.Pop());
                    }
                    if (operadores.Count == 0)
                        throw new InvalidOperationException("Parentesis no balanceados.");
                    operadores.Pop();
                }
                else if (EsOperador(token))
                {
                    while (operadores.Count > 0 && Precedencia(operadores.Peek()) >= Precedencia(token))
                    {
                        output.Add(operadores.Pop());
                    }
                    operadores.Push(token);
                }
                else
                {
                    throw new InvalidOperationException("Token no valido: " + token);
                }
            }

            while (operadores.Count > 0)
            {
                output.Add(operadores.Pop());
            }

            return output;
        }

        private double EvaluarPostfijo(List<string> postfijo)
        {
            var pila = new Stack<double>();

            foreach (var token in postfijo)
            {
                double numero;

                if (double.TryParse(token, out numero))
                {
                    pila.Push(numero);
                }
                else if (EsOperador(token))
                {
                    if (pila.Count < 2)
                        throw new InvalidOperationException("Pila vacia: no hay suficientes operandos para realizar la operacion.");

                    var b = pila.Pop();
                    var a = pila.Pop();
                    var resultado = RealizarOperacion(a, b, token);
                    pila.Push(resultado);
                }
                else
                {
                    throw new InvalidOperationException("Token no valido: " + token);
                }
            }
            if (pila.Count == 0)
                throw new InvalidOperationException("Pila vacia: no se pudo evaluar la expresion.");

            return pila.Pop();
        }

        private bool EsOperador(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }

        private int Precedencia(string operador)
        {
            if (operador == "+" || operador == "-")
            {
                return 1;
            }
            else if (operador == "*" || operador == "/")
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        private double RealizarOperacion(double a, double b, string operador)
        {
            if (operador == "+")
            {
                return a + b;
            }
            else if (operador == "-")
            {
                return a - b;
            }
            else if (operador == "*")
            {
                return a * b;
            }
            else if (operador == "/")
            {
                if (b == 0) throw new DivideByZeroException("No se puede dividir entre cero.");
                return a / b;
            }
            else
            {
                throw new InvalidOperationException("Operador no válido");
            }
        }
    }
}
