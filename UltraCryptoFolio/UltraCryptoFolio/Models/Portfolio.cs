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
            Transactions = transactions ?? new List<Transaction>();
            CryptoValues = new List<CryptoValue>();
            MonetaryValues = new List<MonetaryValue>();

            CalculateMonetaryValueOfTransactions();
            CreateMonetaryValuesList();
            CreateCryptoValuesList();
        }

        private void CalculateMonetaryValueOfTransactions()
        {
            if (Transactions.Count < 1) { return; }
            foreach (var transaction in Transactions)
            {
                if (transaction.TransactionWorth == 0)
                {
                    decimal valueOfOneCrypto = 0;

                    switch (transaction.TransactionType)
                    {
                        case TransactionType.Investment:
                            var investment = (Investment)transaction;
                            transaction.TransactionWorth = Math.Round(investment.AmountSpent, 2);
                            break;
                        case TransactionType.Trade:
                            var trade = (Trade)transaction;
                            valueOfOneCrypto = _priceGetter.GetEuroPriceOnDateAsync(trade.SpendingCurrency, trade.DateTime).Result;
                            transaction.TransactionWorth = Math.Round(GetMonetaryValueOfCrypto(valueOfOneCrypto, trade.AmountSpent, trade.SpendingCurrency), 2);
                            break;
                        case TransactionType.Spend:
                            var spend = (Spend)transaction;
                            valueOfOneCrypto = _priceGetter.GetEuroPriceOnDateAsync(spend.SpendingCurrency, spend.DateTime).Result;
                            transaction.TransactionWorth = Math.Round(GetMonetaryValueOfCrypto(valueOfOneCrypto, spend.AmountSpent, spend.SpendingCurrency), 2);
                            break;
                        case TransactionType.Divestment:
                            var divestment = (Divestment)transaction;
                            transaction.TransactionWorth = Math.Round(divestment.AmountReceived, 2);
                            break;
                        case TransactionType.Dividend:
                            var dividend = (Dividend)transaction;
                            valueOfOneCrypto = _priceGetter.GetEuroPriceOnDateAsync(dividend.ReceivingCurrency, dividend.DateTime).Result;
                            transaction.TransactionWorth = Math.Round(GetMonetaryValueOfCrypto(valueOfOneCrypto, dividend.AmountReceived, dividend.ReceivingCurrency), 2);
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
                    decimal monetaryValueOfCrypto = GetMonetaryValueOfCrypto(valueOfOneCrypto, totalAmountCryptoCurrency, cryptoCurrencyType);

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

        private decimal GetMonetaryValueOfCrypto(decimal valueOfOneCrypto, long amountCrypto, CryptoCurrency cryptoCurrencyType)
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
            var cryptoValue = CryptoValues.FirstOrDefault(c => c.CryptoCurrency == cryptoCurrency);

            var currentValue = cryptoValue.MonetaryValue;

            if (cryptoValue.AmountInvested == 0 && currentValue >= 0)
            {
                return Decimal.MaxValue;
            }
            else if (currentValue == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round((((cryptoValue.MonetaryValue + cryptoValue.AmountDivested) / cryptoValue.AmountInvested) * 100) - 100, 2);
            }
        }

        public decimal GetTotalProfit()
        {
            return Math.Round(GetTotalValue() - GetTotalMonetaryInvestment(), 2);
        }

        public decimal GetTotalMonetaryInvestment()
        {
            return Transactions.Where(t => t.TransactionType == TransactionType.Investment).Sum(t => t.TransactionWorth);
        }
        public decimal GetTotalMonetaryDivestment()
        {
            return Transactions.Where(t => t.TransactionType == TransactionType.Divestment).Sum(t => t.TransactionWorth);
        }

        public decimal GetTotalValue()
        {
            return MonetaryValues.Sum(m => m.Amount) + CryptoValues.Sum(c => c.MonetaryValue);
        }

        public decimal GetTotalCryptoValue()
        {
            return Math.Round(CryptoValues.Sum(c => c.MonetaryValue), 2);
        }
    }
}
