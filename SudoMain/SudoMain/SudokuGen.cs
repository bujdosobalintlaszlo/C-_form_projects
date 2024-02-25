using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoMain
{
    using System;
    using System.IO;

    namespace Sudoku
    {
        class SudokuGen
        {
            private int[,] mat;
            private int N;
            private int SRN;
            private int K;
            private static readonly Random rand = new Random();

            public SudokuGen(int N, int K)
            {
                this.N = N;
                this.K = K;

                double SRNd = Math.Sqrt(N);
                SRN = (int)SRNd;

                mat = new int[N, N];
            }

            public int[,] GenerateSudoku()
            {
                FillValues();
                RemoveKDigits();
                return mat;
            }

            private void FillValues()
            {
                FillDiagonal();
                FillRemaining(0, SRN);
            }

            private void FillDiagonal()
            {
                for (int i = 0; i < N; i += SRN)
                    FillBox(i, i);
            }

            private bool UnUsedInBox(int rowStart, int colStart, int num)
            {
                for (int i = 0; i < SRN; i++)
                    for (int j = 0; j < SRN; j++)
                        if (mat[rowStart + i, colStart + j] == num)
                            return false;

                return true;
            }

            private void FillBox(int row, int col)
            {
                int num;
                for (int i = 0; i < SRN; i++)
                {
                    for (int j = 0; j < SRN; j++)
                    {
                        do
                        {
                            num = RandomGenerator(N);
                        }
                        while (!UnUsedInBox(row, col, num));

                        mat[row + i, col + j] = num;
                    }
                }
            }

            private int RandomGenerator(int num)
            {
                return (int)Math.Floor((double)(rand.NextDouble() * num + 1));
            }

            private bool CheckIfSafe(int i, int j, int num)
            {
                return (UnUsedInRow(i, num) &&
                        UnUsedInCol(j, num) &&
                        UnUsedInBox(i - i % SRN, j - j % SRN, num));
            }

            private bool UnUsedInRow(int i, int num)
            {
                for (int j = 0; j < N; j++)
                    if (mat[i, j] == num)
                        return false;
                return true;
            }

            private bool UnUsedInCol(int j, int num)
            {
                for (int i = 0; i < N; i++)
                    if (mat[i, j] == num)
                        return false;
                return true;
            }

            private bool FillRemaining(int i, int j)
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
                        if (FillRemaining(i, j + 1))
                            return true;

                        mat[i, j] = 0;
                    }
                }
                return false;
            }

            private void RemoveKDigits()
            {
                int count = K;
                while (count != 0)
                {
                    int cellId = RandomGenerator(N * N) - 1;
                    int i = cellId / N;
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
                
            }
        }
    }


