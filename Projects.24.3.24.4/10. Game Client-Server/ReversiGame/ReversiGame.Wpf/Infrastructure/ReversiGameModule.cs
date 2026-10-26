using ReversiGame.BusinessProxy.Services;
using ReversiGame.Common.Contracts.Services;
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
            // Клиент не работает с БД напрямую: сервисы - это прокси к серверу ReversiGame.WebApi
            Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
            Bind<IGameService>().To<GameServiceProxy>().InSingletonScope();
        }
    }
}
