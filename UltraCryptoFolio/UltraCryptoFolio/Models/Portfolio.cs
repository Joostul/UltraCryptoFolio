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

            CalculateTransactionsWorth();
            CalculateMonetaryValues();
            CalculateCryptoValues();
        }

        private void CalculateMonetaryValues()
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

        private void CalculateCryptoValues()
        {
            foreach (var cryptoCurrency in Enum.GetValues(typeof(CryptoCurrency)))
            {
                long totalAmountCryptoCurrency = 0;

                var investments = Transactions.Where(
                    t => t.TransactionType == TransactionType.Investment).Cast<Investment>()
                    .Where(i => i.ReceivingCurrency == (CryptoCurrency)cryptoCurrency).ToList();
                var divestments = Transactions.Where(
                    t => t.TransactionType == TransactionType.Divestment).Cast<Divestment>()
                    .Where(d => d.SpendingCurrency == (CryptoCurrency)cryptoCurrency).ToList();
                var trades = Transactions.Where(
                    t => t.TransactionType == TransactionType.Trade).Cast<Trade>()
                    .Where(t => t.ReceivingCurrency == (CryptoCurrency)cryptoCurrency || t.SpendingCurrency == (CryptoCurrency)cryptoCurrency).ToList();
                var spends = Transactions.Where(
                    t => t.TransactionType == TransactionType.Spend).Cast<Spend>()
                    .Where(t => t.SpendingCurrency == (CryptoCurrency)cryptoCurrency).ToList();
                var dividends = Transactions.Where(
                    t => t.TransactionType == TransactionType.Dividend).Cast<Dividend>()
                    .Where(d => d.ReceivingCurrency == (CryptoCurrency)cryptoCurrency).ToList();

                totalAmountCryptoCurrency = (investments.Sum(i => i.AmountReceived)
                    + dividends.Sum(d => d.AmountReceived)
                    - divestments.Sum(d => d.AmountSpent)
                    + trades.Where(t => t.ReceivingCurrency == (CryptoCurrency)cryptoCurrency)
                    .Sum(t => t.AmountReceived)
                    - trades.Where(t => t.SpendingCurrency == (CryptoCurrency)cryptoCurrency)
                    .Sum(t => t.AmountSpent)
                    - spends.Sum(t => t.AmountSpent));

                if (totalAmountCryptoCurrency != 0 || investments.Count > 0 || trades.Count > 0 || dividends.Count > 0)
                {
                    CryptoValues.Add(new CryptoValue()
                    {
                        CryptoCurrency = (CryptoCurrency)cryptoCurrency,
                        Amount = totalAmountCryptoCurrency,
                        Investments = investments,
                        Divestments = divestments,
                        Dividends = dividends,
                        Spends = spends,
                        Trades = trades
                    });
                }
            }
        }

        private void CalculateMonetaryValueOfCryptoCurrenciesIn(Currency currency)
        {
            CalculateTransactionsWorth();

            MonetaryCurrency = currency;
            foreach (var value in CryptoValues)
            {
                decimal valueOfOne = 0;
                if (MonetaryCurrency == Currency.Euro)
                {
                    valueOfOne = _priceGetter.GetEuroPriceOfAsync(value.CryptoCurrency).Result;
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

        public void CalculateTransactionsWorth()
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
                        default:
                            break;
                    }
                }

            }
        }

        public decimal PortfolioCryptoValue
        {
            get
            {
                return CryptoValues.Sum(v => v.MonetaryValue);
            }
        }

        public decimal PortfolioMonetaryValue
        {
            get
            {
                return MonetaryValues.Sum(v => v.Amount);
            }
        }

        public decimal GetCryptoValue(CryptoCurrency cryptoCurrency)
        {
            decimal monetaryValueOfCrypto = 0;

            //do
            {
                CalculateMonetaryValueOfCryptoCurrenciesIn(MonetaryCurrency);
                monetaryValueOfCrypto = CryptoValues.FirstOrDefault(c => c.CryptoCurrency == cryptoCurrency).MonetaryValue;

            } //while (monetaryValueOfCrypto == 0);

            return monetaryValueOfCrypto;
        }

        public decimal GetPercentHoldings(CryptoCurrency cryptoCurrency)
        {
            var cryptoValue = GetCryptoValue(cryptoCurrency);
            if(cryptoValue == 0m)
            {
                return 0m;
            }
            else
            {
                var percentHoldings = Math.Round(((cryptoValue / PortfolioCryptoValue) * 100), 2);
                return percentHoldings;
            }
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
                return PortfolioCryptoValue + PortfolioMonetaryValue;
            }
        }
    }
}
