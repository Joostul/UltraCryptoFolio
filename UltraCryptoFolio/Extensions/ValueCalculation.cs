using System;
using System.Collections.Generic;
using System.Linq;
using UltraCryptoFolio.Helpers;
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

        public static decimal GetMonetaryValueOfTransactionsOnDate(List<Transaction> transactions, DateTime dateOfTransaction)
        {
            var relevantTransactions = transactions.Where(t => t.DateTime < dateOfTransaction).ToList();
            var portfolio = new Portfolio(new PriceGetter(dateOfTransaction), relevantTransactions);

            return portfolio.GetTotalCryptoValue();
        }
    }
}
