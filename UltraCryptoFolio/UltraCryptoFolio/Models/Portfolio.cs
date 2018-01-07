using System;
using System.Collections.Generic;
using System.Linq;

namespace UltraCryptoFolio.Models
{
    public class Portfolio
    {
        private IPriceGetter _priceGetter;

        public List<Transaction> Transactions;
        public List<CryptoValue> CryptoValues { get; set; }
        public List<MonetaryValue> MonetaryValues { get; set; }
        public Currency MonetaryCurrency { get; set; }

        public Portfolio(IPriceGetter priceGetter, List<Transaction> transactions)
        {
            _priceGetter = priceGetter;
            Transactions = transactions;
            CryptoValues = new List<CryptoValue>();
            MonetaryValues = new List<MonetaryValue>();

            CalculateMonetaryValueOfTransactions();
            CreateMonetaryValuesList();
            CreateCryptoValuesList();
        }

        private void CalculateMonetaryValueOfTransactions()
        {
            foreach (var transaction in Transactions)
            {
                if (transaction.TransactionWorth == 0)
                {
                    switch (transaction.TransactionType)
                    {
                        case TransactionType.Investment:
                            var investment = (Investment)transaction;
                            transaction.TransactionWorth = investment.AmountSpent;
                            break;
                        case TransactionType.Trade:
                            var trade = (Trade)transaction;
                            transaction.TransactionWorth = _priceGetter.GetEuroPriceOnDateAsync(trade.SpendingCurrency, trade.DateTime).Result;
                            break;
                        case TransactionType.Spend:
                            var spend = (Spend)transaction;
                            transaction.TransactionWorth = _priceGetter.GetEuroPriceOnDateAsync(spend.SpendingCurrency, spend.DateTime).Result;
                            break;
                        case TransactionType.Divestment:
                            var divestment = (Divestment)transaction;
                            transaction.TransactionWorth = _priceGetter.GetEuroPriceOnDateAsync(divestment.SpendingCurrency, divestment.DateTime).Result;
                            break;
                        case TransactionType.Dividend:
                            var dividend = (Dividend)transaction;
                            transaction.TransactionWorth = _priceGetter.GetEuroPriceOnDateAsync(dividend.ReceivingCurrency, dividend.DateTime).Result;
                            break;
                    }
                }
            }
        }

        private void CreateCryptoValuesList()
        {
            foreach (var cryptoCurrency in Enum.GetValues(typeof(CryptoCurrency)))
            {
                long totalAmountCryptoCurrency = 0;
                CryptoCurrency cryptoCurrencyType = (CryptoCurrency)cryptoCurrency; 

                var investments = Transactions.Where(
                    t => t.TransactionType == TransactionType.Investment).Cast<Investment>()
                    .Where(i => i.ReceivingCurrency == cryptoCurrencyType).ToList();
                var divestments = Transactions.Where(
                    t => t.TransactionType == TransactionType.Divestment).Cast<Divestment>()
                    .Where(d => d.SpendingCurrency == cryptoCurrencyType).ToList();
                var trades = Transactions.Where(
                    t => t.TransactionType == TransactionType.Trade).Cast<Trade>()
                    .Where(t => t.ReceivingCurrency == cryptoCurrencyType || t.SpendingCurrency == cryptoCurrencyType).ToList();
                var spends = Transactions.Where(
                    t => t.TransactionType == TransactionType.Spend).Cast<Spend>()
                    .Where(t => t.SpendingCurrency == cryptoCurrencyType).ToList();
                var dividends = Transactions.Where(
                    t => t.TransactionType == TransactionType.Dividend).Cast<Dividend>()
                    .Where(d => d.ReceivingCurrency == cryptoCurrencyType).ToList();

                totalAmountCryptoCurrency = (investments.Sum(i => i.AmountReceived)
                    + dividends.Sum(d => d.AmountReceived)
                    - divestments.Sum(d => d.AmountSpent)
                    + trades.Where(t => t.ReceivingCurrency == cryptoCurrencyType)
                    .Sum(t => t.AmountReceived)
                    - trades.Where(t => t.SpendingCurrency == cryptoCurrencyType)
                    .Sum(t => t.AmountSpent)
                    - spends.Sum(t => t.AmountSpent));

                if (totalAmountCryptoCurrency != 0 || investments.Count > 0 || trades.Count > 0 || dividends.Count > 0)
                {
                    var valueOfOneCrypto = _priceGetter.GetEuroPriceOfAsync(cryptoCurrencyType).Result;
                    decimal monetaryValueOfCrypto = 0;
                    if (cryptoCurrencyType == CryptoCurrency.Stellar)
                    {
                        monetaryValueOfCrypto = (valueOfOneCrypto * totalAmountCryptoCurrency);
                    }
                    else
                    {
                        monetaryValueOfCrypto = (valueOfOneCrypto * totalAmountCryptoCurrency) / 100000000;
                    }

                    CryptoValues.Add(new CryptoValue()
                    {
                        CryptoCurrency = (CryptoCurrency)cryptoCurrency,
                        Amount = totalAmountCryptoCurrency,
                        Investments = investments,
                        Divestments = divestments,
                        Dividends = dividends,
                        Spends = spends,
                        Trades = trades,
                        MonetaryValue = monetaryValueOfCrypto
                    });
                }
            }
        }

        private void CreateMonetaryValuesList()
        {
            foreach (var currency in Enum.GetValues(typeof(Currency)))
            {
                decimal totalAmountCurrency = 0;

                var investments = Transactions.Where(
                    t => t.TransactionType == TransactionType.Investment).Cast<Investment>().ToList();
                var divestments = Transactions.Where(
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
        }

        public decimal GetPercentHoldings(CryptoCurrency cryptoCurrency)
        {
            var currentCryptoMonetaryValue = CryptoValues.FirstOrDefault(c => c.CryptoCurrency == cryptoCurrency).MonetaryValue;
            var totalPortfolioValue = CryptoValues.Sum(c => c.MonetaryValue);

            return Math.Round(((currentCryptoMonetaryValue / totalPortfolioValue) * 100), 2);
        }

        public decimal GetPercentGrowth(CryptoCurrency cryptoCurrency)
        {
            return 50;
        }

        public decimal GetTotalProfit()
        {
            return 1000;
        }

        public decimal GetTotalInvestment()
        {
            return 1000;
        }

        public decimal GetTotalValue()
        {
            return 1000;
        }
    }
}
