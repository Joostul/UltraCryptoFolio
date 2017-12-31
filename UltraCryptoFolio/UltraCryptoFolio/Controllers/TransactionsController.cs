using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Data;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Controllers
{
    public class TransactionsController : Controller
    {
        private List<Transaction> _transactions = ExampleTransactions.Transactions;

        public IActionResult Index()
        {
            return View(_transactions);
        }
        
        public IActionResult NewInvestment()
        {
            return View("NewInvestment");
        }

        public IActionResult NewTrade()
        {
            return View("NewTrade");
        }

        public IActionResult NewSpend()
        {
            return View("NewSpend");
        }

        public IActionResult NewDivestment()
        {
            return View("NewDivestment");
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