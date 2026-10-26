using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Repositories;
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
    public class UserRepository : IUserRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        private const string SelectUsersQuery = @"
select
    ""Id"",
    ""Name"",
    ""Wins"",
    ""Losses"",
    ""Draws""
from ""Users""
";

        public IEnumerable<UserDto> GetAll()
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var result = new List<UserDto>();

                using (var command = new NpgsqlCommand(SelectUsersQuery + @"order by ""Id""", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(ReadUser(reader));
                        }
                    }
                }

                return result;
            }
        }

        public UserDto Create(string userName)
        {
            var insertQuery = @"
insert into ""Users"" (""Name"")
values (@userName)
returning ""Id""
";

            int userId;

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("userName", userName);
                    userId = (int)command.ExecuteScalar();
                }
            }

            return GetById(userId);
        }

        public UserDto GetById(int userId)
        {
            return ReadSingleUser(SelectUsersQuery + @"where ""Id"" = @value", userId);
        }

        public UserDto GetByName(string userName)
        {
            return ReadSingleUser(SelectUsersQuery + @"where ""Name"" = @value", userName);
        }

        public void AddGameResult(int userId, GameResult result)
        {
            var updateQuery = @"
update ""Users""
set
    ""Wins"" = ""Wins"" + @wins,
    ""Losses"" = ""Losses"" + @losses,
    ""Draws"" = ""Draws"" + @draws
where
    ""Id"" = @userId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("userId", userId);
                    command.Parameters.AddWithValue("wins", result == GameResult.BlackWins ? 1 : 0);
                    command.Parameters.AddWithValue("losses", result == GameResult.WhiteWins ? 1 : 0);
                    command.Parameters.AddWithValue("draws", result == GameResult.Draw ? 1 : 0);
                    command.ExecuteNonQuery();
                }
            }
        }

        private UserDto ReadSingleUser(string query, object value)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("value", value);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ReadUser(reader);
                        }
                    }
                }
            }

            return null;
        }

        private static UserDto ReadUser(NpgsqlDataReader reader)
        {
            return new UserDto
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Wins = reader.GetInt32(2),
                Losses = reader.GetInt32(3),
                Draws = reader.GetInt32(4),
            };
        }
    }
}
