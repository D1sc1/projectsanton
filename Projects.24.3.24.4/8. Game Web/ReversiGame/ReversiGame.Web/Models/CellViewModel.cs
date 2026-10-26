using ReversiGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReversiGame.Web.Models
{
    public class CellViewModel
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public int Value { get; set; }

        public bool IsValidMove { get; set; }

        public bool IsLastMove { get; set; }

        /// <summary>
        /// CSS-класс фишки в клетке
        /// </summary>
        public string PieceCssClass
        {
            get
            {
                switch (Value)
                {
                    case GameModel.BlackValue:
                        return "piece piece-black";

                    case GameModel.WhiteValue:
                        return "piece piece-white";

                    default:
                        return string.Empty;
                }
            }
        }
    }
}
