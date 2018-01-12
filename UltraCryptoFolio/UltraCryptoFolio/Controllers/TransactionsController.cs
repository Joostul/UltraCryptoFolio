using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Data;
using UltraCryptoFolio.Extensions;
using UltraCryptoFolio.Helpers;
using UltraCryptoFolio.Models;
using UltraCryptoFolio.ViewModels;

namespace UltraCryptoFolio.Controllers
{
    public class TransactionsController : Controller
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public void SetTransactionList(List<Transaction> transactions)
        {
            HttpContext.Session.Set(Constants.SessionKeyInvestments, transactions.Where(t => t.TransactionType == TransactionType.Investment));
            HttpContext.Session.Set(Constants.SessionKeyDivestments, transactions.Where(t => t.TransactionType == TransactionType.Divestment));
            HttpContext.Session.Set(Constants.SessionKeySpends, transactions.Where(t => t.TransactionType == TransactionType.Spend));
            HttpContext.Session.Set(Constants.SessionKeyTrades, transactions.Where(t => t.TransactionType == TransactionType.Trade));
            HttpContext.Session.Set(Constants.SessionKeyDividends, transactions.Where(t => t.TransactionType == TransactionType.Dividend));
        }

        public List<Transaction> GetTransactionList()
        {
            List<Transaction> transactionList = new List<Transaction>();
            var investments = HttpContext.Session.Get<List<Investment>>(Constants.SessionKeyInvestments);
            var divestments = HttpContext.Session.Get<List<Divestment>>(Constants.SessionKeyDivestments);
            var spends = HttpContext.Session.Get<List<Spend>>(Constants.SessionKeySpends);
            var trades = HttpContext.Session.Get<List<Trade>>(Constants.SessionKeyTrades);
            var dividends = HttpContext.Session.Get<List<Dividend>>(Constants.SessionKeyDividends);

            if (investments != null) { transactionList.AddRange(investments); }
            if (divestments != null) { transactionList.AddRange(divestments); }
            if (spends != null) { transactionList.AddRange(spends); }
            if (trades != null) { transactionList.AddRange(trades); }
            if (dividends != null) { transactionList.AddRange(dividends); }

            return transactionList.OrderByDescending(t => t.DateTime).ToList();
        }

        public IActionResult Index()
        {
            _transactions = GetTransactionList();
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
        public IActionResult NewDividend()
        {
            return View("NewDividend");
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
                    TransactionType = TransactionType.Divestment,
                    TransactionWorth = Math.Round(divestmentViewModel.AmountReceived, 2)
                };

                _transactions.Add(divestment);
                _transactions.AddRange(GetTransactionList());
                SetTransactionList(_transactions);
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
                    TransactionType = TransactionType.Investment,
                    TransactionWorth = Math.Round(investmentViewModel.AmountSpent, 2)
                };

                _transactions.Add(investment);
                _transactions.AddRange(GetTransactionList());
                SetTransactionList(_transactions);
            }

            return View("Index", _transactions);
        }

        [HttpPost]
        public IActionResult NewTrade(TradeViewModel tradeViewModel)
        {
            if (ModelState.IsValid)
            {
                decimal transactionWorth = 0;
                using(var priceGetter = new PriceGetter())
                {
                    decimal valueOfOneCrypto = priceGetter.GetEuroPriceOnDateAsync(tradeViewModel.SpendingCurrency, DateTime.UtcNow).Result;
                    transactionWorth = ValueCalculation.GetMonetaryValueOfCrypto(valueOfOneCrypto, tradeViewModel.AmountSpent, tradeViewModel.SpendingCurrency);
                }

                Trade trade = new Trade()
                {
                    AmountReceived = tradeViewModel.AmountReceived,
                    AmountSpent = tradeViewModel.AmountSpent,
                    DateTime = tradeViewModel.DateTime,
                    ExchangeRate = tradeViewModel.ExchangeRate,
                    Fee = tradeViewModel.Fee,
                    ReceivingCurrency = tradeViewModel.ReceivingCurrency,
                    SpendingCurrency = tradeViewModel.SpendingCurrency,
                    TransactionType = TransactionType.Trade,
                    TransactionWorth = transactionWorth
                };

                _transactions.Add(trade);
                _transactions.AddRange(GetTransactionList());
                SetTransactionList(_transactions);
            }

            return View("Index", _transactions);
        }

        [HttpPost]
        public IActionResult NewSpend(SpendViewModel spendViewModel)
        {
            if (ModelState.IsValid)
            {
                decimal transactionWorth = 0;
                using (var priceGetter = new PriceGetter())
                {
                    decimal valueOfOneCrypto = priceGetter.GetEuroPriceOnDateAsync(spendViewModel.SpendingCurrency, DateTime.UtcNow).Result;
                    transactionWorth = ValueCalculation.GetMonetaryValueOfCrypto(valueOfOneCrypto, spendViewModel.AmountSpent, spendViewModel.SpendingCurrency);
                }

                Spend spend = new Spend()
                {
                    AmountSpent = spendViewModel.AmountSpent,
                    DateTime = spendViewModel.DateTime,
                    Fee = spendViewModel.Fee,
                    SpendingCurrency = spendViewModel.SpendingCurrency,
                    TransactionType = TransactionType.Spend,
                    TransactionWorth = transactionWorth
                };

                _transactions.Add(spend);
                _transactions.AddRange(GetTransactionList());
                SetTransactionList(_transactions);
            }

            return View("Index", _transactions);
        }

        [HttpPost]
        public IActionResult NewDividend(DividendViewModel dividendViewModel)
        {
            if (ModelState.IsValid)
            {
                decimal transactionWorth = 0;
                using (var priceGetter = new PriceGetter())
                {
                    decimal valueOfOneCrypto = priceGetter.GetEuroPriceOnDateAsync(dividendViewModel.ReceivingCurrency, DateTime.UtcNow).Result;
                    transactionWorth = ValueCalculation.GetMonetaryValueOfCrypto(valueOfOneCrypto, dividendViewModel.AmountReceived, dividendViewModel.ReceivingCurrency);
                }

                Dividend dividend = new Dividend()
                {
                    AmountReceived = dividendViewModel.AmountReceived,
                    DateTime = dividendViewModel.DateTime,
                    Fee = dividendViewModel.Fee,
                    ReceivingCurrency = dividendViewModel.ReceivingCurrency,
                    TransactionType = TransactionType.Dividend,
                    TransactionWorth = transactionWorth
                };

                _transactions.Add(dividend);
                _transactions.AddRange(GetTransactionList());
                SetTransactionList(_transactions);
            }

            return View("Index", _transactions);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}