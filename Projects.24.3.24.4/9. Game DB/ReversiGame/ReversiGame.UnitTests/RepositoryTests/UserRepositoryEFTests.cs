using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.DataAccess.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class UserRepositoryEFTests : UserRepositoryTestsBase
    {
        protected override IUserRepository CreateRepository() => new UserRepositoryEF();
    }
}
