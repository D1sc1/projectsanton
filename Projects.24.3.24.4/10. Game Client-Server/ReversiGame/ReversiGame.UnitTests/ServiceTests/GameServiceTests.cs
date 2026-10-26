using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.Business.Services;
using ReversiGame.Common.Definitions;
using ReversiGame.Common.Rules;
using ReversiGame.DataAccess.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.UnitTests.ServiceTests
{
    /// <summary>
    /// Сценарные тесты серверной логики: GameService + репозитории EF.
    /// Нужна БД PostgreSQL из app.config (строка EFMain)
    /// </summary>
    [TestClass]
    public class GameServiceTests
    {
        private static string NewUserName() => "TestUser_" + Guid.NewGuid().ToString("N").Substring(0, 8);

        [TestMethod]
        public void GameIsCreatedAndSavedTest()
        {
            var userService = new UserService(new UserRepositoryEF());
            var gameService = new GameService(new GameRepositoryEF(), new UserRepositoryEF());
            var user = userService.GetOrCreateUser(NewUserName());

            var game = gameService.GetByUserId(user.Id);
            var sameGame = gameService.GetByUserId(user.Id);

            Assert.AreEqual(game.Id, sameGame.Id);
            Assert.AreEqual(2, ReversiRules.CountCells(game, Constants.BlackValue));
            Assert.AreEqual(2, ReversiRules.CountCells(game, Constants.WhiteValue));
        }

        [TestMethod]
        public void MoveIsSavedWithComputerAnswerTest()
        {
            var userService = new UserService(new UserRepositoryEF());
            var gameService = new GameService(new GameRepositoryEF(), new UserRepositoryEF());
            var user = userService.GetOrCreateUser(NewUserName());
            var game = gameService.GetByUserId(user.Id);

            var afterMove = gameService.MakeMove(game.Id, 2, 3);
            var saved = gameService.GetByGameId(game.Id);

            Assert.AreEqual(2, afterMove.MoveCount);
            Assert.AreEqual(afterMove.MoveCount, saved.MoveCount);
            Assert.AreEqual(Constants.HumanPlayer, saved.CurrentPlayer);
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Assert.AreEqual(afterMove[row, column], saved[row, column]);
                }
            }
        }

        [TestMethod]
        public void FullGameUpdatesStatisticsTest()
        {
            var userService = new UserService(new UserRepositoryEF());
            var gameService = new GameService(new GameRepositoryEF(), new UserRepositoryEF());
            var user = userService.GetOrCreateUser(NewUserName());
            var game = gameService.GetByUserId(user.Id);
            var random = new Random(11);

            while (!gameService.IsGameOver(game.Id))
            {
                var moves = ReversiRules.GetValidMoves(game, Constants.HumanPlayer);
                var move = moves[random.Next(moves.Count)];
                game = gameService.MakeMove(game.Id, move.Row, move.Column);
            }

            var updatedUser = userService.GetUserById(user.Id);
            Assert.AreEqual(1, updatedUser.Wins + updatedUser.Losses + updatedUser.Draws);

            gameService.RemoveGame(game.Id);
            Assert.IsNull(gameService.GetByGameId(game.Id));
        }
    }
}
