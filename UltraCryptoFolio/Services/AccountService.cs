using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.DomainModels;
using UltraCryptoFolio.Repositories;

namespace UltraCryptoFolio.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private bool _isSignedIn;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> CreateUserAsync(PortfolioUser user)
        {
            var result = new IdentityResult();

            try
            {
                await _userRepository.AddUser(user);
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

        public async Task<IdentityResult> SignInAsync(string userEmail, string password, bool isPersistent)
        {
            var result = new IdentityResult();

            try
            {
                var userInDatabase = await _userRepository.GetUser(userEmail);
                if (userInDatabase.ValidatePassword(password))
                {
                    result.Succeeded = true;
                    _isSignedIn = true;

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
                        RedirectUri = "https://localhost:5001"
                    };
                    result.ClaimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                } else
                {
                    result.Errors = new List<string>
                    {
                        "Invalid login attempt."
                    };
                    result.Succeeded = false;
                }
            }
            catch (KeyNotFoundException)
            {
                result.Succeeded = false;
                result.Errors = new List<string>
                {
                    "User not found."
                };
                return result;
            }
            return result;
        }

        public void SignOutAsync()
        {
            _isSignedIn = false;
        }

        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal)
        {
            return _isSignedIn;
        }
    }
}
