using ReversiGame.Wpf.ViewModels;
using ReversiGame.Wpf.Views;
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
            ExecuteSafely(() => ViewModel.MakeMove(cell.Row, cell.Column, OnGameFinished));
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Начать новую партию? Текущая партия будет удалена.", "Новая партия",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            ExecuteSafely(() => ViewModel.StartNewGame());
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show($"{ViewModel.GetResultText()}\nСыграть еще раз?", "Партия окончена",
                MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new UserLoginWindow { Owner = this, };
            dialog.ViewModel.MainViewModel = ViewModel;
            if (dialog.ShowDialog() != true)
            {
                Close();
            }
        }

        /// <summary>
        /// Ошибки связи с сервером показываются пользователю, а не завершают программу
        /// </summary>
        private void ExecuteSafely(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
