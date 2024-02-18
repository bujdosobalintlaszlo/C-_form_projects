using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuSolver
    {
        public SudokuSolver()
        {
        }

        public void SolveSudoku(char[][] b)
        {
            int n = 9;
            bool[,] rCheck = new bool[n, n + 1], cCheck = new bool[n, n + 1], gCheck = new bool[n, n + 1];
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (b[r][c] != '.')
                    {
                        var digit = b[r][c] - '0';
                        rCheck[r, digit] = cCheck[c, digit] = gCheck[GridID(r, c), digit] = true;
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
                if (b[r][c] != '.')
                    return Fill(r, c + 1);

                for (int digit = 1; digit < 10; digit++)
                    if (!(rCheck[r, digit] || cCheck[c, digit] || gCheck[GridID(r, c), digit]))
                    {
                        rCheck[r, digit] = cCheck[c, digit] = gCheck[GridID(r, c), digit] = true;
                        b[r][c] = (char)(digit + '0');
                        if (Fill(r, c + 1)) return true;
                        rCheck[r, digit] = cCheck[c, digit] = gCheck[GridID(r, c), digit] = false;
                    }
                b[r][c] = '.';
                return false;
            }
        }
        static int GridID(int r, int c) => 3 * (r / 3) + (c / 3);
    }
}
