using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLibrary
{
    public class SudokuGenerater
    {
        // 2 !8 = 29666
        public int[,] grid = new int[9, 9];
        const int LAST = 8;

        public void FillNextGrid(int i, int j)
        {
            var fillList = CommonFunction.GenerateFillList();

            fillList.ForEach(delegate (int item)
            {
                grid[i, j] = item;
                if (CommonFunction.FillSuccess(grid, i, j))
                {
                    if (i == LAST && j == LAST)
                    {
                        PrintResult();
                        return;
                    }
                    else
                    {
                        int nexti = j == LAST ? i + 1 : i;
                        int nextj = j == LAST ? 0 : j + 1;
                        FillNextGrid(nexti, nextj);
                    }
                }
            });
        }

        private void PrintResult()
        {
            throw new SuccessGeneratingException(grid);
        }

    }

    [Serializable]
    public class SuccessGeneratingException : Exception
    {
        public int[,] puzzle;
        public SuccessGeneratingException()
        {
        }

        public SuccessGeneratingException(int [,] grid)
        {
            puzzle = grid;
        }

        public SuccessGeneratingException(string message) : base(message)
        {
        }

        public SuccessGeneratingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SuccessGeneratingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
