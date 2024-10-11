#include <stdio.h>

// Realice un programa que tenga los métodos suma, resta, multiplicación, división en lenguaje c. Programe los mismo sin el uso de punteros.

float suma(float a, float b)
{
    return a + b;
}

float resta(float a, float b)
{
    return a - b;
}

float multiplicacion(float a, float b)
{
    float resultado = 0;
    int signo = 1;
    if (b < 0)
    {
        b = -b;
        signo = -1;
    }
    for (int i = 0; i < (int)b; i++)
    {
        resultado += a;
    }
    resultado *= signo;
    return resultado;
}

float division(float a, float b)
{
    if (b != 0)
        return a / b;
    else
    {
        printf("Division por cero\n");
        return 0;
    }
}

int main()
{
    float num1, num2;

    printf("Ingrese dos numeros: ");
    scanf("%f %f", &num1, &num2);

    printf("\nSuma:  %.2f\n", suma(num1, num2));
    printf("Resta:  %.2f\n", resta(num1, num2));
    printf("Multiplicacion: %.2f\n", multiplicacion(num1, num2));
    if (num2 != 0)
    {
        printf("Division: %.2f\n", division(num1, num2));
    }
    else
    {
        printf("No se puede dividir entr cero\n");
    }
    return 0;
}
