using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLibrary
{
    public static class CommonFunction
    {
        public static Random rnd = new Random();
        public static int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // randomly generate a list containing 1 to 9
        public static List<int> GenerateFillList()
        {
            int[] MyRandomNumbers = numbers.OrderBy(x => rnd.Next()).ToArray();

            return MyRandomNumbers.ToList();
        }

        public static bool FillSuccess(int[,] puzzle, int i, int j)
        {
            // check column
            for (int ii = i - 1; ii >= 0; ii--)
            {
                if (puzzle[i, j] == puzzle[ii, j])
                    return false;
            }

            // check row
            for (int jj = j - 1; jj >= 0; jj--)
            {
                if (puzzle[i, j] == puzzle[i, jj])
                    return false;
            }
            // check small puzzle
            int basei = i - i % 3;
            int basej = j - j % 3;
            for (int ii = basei; ii < basei + 3 && ii < i; ii++)
            {
                for (int jj = basej; jj < basej + 3; jj++)
                {
                    if (j != jj && puzzle[i, j] == puzzle[ii, jj])
                        return false;
                }
            }

            return true;
        }
    }
}
