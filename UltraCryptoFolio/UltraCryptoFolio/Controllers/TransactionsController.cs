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

        public IActionResult Create(Investment investment)
        {
            if(ModelState.IsValid)
            {
                _transactions.Add(investment);
            }
            return View(_transactions);
        }

        public IActionResult Create(Divestment divestment)
        {
            if (ModelState.IsValid)
            {
                _transactions.Add(divestment);
            }
            return View(_transactions);
        }

        public IActionResult Create(Trade trade)
        {
            if (ModelState.IsValid)
            {
                _transactions.Add(trade);
            }
            return View(_transactions);
        }

        public IActionResult Create(Spend spend)
        {
            if (ModelState.IsValid)
            {
                _transactions.Add(spend);
            }
            return View(_transactions);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}