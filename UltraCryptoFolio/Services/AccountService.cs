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
using UltraCryptoFolio.Extensions;
using UltraCryptoFolio.Models.Enums;

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
            var result = new IdentityResult
            {
                Succeeded = false
            };

            try
            {
                if (await _userRepository.GetUserAsync(user.UserEmail) != null)
                {
                    result.Succeeded = false;
                    result.Errors = new List<string>
                    {
                        "Email already registered."
                    };
                    return result;
                }
                await _userRepository.AddTempUserAsync(user);
                result.Succeeded = true;
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
            var userDao = await _userRepository.GetUserAsync(userEmail);
            var user = userDao.ToDomainModel();

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

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        }

        public async Task<PortfolioUser> GetUser()
        {
            var userDao = await _userRepository.GetUserAsync();
            return userDao.ToDomainModel();
        }

        public async Task<IdentityResult> CompleteUserRegistrationAsync(Guid id)
        {
            var result = new IdentityResult
            {
                Succeeded = false
            };

            if (await _userRepository.TempUserExists(id))
            {
                try
                {
                    await _userRepository.RegisterTempUserAsync(id);
                    result.Succeeded = true;
                }
                catch (Exception)
                {
                    result.Succeeded = false;
                    result.Errors = new List<string>
                    {
                        "Something went wrong, please try again."
                    };
                }
            }
            else
            {
                result.Errors = new List<string>
                {
                    "Something went wrong, please try again."
                };
            }

            return result;
        }
    }
}
