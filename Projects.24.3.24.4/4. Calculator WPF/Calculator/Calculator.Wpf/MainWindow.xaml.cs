using Calculator.Wpf.ViewModels;
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

namespace Calculator.Wpf
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

        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (ViewModel.PressKey(e.Text))
            {
                e.Handled = true;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    ViewModel.EqualCommand.Execute(null);
                    break;

                case Key.Back:
                    ViewModel.BackspaceCommand.Execute(null);
                    break;

                case Key.Escape:
                    ViewModel.ClearCommand.Execute(null);
                    break;

                case Key.Delete:
                    ViewModel.ClearEntryCommand.Execute(null);
                    break;

                default:
                    return;
            }

            e.Handled = true;
        }
    }
}
