using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.Common.Definitions;
using ReversiGame.Common.Dtos;
using ReversiGame.DataAccess.EF.DataContext;
using ReversiGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new ReversiGameDataContext())
            {
                return ToDto(context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId));
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new ReversiGameDataContext())
            {
                var games = context.Games.Include("Cells").Where(g => g.UserId == userId).OrderBy(g => g.Id).ToList();
                return games.Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new ReversiGameDataContext())
            {
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    return;
                }

                context.Cells.RemoveRange(game.Cells);
                context.Games.Remove(game);
                context.SaveChanges();
            }
        }

        public int Save(GameDto gameDto)
        {
            if (gameDto.Id == 0)
            {
                return Create(gameDto);
            }

            return Update(gameDto);
        }

        private int Create(GameDto gameDto)
        {
            using (var context = new ReversiGameDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == gameDto.UserId);
                if (user == null)
                {
                    throw new Exception("Нет такого пользователя");
                }

                var newGame = new Game
                {
                    User = user,
                    Cells = new List<Cell>(),
                };

                CopyGameState(gameDto, newGame);
                context.Games.Add(newGame);

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if (gameDto.Cells[row, column] == Constants.EmptyCellValue)
                        {
                            continue;
                        }

                        newGame.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = column,
                            Value = gameDto.Cells[row, column],
                        });
                    }
                }

                context.SaveChanges();
                return newGame.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var context = new ReversiGameDataContext())
            {
                var game = context.Games.Include("Cells").FirstOrDefault(g => g.Id == gameDto.Id);
                if (game == null)
                {
                    throw new Exception("Нет такой игры");
                }

                CopyGameState(gameDto, game);

                // Синхронизация клеток: новые фишки добавляются, перевернутые - меняют значение
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        var value = gameDto.Cells[row, column];
                        var cell = game.Cells.FirstOrDefault(c => c.Row == row && c.Column == column);

                        if (value == Constants.EmptyCellValue)
                        {
                            if (cell != null)
                            {
                                context.Cells.Remove(cell);
                            }

                            continue;
                        }

                        if (cell == null)
                        {
                            game.Cells.Add(new Cell { Row = row, Column = column, Value = value, });
                        }
                        else if (cell.Value != value)
                        {
                            cell.Value = value;
                        }
                    }
                }

                context.SaveChanges();
                return gameDto.Id;
            }
        }

        private static void CopyGameState(GameDto gameDto, Game game)
        {
            game.MoveCount = gameDto.MoveCount;
            game.CurrentPlayer = gameDto.CurrentPlayer;
            game.LastMoveRow = gameDto.LastMoveRow;
            game.LastMoveColumn = gameDto.LastMoveColumn;
        }

        private GameDto ToDto(Game game)
        {
            if (game == null)
            {
                return null;
            }

            var result = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                CurrentPlayer = game.CurrentPlayer,
                LastMoveRow = game.LastMoveRow,
                LastMoveColumn = game.LastMoveColumn,
            };

            foreach (var cell in game.Cells)
            {
                result.Cells[cell.Row, cell.Column] = cell.Value;
            }

            return result;
        }
    }
}
