using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.DomainModels;
using UltraCryptoFolio.Repositories;

namespace UltraCryptoFolio.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> CreateUserAsync(PortfolioUser user)
        {
            var result = new IdentityResult();

            try
            {
                await _userRepository.AddUserAsync(user);
                result.Succeeded = true;
                // TODO: send confimation email
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors = new List<string>
                {
                    ex.Message
                };
            }
            return result;
        }

        public Task<IdentityResult> DeleteUserAsync(PortfolioUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> SignInAsync(string userEmail, string password, bool isPersistent, string redirectUri)
        {
            var result = new IdentityResult();
            var user = await _userRepository.GetUserAsync(userEmail);

            if (user != null && user.ValidatePassword(password))
            {
                result.Succeeded = true;

                var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Email, userEmail),
                    new Claim(ClaimTypes.Role, "User")
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                result.AuthenticationProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15),
                    IsPersistent = isPersistent,
                    IssuedUtc = DateTime.UtcNow,
                    RedirectUri = redirectUri
                };
                result.ClaimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            }
            else
            {
                result.Errors = new List<string>
                    {
                        "Invalid login attempt."
                    };
                result.Succeeded = false;
            }
            return result;
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsSignedIn()
        {
            if (_httpContextAccessor.HttpContext.User != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
