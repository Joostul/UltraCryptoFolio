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

namespace UltraCryptoFolio.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;

        public AccountService(
            IUserRepository userRepository, 
            IHttpContextAccessor httpContextAccessor,
            IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }

        public async Task<bool> CreateUserAsync(PortfolioUser user)
        {
            try
            {
                if (await _userRepository.GetUserAsync(user.UserEmail) != null)
                {
                    return false;
                }
                else
                {
                    var id = await _userRepository.AddTempUserAsync(user);
                    var returnUrlString = $"{_httpContextAccessor.HttpContext.Request.Scheme }://{_httpContextAccessor.HttpContext.Request.Host}/Account/{id}";
                    var email = $"Please confirm your account by <a href='{returnUrlString}'>clicking here</a>.";
                    await _emailSender.SendEmailAsync(user.UserEmail, "Registration", email);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Task<IdentityResult> DeleteUserAsync(PortfolioUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> SignInAsync(string userEmail, string password, bool isPersistent, string redirectUri)
        {
            var result = new IdentityResult()
            {
                Succeeded = false
            };
            var userDao = await _userRepository.GetUserAsync(userEmail);
            if(userDao != null)
            {
                var user = userDao.ToDomainModel();
                if (user.ValidatePassword(password))
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
                } else
                {
                    result.Errors = new List<string>
                    {
                        "Invalid login attempt."
                    };
                }
            }
            else
            {
                result.Errors = new List<string>
                {
                    "Invalid login attempt."
                };
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
