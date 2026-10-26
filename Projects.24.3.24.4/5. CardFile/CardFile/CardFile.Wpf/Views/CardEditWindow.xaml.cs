using CardFile.Wpf.ViewModels;
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

namespace CardFile.Wpf.Views
{
    /// <summary>
    /// Interaction logic for CardEditWindow.xaml
    /// </summary>
    public partial class CardEditWindow : Window
    {
        public CardViewModel ViewModel => (CardViewModel)DataContext;

        public CardEditWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var error = ViewModel.Validate();
            if (error != null)
            {
                MessageBox.Show(error, "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void IsAvailableCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.IsAvailableChecked();
        }

        private void IsAvailableCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ViewModel.IsAvailableUnchecked();
        }
    }
}
