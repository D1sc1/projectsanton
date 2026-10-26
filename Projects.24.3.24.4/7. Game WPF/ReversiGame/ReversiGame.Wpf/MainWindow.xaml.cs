using ReversiGame.Wpf.ViewModels;
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

namespace ReversiGame.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var cell = (CellViewModel)((FrameworkElement)sender).DataContext;
            ViewModel.MakeMove(cell.Row, cell.Column, OnGameFinished);
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show($"{ViewModel.GetResultText()}\nСыграть еще раз?", "Партия окончена",
                MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }
    }
}
