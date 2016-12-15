using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameAPISupport;

namespace RelaxMiniGame.Models
{
    public class VietlottVNViewModel
    {
        public List<int> ListFrequencyNumbers { get; set; }
        public List<NumberCustom> ListNumberCustom { get; set; }

        public VietlottVNViewModel()
        {
            ListFrequencyNumbers = new List<int>();
            ListNumberCustom = new List<NumberCustom>();
        }

    }
}