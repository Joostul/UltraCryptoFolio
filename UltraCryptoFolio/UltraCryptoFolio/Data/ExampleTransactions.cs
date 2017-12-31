using System;
using System.Collections.Generic;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Data
{
    public static class ExampleTransactions
    {
        public static List<Transaction> Transactions => new List<Transaction>()
        {
            new Investment()
                {
                    AmountReceived = 110000000,
                    AmountSpent = 1000.10m,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReceivingCurrency = CryptoCurrency.Bitcoin,
                    SpendingCurrency = Currency.Euro
                },
            new Trade()
                {
                    AmountReceived = 400000000,
                    AmountSpent = 100000000,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReceivingCurrency = CryptoCurrency.BitcoinCash,
                    SpendingCurrency = CryptoCurrency.Bitcoin
                },
            new Investment()
                {
                    AmountReceived = 313000000,
                    AmountSpent = 1000.00m,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReceivingCurrency = CryptoCurrency.BitcoinCash,
                    SpendingCurrency = Currency.Euro
                }
        };
    }
}
