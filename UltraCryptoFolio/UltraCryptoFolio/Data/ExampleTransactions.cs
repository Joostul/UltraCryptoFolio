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
                AmountReceived = 100000000,
                AmountSpent = 329.11m,
                DateTime = new DateTime(2014,2,11),
                ReceivingCurrency = CryptoCurrency.Bitcoin,
                SpendingCurrency = Currency.Euro
            },
            new Investment()
            {
                AmountReceived = 100000000,
                AmountSpent = 340.49m,
                DateTime = new DateTime(2014,3,30),
                ReceivingCurrency = CryptoCurrency.Bitcoin,
                SpendingCurrency = Currency.Euro
            },
            new Divestment()
            {
                AmountReceived = 357.39m,
                AmountSpent = 80000000,
                DateTime = new DateTime(2017,3,20),
                SpendingCurrency = CryptoCurrency.Bitcoin,
                ReceivingCurrency = Currency.Euro
            },
            new Investment()
            {
                AmountReceived = 100000000,
                AmountSpent = 446.80m,
                DateTime = new DateTime(2014,8,10),
                ReceivingCurrency = CryptoCurrency.Bitcoin,
                SpendingCurrency = Currency.Euro
            },
            new Investment()
            {
                AmountReceived = 100000000,
                AmountSpent = 365.03m,
                DateTime = new DateTime(2014,8,18),
                ReceivingCurrency = CryptoCurrency.Bitcoin,
                SpendingCurrency = Currency.Euro
            },
            new Divestment()
            {
                AmountReceived = 243.27m,
                AmountSpent = 65000000,
                DateTime = new DateTime(2014,8,25),
                SpendingCurrency = CryptoCurrency.Bitcoin,
                ReceivingCurrency = Currency.Euro
            },
            new Investment()
            {
                AmountReceived = 100000000,
                AmountSpent = 221.61m,
                DateTime = new DateTime(2015,6,16),
                ReceivingCurrency = CryptoCurrency.Bitcoin,
                SpendingCurrency = Currency.Euro
            },
            new Investment()
            {
                AmountReceived = 100000000,
                AmountSpent = 209.94m,
                DateTime = new DateTime(2015,9,13),
                ReceivingCurrency = CryptoCurrency.Bitcoin,
                SpendingCurrency = Currency.Euro
            },
            new Divestment()
            {
                AmountReceived = 231.88m,
                AmountSpent = 60000000,
                DateTime = new DateTime(2016,2,27),
                SpendingCurrency = CryptoCurrency.Bitcoin,
                ReceivingCurrency = Currency.Euro
            },
            new Divestment()
            {
                AmountReceived = 773.31m,
                AmountSpent = 200000000,
                DateTime = new DateTime(2016,5,22),
                SpendingCurrency = CryptoCurrency.Bitcoin,
                ReceivingCurrency = Currency.Euro
            },
            new Investment()
            {
                AmountReceived = 100000000,
                AmountSpent = 513.26m,
                DateTime = new DateTime(2016,6,3),
                ReceivingCurrency = CryptoCurrency.Bitcoin,
                SpendingCurrency = Currency.Euro
            },
            new Investment()
            {
                AmountReceived = 66051125,
                AmountSpent = 450.00m,
                DateTime = new DateTime(2016,6,18),
                ReceivingCurrency = CryptoCurrency.Bitcoin,
                SpendingCurrency = Currency.Euro
            },
            new Investment()
            {
                AmountReceived = 50000000,
                AmountSpent = 307.07m,
                DateTime = new DateTime(2016,7,4),
                ReceivingCurrency = CryptoCurrency.Bitcoin,
                SpendingCurrency = Currency.Euro
            },
            new Investment()
            {
                AmountReceived = 100000000,
                AmountSpent = 775.96m,
                DateTime = new DateTime(2017,1,11),
                ReceivingCurrency = CryptoCurrency.Bitcoin,
                SpendingCurrency = Currency.Euro
            },
            new Divestment()
            {
                AmountReceived = 2477.96m,
                AmountSpent = 250000000,
                DateTime = new DateTime(2017,3,17),
                SpendingCurrency = CryptoCurrency.Bitcoin,
                ReceivingCurrency = Currency.Euro
            },
            new Divestment()
            {
                AmountReceived = 1910.63m,
                AmountSpent = 100000000,
                DateTime = new DateTime(2017,5,27),
                SpendingCurrency = CryptoCurrency.Bitcoin,
                ReceivingCurrency = Currency.Euro
            },
            new Investment()
            {
                AmountReceived = 337894800,
                AmountSpent = 1000m,
                DateTime = new DateTime(2017,8,8),
                ReceivingCurrency = CryptoCurrency.BitcoinCash,
                SpendingCurrency = Currency.Euro
            },
            new Trade()
            {
                AmountReceived = 50000000,
                AmountSpent = 0,
                DateTime = new DateTime(2017,8,1),
                ReceivingCurrency = CryptoCurrency.BitcoinCash,
                SpendingCurrency = CryptoCurrency.Bitcoin
            },
            new Spend()
            {
                AmountSpent = 111051125,
                DateTime = new DateTime(2017,8,1),
                SpendingCurrency = CryptoCurrency.Bitcoin
            },
            new Trade()
            {
                AmountReceived = 50000000,
                AmountSpent = 0,
                DateTime = new DateTime(2017,8,8),
                ReceivingCurrency = CryptoCurrency.BitcoinGold,
                SpendingCurrency = CryptoCurrency.Bitcoin
            },
            new Trade()
            {
                AmountReceived = 414135088,
                AmountSpent = 40000000,
                DateTime = new DateTime(2017,12,9),
                ReceivingCurrency = CryptoCurrency.BitcoinCash,
                SpendingCurrency = CryptoCurrency.Bitcoin
            },
            new Trade()
            {
                AmountReceived = 36899606,
                AmountSpent = 50000000,
                DateTime = new DateTime(2017,12,20),
                ReceivingCurrency = CryptoCurrency.Monero,
                SpendingCurrency = CryptoCurrency.BitcoinGold
            }
        };
    }
}
