using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.UnitTests.Fakes
{
    /// <summary>
    /// Хранилище пользователей в памяти - для тестов сервисов без базы данных
    /// </summary>
    internal class FakeUserRepository : IUserRepository
    {
        private readonly List<UserDto> _users = new List<UserDto>();

        public IEnumerable<UserDto> GetAll() => _users.Select(Copy).ToList();

        public UserDto Create(string userName)
        {
            var user = new UserDto { Id = _users.Count + 1, Name = userName, };
            _users.Add(user);
            return Copy(user);
        }

        public UserDto GetById(int userId) => Copy(_users.FirstOrDefault(u => u.Id == userId));

        public UserDto GetByName(string userName) => Copy(_users.FirstOrDefault(u => u.Name == userName));

        public void AddGameResult(int userId, GameResult result)
        {
            var user = _users.First(u => u.Id == userId);
            switch (result)
            {
                case GameResult.BlackWins:
                    user.Wins++;
                    break;

                case GameResult.WhiteWins:
                    user.Losses++;
                    break;

                case GameResult.Draw:
                    user.Draws++;
                    break;
            }
        }

        private static UserDto Copy(UserDto user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserDto { Id = user.Id, Name = user.Name, Wins = user.Wins, Losses = user.Losses, Draws = user.Draws, };
        }
    }
}
