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