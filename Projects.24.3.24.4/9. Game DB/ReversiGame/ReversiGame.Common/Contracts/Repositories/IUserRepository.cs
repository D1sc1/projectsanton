using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.Contracts.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetAll();

        UserDto Create(string userName);

        UserDto GetById(int userId);

        UserDto GetByName(string userName);

        /// <summary>
        /// Учитывает результат партии в статистике игрока (игрок играет черными)
        /// </summary>
        void AddGameResult(int userId, GameResult result);
    }
}
