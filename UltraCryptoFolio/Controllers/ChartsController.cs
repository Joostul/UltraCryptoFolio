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
using System.Globalization;

namespace UltraCryptoFolio.Controllers
{
    public class ChartsController : Controller
    {
        public IActionResult Index()
        {
            var _transactions = GetTransactions();

            var portfolio = new Portfolio(new PriceGetter(), _transactions);

            List<DoughnutDataPoint> dataPoints = new List<DoughnutDataPoint>();

            foreach (var cryptoValue in portfolio.CryptoValues.Where(c => c.MonetaryValue > 0).ToList())
            {
                dataPoints.Add(new DoughnutDataPoint(y: (double)cryptoValue.MonetaryValue, name: cryptoValue.CryptoCurrency.ToString()));
            }

            ViewBag.Data = JsonConvert.SerializeObject(dataPoints);

            return View();
        }

        public IActionResult ValueOverTime()
        {
            var _transactions = GetTransactions();

            var portfolio = new Portfolio(new PriceGetter(), _transactions);
            var firstTransactionDate = portfolio.Transactions.OrderBy(t => t.DateTime).FirstOrDefault().DateTime;
            int weeks = (int)(DateTime.UtcNow - firstTransactionDate).TotalDays / 7;
            var firstSunday = NextSunday(firstTransactionDate);


            List<LineDataPoint> dataPoints = new List<LineDataPoint>();
            for (int i = 0; i < weeks; i++)
            {
                var dateOfTransaction = firstTransactionDate.AddDays((i*7)).Date;
                var value = (double)ValueCalculation.GetMonetaryValueOfTransactionsOnDate(portfolio.Transactions, dateOfTransaction);

                dataPoints.Add(new LineDataPoint(value, (Int32)(dateOfTransaction.Subtract(new DateTime(1970, 1, 1))).TotalSeconds));
            }

            ViewBag.Data = JsonConvert.SerializeObject(dataPoints);

            return View();
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

        private static DateTime NextSunday(DateTime from)
        {
            int start = (int)from.DayOfWeek;
            int target = 0;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }

        private static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}