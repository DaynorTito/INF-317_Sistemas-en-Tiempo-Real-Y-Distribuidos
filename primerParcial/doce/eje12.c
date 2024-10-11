#include <stdio.h>
#include <omp.h>

int main() {
    int limite = 20;
    int anterior = 0, actual = 1, siguiente;
    printf("Secuencia Fibonacci: %d, %d", anterior, actual);
    #pragma omp parallel shared(anterior, actual) private(siguiente)
    {
        #pragma omp for
        for (int indice = 2; indice < limite; indice++) {
            #pragma omp critical
            {
                siguiente = anterior + actual;
                anterior = actual;
                actual = siguiente;
                printf(", %d", actual);
            }
        }
    }

    printf("\n");
    return 0;
}
