using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Business.Models
{
    public enum GameResult
    {
        /// <summary>
        /// Партия не окончена
        /// </summary>
        None,

        BlackWins,

        WhiteWins,

        Draw,
    }
}
