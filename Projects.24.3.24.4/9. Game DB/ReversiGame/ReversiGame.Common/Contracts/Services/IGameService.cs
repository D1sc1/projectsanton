using ReversiGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.Contracts.Services
{
    public interface IGameService
    {
        GameModel GetByGameId(int gameId);

        /// <summary>
        /// Текущая (незаконченная) партия игрока; если ее нет - создается новая
        /// </summary>
        GameModel GetByUserId(int userId);

        bool IsGameOver(int gameId);

        /// <summary>
        /// Ход игрока в клетку (row, column) и ответный ход компьютера
        /// </summary>
        GameModel MakeMove(int gameId, int row, int column);

        void RemoveGame(int gameId);
    }
}
