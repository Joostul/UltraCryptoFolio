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
using System.Text;

namespace UltraCryptoFolio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
//#if DEBUG
//            var _transactions = ExampleTransactions.Transactions;
//#else
            var _transactions = GetTransactions();
//#endif
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

            byte[] myFile = ObjectToByteArray(portfolio);
            return File(myFile, "test/plain", fileName);
        }

        public IActionResult ImportPortfolio()
        {
            return View("ImportPortfolio");
        }

        [HttpPost]
        public IActionResult ImportPortfolio(IFormFile file)
        {
            List<Transaction> transactions;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(reader.ReadToEnd());
            }

            SetTransactions(transactions);

            return RedirectToAction("Index");
        }

        // Convert an object to a byte array
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            TextWriter textWriter = new StringWriter();
            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(textWriter, obj);

            var encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(textWriter.ToString());

            return bytes;
        }

        private void SetTransactions(List<Transaction> transactions)
        {
            HttpContext.Session.Set(Constants.SessionKeyInvestments, transactions.Where(t => t.TransactionType == TransactionType.Investment));
            HttpContext.Session.Set(Constants.SessionKeyDivestments, transactions.Where(t => t.TransactionType == TransactionType.Divestment));
            HttpContext.Session.Set(Constants.SessionKeySpends, transactions.Where(t => t.TransactionType == TransactionType.Spend));
            HttpContext.Session.Set(Constants.SessionKeyTrades, transactions.Where(t => t.TransactionType == TransactionType.Trade));
            HttpContext.Session.Set(Constants.SessionKeyDividends, transactions.Where(t => t.TransactionType == TransactionType.Dividend));
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
