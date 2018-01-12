using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Extensions
{
    public static class ValueCalculation
    {
        public static decimal GetMonetaryValueOfCrypto(decimal valueOfOneCrypto, long amountCrypto, CryptoCurrency cryptoCurrencyType)
        {
            if (cryptoCurrencyType == CryptoCurrency.Stellar)
            {
                return (valueOfOneCrypto * amountCrypto);
            }
            else
            {
                return (valueOfOneCrypto * amountCrypto) / 100000000;
            }
        }
    }
}
