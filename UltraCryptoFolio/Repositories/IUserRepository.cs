using System;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.DomainModels;

namespace UltraCryptoFolio.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserNameExists(string userName);
        Task<PortfolioUser> GetUser(string userName);
        Task<Uri> AddUser(PortfolioUser user);
        Task RemoveUser(PortfolioUser user);
        Task UpdateUserPassword(PortfolioUser user);
        Task UpdateUsername(PortfolioUser user);
    }
}
