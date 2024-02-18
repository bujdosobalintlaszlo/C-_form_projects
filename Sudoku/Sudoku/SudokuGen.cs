using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuGen
    {
        int[,] mat;
        int N;
        int SRN;
        int K;

        public SudokuGen(int N, int K)
        {
            this.N = N;
            this.K = K;

            double SRNd = Math.Sqrt(N);
            SRN = (int)SRNd;

            mat = new int[N, N];
        }

        public void fillValues()
        {
            fillDiagonal();
            fillRemaining(0, SRN);
            removeKDigits();
            WriteIntoFile();
        }

        void fillDiagonal()
        {

            for (int i = 0; i < N; i = i + SRN)
                fillBox(i, i);
        }

        bool unUsedInBox(int rowStart, int colStart, int num)
        {
            for (int i = 0; i < SRN; i++)
                for (int j = 0; j < SRN; j++)
                    if (mat[rowStart + i, colStart + j] == num)
                        return false;

            return true;
        }

        void fillBox(int row, int col)
        {
            int num;
            for (int i = 0; i < SRN; i++)
            {
                for (int j = 0; j < SRN; j++)
                {
                    do
                    {
                        num = randomGenerator(N);
                    }
                    while (!unUsedInBox(row, col, num));

                    mat[row + i, col + j] = num;
                }
            }
        }

        int randomGenerator(int num)
        {
            Random rand = new Random();
            return (int)Math.Floor((double)(rand.NextDouble() * num + 1));
        }

        bool CheckIfSafe(int i, int j, int num)
        {
            return (unUsedInRow(i, num) &&
                    unUsedInCol(j, num) &&
                    unUsedInBox(i - i % SRN, j - j % SRN, num));
        }

        bool unUsedInRow(int i, int num)
        {
            for (int j = 0; j < N; j++)
                if (mat[i, j] == num)
                    return false;
            return true;
        }

        bool unUsedInCol(int j, int num)
        {
            for (int i = 0; i < N; i++)
                if (mat[i, j] == num)
                    return false;
            return true;
        }

        bool fillRemaining(int i, int j)
        {
            if (j >= N && i < N - 1)
            {
                i = i + 1;
                j = 0;
            }
            if (i >= N && j >= N)
                return true;

            if (i < SRN)
            {
                if (j < SRN)
                    j = SRN;
            }
            else if (i < N - SRN)
            {
                if (j == (int)(i / SRN) * SRN)
                    j = j + SRN;
            }
            else
            {
                if (j == N - SRN)
                {
                    i = i + 1;
                    j = 0;
                    if (i >= N)
                        return true;
                }
            }

            for (int num = 1; num <= N; num++)
            {
                if (CheckIfSafe(i, j, num))
                {
                    mat[i, j] = num;
                    if (fillRemaining(i, j + 1))
                        return true;

                    mat[i, j] = 0;
                }
            }
            return false;
        }

        public void removeKDigits()
        {
            int count = K;
            while (count != 0)
            {
                int cellId = randomGenerator(N * N) - 1;
                int i = (cellId / N);
                int j = cellId % N;
                if (j != 0)
                    j = j - 1;

                if (mat[i, j] != 0)
                {
                    count--;
                    mat[i, j] = 0;
                }
            }
        }
        void WriteIntoFile()
        {
            StreamWriter w = new StreamWriter("generated.txt");

            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (i == mat.GetLength(0) - 1 && j == mat.GetLength(1) - 1)
                    {
                        w.Write($"{mat[i, j]};");
                    }
                    else
                    {
                        w.Write($"{mat[i, j]},");
                    }
                }
            }

            w.Close();
        }
    }
}
