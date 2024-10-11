#include <stdio.h>
#include <mpi.h>
#include <string.h>

int main(int argc, char **argv)
{
    int rank, size;
    const int VECTOR_SIZE = 10;
    char data[VECTOR_SIZE][20];
    MPI_Init(&argc, &argv);

    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    if (rank == 0)
    {
        for (int i = 0; i < VECTOR_SIZE; i++)
        {
            snprintf(data[i], sizeof(data[i]), "String %d", i);
        }
        for (int i = 0; i < VECTOR_SIZE; i++)
        {
            printf("  %s\n", data[i]);
        }
    }

    MPI_Bcast(data, VECTOR_SIZE * 20, MPI_CHAR, 0, MPI_COMM_WORLD);
    if (rank )
    {
        printf("Proceso %d impar:\n", rank);
        for (int i = 1; i < VECTOR_SIZE; i += 2)
        {
            printf("  Posición impar %d: %s\n", i, data[i]);
        }
    }
    else if (rank == 2)
    {
        printf("Proceso %d par:\n", rank);
        for (int i = 0; i < VECTOR_SIZE; i += 2)
        {
            printf(" Posicion par %d: %s\n", i, data[i]);
        }
    }

    MPI_Finalize();
    return 0;
}