using System;

class Program
{
    static void Main()
    {
        int[,] A = { { 0, 2, 0, 4 }, { 0, 0, 0, 0 }, { 0, 10, 11, 12 }, { 0, 14, 15, 16 } };
        bool[] zeroRows, zeroCols;
        int[,] B = CompressMatrix(A, out zeroRows, out zeroCols);
        int n = B.GetLength(0);
        int m = B.GetLength(1);

        Console.WriteLine("Original Matrix:");
        PrintMatrix(A);
        Console.WriteLine();
        Console.WriteLine("Compressed Matrix:");
        PrintMatrix(B);
        Console.WriteLine("Zero Rows:");
        PrintArray(zeroRows);
        Console.WriteLine("Zero Columns:");
        PrintArray(zeroCols);

        Console.ReadKey();
    }

    static int[,] CompressMatrix(int[,] A, out bool[] zeroRows, out bool[] zeroCols)
    {
        int n = A.GetLength(0);
        int m = A.GetLength(1);
        zeroRows = new bool[n];
        zeroCols = new bool[m];
        int[] rowOffset = new int[n];
        int[] colOffset = new int[m];
        int newRow = 0;
        int newCol = 0;

        // Identify zero rows and columns
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (A[i, j] != 0)
                {
                    zeroRows[i] = false;
                    zeroCols[j] = false;
                }
                else
                {
                    zeroRows[i] = true;
                    zeroCols[j] = true;
                }
            }
        }

        // Compute row and column offsets
        for (int i = 0; i < n; i++)
        {
            if (!zeroRows[i])
            {
                rowOffset[i] = newRow;
                newRow++;
            }
        }
        for (int j = 0; j < m; j++)
        {
            if (!zeroCols[j])
            {
                colOffset[j] = newCol;
                newCol++;
            }
        }

        // Create new compressed matrix
        int[,] B = new int[newRow, newCol];
        for (int i = 0; i < n; i++)
        {
            if (!zeroRows[i])
            {
                for (int j = 0; j < m; j++)
                {
                    if (!zeroCols[j])
                    {
                        B[rowOffset[i], colOffset[j]] = A[i, j];
                    }
                }
            }
        }
        return B;
    }

    static void PrintMatrix(int[,] A)
    {
        int n = A.GetLength(0);
        int m = A.GetLength(1);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write("{0,4}", A[i, j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static void PrintArray(bool[] A)
    {
        int n = A.Length;
        for (int i = 0; i < n; i++)
        {
            Console.Write("{0,5}", A[i]);
        }
        Console.WriteLine();
        Console.WriteLine();
    }
}