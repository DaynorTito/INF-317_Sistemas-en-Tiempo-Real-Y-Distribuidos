#include <stdio.h>
#define TAM 20

// Realice el algoritmo de fibonacci, utilizando solo los términos iniciales en cada vector, conservando la forma de cálculo convencional.

void fibonacci_vector(int vector[], int n) {
    int primer_termino[1];
    int segundo_termino[1];
    
    primer_termino[0] = 0;
    segundo_termino[0] = 1;
    
    vector[0] = primer_termino[0];
    vector[1] = segundo_termino[0];
    
    for(int i = 2; i < n; i++) {
        vector[i] = primer_termino[0] + segundo_termino[0];
        primer_termino[0] = segundo_termino[0];
        segundo_termino[0] = vector[i];
    }
}

void imprimir_vector(int vector[], int n) {
    printf("Serie de Fibonacci:\n");
    for(int i = 0; i < n; i++) {
        printf("%d, ", vector[i]);
    }
    printf("\n");
}

int main() {
    int n = TAM;
    int vector_fib[TAM];
    
    fibonacci_vector(vector_fib, n);
    
    imprimir_vector(vector_fib, n);
    
    return 0;
}