﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();

            TextBox[,] puzzleArray = new TextBox[9, 9];
            SudokuGenerater sudo = new SudokuGenerater();
            int[,] puzzle;

            try
            {
                sudo.FillNextGrid(0, 0);
            }
            catch (SudokuLibrary.SuccessGeneratingException ex)
            {
                puzzle = ex.puzzle;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        puzzleArray[i, j] = new TextBox();
                        MyGrid.Children.Add(puzzleArray[i, j]);
                        Grid.SetRow(puzzleArray[i, j], i);
                        Grid.SetColumn(puzzleArray[i, j], j);
                        puzzleArray[i, j].Text = puzzle[i, j].ToString();
                        puzzleArray[i, j].FontSize = 30;
                        puzzleArray[i, j].FontWeight = FontWeights.Bold;
                        puzzleArray[i, j].HorizontalContentAlignment = HorizontalAlignment.Center;
                        puzzleArray[i, j].VerticalContentAlignment = VerticalAlignment.Center;
                    }
                }
            }
        }
    }
}
