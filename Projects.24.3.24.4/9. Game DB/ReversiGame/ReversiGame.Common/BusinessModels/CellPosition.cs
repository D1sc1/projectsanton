using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.BusinessModels
{
    /// <summary>
    /// Координаты клетки на доске
    /// </summary>
    public class CellPosition
    {
        public CellPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }

        public int Column { get; }
    }
}
