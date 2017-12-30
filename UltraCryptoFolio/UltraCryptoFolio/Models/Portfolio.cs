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
                switch (MonetaryCurrency)
                {
                    case Currency.Euro:
                        foreach (var value in PortfolioValues)
                        {
                            var valueOfOne = (long)priceGetter.GetEuroPriceOfAsync(value.Currency).Result;

                            value.MonetaryValue = ((valueOfOne * value.Amount) / 100000000);
                        }
                        break;
                    case Currency.Dollar:
                        foreach (var value in PortfolioValues)
                        {
                            var valueOfOne = (long)priceGetter.GetDollarPriceOfAsync(value.Currency).Result;

                            value.MonetaryValue = ((valueOfOne * value.Amount) / 100000000);
                        }
                        break;
                    case Currency.Unknown:
                    case Currency.Bitcoin:
                    case Currency.BitcoinCash:
                    case Currency.Etherium:
                    case Currency.Ripple:
                    case Currency.Monero:
                    default:
                        break;
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
