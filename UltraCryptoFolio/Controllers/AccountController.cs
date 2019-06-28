using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            var domainModel = model.ToDomainModel(UserRole.FreeUser, UserState.Verified); // TODO: make verify functionality and verify accounts
            var result = await _accountService.CreateUserAsync(domainModel);
            if (result.Succeeded)
            {
                return RedirectToAction("RegisterCompleted");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet]
        public IActionResult RegisterCompleted()
        {
            return View();
        }
    }
}