using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.Business.Services;
using ReversiGame.Common.BusinessModels;
using ReversiGame.UnitTests.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.UnitTests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void GetOrCreateUserTest()
        {
            var userService = new UserService(new FakeUserRepository());

            var created = userService.GetOrCreateUser("Игрок");
            var same = userService.GetOrCreateUser("Игрок");

            Assert.AreNotEqual(0, created.Id);
            Assert.AreEqual(created.Id, same.Id);
            Assert.AreEqual(1, userService.GetAllUsers().Count());
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var userService = new UserService(new FakeUserRepository());
            userService.GetOrCreateUser("Игрок");

            Assert.IsNotNull(userService.GetUserByName("Игрок"));
            Assert.IsNull(userService.GetUserByName("Нет такого"));
        }

        [TestMethod]
        public void StatisticsTest()
        {
            var repository = new FakeUserRepository();
            var userService = new UserService(repository);
            var user = userService.GetOrCreateUser("Игрок");

            repository.AddGameResult(user.Id, GameResult.BlackWins);
            repository.AddGameResult(user.Id, GameResult.BlackWins);
            repository.AddGameResult(user.Id, GameResult.Draw);

            var updated = userService.GetUserById(user.Id);
            Assert.AreEqual(2, updated.Wins);
            Assert.AreEqual(0, updated.Losses);
            Assert.AreEqual(1, updated.Draws);
        }
    }
}
