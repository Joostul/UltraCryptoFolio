using System;
using System.Collections.Generic;
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

        public static decimal GetMonetaryValueOfTransactionsOnDate(List<Transaction> transactions, DateTime dateTime)
        {
            Random rnd = new Random();

            return rnd.Next(500,5000);
        }
    }
}
