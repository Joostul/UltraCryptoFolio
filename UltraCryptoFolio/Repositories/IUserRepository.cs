using System;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.DomainModels;
using UltraCryptoFolio.Repositories.DataAccessObjects;

namespace UltraCryptoFolio.Repositories
{
    public interface IUserRepository
    {
        Task<PortfolioUserDao> GetUserAsync();
        Task<PortfolioUserDao> GetUserAsync(string userName);
        Task<PortfolioUserDao> GetTempUserAsync(Guid userId);
        Task<bool> TempUserExists(Guid userId);
        Task AddTempUserAsync(PortfolioUser user);
        Task RegisterTempUserAsync(Guid userId);
        Task RemoveUserAsync(PortfolioUser user);
        Task UpdateUserPassword(PortfolioUser user);
        Task UpdateUsername(PortfolioUser user);
    }
}
