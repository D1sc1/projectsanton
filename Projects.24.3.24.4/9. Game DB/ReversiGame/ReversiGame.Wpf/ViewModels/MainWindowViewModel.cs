using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Services;
using ReversiGame.Common.Definitions;
using ReversiGame.Common.Infrastucture;
using ReversiGame.Common.Rules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IGameService _service = NinjectKernel.Instance.Get<IGameService>();

        private IUserService _userService = NinjectKernel.Instance.Get<IUserService>();

        private GameModel _model = new GameModel();

        private UserModel _user;

        private bool _isHintShown = true;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public string StatisticsText => _user == null ?
            string.Empty :
            $"Побед: {_user.Wins}, поражений: {_user.Losses}, ничьих: {_user.Draws}";

        public int MoveCount => _model?.MoveCount ?? 0;

        public int BlackCount => ReversiRules.CountCells(_model, Constants.BlackValue);

        public int WhiteCount => ReversiRules.CountCells(_model, Constants.WhiteValue);

        public string StatusText
        {
            get
            {
                if (_user == null)
                {
                    return string.Empty;
                }

                return ReversiRules.IsGameOver(_model) ? GetResultText() : "Ваш ход (черные)";
            }
        }

        public string MessageText => _model?.Message ?? string.Empty;

        /// <summary>
        /// Показывать клетки, в которые можно сходить
        /// </summary>
        public bool IsHintShown
        {
            get => _isHintShown;
            set
            {
                _isHintShown = value;
                LoadViewModel(_model);
            }
        }

        public MainWindowViewModel()
        {
            _model = new GameModel();
            LoadViewModel(_model);
        }

        public void CommitUser(UserModel userModel)
        {
            _user = userModel;
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(StatisticsText));

            Initialize();
        }

        public void Initialize()
        {
            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        /// <summary>
        /// Новая партия: текущая удаляется без записи результата
        /// </summary>
        public void StartNewGame()
        {
            if (_user == null)
            {
                return;
            }

            _service.RemoveGame(_model.Id);
            Initialize();
        }

        public void MakeMove(int row, int column, Action gameFinishedAction)
        {
            if (_user == null || !ReversiRules.IsValidMove(_model, row, column, _model.CurrentPlayer))
            {
                return;
            }

            _model = _service.MakeMove(_model.Id, row, column);
            LoadViewModel(_model);

            if (_service.IsGameOver(_model.Id))
            {
                _service.RemoveGame(_model.Id);

                // Статистика обновлена на стороне сервиса - перечитываем пользователя
                _user = _userService.GetUserById(_user.Id) ?? _user;
                OnPropertyChanged(nameof(StatisticsText));

                gameFinishedAction?.Invoke();
            }
        }

        public string GetResultText()
        {
            var black = BlackCount;
            var white = WhiteCount;

            switch (ReversiRules.GetResult(_model))
            {
                case GameResult.BlackWins:
                    return $"Вы победили! {black} : {white}";

                case GameResult.WhiteWins:
                    return $"Победил компьютер {white} : {black}";

                case GameResult.Draw:
                    return $"Ничья {black} : {white}";
            }

            return string.Empty;
        }

        private void LoadViewModel(GameModel model)
        {
            var validMoves = _user == null ?
                new List<CellPosition>() :
                ReversiRules.GetValidMoves(model, model.CurrentPlayer);

            Cells.Clear();
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Value = model[row, column],
                        IsHintVisible = IsHintShown && validMoves.Any(m => m.Row == row && m.Column == column),
                        IsLastMove = row == model.LastMoveRow && column == model.LastMoveColumn,
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(BlackCount));
            OnPropertyChanged(nameof(WhiteCount));
            OnPropertyChanged(nameof(StatusText));
            OnPropertyChanged(nameof(MessageText));
        }
    }
}
