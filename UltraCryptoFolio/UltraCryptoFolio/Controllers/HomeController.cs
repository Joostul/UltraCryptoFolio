using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Data;
using UltraCryptoFolio.Extensions;
using UltraCryptoFolio.Helpers;
using UltraCryptoFolio.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UltraCryptoFolio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
#if DEBUG
            var _transactions = ExampleTransactions.Transactions;
#else
            var _transactions = GetTransactions();
#endif
            if(_transactions.Count < 1)
            {
                return View(new Portfolio(new PriceGetter(), new List<Transaction>()));
            }

            var portfolio = new Portfolio(new PriceGetter(), _transactions);
            SetTransactions(_transactions);

            return View(portfolio);
        }

        public IActionResult ExportPortfolio()
        {
            var portfolio = GetTransactions();
            var fileName = "UltraCryptoFolio.txt";

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot",
                           fileName);

            using (var file = System.IO.File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, portfolio);
            }
            var txtfile = File(path, "text/plain", Path.GetFileName(path));

            return txtfile;
        }

        public IActionResult ImportPortfolio()
        {
            return RedirectToAction("Index", new Portfolio(new PriceGetter(), GetTransactions()));
        }

        private void SetTransactions(List<Transaction> transactions)
        {
            HttpContext.Session.Set(Constants.SessionKeyInvestments, transactions.Where(t => t.TransactionType == TransactionType.Investment));
            HttpContext.Session.Set(Constants.SessionKeyDivestments, transactions.Where(t => t.TransactionType == TransactionType.Divestment));
            HttpContext.Session.Set(Constants.SessionKeySpends, transactions.Where(t => t.TransactionType == TransactionType.Spend));
            HttpContext.Session.Set(Constants.SessionKeyTrades, transactions.Where(t => t.TransactionType == TransactionType.Trade));
            HttpContext.Session.Set(Constants.SessionKeyDividends, transactions.Where(t => t.TransactionType == TransactionType.Dividend));
            //HttpContext.Session.Set(Constants.SessionKeyCryptoValues, _portfolio.CryptoValues);
            //HttpContext.Session.Set(Constants.SessionKeyMonetaryValues, _portfolio.MonetaryValues);
        }

        private List<Transaction> GetTransactions()
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

            return transactionList;
        }
    }
}
