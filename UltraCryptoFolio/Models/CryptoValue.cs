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
                decimal tradeValue = 0;
                decimal investmentValue = 0;

                if (Investments != null)
                {
                    investmentValue = Investments.Sum(i => i.AmountSpent);
                } 
                if(Trades != null)
                {
                    tradeValue = Trades.Where(t => t.ReceivingCurrency == CryptoCurrency).Sum(t => t.TransactionWorth);
                }
                
                return investmentValue + tradeValue ;
            }
        }

        public decimal AmountDivested
        {
            get
            {
                decimal tradeValue = 0;
                decimal divestmentValue = 0;

                if (Investments != null)
                {
                    divestmentValue = Divestments.Sum(i => i.AmountReceived);
                }
                if (Trades != null)
                {
                    tradeValue = Trades.Where(t => t.SpendingCurrency == CryptoCurrency).Sum(t => t.TransactionWorth);
                }

                return divestmentValue + tradeValue;
            }
        }

        public decimal AmountSpent
        {
            get
            {
                if (Spends != null) return Spends.Sum(s => s.TransactionWorth);

                return 0;
            }
        }

        public decimal DividendsReceived
        {
            get
            {
                if (Dividends != null) return Dividends.Sum(d => d.TransactionWorth);

                return 0;
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
