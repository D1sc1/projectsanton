using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.Common.BusinessModels
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Количество побед над компьютером
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Количество поражений
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Количество ничьих
        /// </summary>
        public int Draws { get; set; }
    }
}
