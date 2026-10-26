using ReversiGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReversiGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameModel.RowCount;

        public int ColumnCount => GameModel.ColumnCount;

        public CellViewModel[,] Cells { get; set; } = new CellViewModel[GameModel.RowCount, GameModel.ColumnCount];

        public int BlackCount { get; set; }

        public int WhiteCount { get; set; }

        public int MoveCount { get; set; }

        public bool IsVsComputer { get; set; }

        public bool IsGameOver { get; set; }

        public string StatusText { get; set; }

        public string MessageText { get; set; }
    }
}
