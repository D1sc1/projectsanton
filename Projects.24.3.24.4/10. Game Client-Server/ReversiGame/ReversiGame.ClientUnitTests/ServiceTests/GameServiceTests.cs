using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.BusinessProxy.Services;
using ReversiGame.Common.Definitions;
using ReversiGame.Common.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.ClientUnitTests.ServiceTests
{
    /// <summary>
    /// Тесты клиента через HTTP: должен быть запущен сервер ReversiGame.WebApi (адрес - в app.config)
    /// </summary>
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public void PlayGameThroughServerTest()
        {
            var userService = new UserServiceProxy();
            var gameService = new GameServiceProxy();
            var user = userService.GetOrCreateUser("TestUser_" + Guid.NewGuid().ToString("N").Substring(0, 8));

            var game = gameService.GetByUserId(user.Id);
            Assert.AreEqual(4, ReversiRules.CountCells(game, Constants.BlackValue) + ReversiRules.CountCells(game, Constants.WhiteValue));

            var random = new Random(5);
            while (!gameService.IsGameOver(game.Id))
            {
                var moves = ReversiRules.GetValidMoves(game, Constants.HumanPlayer);
                var move = moves[random.Next(moves.Count)];
                var before = game.MoveCount;

                game = gameService.MakeMove(game.Id, move.Row, move.Column);
                Assert.IsTrue(game.MoveCount > before);
            }

            var updatedUser = userService.GetUserById(user.Id);
            Assert.AreEqual(1, updatedUser.Wins + updatedUser.Losses + updatedUser.Draws);

            gameService.RemoveGame(game.Id);
            Assert.IsNull(gameService.GetByGameId(game.Id));
        }
    }
}
