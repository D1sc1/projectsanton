using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.Business.Services;
using ReversiGame.DataAccess.EF.Repositories;
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
        private static string NewUserName() => "TestUser_" + Guid.NewGuid().ToString("N").Substring(0, 8);

        [TestMethod]
        public void GetOrCreateUserTest()
        {
            var userService = new UserService(new UserRepositoryEF());
            var userName = NewUserName();

            var created = userService.GetOrCreateUser(userName);
            var same = userService.GetOrCreateUser(userName);

            Assert.AreNotEqual(0, created.Id);
            Assert.AreEqual(created.Id, same.Id);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var userService = new UserService(new UserRepositoryEF());
            var created = userService.GetOrCreateUser(NewUserName());

            Assert.IsTrue(userService.GetAllUsers().Any(u => u.Id == created.Id));
        }

        [TestMethod]
        public void GetUserByNameAndIdTest()
        {
            var userService = new UserService(new UserRepositoryEF());
            var created = userService.GetOrCreateUser(NewUserName());

            Assert.AreEqual(created.Id, userService.GetUserByName(created.Name).Id);
            Assert.AreEqual(created.Name, userService.GetUserById(created.Id).Name);
            Assert.IsNull(userService.GetUserByName(NewUserName()));
        }
    }
}
