using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorModel
    {
        public const string ErrorText = "Ошибка";

        /// <summary>
        /// Регистр X - число на индикаторе
        /// </summary>
        public double RegisterX { get; internal set; }

        /// <summary>
        /// Регистр Y - первый операнд двуместной операции
        /// </summary>
        internal double RegisterY { get; set; }

        /// <summary>
        /// Код отложенной двуместной операции (+, -, *, /, ^)
        /// </summary>
        internal string OperationCode { get; set; }

        /// <summary>
        /// Идет набор числа (следующая цифра дописывается к числу)
        /// </summary>
        internal bool IsLastDigitPressed { get; set; }

        /// <summary>
        /// В регистре X находится новый операнд, введенный после выбора операции
        /// </summary>
        internal bool HasOperand { get; set; }

        /// <summary>
        /// Набираемое число в виде строки (с точкой в качестве разделителя)
        /// </summary>
        internal string InputText { get; set; } = "0";

        /// <summary>
        /// Произошла ошибка (деление на ноль, корень из отрицательного числа и т.п.)
        /// </summary>
        public bool IsError { get; internal set; }

        /// <summary>
        /// Ячейка памяти
        /// </summary>
        public double Memory { get; internal set; }

        /// <summary>
        /// В памяти есть число
        /// </summary>
        public bool HasMemory { get; internal set; }

        /// <summary>
        /// Текст на индикаторе
        /// </summary>
        public string DisplayText
        {
            get
            {
                if (IsError)
                {
                    return ErrorText;
                }

                if (IsLastDigitPressed)
                {
                    return InputText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                }

                return FormatNumber(RegisterX);
            }
        }

        /// <summary>
        /// Строка с отложенной операцией, например "12 +"
        /// </summary>
        public string ExpressionText =>
            !IsError && OperationCode != null ? $"{FormatNumber(RegisterY)} {OperationCode}" : string.Empty;

        /// <summary>
        /// Индикатор памяти
        /// </summary>
        public string MemoryText => HasMemory ? $"M = {FormatNumber(Memory)}" : string.Empty;

        internal static string FormatNumber(double value)
        {
            // 15 значащих цифр убирают "хвосты" вида 0.30000000000000004
            return value.ToString("G15", CultureInfo.CurrentCulture);
        }
    }
}
