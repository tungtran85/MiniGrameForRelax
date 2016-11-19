using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelaxMiniGame.Models
{
    public class VietlottVNViewModel
    {
        public List<int> ListFrequencyNumbers { get; set; }

        public VietlottVNViewModel()
        {
            ListFrequencyNumbers = new List<int>();
        }

    }
}