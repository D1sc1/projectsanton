using ReversiGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Business.Services
{
    /// <summary>
    /// Компьютерный соперник: выбирает ход по "весу" клетки и количеству перевернутых фишек.
    /// Углы ценнее всего (их нельзя перевернуть), клетки рядом с углами - опасны
    /// </summary>
    public class ComputerPlayer
    {
        private static readonly int[,] Weights =
        {
            { 100, -20, 10,  5,  5, 10, -20, 100, },
            { -20, -50, -2, -2, -2, -2, -50, -20, },
            {  10,  -2, -1, -1, -1, -1,  -2,  10, },
            {   5,  -2, -1, -1, -1, -1,  -2,   5, },
            {   5,  -2, -1, -1, -1, -1,  -2,   5, },
            {  10,  -2, -1, -1, -1, -1,  -2,  10, },
            { -20, -50, -2, -2, -2, -2, -50, -20, },
            { 100, -20, 10,  5,  5, 10, -20, 100, },
        };

        public CellPosition ChooseMove(GameService service, GameModel model, int player)
        {
            CellPosition bestMove = null;
            var bestScore = int.MinValue;

            foreach (var move in service.GetValidMoves(model, player))
            {
                var score = Weights[move.Row, move.Column] * 10 +
                    service.GetFlippedCells(model, move.Row, move.Column, player).Count;

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }
    }
}
