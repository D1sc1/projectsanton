using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.Common.Contracts.Services;
using ReversiGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _repository.GetAll()
                .Select(FromDto)
                .ToList();
        }

        public UserModel GetOrCreateUser(string userName)
        {
            var userDto = _repository.GetByName(userName);

            if (userDto == null)
            {
                userDto = _repository.Create(userName);
            }

            return FromDto(userDto);
        }

        public UserModel GetUserByName(string userName)
        {
            return FromDto(_repository.GetByName(userName));
        }

        public UserModel GetUserById(int userId)
        {
            return FromDto(_repository.GetById(userId));
        }

        private UserModel FromDto(UserDto userDto)
        {
            if (userDto == null)
            {
                return null;
            }

            return new UserModel
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Wins = userDto.Wins,
                Losses = userDto.Losses,
                Draws = userDto.Draws,
            };
        }
    }
}
