using ReversiGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Business.Services
{
    /// <summary>
    /// Правила игры Реверси (Отелло).
    /// Ход возможен в пустую клетку, если хотя бы по одному из 8 направлений за ней идет
    /// непрерывная линия фишек соперника, замкнутая своей фишкой. Все такие фишки соперника переворачиваются.
    /// Если ходить некуда, ход переходит к сопернику; если не могут ходить оба - партия окончена.
    /// </summary>
    public class GameService
    {
        private static readonly int[] RowSteps = { -1, -1, -1, 0, 0, 1, 1, 1 };

        private static readonly int[] ColumnSteps = { -1, 0, 1, -1, 1, -1, 0, 1 };

        private readonly ComputerPlayer _computerPlayer = new ComputerPlayer();

        public void Initialize(GameModel model, bool isVsComputer)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = GameModel.EmptyCellValue;
                }
            }

            var center = GameModel.RowCount / 2;
            model[center - 1, center - 1] = GameModel.WhiteValue;
            model[center - 1, center] = GameModel.BlackValue;
            model[center, center - 1] = GameModel.BlackValue;
            model[center, center] = GameModel.WhiteValue;

            model.CurrentPlayer = GameModel.BlackValue;
            model.MoveCount = 0;
            model.IsVsComputer = isVsComputer;
            model.LastMoveRow = -1;
            model.LastMoveColumn = -1;
            model.Message = null;
        }

        /// <summary>
        /// Фишки соперника, которые перевернутся при ходе player в клетку (row, column)
        /// </summary>
        public IList<CellPosition> GetFlippedCells(GameModel model, int row, int column, int player)
        {
            var result = new List<CellPosition>();

            if (!IsInside(row, column) || model[row, column] != GameModel.EmptyCellValue)
            {
                return result;
            }

            var opponent = GameModel.Opponent(player);

            for (int direction = 0; direction < RowSteps.Length; direction++)
            {
                var line = new List<CellPosition>();
                var r = row + RowSteps[direction];
                var c = column + ColumnSteps[direction];

                while (IsInside(r, c) && model[r, c] == opponent)
                {
                    line.Add(new CellPosition(r, c));
                    r += RowSteps[direction];
                    c += ColumnSteps[direction];
                }

                if (line.Count > 0 && IsInside(r, c) && model[r, c] == player)
                {
                    result.AddRange(line);
                }
            }

            return result;
        }

        public bool IsValidMove(GameModel model, int row, int column, int player)
        {
            return GetFlippedCells(model, row, column, player).Count > 0;
        }

        public bool IsValidMove(GameModel model, int row, int column)
        {
            return IsValidMove(model, row, column, model.CurrentPlayer);
        }

        public IList<CellPosition> GetValidMoves(GameModel model, int player)
        {
            var result = new List<CellPosition>();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (IsValidMove(model, row, column, player))
                    {
                        result.Add(new CellPosition(row, column));
                    }
                }
            }

            return result;
        }

        public IList<CellPosition> GetValidMoves(GameModel model)
        {
            return GetValidMoves(model, model.CurrentPlayer);
        }

        /// <summary>
        /// Ход текущего игрока. В игре против компьютера сразу за ним отвечает компьютер (белые).
        /// Возвращает false, если ход невозможен
        /// </summary>
        public bool MakeMove(GameModel model, int row, int column)
        {
            if (IsGameOver(model) || !IsValidMove(model, row, column))
            {
                return false;
            }

            model.Message = null;
            PlacePiece(model, row, column);

            if (model.IsVsComputer)
            {
                MakeComputerMoves(model);
            }

            return true;
        }

        /// <summary>
        /// Компьютер ходит за белых, пока очередь хода у белых (черные могут пропускать ход)
        /// </summary>
        public void MakeComputerMoves(GameModel model)
        {
            while (!IsGameOver(model) && model.CurrentPlayer == GameModel.WhiteValue)
            {
                var move = _computerPlayer.ChooseMove(this, model, GameModel.WhiteValue);
                if (move == null)
                {
                    break;
                }

                PlacePiece(model, move.Row, move.Column);
            }
        }

        public bool IsGameOver(GameModel model)
        {
            return GetValidMoves(model, GameModel.BlackValue).Count == 0 &&
                GetValidMoves(model, GameModel.WhiteValue).Count == 0;
        }

        public int CountCells(GameModel model, int value)
        {
            var count = 0;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] == value)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public GameResult GetResult(GameModel model)
        {
            if (!IsGameOver(model))
            {
                return GameResult.None;
            }

            var blackCount = CountCells(model, GameModel.BlackValue);
            var whiteCount = CountCells(model, GameModel.WhiteValue);

            if (blackCount > whiteCount)
            {
                return GameResult.BlackWins;
            }

            return whiteCount > blackCount ? GameResult.WhiteWins : GameResult.Draw;
        }

        private void PlacePiece(GameModel model, int row, int column)
        {
            var player = model.CurrentPlayer;
            var flippedCells = GetFlippedCells(model, row, column, player);

            model[row, column] = player;
            foreach (var cell in flippedCells)
            {
                model[cell.Row, cell.Column] = player;
            }

            model.MoveCount++;
            model.LastMoveRow = row;
            model.LastMoveColumn = column;

            PassTurn(model);
        }

        private void PassTurn(GameModel model)
        {
            var opponent = GameModel.Opponent(model.CurrentPlayer);

            if (GetValidMoves(model, opponent).Count > 0)
            {
                model.CurrentPlayer = opponent;
                return;
            }

            if (GetValidMoves(model, model.CurrentPlayer).Count > 0)
            {
                AddMessage(model, $"{Capitalize(GameModel.PlayerName(opponent))} пропускают ход");
                return;
            }

            AddMessage(model, "Ходов больше нет, партия окончена");
        }

        private static void AddMessage(GameModel model, string message)
        {
            model.Message = string.IsNullOrEmpty(model.Message) ? message : $"{model.Message}. {message}";
        }

        private static string Capitalize(string text)
        {
            return char.ToUpper(text[0]) + text.Substring(1);
        }

        private static bool IsInside(int row, int column)
        {
            return row >= 0 && row < GameModel.RowCount && column >= 0 && column < GameModel.ColumnCount;
        }
    }
}
