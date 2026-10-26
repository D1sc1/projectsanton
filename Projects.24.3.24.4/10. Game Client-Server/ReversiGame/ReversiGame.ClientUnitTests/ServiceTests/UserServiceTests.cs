using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.BusinessProxy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.ClientUnitTests.ServiceTests
{
    /// <summary>
    /// Тесты клиента через HTTP: должен быть запущен сервер ReversiGame.WebApi (адрес - в app.config)
    /// </summary>
    [TestClass]
    public class UserServiceTests
    {
        private static string NewUserName() => "Тестовый игрок " + Guid.NewGuid().ToString("N").Substring(0, 6);

        [TestMethod]
        public void GetOrCreateUserTest()
        {
            var userService = new UserServiceProxy();
            var userName = NewUserName();

            var created = userService.GetOrCreateUser(userName);
            var same = userService.GetOrCreateUser(userName);

            Assert.IsNotNull(created);
            Assert.AreEqual(userName, created.Name);
            Assert.AreEqual(created.Id, same.Id);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var userService = new UserServiceProxy();
            var created = userService.GetOrCreateUser(NewUserName());

            Assert.IsTrue(userService.GetAllUsers().Any(u => u.Id == created.Id));
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var userService = new UserServiceProxy();
            var created = userService.GetOrCreateUser(NewUserName());

            var found = userService.GetUserByName(created.Name);

            Assert.IsNotNull(found, "Имя с пробелом и кириллицей должно передаваться в URL");
            Assert.AreEqual(created.Id, found.Id);
            Assert.IsNull(userService.GetUserByName(NewUserName()));
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            var userService = new UserServiceProxy();
            var created = userService.GetOrCreateUser(NewUserName());

            Assert.AreEqual(created.Name, userService.GetUserById(created.Id).Name);
        }
    }
}
