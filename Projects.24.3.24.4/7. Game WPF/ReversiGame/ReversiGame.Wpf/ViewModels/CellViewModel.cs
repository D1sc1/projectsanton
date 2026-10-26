using ReversiGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public int Value { get; set; }

        public bool IsBlack => Value == GameModel.BlackValue;

        public bool IsWhite => Value == GameModel.WhiteValue;

        public bool IsEmpty => Value == GameModel.EmptyCellValue;

        /// <summary>
        /// Сюда можно сходить (показывается подсказка)
        /// </summary>
        public bool IsHintVisible { get; set; }

        /// <summary>
        /// Клетка последнего хода
        /// </summary>
        public bool IsLastMove { get; set; }
    }
}
