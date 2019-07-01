using System.Linq;
using UltraCryptoFolio.Models.DomainModels;
using UltraCryptoFolio.Models.Enums;
using UltraCryptoFolio.Models.ViewModels;
using UltraCryptoFolio.Repositories.DataAccessObjects;

namespace UltraCryptoFolio.Extensions
{
    public static class MappingExtensions
    {
        public static PortfolioUserDao ToDao(this PortfolioUser user)
        {
            return new PortfolioUserDao
            {
                HashedPassword = user.Password,
                UserName = user.UserEmail,
                Role = user.Role,
                State = user.State
            };
        }

        public static PortfolioUser ToDomainModel(this PortfolioUserDao dao)
        {
            return new PortfolioUser(dao.UserName, dao.HashedPassword, false)
            {
                Role = dao.Role,
                State = dao.State
            };
        }

        public static PortfolioUser ToDomainModel(this RegisterViewModel model, UserRole role, UserState state)
        {
            return new PortfolioUser(model.UserEmail, model.Password, true)
            {
                Role = role,
                State = state
            };
        }

        public static PortolioUserViewModel ToViewModel(this PortfolioUser user)
        {
            return new PortolioUserViewModel
            {
                UserEmail = user.UserEmail
            };
        }

        public static Portfolio ToDomainModel(this PortfolioDao dao)
        {
            return new Portfolio
            {
                Transactions = dao.Transactions.Select(t => t.ToDomainModel()).ToList()
            };
        }

        public static Transaction ToDomainModel(this TransactionDao dao)
        {
            return new Transaction
            {
                Id = dao.Id,
                AmountReceived = dao.AmountReceived,
                AmountSpent = dao.AmountSpent,
                DateTime = dao.DateTime,
                Fee = dao.Fee,
                ReceivedCurrency = dao.ReceivedCurrency,
                ReceivedCurrencyPrice = dao.ReceivedCurrencyPrice,
                SpentCurrency = dao.SpentCurrency,
                SpentCurrencyPrice = dao.SpentCurrencyPrice
            };
        }

        public static TransactionViewModel ToViewModel(this Transaction domainModel)
        {
            return new TransactionViewModel
            {
                AmountReceived = domainModel.AmountReceived,
                AmountSpent = domainModel.AmountSpent,
                DateTime = domainModel.DateTime,
                Fee = domainModel.Fee,
                ReceivedCurrency = domainModel.ReceivedCurrency,
                ReceivedCurrencyPrice = domainModel.ReceivedCurrencyPrice,
                SpentCurrency = domainModel.SpentCurrency,
                SpentCurrencyPrice = domainModel.SpentCurrencyPrice
            };
        }
    }
}
