using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Tamaño del arreglo
        int[] arreglo = { 10, 5, 8, 2, 7, 1, 3, 6, 4, 9 };
        int elementoBuscado = 20;

        // Crear copia del arreglo para cada algoritmo
        int[] arregloBusquedaSecuencial = arreglo.Clone() as int[];
        int[] arregloBusquedaBinaria = arreglo.Clone() as int[];
        int[] arregloBurbuja = arreglo.Clone() as int[];
        int[] arregloQuickSort = arreglo.Clone() as int[];
        int[] arregloInsercion = arreglo.Clone() as int[];

        // Medir el tiempo de ejecución de cada algoritmo
        Stopwatch stopwatch = new Stopwatch();

        Parallel.Invoke(
            () =>
            {
                stopwatch.Start();
                bool encontrado = BusquedaSecuencial(arregloBusquedaSecuencial, elementoBuscado);
                stopwatch.Stop();
                Console.WriteLine($"Búsqueda Secuencial: {stopwatch.ElapsedMilliseconds} ms");
            },
            () =>
            {
                stopwatch.Restart();
                Array.Sort(arregloBusquedaBinaria);
                int indice = BusquedaBinaria(arregloBusquedaBinaria, elementoBuscado);
                stopwatch.Stop();
                Console.WriteLine($"Búsqueda Binaria: {stopwatch.ElapsedMilliseconds} ms");
            },
            () =>
            {
                stopwatch.Restart();
                Burbuja(arregloBurbuja);
                stopwatch.Stop();
                Console.WriteLine($"Ordenamiento de Burbuja: {stopwatch.ElapsedMilliseconds} ms");
            },
            () =>
            {
                stopwatch.Restart();
                QuickSort(arregloQuickSort, 0, arregloQuickSort.Length - 1);
                stopwatch.Stop();
                Console.WriteLine($"Quick Sort: {stopwatch.ElapsedMilliseconds} ms");
            },
            () =>
            {
                stopwatch.Restart();
                Insercion(arregloInsercion);
                stopwatch.Stop();
                Console.WriteLine($"Método de Inserción: {stopwatch.ElapsedMilliseconds} ms");
            }
        );

        Console.WriteLine("Proceso completo.");
    }

    static bool BusquedaSecuencial(int[] arreglo, int elemento)
    {
        foreach (var num in arreglo)
        {
            if (num == elemento)
                return true;
        }
        return false;
    }

    static int BusquedaBinaria(int[] arreglo, int elemento)
    {
        int inicio = 0;
        int fin = arreglo.Length - 1;

        while (inicio <= fin)
        {
            int medio = (inicio + fin) / 2;

            if (arreglo[medio] == elemento)
                return medio;
            else if (arreglo[medio] < elemento)
                inicio = medio + 1;
            else
                fin = medio - 1;
        }

        return -1;
    }

    static void Burbuja(int[] arreglo)
    {
        for (int i = 0; i < arreglo.Length - 1; i++)
        {
            for (int j = 0; j < arreglo.Length - i - 1; j++)
            {
                if (arreglo[j] > arreglo[j + 1])
                {
                    int temp = arreglo[j];
                    arreglo[j] = arreglo[j + 1];
                    arreglo[j + 1] = temp;
                }
            }
        }
    }

    static void QuickSort(int[] arreglo, int inicio, int fin)
    {
        if (inicio < fin)
        {
            int particion = Particionar(arreglo, inicio, fin);
            QuickSort(arreglo, inicio, particion - 1);
            QuickSort(arreglo, particion + 1, fin);
        }
    }

    static int Particionar(int[] arreglo, int inicio, int fin)
    {
        int pivote = arreglo[fin];
        int i = inicio - 1;

        for (int j = inicio; j < fin; j++)
        {
            if (arreglo[j] < pivote)
            {
                i++;
                int temp = arreglo[i];
                arreglo[i] = arreglo[j];
                arreglo[j] = temp;
            }
        }

        int temp2 = arreglo[i + 1];
        arreglo[i + 1] = arreglo[fin];
        arreglo[fin] = temp2;

        return i + 1;
    }

    static void Insercion(int[] arreglo)
    {
        for (int i = 1; i < arreglo.Length; i++)
        {
            int clave = arreglo[i];
            int j = i - 1;

            while (j >= 0 && arreglo[j] > clave)
            {
                arreglo[j + 1] = arreglo[j];
                j = j - 1;
            }

            arreglo[j + 1] = clave;
        }
    }
}

