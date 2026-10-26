using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.BusinessDtos
{
    public class GameReply
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        public int CurrentPlayer { get; set; }

        public int LastMoveRow { get; set; }

        public int LastMoveColumn { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// Клетки доски построчно: RowCount * ColumnCount значений
        /// </summary>
        public int[] Cells { get; set; }
    }
}
