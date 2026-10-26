using ReversiGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.Contracts.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();

        UserModel GetOrCreateUser(string userName);

        UserModel GetUserByName(string userName);

        UserModel GetUserById(int userId);
    }
}
