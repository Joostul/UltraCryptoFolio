using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Controllers
{
    public class TransactionsController : Controller
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

        public IActionResult Index()
        {
            return View(_transactions);
        }
        
        public IActionResult New()
        {
            return View("NewTransaction");
        }
        
        public IActionResult Save(Transaction transaction)
        {
            if(ModelState.IsValid)
            {
                _transactions.Add(transaction);
            }
            return View(_transactions);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}