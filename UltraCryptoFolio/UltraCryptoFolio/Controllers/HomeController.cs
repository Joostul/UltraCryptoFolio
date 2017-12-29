using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Controllers
{
    public class HomeController : Controller
    {
        private List<Transaction> _transactions = new List<Transaction>()
        {
            new Transaction()
                {
                    AmountReceived = 4000000000,
                    AmountSpent = 1000000000,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReveicingCurrency = Currency.BitcoinCash,
                    SpendingCurrency = Currency.Bitcoin
                },
            new Transaction()
                {
                    AmountReceived = 3130000000,
                    AmountSpent = 1000,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReveicingCurrency = Currency.BitcoinCash,
                    SpendingCurrency = Currency.Euro
                }
        };

        private Portfolio _currencyPortfolio = new Portfolio();

        public IActionResult Index()
        {
            foreach (var currency in Enum.GetValues(typeof(Currency)))
            {
                long totalAmountCurrency = 0;

                var positiveTransactions = _transactions.Where(
                    t => t.ReveicingCurrency == (Currency)currency).ToList();
                var negativeTransactions = _transactions.Where(
                    t => t.SpendingCurrency == (Currency)currency).ToList();

                foreach (var pTransaction in positiveTransactions)
                {
                    totalAmountCurrency += pTransaction.AmountReceived;
                }

                foreach (var nTransaction in negativeTransactions)
                {
                    totalAmountCurrency -= nTransaction.AmountReceived;
                }

                if (totalAmountCurrency != 0)
                {
                    _currencyPortfolio.PortfolioValues.Add((Currency)currency, totalAmountCurrency);
                }
            }

            return View(_currencyPortfolio);
        }
    }
}
