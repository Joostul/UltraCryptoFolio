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

                totalAmountCurrency = (divestments.Where(d => d.ReceivingCurrency == (Currency)currency)
                    .Sum(d => d.AmountReceived))
                    - (investments.Where(i => i.SpendingCurrency == (Currency)currency)
                    .Sum(i => i.AmountSpent));

                if (totalAmountCurrency != 0)
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
                var dividends = transactions.Where(
                    t => t.TransactionType == TransactionType.Dividend).Cast<Dividend>().ToList();

                totalAmountCryptoCurrency = (investments.Where(i => i.ReceivingCurrency == (CryptoCurrency)cryptoCurrency)
                    .Sum(i => i.AmountReceived)
                    + dividends.Where(d => d.ReceivingCurrency == (CryptoCurrency)cryptoCurrency)
                    .Sum(d => d.AmountReceived)
                    - divestments.Where(d => d.SpendingCurrency == (CryptoCurrency)cryptoCurrency)
                    .Sum(d => d.AmountSpent)
                    + trades.Where(t => t.ReceivingCurrency == (CryptoCurrency)cryptoCurrency)
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

                    if (value.CryptoCurrency == CryptoCurrency.Stellar)
                    {
                        value.MonetaryValue = (valueOfOne * value.Amount);
                    }
                    else
                    {
                        value.MonetaryValue = ((valueOfOne * value.Amount) / 100000000);
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

        public decimal GetCryptoValue(CryptoCurrency cryptoCurrency)
        {
            return CryptoValues.FirstOrDefault(c => c.CryptoCurrency == cryptoCurrency).MonetaryValue;
        }

        public decimal GetPercentHoldings(CryptoCurrency cryptoCurrency)
        {
            var cryptoValue = GetCryptoValue(cryptoCurrency);
            var percentHoldings = Math.Round(((cryptoValue / TotalCryptoValue) * 100), 2);

            return percentHoldings;
        }

        public decimal GetPercentGrowth(CryptoCurrency cryptoCurrency)
        {
            var cryptoValue = CryptoValues.FirstOrDefault(c => c.CryptoCurrency == cryptoCurrency);

            var profit = cryptoValue.CurrentProfit;
            var invested = cryptoValue.AmountInvested - cryptoValue.AmountDivested - cryptoValue.AmountSpent;

            return Math.Round(((profit / invested) * 100), 2);
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
