using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.DataAccess.EF.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        public int CurrentPlayer { get; set; }

        public int LastMoveRow { get; set; }

        public int LastMoveColumn { get; set; }

        public User User { get; set; }

        public List<Cell> Cells { get; set; }
    }
}
