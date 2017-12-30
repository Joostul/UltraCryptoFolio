using System;
using System.Collections.Generic;
using System.Linq;
using UltraCryptoFolio.Helpers;

namespace UltraCryptoFolio.Models
{
    public class Portfolio
    {
        public Portfolio()
        {
            PortfolioValues = new List<Value>();
        }

        public void BuildPortfolioFromTransacions(List<Transaction> transactions)
        {
            foreach (var currency in Enum.GetValues(typeof(Currency)))
            {
                long totalAmountCurrency = 0;

                var positiveTransactions = transactions.Where(
                    t => t.ReveicingCurrency == (Currency)currency).ToList();
                var negativeTransactions = transactions.Where(
                    t => t.SpendingCurrency == (Currency)currency).ToList();

                foreach (var pTransaction in positiveTransactions)
                {
                    totalAmountCurrency += pTransaction.AmountReceived;
                }

                foreach (var nTransaction in negativeTransactions)
                {
                    totalAmountCurrency -= nTransaction.AmountSpent;
                }

                if (totalAmountCurrency != 0)
                {
                    PortfolioValues.Add(new Value()
                    {
                        Currency = (Currency)currency,
                        Amount = totalAmountCurrency
                    });
                }
            }
        }

        public void CalculateMonetaryValue(Currency currency)
        {
            MonetaryCurrency = currency;

            using (var priceGetter = new PriceGetter())
            {
                foreach (var value in PortfolioValues)
                {
                    double valueOfOne = 0;
                    if(MonetaryCurrency == Currency.Euro)
                    {
                        valueOfOne = priceGetter.GetEuroPriceOfAsync(value.Currency).Result;
                    } if(MonetaryCurrency == Currency.Dollar)
                    {
                        valueOfOne = priceGetter.GetDollarPriceOfAsync(value.Currency).Result;
                    }

                    if (value.Currency == Currency.Bitcoin || value.Currency == Currency.BitcoinCash)
                    {
                        value.MonetaryValue = (long)((valueOfOne * value.Amount) / 100000000);
                    }
                    else
                    {
                        value.MonetaryValue = (long)(valueOfOne * value.Amount);
                    }
                }
            }
        }

        //public void AddTransactions(List<Transaction> transactions)
        //{ 
        //    foreach (var transaction in transactions)
        //    {

        //    }
        //}

        public List<Value> PortfolioValues { get; set; }
        public Currency MonetaryCurrency { get; set; }
    }
}
