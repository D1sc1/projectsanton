using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private CalculatorModel _calculatorModel = new CalculatorModel();

        private CalculatorService _calculatorService = new CalculatorService();

        public CalculatorForm()
        {
            InitializeComponent();
            DisplayResult();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var digitString = ((Button)sender).Text;
            _calculatorService.PressDigit(_calculatorModel, digitString);
            DisplayResult();
        }

        private void DecimalPointButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressDecimalPoint(_calculatorModel);
            DisplayResult();
        }

        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressBackspace(_calculatorModel);
            DisplayResult();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressClear(_calculatorModel);
            DisplayResult();
        }

        private void ClearEntryButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressClearEntry(_calculatorModel);
            DisplayResult();
        }

        private void MoveXToYButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressMoveXToY(_calculatorModel);
            DisplayResult();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var operationCode = ((Button)sender).Text;
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            DisplayResult();
        }

        private void UnaryOperationButton_Click(object sender, EventArgs e)
        {
            var operationCode = ((Button)sender).Text;
            _calculatorService.PressUnaryOperation(_calculatorModel, operationCode);
            DisplayResult();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressEqual(_calculatorModel);
            DisplayResult();
        }

        private void MemoryClearButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressMemoryClear(_calculatorModel);
            DisplayResult();
        }

        private void MemoryRecallButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressMemoryRecall(_calculatorModel);
            DisplayResult();
        }

        private void MemoryAddButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressMemoryAdd(_calculatorModel);
            DisplayResult();
        }

        private void MemorySubtractButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressMemorySubtract(_calculatorModel);
            DisplayResult();
        }

        /// <summary>
        /// Ввод с клавиатуры: цифры, точка/запятая, + - * / ^ и %
        /// </summary>
        private void CalculatorForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            var key = e.KeyChar;

            if (char.IsDigit(key))
            {
                _calculatorService.PressDigit(_calculatorModel, key.ToString());
            }
            else if (key == '.' || key == ',')
            {
                _calculatorService.PressDecimalPoint(_calculatorModel);
            }
            else if ("+-*/^".IndexOf(key) >= 0)
            {
                _calculatorService.PressOperation(_calculatorModel, key.ToString());
            }
            else if (key == '%')
            {
                _calculatorService.PressUnaryOperation(_calculatorModel, "%");
            }
            else if (key == '=')
            {
                _calculatorService.PressEqual(_calculatorModel);
            }
            else
            {
                return;
            }

            e.Handled = true;
            DisplayResult();
        }

        /// <summary>
        /// Служебные клавиши перехватываются до того, как их обработает кнопка с фокусом
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    _calculatorService.PressEqual(_calculatorModel);
                    break;

                case Keys.Back:
                    _calculatorService.PressBackspace(_calculatorModel);
                    break;

                case Keys.Escape:
                    _calculatorService.PressClear(_calculatorModel);
                    break;

                case Keys.Delete:
                    _calculatorService.PressClearEntry(_calculatorModel);
                    break;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            DisplayResult();
            return true;
        }

        private void DisplayResult()
        {
            DisplayLabel.Text = _calculatorModel.DisplayText;
            ExpressionLabel.Text = _calculatorModel.ExpressionText;
            MemoryLabel.Text = _calculatorModel.MemoryText;

            MemoryClearButton.Enabled = _calculatorModel.HasMemory;
            MemoryRecallButton.Enabled = _calculatorModel.HasMemory;
        }
    }
}
