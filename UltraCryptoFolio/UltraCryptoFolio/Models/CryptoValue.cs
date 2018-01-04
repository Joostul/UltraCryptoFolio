using System;
using System.Collections.Generic;
using System.Linq;
using UltraCryptoFolio.Data;

namespace UltraCryptoFolio.Models
{
    public class CryptoValue
    {
        private decimal _monetaryValue;
        public CryptoCurrency CryptoCurrency { get; set; }
        public long Amount { get; set; }
        public decimal MonetaryValue
        {
            get
            {
                return Math.Round(_monetaryValue, 2);
            }
            set
            {
                _monetaryValue = value;
            }
        }

        public List<Investment> Investments
        {
            get
            {
                return ExampleTransactions.Transactions.Where(t => t.TransactionType == TransactionType.Investment).Cast<Investment>()
                    .Where(i => i.ReceivingCurrency == CryptoCurrency).ToList();
            }
        }

        public List<Divestment> Divestments
        {
            get
            {
                return ExampleTransactions.Transactions.Where(t => t.TransactionType == TransactionType.Divestment).Cast<Divestment>().ToList()
                    .Where(i => i.SpendingCurrency == CryptoCurrency).ToList();
            }
        }

        public List<Trade> Trades
        {
            get
            {
                return ExampleTransactions.Transactions.Where(t => t.TransactionType == TransactionType.Trade).Cast<Trade>().ToList()
                    .Where(i => i.ReceivingCurrency == CryptoCurrency || i.SpendingCurrency == CryptoCurrency).ToList();
            }
        }

        public List<Spend> Spends
        {
            get
            {
                return ExampleTransactions.Transactions.Where(t => t.TransactionType == TransactionType.Spend).Cast<Spend>().ToList()
                    .Where(i => i.SpendingCurrency == CryptoCurrency).ToList();
            }
        }

        public List<Dividend> Dividends
        {
            get
            {
                return ExampleTransactions.Transactions.Where(t => t.TransactionType == TransactionType.Dividend).Cast<Dividend>().ToList()
                    .Where(i => i.ReceivingCurrency == CryptoCurrency).ToList();
            }
        }

        public decimal AmountInvested
        {
            get
            {
                return (Investments.Sum(i => i.AmountSpent) 
                    + Trades.Where(t => t.ReceivingCurrency == CryptoCurrency).Sum(t => t.TransactionWorth));
            }
        }

        public decimal AmountDivested
        {
            get
            {
                return (Divestments.Sum(i => i.AmountReceived) 
                    + Trades.Where(t => t.SpendingCurrency == CryptoCurrency).Sum(t => t.TransactionWorth));
            }
        }

        public decimal CurrentProfit
        {
            get
            {
                return Math.Round(AmountDivested + Dividends.Sum(d => d.TransactionWorth) - AmountInvested + MonetaryValue, 2);
            }
        }
    }
}
