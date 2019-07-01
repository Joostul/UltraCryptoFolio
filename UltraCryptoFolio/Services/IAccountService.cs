using System;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.DomainModels;

namespace UltraCryptoFolio.Services
{
    public interface IAccountService
    {
        Task<bool> CreateUserAsync(PortfolioUser user);
        Task<IdentityResult> CompleteUserRegistrationAsync(Guid id);
        Task<IdentityResult> DeleteUserAsync(PortfolioUser user);
        Task<IdentityResult> SignInAsync(string userEmail, string password, bool isPersistent, string redirectUri);
        Task SignOutAsync();
        bool IsSignedIn();
        string GetUserName();
        Task<PortfolioUser> GetUser();
    }
}
