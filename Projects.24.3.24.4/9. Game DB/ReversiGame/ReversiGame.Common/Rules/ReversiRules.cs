using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.Rules
{
    /// <summary>
    /// Правила Реверси. Лежат в Common, потому что нужны и серверу (проверка ходов),
    /// и клиенту (подсказки, подсчет фишек)
    /// </summary>
    public static class ReversiRules
    {
        private static readonly int[] RowSteps = { -1, -1, -1, 0, 0, 1, 1, 1 };

        private static readonly int[] ColumnSteps = { -1, 0, 1, -1, 1, -1, 0, 1 };

        public static int Opponent(int player) => player == Constants.BlackValue ? Constants.WhiteValue : Constants.BlackValue;

        public static string PlayerName(int player) => player == Constants.BlackValue ? "черные" : "белые";

        public static bool IsInside(int row, int column) =>
            row >= 0 && row < Constants.RowCount && column >= 0 && column < Constants.ColumnCount;

        /// <summary>
        /// Начальная расстановка: по две фишки каждого цвета в центре, ходят черные
        /// </summary>
        public static void Initialize(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = Constants.EmptyCellValue;
                }
            }

            var center = Constants.RowCount / 2;
            model[center - 1, center - 1] = Constants.WhiteValue;
            model[center - 1, center] = Constants.BlackValue;
            model[center, center - 1] = Constants.BlackValue;
            model[center, center] = Constants.WhiteValue;

            model.CurrentPlayer = Constants.BlackValue;
            model.MoveCount = 0;
            model.LastMoveRow = Constants.NoMove;
            model.LastMoveColumn = Constants.NoMove;
            model.Message = null;
        }

        /// <summary>
        /// Фишки соперника, которые перевернутся при ходе player в клетку (row, column)
        /// </summary>
        public static IList<CellPosition> GetFlippedCells(GameModel model, int row, int column, int player)
        {
            var result = new List<CellPosition>();

            if (!IsInside(row, column) || model[row, column] != Constants.EmptyCellValue)
            {
                return result;
            }

            var opponent = Opponent(player);

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

        public static bool IsValidMove(GameModel model, int row, int column, int player)
        {
            return GetFlippedCells(model, row, column, player).Count > 0;
        }

        public static IList<CellPosition> GetValidMoves(GameModel model, int player)
        {
            var result = new List<CellPosition>();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (IsValidMove(model, row, column, player))
                    {
                        result.Add(new CellPosition(row, column));
                    }
                }
            }

            return result;
        }

        public static bool IsGameOver(GameModel model)
        {
            return GetValidMoves(model, Constants.BlackValue).Count == 0 &&
                GetValidMoves(model, Constants.WhiteValue).Count == 0;
        }

        public static int CountCells(GameModel model, int value)
        {
            var count = 0;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column] == value)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public static GameResult GetResult(GameModel model)
        {
            if (!IsGameOver(model))
            {
                return GameResult.None;
            }

            var blackCount = CountCells(model, Constants.BlackValue);
            var whiteCount = CountCells(model, Constants.WhiteValue);

            if (blackCount > whiteCount)
            {
                return GameResult.BlackWins;
            }

            return whiteCount > blackCount ? GameResult.WhiteWins : GameResult.Draw;
        }

        /// <summary>
        /// Ставит фишку текущего игрока, переворачивает фишки соперника и передает ход.
        /// Корректность хода должна быть проверена заранее
        /// </summary>
        public static void PlacePiece(GameModel model, int row, int column)
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

        private static void PassTurn(GameModel model)
        {
            var opponent = Opponent(model.CurrentPlayer);

            if (GetValidMoves(model, opponent).Count > 0)
            {
                model.CurrentPlayer = opponent;
                return;
            }

            if (GetValidMoves(model, model.CurrentPlayer).Count > 0)
            {
                var name = PlayerName(opponent);
                AddMessage(model, $"{char.ToUpper(name[0])}{name.Substring(1)} пропускают ход");
                return;
            }

            AddMessage(model, "Ходов больше нет, партия окончена");
        }

        private static void AddMessage(GameModel model, string message)
        {
            model.Message = string.IsNullOrEmpty(model.Message) ? message : $"{model.Message}. {message}";
        }
    }
}
