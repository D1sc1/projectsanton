using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Services;
using ReversiGame.Common.Infrastucture;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel : ViewModelBase
    {
        private readonly IUserService _userService = NinjectKernel.Instance.Get<IUserService>();

        private UserModel _userModel;

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public bool IsUserNameValid => !string.IsNullOrWhiteSpace(UserName);

        public bool FindUser()
        {
            _userModel = _userService.GetUserByName(UserName.Trim());
            return _userModel != null;
        }

        public void CreateUser()
        {
            _userModel = _userService.GetOrCreateUser(UserName.Trim());
        }

        public void CommitUser()
        {
            MainViewModel.CommitUser(_userModel);
        }
    }
}
