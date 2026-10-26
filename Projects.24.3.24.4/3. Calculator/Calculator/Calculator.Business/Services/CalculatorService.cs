using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        /// <summary>
        /// Максимальное количество символов в набираемом числе
        /// </summary>
        public const int MaxInputLength = 16;

        public void PressDigit(CalculatorModel calculatorModel, string digitString)
        {
            if (!int.TryParse(digitString, out var digit) || digit < 0 || digit > 9)
            {
                return;
            }

            if (calculatorModel.IsError)
            {
                PressClear(calculatorModel);
            }

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.InputText = "0";
                calculatorModel.IsLastDigitPressed = true;
            }

            if (calculatorModel.InputText.Length >= MaxInputLength)
            {
                return;
            }

            if (calculatorModel.InputText == "0")
            {
                calculatorModel.InputText = digitString;
            }
            else if (calculatorModel.InputText == "-0")
            {
                calculatorModel.InputText = "-" + digitString;
            }
            else
            {
                calculatorModel.InputText += digitString;
            }

            UpdateRegisterXFromInput(calculatorModel);
        }

        public void PressDecimalPoint(CalculatorModel calculatorModel)
        {
            if (calculatorModel.IsError)
            {
                PressClear(calculatorModel);
            }

            if (!calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.InputText = "0";
                calculatorModel.IsLastDigitPressed = true;
            }

            if (calculatorModel.InputText.Contains(".") || calculatorModel.InputText.Length >= MaxInputLength)
            {
                return;
            }

            calculatorModel.InputText += ".";
            UpdateRegisterXFromInput(calculatorModel);
        }

        public void PressBackspace(CalculatorModel calculatorModel)
        {
            if (calculatorModel.IsError || !calculatorModel.IsLastDigitPressed)
            {
                return;
            }

            var text = calculatorModel.InputText.Substring(0, calculatorModel.InputText.Length - 1);
            if (text == string.Empty || text == "-")
            {
                text = "0";
            }

            calculatorModel.InputText = text;
            UpdateRegisterXFromInput(calculatorModel);
        }

        /// <summary>
        /// C - полный сброс калькулятора (память не очищается)
        /// </summary>
        public void PressClear(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = 0;
            calculatorModel.RegisterY = 0;
            calculatorModel.OperationCode = null;
            calculatorModel.InputText = "0";
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.HasOperand = false;
            calculatorModel.IsError = false;
        }

        /// <summary>
        /// CE - сброс только набираемого числа
        /// </summary>
        public void PressClearEntry(CalculatorModel calculatorModel)
        {
            if (calculatorModel.IsError)
            {
                PressClear(calculatorModel);
                return;
            }

            calculatorModel.RegisterX = 0;
            calculatorModel.InputText = "0";
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.HasOperand = true;
        }

        public void PressMoveXToY(CalculatorModel calculatorModel)
        {
            if (calculatorModel.IsError)
            {
                return;
            }

            calculatorModel.RegisterY = calculatorModel.RegisterX;
            calculatorModel.IsLastDigitPressed = false;
        }

        /// <summary>
        /// Двуместная операция: +, -, *, /, ^ (возведение в степень)
        /// </summary>
        public void PressOperation(CalculatorModel calculatorModel, string operationCode)
        {
            if (calculatorModel.IsError)
            {
                return;
            }

            if (operationCode == "=")
            {
                PressEqual(calculatorModel);
                return;
            }

            // Повторное нажатие операции без ввода второго операнда только меняет операцию
            if (calculatorModel.OperationCode != null && !calculatorModel.HasOperand)
            {
                calculatorModel.OperationCode = operationCode;
                return;
            }

            CompleteOperation(calculatorModel);
            if (calculatorModel.IsError)
            {
                return;
            }

            PressMoveXToY(calculatorModel);
            calculatorModel.OperationCode = operationCode;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.HasOperand = false;
        }

        public void PressEqual(CalculatorModel calculatorModel)
        {
            if (calculatorModel.IsError)
            {
                return;
            }

            CompleteOperation(calculatorModel);
            calculatorModel.OperationCode = null;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.HasOperand = true;
        }

        /// <summary>
        /// Одноместная операция над регистром X: √, x², 1/x, ±, %
        /// </summary>
        public void PressUnaryOperation(CalculatorModel calculatorModel, string operationCode)
        {
            if (calculatorModel.IsError)
            {
                return;
            }

            // Смена знака во время набора меняет набираемую строку
            if (operationCode == "±" && calculatorModel.IsLastDigitPressed)
            {
                calculatorModel.InputText = calculatorModel.InputText.StartsWith("-") ?
                    calculatorModel.InputText.Substring(1) :
                    "-" + calculatorModel.InputText;
                UpdateRegisterXFromInput(calculatorModel);
                return;
            }

            var x = calculatorModel.RegisterX;
            double result;

            switch (operationCode)
            {
                case "√":
                    if (x < 0)
                    {
                        SetError(calculatorModel);
                        return;
                    }

                    result = Math.Sqrt(x);
                    break;

                case "x²":
                    result = x * x;
                    break;

                case "1/x":
                    if (x == 0)
                    {
                        SetError(calculatorModel);
                        return;
                    }

                    result = 1 / x;
                    break;

                case "±":
                    result = -x;
                    break;

                case "%":
                    // a + b% = a + a*b/100; для * и / процент - это просто b/100
                    result = calculatorModel.OperationCode == "+" || calculatorModel.OperationCode == "-" ?
                        calculatorModel.RegisterY * x / 100 :
                        x / 100;
                    break;

                default:
                    return;
            }

            SetResult(calculatorModel, result);
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.HasOperand = true;
        }

        public void PressMemoryClear(CalculatorModel calculatorModel)
        {
            calculatorModel.Memory = 0;
            calculatorModel.HasMemory = false;
        }

        public void PressMemoryRecall(CalculatorModel calculatorModel)
        {
            if (!calculatorModel.HasMemory)
            {
                return;
            }

            if (calculatorModel.IsError)
            {
                PressClear(calculatorModel);
            }

            calculatorModel.RegisterX = calculatorModel.Memory;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.HasOperand = true;
        }

        public void PressMemoryAdd(CalculatorModel calculatorModel)
        {
            ChangeMemory(calculatorModel, calculatorModel.RegisterX);
        }

        public void PressMemorySubtract(CalculatorModel calculatorModel)
        {
            ChangeMemory(calculatorModel, -calculatorModel.RegisterX);
        }

        private void ChangeMemory(CalculatorModel calculatorModel, double delta)
        {
            if (calculatorModel.IsError)
            {
                return;
            }

            calculatorModel.Memory += delta;
            calculatorModel.HasMemory = true;
            calculatorModel.IsLastDigitPressed = false;
        }

        private void CompleteOperation(CalculatorModel calculatorModel)
        {
            var x = calculatorModel.RegisterX;
            var y = calculatorModel.RegisterY;

            switch (calculatorModel.OperationCode)
            {
                case "+":
                    SetResult(calculatorModel, y + x);
                    break;

                case "-":
                    SetResult(calculatorModel, y - x);
                    break;

                case "*":
                    SetResult(calculatorModel, y * x);
                    break;

                case "/":
                    if (x == 0)
                    {
                        SetError(calculatorModel);
                        return;
                    }

                    SetResult(calculatorModel, y / x);
                    break;

                case "^":
                    SetResult(calculatorModel, Math.Pow(y, x));
                    break;
            }
        }

        private void SetResult(CalculatorModel calculatorModel, double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                SetError(calculatorModel);
                return;
            }

            calculatorModel.RegisterX = value;
        }

        private void SetError(CalculatorModel calculatorModel)
        {
            calculatorModel.IsError = true;
            calculatorModel.OperationCode = null;
            calculatorModel.IsLastDigitPressed = false;
            calculatorModel.HasOperand = false;
        }

        private void UpdateRegisterXFromInput(CalculatorModel calculatorModel)
        {
            calculatorModel.RegisterX = double.Parse(calculatorModel.InputText.TrimEnd('.') == "-" ? "0" : calculatorModel.InputText.TrimEnd('.'),
                NumberStyles.Float, CultureInfo.InvariantCulture);
            calculatorModel.HasOperand = true;
        }
    }
}
