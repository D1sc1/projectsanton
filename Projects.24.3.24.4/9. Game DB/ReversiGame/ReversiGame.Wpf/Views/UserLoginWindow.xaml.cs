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
using System.Windows.Shapes;

namespace ReversiGame.Wpf.Views
{
    /// <summary>
    /// Interaction logic for UserLoginWindow.xaml
    /// </summary>
    public partial class UserLoginWindow : Window
    {
        public UserLoginWindowViewModel ViewModel => (UserLoginWindowViewModel)DataContext;

        public UserLoginWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.IsUserNameValid)
            {
                MessageBox.Show("Введите имя игрока", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (!ViewModel.FindUser())
                {
                    var dialogResult = MessageBox.Show("Такого игрока нет в системе. Создать?", "Внимание!",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (dialogResult != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    ViewModel.CreateUser();
                }

                ViewModel.CommitUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось подключиться к базе данных:\n{ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
        }
    }
}
