using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Data;
using UltraCryptoFolio.Models;
using UltraCryptoFolio.ViewModels;

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

        [HttpPost]
        public IActionResult NewDivestment(DivestmentViewModel divestmentViewModel)
        {
            if (ModelState.IsValid)
            {
                Divestment divestment = new Divestment()
                {
                    AmountReceived = divestmentViewModel.AmountReceived,
                    AmountSpent = divestmentViewModel.AmountSpent,
                    DateTime = divestmentViewModel.DateTime,
                    ExchangeRate = divestmentViewModel.ExchangeRate,
                    Fee = divestmentViewModel.Fee,
                    ReceivingCurrency = divestmentViewModel.Receivingurrency,
                    SpendingCurrency = divestmentViewModel.SpendingCurrency,
                    TransactionType = TransactionType.Divestment
                };

                _transactions.Add(divestment);
            }

            return View("Index", _transactions);
        }

        [HttpPost]
        public IActionResult NewInvestment(InvestmentViewModel investmentViewModel)
        {

            if (ModelState.IsValid)
            {
                Investment investment = new Investment()
                {
                    AmountReceived = investmentViewModel.AmountReceived,
                    AmountSpent = investmentViewModel.AmountSpent,
                    DateTime = investmentViewModel.DateTime,
                    ExchangeRate = investmentViewModel.ExchangeRate,
                    Fee = investmentViewModel.Fee,
                    ReceivingCurrency = investmentViewModel.ReceivingCurrency,
                    SpendingCurrency = investmentViewModel.SpendingCurrency,
                    TransactionType = TransactionType.Investment
                };

                _transactions.Add(investment);
            }

            return View("Index", _transactions);
        }

        [HttpPost]
        public IActionResult NewTrade(TradeViewModel tradeViewModel)
        {
            if (ModelState.IsValid)
            {
                Trade trade = new Trade()
                {
                    AmountReceived = tradeViewModel.AmountReceived,
                    AmountSpent = tradeViewModel.AmountSpent,
                    DateTime = tradeViewModel.DateTime,
                    ExchangeRate = tradeViewModel.ExchangeRate,
                    Fee = tradeViewModel.Fee,
                    ReceivingCurrency = tradeViewModel.ReceivingCurrency,
                    SpendingCurrency = tradeViewModel.SpendingCurrency,
                    TransactionType = TransactionType.Trade
                };

                _transactions.Add(trade);
            }

            return View("Index", _transactions);
        }

        [HttpPost]
        public IActionResult NewSpend(SpendViewModel spendViewModel)
        {
            if (ModelState.IsValid)
            {
                Spend spend = new Spend()
                {
                    AmountSpent = spendViewModel.AmountSpent,
                    DateTime = spendViewModel.DateTime,
                    Fee = spendViewModel.Fee,
                    SpendingCurrency = spendViewModel.SpendingCurrency,
                    TransactionType = TransactionType.Spend
                };

                _transactions.Add(spend);
            }

            return View("Index", _transactions);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}