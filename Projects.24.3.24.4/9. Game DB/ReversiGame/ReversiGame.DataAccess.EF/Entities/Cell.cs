using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.DataAccess.EF.Entities
{
    /// <summary>
    /// Занятая клетка доски. Пустые клетки в БД не хранятся
    /// </summary>
    public class Cell
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int Value { get; set; }

        public Game Game { get; set; }
    }
}
