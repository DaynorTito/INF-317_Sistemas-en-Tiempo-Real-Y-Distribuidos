#include <stdio.h>

void pi_recursivo(double *pi, int n, int i, double termino, int signo) {
    if (i == n) {
        return;
    }
    *pi += signo * (4.0 / termino);
    pi_recursivo(pi, n, i + 1, termino + 2.0, -signo);
}

void calcular_pi_recursivo(double *pi, int n) {
    *pi = 0.0;
    pi_recursivo(pi, n, 0, 1.0, 1);
}

int main() {
    double pi;
    int n;
    n = 100000;
    calcular_pi_recursivo(&pi, n);
    printf("El valor aproximado de pi es: %.30f\n", pi);
    return 0;
}