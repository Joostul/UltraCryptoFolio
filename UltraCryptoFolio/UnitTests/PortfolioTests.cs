using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UltraCryptoFolio.Models;
using UnitTests.Mock;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        // TotalCryptoValue
        [TestMethod]
        public void SingleInvestmentTotalCryptoValueCalculatedCorrectly()
        {
            //Arrange
            var mockPriceGetter = new MockPriceGetter();
            List<Transaction> transactions = new List<Transaction>()
            {
                new Investment()
                {
                    AmountReceived = 100000000,
                    AmountSpent = 10000,
                    DateTime = DateTime.UtcNow,
                    ReceivingCurrency = CryptoCurrency.Bitcoin,
                    SpendingCurrency = Currency.Euro
                }
            };

            // Act
            var portfolio = new Portfolio(mockPriceGetter, transactions);

            // Assert
            Assert.AreEqual(Constants.BitcoinPrice, portfolio.PortfolioCryptoValue);
        }

        [TestMethod]
        public void TwoInvestmentsTotalCryptoValueCalculatedCorrectly()
        {
            //Arrange
            var mockPriceGetter = new MockPriceGetter();
            List<Transaction> transactions = new List<Transaction>()
            {
                new Investment()
                {
                    AmountReceived = 100000000,
                    AmountSpent = 10000,
                    DateTime = DateTime.UtcNow,
                    ReceivingCurrency = CryptoCurrency.Bitcoin,
                    SpendingCurrency = Currency.Euro
                },
                new Investment()
                {
                    AmountReceived = 100000000,
                    AmountSpent = 10000,
                    DateTime = DateTime.UtcNow,
                    ReceivingCurrency = CryptoCurrency.Bitcoin,
                    SpendingCurrency = Currency.Euro
                }
            };

            // Act
            var portfolio = new Portfolio(mockPriceGetter, transactions);

            // Assert
            Assert.AreEqual(Constants.BitcoinPrice * transactions.Count, portfolio.PortfolioCryptoValue);
        }

        [TestMethod]
        public void OneInvestmentOneDivestmentTotalCryptoValueCalculatedCorrectly()
        {
            //Arrange
            var mockPriceGetter = new MockPriceGetter();
            List<Transaction> transactions = new List<Transaction>()
            {
                new Investment()
                {
                    AmountReceived = 100000000,
                    AmountSpent = 1000,
                    DateTime = DateTime.UtcNow,
                    ReceivingCurrency = CryptoCurrency.Bitcoin,
                    SpendingCurrency = Currency.Euro
                },
                new Divestment()
                {
                    AmountReceived = 10000,
                    AmountSpent = 100000000,
                    DateTime = DateTime.UtcNow,
                    ReceivingCurrency = Currency.Euro,
                    SpendingCurrency = CryptoCurrency.Bitcoin
                }
            };

            // Act
            var portfolio = new Portfolio(mockPriceGetter, transactions);

            // Assert
            Assert.AreEqual(0, portfolio.PortfolioCryptoValue);
            Assert.AreEqual(9000, portfolio.PortfolioMonetaryValue);
        }

        // CalculateMonetaryValue
        [TestMethod]
        public void CalculateTransactionsWorthsOfAnInvestment()
        {
            //Arrange
            var mockPriceGetter = new MockPriceGetter();
            List<Transaction> transactions = new List<Transaction>()
            {
                new Investment()
                {
                    AmountReceived = 100000000,
                    AmountSpent = 10000,
                    DateTime = DateTime.UtcNow,
                    ReceivingCurrency = CryptoCurrency.Bitcoin,
                    SpendingCurrency = Currency.Euro
                }
            };

            // Act
            var portfolio = new Portfolio(mockPriceGetter, transactions);

            // Assert
            Assert.IsTrue(false);
        }
    }
}
