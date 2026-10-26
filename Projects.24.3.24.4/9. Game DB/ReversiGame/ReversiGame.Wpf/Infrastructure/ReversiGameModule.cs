using ReversiGame.Business.Services;
using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.Common.Contracts.Services;
using ReversiGame.DataAccess.EF.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Wpf.Infrastructure
{
    internal class ReversiGameModule : NinjectModule
    {
        public override void Load()
        {
            // Доступ к данным через Entity Framework (строка подключения EFMain).
            // Для ADO.NET нужно подключить проект ReversiGame.DataAccess и привязать UserRepository / GameRepository
            Bind<IUserRepository>().To<UserRepositoryEF>().InSingletonScope();
            Bind<IGameRepository>().To<GameRepositoryEF>().InSingletonScope();

            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IGameService>().To<GameService>().InSingletonScope();
        }
    }
}
