using ReversiGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int[,] Cells { get; } = new int[Constants.RowCount, Constants.ColumnCount];

        public int MoveCount { get; set; }

        public int CurrentPlayer { get; set; } = Constants.BlackValue;

        public int LastMoveRow { get; set; } = Constants.NoMove;

        public int LastMoveColumn { get; set; } = Constants.NoMove;
    }
}
