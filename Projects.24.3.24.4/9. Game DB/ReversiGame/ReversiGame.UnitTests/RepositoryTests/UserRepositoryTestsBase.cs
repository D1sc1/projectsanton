using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.UnitTests.RepositoryTests
{
    /// <summary>
    /// Общие тесты репозитория пользователей для ADO.NET и EF.
    /// Это интеграционные тесты: нужна БД PostgreSQL из App.config
    /// </summary>
    public abstract class UserRepositoryTestsBase
    {
        protected abstract IUserRepository CreateRepository();

        protected static string NewUserName() => "TestUser_" + Guid.NewGuid().ToString("N").Substring(0, 8);

        [TestMethod]
        public void CreateUserTest()
        {
            var userName = NewUserName();
            var repository = CreateRepository();

            var result = repository.Create(userName);

            Assert.AreEqual(userName, result.Name);
            Assert.AreNotEqual(0, result.Id);
            Assert.AreEqual(0, result.Wins + result.Losses + result.Draws);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var repository = CreateRepository();
            repository.Create(NewUserName());

            Assert.IsTrue(repository.GetAll().Any());
        }

        [TestMethod]
        public void GetUserByNameAndIdTest()
        {
            var repository = CreateRepository();
            var created = repository.Create(NewUserName());

            var byName = repository.GetByName(created.Name);
            var byId = repository.GetById(created.Id);

            Assert.AreEqual(created.Id, byName.Id);
            Assert.AreEqual(created.Name, byId.Name);
            Assert.IsNull(repository.GetByName(NewUserName()));
        }

        [TestMethod]
        public void AddGameResultTest()
        {
            var repository = CreateRepository();
            var user = repository.Create(NewUserName());

            repository.AddGameResult(user.Id, GameResult.BlackWins);
            repository.AddGameResult(user.Id, GameResult.WhiteWins);
            repository.AddGameResult(user.Id, GameResult.WhiteWins);
            repository.AddGameResult(user.Id, GameResult.Draw);

            var updated = repository.GetById(user.Id);
            Assert.AreEqual(1, updated.Wins);
            Assert.AreEqual(2, updated.Losses);
            Assert.AreEqual(1, updated.Draws);
        }
    }
}
