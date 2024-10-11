#include <stdio.h>

// Realice el cálculo de pi secuencial con el uso de punteros, hágalo iterativamente y recursivamente.
void calcular_pi_iterativo(double *pi, int n) {
    *pi = 0.0;
    double termino = 1.0;
    int signo = 1;
    
    for (int i = 0; i < n; i++) {
        *pi += signo * (4.0 / termino);
        termino += 2.0;
        signo *= -1;
    }
}

int main() {
    double pi;
    int n;
    n = 1000000; 
    calcular_pi_iterativo(&pi, n);
    printf("El valor aproximado de pi es: %.50f\ncon %d términos de aproximación\n", pi, n);
    return 0;
}