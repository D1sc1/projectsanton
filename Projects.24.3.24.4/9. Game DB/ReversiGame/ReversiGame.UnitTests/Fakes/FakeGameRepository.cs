using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.Common.Definitions;
using ReversiGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.UnitTests.Fakes
{
    /// <summary>
    /// Хранилище партий в памяти - для тестов сервисов без базы данных.
    /// Хранит копии, чтобы тесты не могли случайно менять "сохраненные" данные
    /// </summary>
    internal class FakeGameRepository : IGameRepository
    {
        private readonly List<GameDto> _games = new List<GameDto>();

        private int _nextId = 1;

        public int SaveCount { get; private set; }

        public GameDto GetByGameId(int gameId) => Copy(_games.FirstOrDefault(g => g.Id == gameId));

        public IEnumerable<GameDto> GetByUserId(int userId) => _games.Where(g => g.UserId == userId).Select(g => Copy(g)).ToList();

        public void Remove(int gameId) => _games.RemoveAll(g => g.Id == gameId);

        public int Save(GameDto gameDto)
        {
            SaveCount++;

            var copy = Copy(gameDto, gameDto.Id == 0 ? _nextId++ : gameDto.Id);
            _games.RemoveAll(g => g.Id == copy.Id);
            _games.Add(copy);
            return copy.Id;
        }

        private static GameDto Copy(GameDto game) => game == null ? null : Copy(game, game.Id);

        private static GameDto Copy(GameDto game, int id)
        {
            var result = new GameDto
            {
                Id = id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                CurrentPlayer = game.CurrentPlayer,
                LastMoveRow = game.LastMoveRow,
                LastMoveColumn = game.LastMoveColumn,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.Cells[row, column] = game.Cells[row, column];
                }
            }

            return result;
        }
    }
}
