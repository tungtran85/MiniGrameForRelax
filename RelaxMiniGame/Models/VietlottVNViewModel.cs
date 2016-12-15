using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameAPISupport;

namespace RelaxMiniGame.Models
{
    public class VietlottVNViewModel
    {
        public List<int> ListFrequencyNumbersMin { get; set; }

        public List<int> ListFrequencyNumbersMax { get; set; }

        public List<NumberCustom> ListNumberCustom { get; set; }

        public int TotalRound { get; set; }

        public VietlottVNViewModel()
        {
            ListFrequencyNumbersMin = new List<int>();
            ListFrequencyNumbersMax = new List<int>();
            ListNumberCustom = new List<NumberCustom>();
        }

    }
}