using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _calculatorModel = new CalculatorModel();

        private readonly CalculatorService _calculatorService = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            DigitCommand = new RelayCommand(p => Execute(() => _calculatorService.PressDigit(_calculatorModel, p as string)));
            DecimalPointCommand = new RelayCommand(p => Execute(() => _calculatorService.PressDecimalPoint(_calculatorModel)));
            OperationCommand = new RelayCommand(p => Execute(() => _calculatorService.PressOperation(_calculatorModel, p as string)));
            UnaryOperationCommand = new RelayCommand(p => Execute(() => _calculatorService.PressUnaryOperation(_calculatorModel, p as string)));
            EqualCommand = new RelayCommand(p => Execute(() => _calculatorService.PressEqual(_calculatorModel)));
            ClearCommand = new RelayCommand(p => Execute(() => _calculatorService.PressClear(_calculatorModel)));
            ClearEntryCommand = new RelayCommand(p => Execute(() => _calculatorService.PressClearEntry(_calculatorModel)));
            BackspaceCommand = new RelayCommand(p => Execute(() => _calculatorService.PressBackspace(_calculatorModel)));
            MoveXToYCommand = new RelayCommand(p => Execute(() => _calculatorService.PressMoveXToY(_calculatorModel)));
            MemoryClearCommand = new RelayCommand(p => Execute(() => _calculatorService.PressMemoryClear(_calculatorModel)), p => _calculatorModel.HasMemory);
            MemoryRecallCommand = new RelayCommand(p => Execute(() => _calculatorService.PressMemoryRecall(_calculatorModel)), p => _calculatorModel.HasMemory);
            MemoryAddCommand = new RelayCommand(p => Execute(() => _calculatorService.PressMemoryAdd(_calculatorModel)));
            MemorySubtractCommand = new RelayCommand(p => Execute(() => _calculatorService.PressMemorySubtract(_calculatorModel)));
        }

        public string DisplayValue => _calculatorModel.DisplayText;

        public string ExpressionText => _calculatorModel.ExpressionText;

        public string MemoryText => _calculatorModel.MemoryText;

        public bool IsError => _calculatorModel.IsError;

        public ICommand DigitCommand { get; }

        public ICommand DecimalPointCommand { get; }

        public ICommand OperationCommand { get; }

        public ICommand UnaryOperationCommand { get; }

        public ICommand EqualCommand { get; }

        public ICommand ClearCommand { get; }

        public ICommand ClearEntryCommand { get; }

        public ICommand BackspaceCommand { get; }

        public ICommand MoveXToYCommand { get; }

        public ICommand MemoryClearCommand { get; }

        public ICommand MemoryRecallCommand { get; }

        public ICommand MemoryAddCommand { get; }

        public ICommand MemorySubtractCommand { get; }

        /// <summary>
        /// Ввод символа с клавиатуры. Возвращает true, если символ обработан
        /// </summary>
        public bool PressKey(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length != 1)
            {
                return false;
            }

            var key = text[0];

            if (char.IsDigit(key))
            {
                DigitCommand.Execute(text);
            }
            else if (key == '.' || key == ',')
            {
                DecimalPointCommand.Execute(null);
            }
            else if ("+-*/^".IndexOf(key) >= 0)
            {
                OperationCommand.Execute(text);
            }
            else if (key == '%')
            {
                UnaryOperationCommand.Execute("%");
            }
            else if (key == '=')
            {
                EqualCommand.Execute(null);
            }
            else
            {
                return false;
            }

            return true;
        }

        private void Execute(Action action)
        {
            action();
            OnPropertyChanged(nameof(DisplayValue));
            OnPropertyChanged(nameof(ExpressionText));
            OnPropertyChanged(nameof(MemoryText));
            OnPropertyChanged(nameof(IsError));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
