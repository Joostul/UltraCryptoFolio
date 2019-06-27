using System.Security.Claims;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.DomainModels;

namespace UltraCryptoFolio.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUserAsync(PortfolioUser user);
        Task<IdentityResult> DeleteUserAsync(PortfolioUser user);
        Task<IdentityResult> SignInAsync(string userEmail, string password, bool isPersistent);
        void SignOutAsync();
        bool IsSignedIn(ClaimsPrincipal claimsPrincipal);
    }
}
