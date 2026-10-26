using ReversiGame.Business.Models;
using ReversiGame.Business.Services;
using ReversiGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReversiGame.Web.Controllers
{
    public class GameController : Controller
    {
        private const string GameModelKey = "GameModel";

        private readonly GameService _service = new GameService();

        // GET: Game
        public ActionResult Index()
        {
            var model = GetGameModel();
            return View(ToViewModel(model));
        }

        public ActionResult NewGame(bool vsComputer = true)
        {
            var model = new GameModel();
            _service.Initialize(model, vsComputer);
            SaveGameModel(model);

            return RedirectToAction("Index");
        }

        public ActionResult MakeMove(int row, int column)
        {
            var model = GetGameModel();
            _service.MakeMove(model, row, column);
            SaveGameModel(model);

            // После хода - перенаправление, чтобы обновление страницы (F5) не повторяло запрос хода
            return RedirectToAction("Index");
        }

        private GameModel GetGameModel()
        {
            var model = Session[GameModelKey] as GameModel;
            if (model == null)
            {
                model = new GameModel();
                _service.Initialize(model, true);
                SaveGameModel(model);
            }

            return model;
        }

        private void SaveGameModel(GameModel model)
        {
            Session[GameModelKey] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var isGameOver = _service.IsGameOver(model);
            var validMoves = _service.GetValidMoves(model);

            var result = new GameViewModel
            {
                BlackCount = _service.CountCells(model, GameModel.BlackValue),
                WhiteCount = _service.CountCells(model, GameModel.WhiteValue),
                MoveCount = model.MoveCount,
                IsVsComputer = model.IsVsComputer,
                IsGameOver = isGameOver,
                MessageText = model.Message,
            };

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    result.Cells[row, column] = new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Value = model[row, column],
                        IsValidMove = !isGameOver && validMoves.Any(m => m.Row == row && m.Column == column),
                        IsLastMove = row == model.LastMoveRow && column == model.LastMoveColumn,
                    };
                }
            }

            result.StatusText = isGameOver ? GetResultText(model, result) : GetTurnText(model);
            return result;
        }

        private string GetTurnText(GameModel model)
        {
            var player = GameModel.PlayerName(model.CurrentPlayer);
            return model.IsVsComputer ? $"Ваш ход ({player})" : $"Ходят {player}";
        }

        private string GetResultText(GameModel model, GameViewModel viewModel)
        {
            var black = viewModel.BlackCount;
            var white = viewModel.WhiteCount;

            switch (_service.GetResult(model))
            {
                case GameResult.BlackWins:
                    return model.IsVsComputer ? $"Вы победили! {black} : {white}" : $"Победили черные {black} : {white}";

                case GameResult.WhiteWins:
                    return model.IsVsComputer ? $"Победил компьютер {white} : {black}" : $"Победили белые {white} : {black}";

                default:
                    return $"Ничья {black} : {white}";
            }
        }
    }
}
