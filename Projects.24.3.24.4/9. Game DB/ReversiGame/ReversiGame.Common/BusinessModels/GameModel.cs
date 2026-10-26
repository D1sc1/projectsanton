using ReversiGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.BusinessModels
{
    public class GameModel
    {
        private int[,] _cells = new int[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }

        public int UserId { get; set; }

        /// <summary>
        /// Содержимое клетки: Constants.EmptyCellValue, BlackValue или WhiteValue
        /// </summary>
        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int MoveCount { get; set; }

        /// <summary>
        /// Чей ход
        /// </summary>
        public int CurrentPlayer { get; set; } = Constants.BlackValue;

        public int LastMoveRow { get; set; } = Constants.NoMove;

        public int LastMoveColumn { get; set; } = Constants.NoMove;

        /// <summary>
        /// Сообщение о событиях последнего хода (пропуск хода и т.п.). В БД не хранится
        /// </summary>
        public string Message { get; set; }
    }
}
