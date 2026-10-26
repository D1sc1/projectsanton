using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Repositories;
using ReversiGame.Common.Contracts.Services;
using ReversiGame.Common.Definitions;
using ReversiGame.Common.Dtos;
using ReversiGame.Common.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        private readonly IUserRepository _userRepository;

        private readonly ComputerPlayer _computerPlayer = new ComputerPlayer();

        public GameService(IGameRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            var game = _repository.GetByUserId(userId)
                .Select(FromDto)
                .LastOrDefault(g => !ReversiRules.IsGameOver(g));

            if (game != null)
            {
                return game;
            }

            game = new GameModel
            {
                UserId = userId,
            };

            ReversiRules.Initialize(game);
            var gameId = _repository.Save(ToDto(game));

            return GetByGameId(gameId);
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return dto == null || ReversiRules.IsGameOver(FromDto(dto));
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            var dto = _repository.GetByGameId(gameId);
            var model = FromDto(dto);
            if (model == null)
            {
                return null;
            }

            if (!MakeMove(model, row, column))
            {
                return model;
            }

            _repository.Save(ToDto(model));

            if (ReversiRules.IsGameOver(model))
            {
                _userRepository.AddGameResult(model.UserId, ReversiRules.GetResult(model));
            }

            return model;
        }

        /// <summary>
        /// Ход человека (черные) и ответные ходы компьютера (белые).
        /// Компьютер ходит, пока у черных нет ходов и игра не окончена
        /// </summary>
        public bool MakeMove(GameModel model, int row, int column)
        {
            if (ReversiRules.IsGameOver(model) ||
                model.CurrentPlayer != Constants.HumanPlayer ||
                !ReversiRules.IsValidMove(model, row, column, model.CurrentPlayer))
            {
                return false;
            }

            model.Message = null;
            ReversiRules.PlacePiece(model, row, column);

            while (!ReversiRules.IsGameOver(model) && model.CurrentPlayer == Constants.ComputerPlayer)
            {
                var move = _computerPlayer.ChooseMove(model, Constants.ComputerPlayer);
                if (move == null)
                {
                    break;
                }

                ReversiRules.PlacePiece(model, move.Row, move.Column);
            }

            return true;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private GameModel FromDto(GameDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                CurrentPlayer = dto.CurrentPlayer,
                LastMoveRow = dto.LastMoveRow,
                LastMoveColumn = dto.LastMoveColumn,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = dto.Cells[row, column];
                }
            }

            return result;
        }

        private GameDto ToDto(GameModel model)
        {
            var result = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                CurrentPlayer = model.CurrentPlayer,
                LastMoveRow = model.LastMoveRow,
                LastMoveColumn = model.LastMoveColumn,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.Cells[row, column] = model[row, column];
                }
            }

            return result;
        }
    }
}
