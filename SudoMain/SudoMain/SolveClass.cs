using System;

namespace SudoMain
{
    class SolveClass
    {
        public SolveClass()
        {
        }

        public int[,] SolveSudoku(int[,] b)
        {
            int n = 9;
            bool[,] rCheck = new bool[n, n + 1], cCheck = new bool[n, n + 1], gCheck = new bool[n, n + 1];
            int[,] result = new int[n, n];

            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (b[r, c] != 0)
                    {
                        var digit = b[r, c];
                        rCheck[r, digit] = cCheck[c, digit] = gCheck[GridID(r, c), digit] = true;
                        result[r, c] = digit;
                    }

            Fill(0, 0);

            bool Fill(int r, int c)
            {
                if (c == n)
                {
                    r = r + 1;
                    c = 0;
                }
                if (r == n) return true;
                if (b[r, c] != 0)
                {
                    return Fill(r, c + 1);
                }

                for (int digit = 1; digit <= 9; digit++)
                {
                    if (!(rCheck[r, digit] || cCheck[c, digit] || gCheck[GridID(r, c), digit]))
                    {
                        rCheck[r, digit] = cCheck[c, digit] = gCheck[GridID(r, c), digit] = true;
                        b[r, c] = digit;
                        result[r, c] = digit;
                        if (Fill(r, c + 1)) return true;
                        rCheck[r, digit] = cCheck[c, digit] = gCheck[GridID(r, c), digit] = false;
                    }
                }

                b[r, c] = 0;
                result[r, c] = 0;
                return false;
            }

            if (Fill(0, 0))
                return result;
            else
                throw new Exception("No solution found.");
        }

        static int GridID(int r, int c) => 3 * (r / 3) + (c / 3);
    }
}
