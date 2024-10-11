#include <mpi.h>
#include <stdio.h>
#include <stdlib.h>

void multiplicar_fila(int *matrizA, int *matrizB, int fila, int tam, int *resultado) {
    for (int col = 0; col < tam; col++) {
        resultado[col] = 0;
        for (int k = 0; k < tam; k++) {
            resultado[col] += matrizA[fila * tam + k] * matrizB[k * tam + col];
        }
    }
}

int main(int argc, char **argv) {
    int id_proceso, num_procesos;
    int tam;

    MPI_Init(&argc, &argv);
    MPI_Comm_rank(MPI_COMM_WORLD, &id_proceso);
    MPI_Comm_size(MPI_COMM_WORLD, &num_procesos);

    if (num_procesos != 3) {
        if (id_proceso == 0) {
            printf("Este programa requiere 3 procesos.\n");
        }
        MPI_Finalize();
        return 1;
    }

    if (id_proceso == 0) {
        printf("Ingrese la longitud de las matrices: ");
        scanf("%d", &tam);
    }

    MPI_Bcast(&tam, 1, MPI_INT, 0, MPI_COMM_WORLD);

    int *matrizA = (int *)malloc(tam * tam * sizeof(int));
    int *matrizB = (int *)malloc(tam * tam * sizeof(int));
    int *matrizC = (int *)malloc(tam * tam * sizeof(int));

    if (id_proceso == 0) {
        printf("Ingrese los datos de la matriz A (%d x %d):\n", tam, tam);
        for (int i = 0; i < tam; i++) {
            for (int j = 0; j < tam; j++) {
                scanf("%d", &matrizA[i * tam + j]);
            }
        }

        printf("Ingrese los datos de la matriz B (%d x %d):\n", tam, tam);
        for (int i = 0; i < tam; i++) {
            for (int j = 0; j < tam; j++) {
                scanf("%d", &matrizB[i * tam + j]);
            }
        }
    }

    MPI_Bcast(matrizA, tam * tam, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_Bcast(matrizB, tam * tam, MPI_INT, 0, MPI_COMM_WORLD);

    if (id_proceso == 0) {
        int resultado[tam];
        multiplicar_fila(matrizA, matrizB, 0, tam, resultado);
        MPI_Send(resultado, tam, MPI_INT, 2, 0, MPI_COMM_WORLD);
    } else if (id_proceso == 1) {
        int resultado[tam];
        multiplicar_fila(matrizA, matrizB, 1, tam, resultado);
        MPI_Send(resultado, tam, MPI_INT, 2, 1, MPI_COMM_WORLD);
    } else if (id_proceso == 2) {
        int fila1[tam], fila2[tam], fila3[tam];
        MPI_Recv(fila1, tam, MPI_INT, 0, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        MPI_Recv(fila2, tam, MPI_INT, 1, 1, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        multiplicar_fila(matrizA, matrizB, 2, tam, fila3);

        for (int col = 0; col < tam; col++) {
            matrizC[0 * tam + col] = fila1[col];
            matrizC[1 * tam + col] = fila2[col];
            matrizC[2 * tam + col] = fila3[col];
        }

        printf("Resultado de la multiplicación:\n");
        for (int i = 0; i < 3; i++) {
            for (int col = 0; col < tam; col++) {
                printf("%d ", matrizC[i * tam + col]);
            }
            printf("\n");
        }
    }

    free(matrizA);
    free(matrizB);
    free(matrizC);
    MPI_Finalize();
    return 0;
}
