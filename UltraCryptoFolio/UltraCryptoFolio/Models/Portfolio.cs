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
            CryptoValues = new List<CryptoValue>();
            MonetaryValues = new List<MonetaryValue>();
        }

        public void BuildPortfolioFromTransacions(List<Transaction> transactions)
        {
            foreach (var currency in Enum.GetValues(typeof(Currency)))
            {
                decimal totalAmountCurrency = 0;

                var investments = transactions.Where(
                    t => t.TransactionType == TransactionType.Investment).Cast<Investment>().ToList();
                var divestments = transactions.Where(
                    t => t.TransactionType == TransactionType.Divestment).Cast<Divestment>().ToList();

                totalAmountCurrency = (divestments.Where(d => d.Receivingurrency == (Currency)currency)
                    .Sum(d => d.AmountReceived)) 
                    - (investments.Where(i=> i.SpendingCurrency == (Currency)currency)
                    .Sum(i => i.AmountSpent));

                if(totalAmountCurrency != 0)
                {
                    MonetaryValues.Add(new MonetaryValue()
                    {
                        Amount = totalAmountCurrency,
                        Currency = (Currency)currency
                    });
                }
            }

            foreach (var cryptoCurrency in Enum.GetValues(typeof(CryptoCurrency)))
            {
                long totalAmountCryptoCurrency = 0;

                var investments = transactions.Where(
                    t => t.TransactionType == TransactionType.Investment).Cast<Investment>().ToList();
                var divestments = transactions.Where(
                    t => t.TransactionType == TransactionType.Divestment).Cast<Divestment>().ToList();
                var trades = transactions.Where(
                    t => t.TransactionType == TransactionType.Trade).Cast<Trade>().ToList();
                var spends = transactions.Where(
                    t => t.TransactionType == TransactionType.Spend).Cast<Spend>().ToList();

                totalAmountCryptoCurrency = (investments.Where(i => i.ReveicingCurrency == (CryptoCurrency)cryptoCurrency)
                    .Sum(i => i.AmountReceived)
                    - divestments.Where(d => d.SpendingCurrency == (CryptoCurrency)cryptoCurrency)
                    .Sum(d => d.AmountSpent)
                    + trades.Where(t => t.ReveicingCurrency == (CryptoCurrency)cryptoCurrency)
                    .Sum(t => t.AmountReceived)
                    - trades.Where(t => t.SpendingCurrency == (CryptoCurrency)cryptoCurrency)
                    .Sum(t => t.AmountSpent)
                    - spends.Where(t => t.SpendingCurrency == (CryptoCurrency)cryptoCurrency)
                    .Sum(t => t.AmountSpent));

                if (totalAmountCryptoCurrency != 0)
                {
                    CryptoValues.Add(new CryptoValue()
                    {
                        CryptoCurrency = (CryptoCurrency)cryptoCurrency,
                        Amount = totalAmountCryptoCurrency
                    });
                }
            }
        }

        public void CalculateMonetaryValue(Currency currency)
        {
            MonetaryCurrency = currency;

            using (var priceGetter = new PriceGetter())
            {
                foreach (var value in CryptoValues)
                {
                    decimal valueOfOne = 0;
                    if (MonetaryCurrency == Currency.Euro)
                    {
                        valueOfOne = priceGetter.GetEuroPriceOfAsync(value.CryptoCurrency).Result;
                    }
                    //if (MonetaryCurrency == Currency.Dollar)
                    //{
                    //    valueOfOne = priceGetter.GetDollarPriceOfAsync(value.CryptoCurrency).Result;
                    //}

                    if (value.CryptoCurrency == CryptoCurrency.Bitcoin || value.CryptoCurrency == CryptoCurrency.BitcoinCash)
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

        public List<CryptoValue> CryptoValues { get; set; }
        public List<MonetaryValue> MonetaryValues { get; set; }
        public Currency MonetaryCurrency { get; set; }

        public decimal TotalCryptoValue
        {
            get
            {
                return CryptoValues.Sum(v => v.MonetaryValue);
            }
        }

        public decimal TotalMonetaryValue
        {
            get
            {
                return MonetaryValues.Sum(v => v.Amount);
            }
        }

        public decimal TotalProfit
        {
            get
            {
                return TotalCryptoValue + TotalMonetaryValue;
            }
        }
    }
}
