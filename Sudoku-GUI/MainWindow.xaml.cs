using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SudokuLibrary;

namespace Sudoku_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox[,] puzzleArray = new TextBox[9, 9];

        public MainWindow()
        {
            InitializeComponent();

            // My code below
            SudokuGenerater sudo = new SudokuGenerater();
            int[,] puzzle;

            try
            {
                sudo.FillNextGrid(0, 0);
            }
            catch (SudokuLibrary.SuccessGeneratingException ex)
            {
                puzzle = ex.puzzle;

                // 挖空
                var rnd = new Random();
                var digNumbers = new int[9];

                // 生成9个随机数 range: 2~9;
                do
                {
                    for (int i = 0; i < 9; i++)
                    {
                        digNumbers[i] = rnd.Next(2, 10);
                    }
                }
                while (digNumbers.Sum() > 60 || digNumbers.Sum() < 30);

                // 挖空，即标志该位为0
                var grids = new int[,] { {0, 0}, {0, 1}, {0, 2},
                                         {1, 0}, {1, 1}, {1, 2},
                                         {2, 0}, {2, 1}, {2, 2} };
                int[] numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

                for (int i = 0; i < 9; i++)
                {
                    int basex = (i / 3) * 3;
                    int basey = (i % 3) * 3;
                    int[] MyRandomNumbers = numbers.OrderBy(x => rnd.Next()).ToArray();

                    for (int j = 0; j < digNumbers[i]; j++)
                    {
                        int digx = grids[MyRandomNumbers[j], 0];
                        int digy = grids[MyRandomNumbers[j], 1];

                        puzzle[basex + digx, basey + digy] = 0;
                    }
                }
                // 显示初始数独
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        puzzleArray[i, j] = new TextBox();
                        MyGrid.Children.Add(puzzleArray[i, j]);
                        Grid.SetRow(puzzleArray[i, j], i);
                        Grid.SetColumn(puzzleArray[i, j], j);
                        puzzleArray[i, j].FontSize = 30;
                        puzzleArray[i, j].HorizontalContentAlignment = HorizontalAlignment.Center;
                        puzzleArray[i, j].VerticalContentAlignment = VerticalAlignment.Center;
                        puzzleArray[i, j].BorderBrush = Brushes.Black;

                        if (puzzle[i, j] != 0)
                        {
                            // 未挖空的数字显示为数字，只读，并加粗
                            puzzleArray[i, j].IsReadOnly = true;
                            puzzleArray[i, j].Text = puzzle[i, j].ToString();
                            puzzleArray[i, j].FontWeight = FontWeights.Bold;
                        }
                        else
                        {
                            puzzleArray[i, j].Text = "";
                        }

                        // 加粗小宫格的边框
                        if (i % 3 == 0)
                        {
                            if (j % 3 == 0)
                                puzzleArray[i, j].BorderThickness = new Thickness(2.5, 2.5, 1.5, 1.5);
                            else if (j % 3 == 2)
                                puzzleArray[i, j].BorderThickness = new Thickness(1.5, 2.5, 2.5, 1.5);
                            else
                                puzzleArray[i, j].BorderThickness = new Thickness(1.5, 2.5, 1.5, 1.5);
                        }
                        else if (i % 3 == 2)
                        {
                            if (j % 3 == 0)
                                puzzleArray[i, j].BorderThickness = new Thickness(2.5, 1.5, 1.5, 2.5);
                            else if (j % 3 == 2)
                                puzzleArray[i, j].BorderThickness = new Thickness(1.5, 1.5, 2.5, 2.5);
                            else
                                puzzleArray[i, j].BorderThickness = new Thickness(1.5, 2.5, 1.5, 2.5);
                        }
                        else
                        {
                            if (j % 3 == 0)
                                puzzleArray[i, j].BorderThickness = new Thickness(2.5, 1.5, 1.5, 1.5);
                            else if (j % 3 == 2)
                                puzzleArray[i, j].BorderThickness = new Thickness(1.5, 1.5, 2.5, 1.5);
                            else
                                puzzleArray[i, j].BorderThickness = new Thickness(1.5, 2.5, 1.5, 1.5);
                        }
                    }
                }
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // 将TextBlock数组转换为，int数组
            var answer = new int[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    try
                    {
                        answer[i, j] = Int32.Parse(puzzleArray[i, j].Text);
                        if (answer[i, j] > 9 || answer[i, j] < 1)
                        {
                            MessageBox.Show(String.Format("Not a recognisable integer (1~9) at ({0}, {1})", i + 1, j + 1));
                            return;
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show(String.Format("Not a recognisable integer at ({0}, {1})", i + 1, j + 1));
                        return;
                    }
                }
            }

            // 检查int [9,9] answer的正确性
            const int SIZE = 9;
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    var real = CommonFunction.FillSuccess(answer, i, j);
                    if (real == false)
                    {
                        MessageBox.Show(String.Format("Not a right answer; check mistake at ({0}, {1})", i + 1, j + 1));
                    }
                }
            }

            MessageBox.Show("Congradulations!");

        }
    }
}
