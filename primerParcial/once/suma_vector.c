#include <stdio.h>
#include <stdlib.h>
#include <mpi.h>

#define N 10

int main(int argc, char *argv[]) {
    int rank, size;
    int vector1[N], vector2[N], result[N];

    MPI_Init(&argc, &argv);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    if (rank == 0) {
        for (int i = 0; i < N; i++) {
            vector1[i] = i + 1;   // 1, 2, ..., N
            vector2[i] = (i + 1) * 2; //  2, 4, ..., 2*N
        }

        printf("Vector 1: ");
        for (int i = 0; i < N; i++) {
            printf("%d ", vector1[i]);
        }
        printf("\n");

        printf("Vector 2: ");
        for (int i = 0; i < N; i++) {
            printf("%d ", vector2[i]);
        }
        printf("\n");
    }

    MPI_Bcast(vector1, N, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_Bcast(vector2, N, MPI_INT, 0, MPI_COMM_WORLD);

    for (int i = rank; i < N; i += size) {
        result[i] = vector1[i] + vector2[i];
    }

    MPI_Gather(result, N / size, MPI_INT, result, N / size, MPI_INT, 0, MPI_COMM_WORLD);

    if (rank == 0) {
        printf("Suma de vectores:\n");
        for (int i = 0; i < N; i++) {
            printf("%d ", result[i]);
        }
        printf("\n");
    }

    MPI_Finalize();
    return 0;
}
