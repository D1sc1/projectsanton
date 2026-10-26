using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.Common.Definitions;
using ReversiGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.UnitTests.RepositoryTests
{
    /// <summary>
    /// Общие тесты репозитория партий для ADO.NET и EF.
    /// Это интеграционные тесты: нужна БД PostgreSQL из App.config
    /// </summary>
    public abstract class GameRepositoryTestsBase
    {
        protected abstract IGameRepository CreateGameRepository();

        protected abstract IUserRepository CreateUserRepository();

        [TestMethod]
        public void CreateAndReadTest()
        {
            var gameRepository = CreateGameRepository();
            var gameDto = CreateInitialGame();

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);
            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.AreEqual(gameDto.CurrentPlayer, readGameDto.CurrentPlayer);
            AssertCellsEqual(gameDto, readGameDto);
        }

        [TestMethod]
        public void UpdateGameTest()
        {
            var gameRepository = CreateGameRepository();
            var gameDto = CreateInitialGame();
            gameDto.Id = gameRepository.Save(gameDto);

            // Ход черных в (2, 3): новая фишка и одна перевернутая
            gameDto.Cells[2, 3] = Constants.BlackValue;
            gameDto.Cells[3, 3] = Constants.BlackValue;
            gameDto.MoveCount = 1;
            gameDto.CurrentPlayer = Constants.WhiteValue;
            gameDto.LastMoveRow = 2;
            gameDto.LastMoveColumn = 3;
            gameRepository.Save(gameDto);

            var readGameDto = gameRepository.GetByGameId(gameDto.Id);
            Assert.AreEqual(1, readGameDto.MoveCount);
            Assert.AreEqual(Constants.WhiteValue, readGameDto.CurrentPlayer);
            Assert.AreEqual(2, readGameDto.LastMoveRow);
            Assert.AreEqual(3, readGameDto.LastMoveColumn);
            AssertCellsEqual(gameDto, readGameDto);
        }

        [TestMethod]
        public void GetGamesByUserIdAndRemoveTest()
        {
            var gameRepository = CreateGameRepository();
            var gameDto = CreateInitialGame();
            var firstId = gameRepository.Save(gameDto);
            var secondId = gameRepository.Save(gameDto);

            var games = gameRepository.GetByUserId(gameDto.UserId).ToList();
            CollectionAssert.AreEqual(new[] { firstId, secondId }, games.Select(g => g.Id).ToArray());

            gameRepository.Remove(firstId);
            Assert.IsNull(gameRepository.GetByGameId(firstId));
            Assert.AreEqual(1, gameRepository.GetByUserId(gameDto.UserId).Count());
        }

        private GameDto CreateInitialGame()
        {
            var user = CreateUserRepository().Create("TestUser_" + Guid.NewGuid().ToString("N").Substring(0, 8));
            var gameDto = new GameDto
            {
                UserId = user.Id,
                MoveCount = 0,
                CurrentPlayer = Constants.BlackValue,
            };

            gameDto.Cells[3, 3] = Constants.WhiteValue;
            gameDto.Cells[3, 4] = Constants.BlackValue;
            gameDto.Cells[4, 3] = Constants.BlackValue;
            gameDto.Cells[4, 4] = Constants.WhiteValue;
            return gameDto;
        }

        private static void AssertCellsEqual(GameDto expected, GameDto actual)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Assert.AreEqual(expected.Cells[row, column], actual.Cells[row, column], $"Клетка ({row}, {column})");
                }
            }
        }
    }
}
