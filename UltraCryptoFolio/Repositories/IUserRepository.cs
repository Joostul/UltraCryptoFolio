using System;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.DomainModels;

namespace UltraCryptoFolio.Repositories
{
    public interface IUserRepository
    {
        Task<PortfolioUser> GetUserAsync();
        Task<PortfolioUser> GetUserAsync(string userName);
        Task AddUserAsync(PortfolioUser user);
        Task RemoveUserAsync(PortfolioUser user);
        Task UpdateUserPassword(PortfolioUser user);
        Task UpdateUsername(PortfolioUser user);
    }
}
