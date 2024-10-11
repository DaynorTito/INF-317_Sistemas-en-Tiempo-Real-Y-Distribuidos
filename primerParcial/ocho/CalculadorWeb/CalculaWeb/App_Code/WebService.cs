using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text.RegularExpressions;


/// <summary>
/// Descripción breve de WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hola a todos";
    }

    [WebMethod]
    public double suma(double a, double b)
    {
        return a + b;
    }

    [WebMethod]
    public double resta(double a, double b)
    {
        return a - b;
    }

    [WebMethod]
    public double multi(double a, double b)
    {
        return a * b;
    }

    [WebMethod]
    public double divi(double a, double b)
    {
        return a / b;
    }

    [WebMethod]
    public double RealizarOperacion(double a, double b, string operador)
    {
        if (operador == "+")
        {
            return suma(a, b);
        }
        else if (operador == "-")
        {
            return resta(a, b);
        }
        else if (operador == "*")
        {
            return multi(a, b);
        }
        else if (operador == "/")
        {
            if (b == 0) throw new DivideByZeroException("No se puede dividir entre cero.");
            return divi(a,b);
        }
        else
        {
            throw new InvalidOperationException("Operador no válido");
        }
    }

    [WebMethod]
    public int Precedencia(string operador)
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

    [WebMethod]
    public bool EsOperador(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    [WebMethod]
    public double EvaluarPostfijo(List<string> postfijo)
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

    [WebMethod]
    public List<string> ConvertirAPostfijo(string expresion)
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

}
