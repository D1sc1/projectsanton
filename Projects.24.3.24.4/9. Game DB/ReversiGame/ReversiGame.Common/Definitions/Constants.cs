using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.Definitions
{
    public static class Constants
    {
        public const int RowCount = 8;

        public const int ColumnCount = 8;

        public const int EmptyCellValue = 0;

        public const int BlackValue = 1;

        public const int WhiteValue = 2;

        /// <summary>
        /// Человек играет черными (ходит первым)
        /// </summary>
        public const int HumanPlayer = BlackValue;

        /// <summary>
        /// Компьютер играет белыми
        /// </summary>
        public const int ComputerPlayer = WhiteValue;

        /// <summary>
        /// Последнего хода нет
        /// </summary>
        public const int NoMove = -1;
    }
}
