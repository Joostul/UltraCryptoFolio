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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _accountService.SignInAsync(model.UserEmail, model.PassWord, model.RememberMe, HttpContext.Request.Path);
            if (result.Succeeded)
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    result.ClaimsPrincipal,
                    result.AuthenticationProperties);
            }
            else
            {
                ModelState.AddModelError(nameof(model.PassWord), result.Errors.First());
            }

            return View();
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            return RedirectToAction("signoutasync");
        }

        [HttpPost]
        public async Task<IActionResult> SignOutAsync()
        {
            await _accountService.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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

        [HttpGet]
        public IActionResult RegisterCompleted()
        {
            return View();
        }

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