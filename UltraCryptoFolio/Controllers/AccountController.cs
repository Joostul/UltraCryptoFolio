using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Extensions;
using UltraCryptoFolio.Models.Enums;
using UltraCryptoFolio.Models.ViewModels;
using UltraCryptoFolio.Services;

namespace UltraCryptoFolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(Policy = "RegisteredUser")]
        public async Task<IActionResult> Index()
        {
            var user = await _accountService.GetUser();
            var userViewModel = user.ToViewModel();
            return View(userViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _accountService.SignInAsync(model.UserEmail, model.Password, model.RememberMe, HttpContext.Request.Path);
            if (result.Succeeded)
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    result.ClaimsPrincipal,
                    result.AuthenticationProperties);
            }
            else
            {
                ModelState.AddModelError(nameof(model.Password), result.Errors.First());
            }

            return RedirectToAction("index", "portfolio");
        }

        [Authorize(Policy = "RegisteredUser")]
        [HttpGet]
        public IActionResult SignOut()
        {
            return RedirectToAction("signoutasync");
        }

        [Authorize(Policy = "RegisteredUser")]
        [HttpPost]
        public async Task<IActionResult> SignOutAsync()
        {
            await _accountService.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var domainModel = model.ToDomainModel(UserRole.FreeUser, UserState.Unverified);
            var result = await _accountService.CreateUserAsync(domainModel);
            if (result.Succeeded)
            {
                return RedirectToAction("RegisterCompleted");
            }
            else
            {
                ModelState.AddModelError("", result.Errors.First());
                return View();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RegisterCompleted()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("/Account/{id}")]
        [HttpGet]
        public async Task<IActionResult> VerifyEmail([FromRoute]Guid id)
        {
            var result = await _accountService.CompleteUserRegistrationAsync(id);
            if (result.Succeeded)
            {
                return RedirectToAction("AccountVerified");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
    }
}