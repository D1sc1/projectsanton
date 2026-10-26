using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.DataAccess.EF.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Draws { get; set; }

        public List<Game> Games { get; set; }
    }
}
