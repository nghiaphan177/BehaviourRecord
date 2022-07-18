using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Account;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountAPIClient _accountAPIClient;
        private readonly IUserAPIClient _userAPIClient;
        private readonly IConfiguration _config;
        public AccountController(IAccountAPIClient accountAPIClient, IUserAPIClient userAPIClient, IConfiguration configuration)
        {
            _accountAPIClient = accountAPIClient;
            _userAPIClient = userAPIClient;
            _config = configuration;
        }
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string ReturnUrl = "/Admin/Home")
        {
            await HttpContext.SignOutAsync(
               CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request, string ReturnUrl = "/Admin/Home")
        {
            //var user = HttpContext.Session.GetString("USER");
            //if (!string.IsNullOrEmpty(user))
            //    return RedirectToAction("Index", "Home", new { area = "Admin" });

            if (!ModelState.IsValid)
                return View(request);

            var result = await _accountAPIClient.Login(request);

            if (result.Result == null)
            {
                ModelState.AddModelError("", "Đăng nhập không thành công");
                return View();
            }
            
            var userPrincipal = ValidateToken(result.Result);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = request.Remember
            };
            if (request.Remember)
            {
                authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);
            }
            HttpContext.Session.SetString("Token", result.Result);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);
            //return RedirectToAction("Index", "Home",new {area = "Admin" });
            return LocalRedirect(ReturnUrl);
        }
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            //HttpContext.Session.SetString("USER", "");
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        public async Task<ActionResult> Detail(string id)
        {
            var user = await _userAPIClient.GetUserById(id);
            return View(user.Result);
        }
        private ClaimsPrincipal ValidateToken(string token)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _config["Tokens:Issuer"];
            validationParameters.ValidIssuer = _config["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);

            return principal;
        }

    }
}
