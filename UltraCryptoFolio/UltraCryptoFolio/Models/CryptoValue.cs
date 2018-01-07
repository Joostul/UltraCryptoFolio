using System;
using System.Collections.Generic;
using System.Linq;

namespace UltraCryptoFolio.Models
{
    public class CryptoValue
    {
        private decimal _monetaryValue;
        public CryptoCurrency CryptoCurrency { get; set; }
        public long Amount { get; set; }
        public decimal MonetaryValue
        {
            get { return Math.Round(_monetaryValue, 2); }
            set { _monetaryValue = value; }
        }

        public List<Investment> Investments { get; set; }

        public List<Divestment> Divestments { get; set; }

        public List<Trade> Trades { get; set; }

        public List<Spend> Spends { get; set; }

        public List<Dividend> Dividends { get; set; }

        public decimal AmountInvested
        {
            get
            {
                return Investments.Sum(i => i.AmountSpent)
                    + Trades.Where(t => t.ReceivingCurrency == CryptoCurrency).Sum(t => t.TransactionWorth);
            }
        }

        public decimal AmountDivested
        {
            get
            {
                return Divestments.Sum(i => i.AmountReceived
                    + Trades.Where(t => t.SpendingCurrency == CryptoCurrency).Sum(t => t.TransactionWorth));
            }
        }

        public decimal AmountSpent
        {
            get
            {
                return Spends.Sum(s => s.TransactionWorth);
            }
        }

        public decimal DividendsReceived
        {
            get
            {
                return Dividends.Sum(d => d.TransactionWorth);
            }
        }


        public decimal CurrentProfit
        {
            get
            {
                return Math.Round(AmountDivested + DividendsReceived + AmountSpent - AmountInvested + MonetaryValue, 2);
            }
        }
    }
}
