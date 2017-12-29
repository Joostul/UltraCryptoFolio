using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltraCryptoFolio.Models
{
    public class Portfolio
    {
        public Portfolio()
        {
            PortfolioValues = new Dictionary<Currency, long>();
        }

        public Dictionary<Currency, long> PortfolioValues { get; set; }
    }
}
