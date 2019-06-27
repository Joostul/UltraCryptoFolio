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
    }
}
