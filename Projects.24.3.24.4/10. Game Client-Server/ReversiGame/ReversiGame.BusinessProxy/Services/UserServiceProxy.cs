using ReversiGame.BusinessProxy.Infrastucture;
using ReversiGame.Common.BusinessDtos;
using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReversiGame.BusinessProxy.Services
{
    /// <summary>
    /// Клиентская реализация IUserService: все вызовы передаются на сервер ReversiGame.WebApi
    /// </summary>
    public class UserServiceProxy : IUserService
    {
        public IEnumerable<UserModel> GetAllUsers()
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync("api/users/get-all").Result;
                var reply = HttpConnection.ReadReply<UsersReply>(response);
                return reply?.Users?.Select(FromDto).ToList() ?? new List<UserModel>();
            }
        }

        public UserModel GetOrCreateUser(string userName)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var httpContent = JsonContent.Create(new UserNameRequest { UserName = userName });
                var response = httpClient.PostAsync("api/users/get-or-create", httpContent).Result;
                return FromDto(HttpConnection.ReadReply<UserReply>(response));
            }
        }

        public UserModel GetUserByName(string userName)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                // Имя может содержать пробелы и кириллицу - кодируем его для URL
                var response = httpClient.GetAsync($"api/users/get-by-name/{Uri.EscapeDataString(userName)}").Result;
                return FromDto(HttpConnection.ReadReply<UserReply>(response));
            }
        }

        public UserModel GetUserById(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/users/get-by-id/{userId}").Result;
                return FromDto(HttpConnection.ReadReply<UserReply>(response));
            }
        }

        private UserModel FromDto(UserReply reply)
        {
            if (reply == null)
            {
                return null;
            }

            return new UserModel
            {
                Id = reply.Id,
                Name = reply.Name,
                Wins = reply.Wins,
                Losses = reply.Losses,
                Draws = reply.Draws,
            };
        }
    }
}
