using ReversiGame.BusinessProxy.Infrastucture;
using ReversiGame.Common.BusinessDtos;
using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Contracts.Services;
using ReversiGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReversiGame.BusinessProxy.Services
{
    /// <summary>
    /// Клиентская реализация IGameService: все вызовы передаются на сервер ReversiGame.WebApi
    /// </summary>
    public class GameServiceProxy : IGameService
    {
        public GameModel GetByGameId(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/get-by-game-id/{gameId}").Result;
                return FromDto(HttpConnection.ReadReply<GameReply>(response));
            }
        }

        public GameModel GetByUserId(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/get-by-user-id/{userId}").Result;
                return FromDto(HttpConnection.ReadReply<GameReply>(response));
            }
        }

        public bool IsGameOver(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/is-over/{gameId}").Result;
                var reply = HttpConnection.ReadReply<BooleanReply>(response);
                return reply?.IsTrue ?? true;
            }
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var httpContent = JsonContent.Create(new MakeMoveRequest { GameId = gameId, Row = row, Column = column, });
                var response = httpClient.PostAsync("api/games/make-move", httpContent).Result;
                return FromDto(HttpConnection.ReadReply<GameReply>(response));
            }
        }

        public void RemoveGame(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.DeleteAsync($"api/games/remove/{gameId}").Result;
                response.EnsureSuccessStatusCode();
            }
        }

        private GameModel FromDto(GameReply game)
        {
            if (game == null)
            {
                return null;
            }

            var result = new GameModel
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                CurrentPlayer = game.CurrentPlayer,
                LastMoveRow = game.LastMoveRow,
                LastMoveColumn = game.LastMoveColumn,
                Message = game.Message,
            };

            int i = 0;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = game.Cells[i++];
                }
            }

            return result;
        }
    }
}
