using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.Business.Services;
using ReversiGame.Common.Definitions;
using ReversiGame.Common.Rules;
using ReversiGame.UnitTests.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.UnitTests.ServiceTests
{
    [TestClass]
    public class GameServiceTests
    {
        private FakeGameRepository _gameRepository;

        private FakeUserRepository _userRepository;

        private GameService _service;

        private int _userId;

        [TestInitialize]
        public void Initialize()
        {
            _gameRepository = new FakeGameRepository();
            _userRepository = new FakeUserRepository();
            _service = new GameService(_gameRepository, _userRepository);
            _userId = _userRepository.Create("Tester").Id;
        }

        [TestMethod]
        public void NewGameIsCreatedForUserTest()
        {
            var game = _service.GetByUserId(_userId);

            Assert.AreNotEqual(0, game.Id);
            Assert.AreEqual(_userId, game.UserId);
            Assert.AreEqual(4, ReversiRules.CountCells(game, Constants.BlackValue) + ReversiRules.CountCells(game, Constants.WhiteValue));
            Assert.AreEqual(game.Id, _service.GetByUserId(_userId).Id, "Повторный вызов возвращает ту же партию");
        }

        [TestMethod]
        public void ComputerAnswersAfterHumanMoveTest()
        {
            var game = _service.GetByUserId(_userId);

            var afterMove = _service.MakeMove(game.Id, 2, 3);

            Assert.AreEqual(2, afterMove.MoveCount, "Ход человека и ответ компьютера");
            Assert.AreEqual(Constants.HumanPlayer, afterMove.CurrentPlayer);

            var saved = _service.GetByGameId(game.Id);
            Assert.AreEqual(afterMove.MoveCount, saved.MoveCount);
            Assert.AreEqual(afterMove[afterMove.LastMoveRow, afterMove.LastMoveColumn], saved[saved.LastMoveRow, saved.LastMoveColumn]);
        }

        [TestMethod]
        public void InvalidMoveIsIgnoredTest()
        {
            var game = _service.GetByUserId(_userId);
            var saveCount = _gameRepository.SaveCount;

            var afterMove = _service.MakeMove(game.Id, 0, 0);

            Assert.AreEqual(0, afterMove.MoveCount);
            Assert.AreEqual(saveCount, _gameRepository.SaveCount, "Неверный ход не сохраняется");
        }

        [TestMethod]
        public void FullGameUpdatesStatisticsTest()
        {
            var game = _service.GetByUserId(_userId);
            var random = new Random(7);

            while (!_service.IsGameOver(game.Id))
            {
                var moves = ReversiRules.GetValidMoves(game, Constants.HumanPlayer);
                var move = moves[random.Next(moves.Count)];
                game = _service.MakeMove(game.Id, move.Row, move.Column);
            }

            var user = _userRepository.GetById(_userId);
            Assert.AreEqual(1, user.Wins + user.Losses + user.Draws, "Результат партии учтен один раз");
            Assert.AreEqual(64, ReversiRules.CountCells(game, Constants.BlackValue) + ReversiRules.CountCells(game, Constants.WhiteValue) +
                ReversiRules.CountCells(game, Constants.EmptyCellValue));

            _service.RemoveGame(game.Id);
            Assert.IsNull(_service.GetByGameId(game.Id));
            Assert.AreNotEqual(game.Id, _service.GetByUserId(_userId).Id, "После окончания партии создается новая");
        }
    }
}
