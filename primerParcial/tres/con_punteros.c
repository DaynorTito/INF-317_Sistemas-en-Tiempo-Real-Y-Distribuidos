#include <stdio.h>

//     Realice un programa que tenga los métodos suma, resta, multiplicación, división en lenguaje c. Programe los mismo con el uso de punteros.

void suma(float *resultado, float *a, float *b)
{
    *resultado = *a + *b;
}

void resta(float *resultado, float *a, float *b)
{
    *resultado = *a - *b;
}

void multiplicacion(float *resultado, float *a, float *b)
{
    *resultado = 0;
    int signo = 1;
    if (*b < 0)
    {
        *b = -*b;
        signo = -1;
    }

    for (int i = 0; i < (int)*b; i++)
    {
        *resultado += *a;
    }
    *resultado *= signo;
}

void division(float *resultado, float *a, float *b)
{
    if (*b != 0)
    {
        *resultado = *a / *b;
    }
    else
    {
        printf("Division por cero\n");
        *resultado = 0;
    }
}

int main()
{
    float num1, num2, resultado;

    printf("Ingrese dos numeros: ");
    scanf("%f %f", &num1, &num2);

    suma(&resultado, &num1, &num2);
    printf("\nSuma: %.2f\n", resultado);

    resta(&resultado, &num1, &num2);
    printf("Resta: %.2f\n", resultado);

    multiplicacion(&resultado, &num1, &num2);
    printf("Multiplicacion: %.2f\n", resultado);

    division(&resultado, &num1, &num2);
    printf("Division: %.2f\n", resultado);

    return 0;
}
