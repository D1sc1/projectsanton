using ReversiGame.Business.Models;
using ReversiGame.Business.Services;
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
        private GameService _service = new GameService();

        private GameModel _model = new GameModel();

        private bool _isHintShown = true;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        /// <summary>
        /// Режим следующей партии: против компьютера или вдвоем за одним компьютером
        /// </summary>
        public bool IsVsComputer { get; set; } = true;

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

        public int BlackCount => _service.CountCells(_model, GameModel.BlackValue);

        public int WhiteCount => _service.CountCells(_model, GameModel.WhiteValue);

        public int MoveCount => _model.MoveCount;

        public string StatusText
        {
            get
            {
                if (_service.IsGameOver(_model))
                {
                    return GetResultText();
                }

                var player = GameModel.PlayerName(_model.CurrentPlayer);
                return _model.IsVsComputer ? $"Ваш ход ({player})" : $"Ходят {player}";
            }
        }

        public string MessageText => _model.Message ?? string.Empty;

        public string ModeText => _model.IsVsComputer ? "Вы играете черными против компьютера" : "Игра вдвоем";

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model, IsVsComputer);
            LoadViewModel(_model);
        }

        public void MakeMove(int row, int column, Action gameFinishedAction)
        {
            if (!_service.MakeMove(_model, row, column))
            {
                return;
            }

            LoadViewModel(_model);

            if (_service.IsGameOver(_model))
            {
                gameFinishedAction?.Invoke();
            }
        }

        public string GetResultText()
        {
            var black = BlackCount;
            var white = WhiteCount;

            switch (_service.GetResult(_model))
            {
                case GameResult.BlackWins:
                    return _model.IsVsComputer ? $"Вы победили! {black} : {white}" : $"Победили черные {black} : {white}";

                case GameResult.WhiteWins:
                    return _model.IsVsComputer ? $"Победил компьютер {white} : {black}" : $"Победили белые {white} : {black}";

                case GameResult.Draw:
                    return $"Ничья {black} : {white}";
            }

            return string.Empty;
        }

        private void LoadViewModel(GameModel model)
        {
            var validMoves = _service.GetValidMoves(model);

            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
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

            OnPropertyChanged(nameof(BlackCount));
            OnPropertyChanged(nameof(WhiteCount));
            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(StatusText));
            OnPropertyChanged(nameof(MessageText));
            OnPropertyChanged(nameof(ModeText));
        }
    }
}
