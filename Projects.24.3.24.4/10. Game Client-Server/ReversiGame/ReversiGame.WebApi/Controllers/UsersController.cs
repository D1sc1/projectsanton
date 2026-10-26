using ReversiGame.Common.BusinessDtos;
using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Services;
using ReversiGame.Common.Infrastucture;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReversiGame.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private IUserService _userService = NinjectKernel.Instance.Get<IUserService>();

        [HttpGet]
        [Route("api/users/get-all")]
        public UsersReply GetAll()
        {
            var reply = _userService.GetAllUsers();
            return new UsersReply
            {
                Users = reply.Select(ToDto).ToList(),
            };
        }

        [HttpGet]
        [Route("api/users/get-by-name/{userName}")]
        public UserReply GetByName(string userName)
        {
            var reply = _userService.GetUserByName(userName);
            return ToDto(reply);
        }

        [HttpGet]
        [Route("api/users/get-by-id/{userId}")]
        public UserReply GetById(int userId)
        {
            var reply = _userService.GetUserById(userId);
            return ToDto(reply);
        }

        [HttpPost]
        [Route("api/users/get-or-create")]
        public UserReply GetOrCreate([FromBody] UserNameRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.UserName))
            {
                return null;
            }

            var reply = _userService.GetOrCreateUser(request.UserName);
            return ToDto(reply);
        }

        private UserReply ToDto(UserModel user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserReply
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
