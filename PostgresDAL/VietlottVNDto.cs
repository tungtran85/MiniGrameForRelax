using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresDAL
{
    public class VietlottVNDto
    {
        public int VietLottID { get; set; }

        public int DrawId { get; set; }

        public DateTime DayPrize { get; set; }

        public string FullBlockNumber { get; set; }

        public int NumberOne { get; set; }

        public int NumberTwo { get; set; }

        public int NumberThree { get; set; }

        public int NumberFour { get; set; }

        public int NumberFive { get; set; }

        public int NumberSix { get; set; }

        public DateTime ImportDate { get; set; }
    }
}
