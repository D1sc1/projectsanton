using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class GameRepositoryTests : GameRepositoryTestsBase
    {
        protected override IGameRepository CreateGameRepository() => new GameRepository();

        protected override IUserRepository CreateUserRepository() => new UserRepository();
    }
}
