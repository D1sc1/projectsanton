using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.BusinessDtos
{
    public class UserReply
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Draws { get; set; }
    }
}
