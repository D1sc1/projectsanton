using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.Common.Definitions;
using ReversiGame.Common.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        private const string SelectGamesQuery = @"
select
    ""Id"",
    ""UserId"",
    ""MoveCount"",
    ""CurrentPlayer"",
    ""LastMoveRow"",
    ""LastMoveColumn""
from ""Games""
";

        private const string SelectCellsQuery = @"
select
    c.""GameId"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from
    ""Cells"" c
    join ""Games"" g on g.""Id"" = c.""GameId""
";

        public GameDto GetByGameId(int gameId)
        {
            var games = ReadGames(
                SelectGamesQuery + @"where ""Id"" = @id",
                SelectCellsQuery + @"where g.""Id"" = @id",
                gameId);

            return games.FirstOrDefault();
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            return ReadGames(
                SelectGamesQuery + @"where ""UserId"" = @id order by ""Id""",
                SelectCellsQuery + @"where g.""UserId"" = @id",
                userId);
        }

        public void Remove(int gameId)
        {
            var deleteCellsQuery = @"
delete from ""Cells""
where ""GameId"" = @gameId
";

            var deleteGameQuery = @"
delete from ""Games""
where ""Id"" = @gameId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new NpgsqlCommand(deleteCellsQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("gameId", gameId);
                        command.ExecuteNonQuery();
                    }

                    using (var command = new NpgsqlCommand(deleteGameQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("gameId", gameId);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
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
            var insertGameQuery = @"
insert into ""Games"" (""UserId"", ""MoveCount"", ""CurrentPlayer"", ""LastMoveRow"", ""LastMoveColumn"")
values (@userId, @moveCount, @currentPlayer, @lastMoveRow, @lastMoveColumn)
returning ""Id""
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    int gameId;

                    using (var command = new NpgsqlCommand(insertGameQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("userId", gameDto.UserId);
                        AddGameStateParameters(command, gameDto);
                        gameId = (int)command.ExecuteScalar();
                    }

                    InsertCells(connection, transaction, gameId, gameDto);

                    transaction.Commit();
                    return gameId;
                }
            }
        }

        /// <summary>
        /// За один ход в Реверси переворачивается много фишек, поэтому клетки партии
        /// проще удалить и записать заново (в одной транзакции)
        /// </summary>
        private int Update(GameDto gameDto)
        {
            var updateGameQuery = @"
update ""Games""
set
    ""MoveCount"" = @moveCount,
    ""CurrentPlayer"" = @currentPlayer,
    ""LastMoveRow"" = @lastMoveRow,
    ""LastMoveColumn"" = @lastMoveColumn
where
    ""Id"" = @gameId
";

            var deleteCellsQuery = @"
delete from ""Cells""
where ""GameId"" = @gameId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new NpgsqlCommand(updateGameQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("gameId", gameDto.Id);
                        AddGameStateParameters(command, gameDto);

                        if (command.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Нет такой игры");
                        }
                    }

                    using (var command = new NpgsqlCommand(deleteCellsQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("gameId", gameDto.Id);
                        command.ExecuteNonQuery();
                    }

                    InsertCells(connection, transaction, gameDto.Id, gameDto);

                    transaction.Commit();
                    return gameDto.Id;
                }
            }
        }

        private static void AddGameStateParameters(NpgsqlCommand command, GameDto gameDto)
        {
            command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
            command.Parameters.AddWithValue("currentPlayer", gameDto.CurrentPlayer);
            command.Parameters.AddWithValue("lastMoveRow", gameDto.LastMoveRow);
            command.Parameters.AddWithValue("lastMoveColumn", gameDto.LastMoveColumn);
        }

        private static void InsertCells(NpgsqlConnection connection, NpgsqlTransaction transaction, int gameId, GameDto gameDto)
        {
            var insertCellQuery = @"
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""Value"")
values (@gameId, @row, @column, @value)
";

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (gameDto.Cells[row, column] == Constants.EmptyCellValue)
                    {
                        continue;
                    }

                    using (var command = new NpgsqlCommand(insertCellQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("gameId", gameId);
                        command.Parameters.AddWithValue("row", row);
                        command.Parameters.AddWithValue("column", column);
                        command.Parameters.AddWithValue("value", gameDto.Cells[row, column]);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Читает партии первым запросом и их клетки вторым запросом
        /// </summary>
        private List<GameDto> ReadGames(string gamesQuery, string cellsQuery, int id)
        {
            var result = new List<GameDto>();

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(gamesQuery, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new GameDto
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                MoveCount = reader.GetInt32(2),
                                CurrentPlayer = reader.GetInt32(3),
                                LastMoveRow = reader.GetInt32(4),
                                LastMoveColumn = reader.GetInt32(5),
                            });
                        }
                    }
                }

                if (result.Count == 0)
                {
                    return result;
                }

                using (var command = new NpgsqlCommand(cellsQuery, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var game = result.FirstOrDefault(g => g.Id == reader.GetInt32(0));
                            if (game == null)
                            {
                                continue;
                            }

                            game.Cells[reader.GetInt32(1), reader.GetInt32(2)] = reader.GetInt32(3);
                        }
                    }
                }
            }

            return result;
        }
    }
}
