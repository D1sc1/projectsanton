using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Business.Models
{
    /// <summary>
    /// Состояние партии в Реверси
    /// </summary>
    public class GameModel
    {
        public const int RowCount = 8;

        public const int ColumnCount = 8;

        public const int EmptyCellValue = 0;

        public const int BlackValue = 1;

        public const int WhiteValue = 2;

        private int[,] _cells = new int[RowCount, ColumnCount];

        /// <summary>
        /// Содержимое клетки: EmptyCellValue, BlackValue или WhiteValue
        /// </summary>
        public int this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }

        /// <summary>
        /// Чей ход: BlackValue или WhiteValue
        /// </summary>
        public int CurrentPlayer { get; internal set; } = BlackValue;

        /// <summary>
        /// Количество сделанных ходов (обоими игроками)
        /// </summary>
        public int MoveCount { get; internal set; }

        /// <summary>
        /// Играет ли за белых компьютер
        /// </summary>
        public bool IsVsComputer { get; internal set; } = true;

        /// <summary>
        /// Клетка последнего хода (-1, если ходов еще не было)
        /// </summary>
        public int LastMoveRow { get; internal set; } = -1;

        public int LastMoveColumn { get; internal set; } = -1;

        /// <summary>
        /// Сообщение о последнем событии (например, пропуск хода)
        /// </summary>
        public string Message { get; internal set; }

        public static int Opponent(int player) => player == BlackValue ? WhiteValue : BlackValue;

        public static string PlayerName(int player) => player == BlackValue ? "черные" : "белые";
    }
}
