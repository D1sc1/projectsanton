using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Repositories;
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
    public class UserRepositoryEF : IUserRepository
    {
        public UserDto Create(string userName)
        {
            using (var context = new ReversiGameDataContext())
            {
                var newUser = new User { Name = userName };
                context.Users.Add(newUser);
                context.SaveChanges();

                return ToDto(newUser);
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var context = new ReversiGameDataContext())
            {
                return context.Users.OrderBy(u => u.Id).ToList().Select(ToDto).ToList();
            }
        }

        public UserDto GetById(int userId)
        {
            using (var context = new ReversiGameDataContext())
            {
                return ToDto(context.Users.FirstOrDefault(u => u.Id == userId));
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var context = new ReversiGameDataContext())
            {
                return ToDto(context.Users.FirstOrDefault(u => u.Name == userName));
            }
        }

        public void AddGameResult(int userId, GameResult result)
        {
            using (var context = new ReversiGameDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return;
                }

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

                context.SaveChanges();
            }
        }

        public UserDto ToDto(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Wins = user.Wins,
                Losses = user.Losses,
                Draws = user.Draws,
            };
        }
    }
}
