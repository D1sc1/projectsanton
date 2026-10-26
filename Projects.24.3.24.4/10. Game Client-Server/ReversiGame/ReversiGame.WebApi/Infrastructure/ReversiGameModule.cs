using ReversiGame.Business.Services;
using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.Common.Contracts.Services;
using ReversiGame.DataAccess.EF.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReversiGame.WebApi.Infrastructure
{
    public class ReversiGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IGameService>().To<GameService>().InSingletonScope();

            Bind<IUserRepository>().To<UserRepositoryEF>().InSingletonScope();
            Bind<IGameRepository>().To<GameRepositoryEF>().InSingletonScope();
        }
    }
}