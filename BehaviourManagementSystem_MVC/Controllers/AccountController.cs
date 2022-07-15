using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BehaviourManagementSystem_MVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountAPIClient _accountAPIClient;
		private readonly IConfiguration _configuration;
		private readonly IEmailSender _emailSender;
		public AccountController(IAccountAPIClient accountAPIClient, IConfiguration configuration, IEmailSender emailSender)
		{
			_accountAPIClient = accountAPIClient;
			_configuration = configuration;
			_emailSender = emailSender;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}


		/// <summary>
		/// Action Register
		/// Writer: DuyLH4
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Register(RegisterRequest request)
		{
			if(!ModelState.IsValid)
				return View(request);

			var response = await _accountAPIClient.Register(request);

			if(response.Success)
			{
				if(response.Result.Id == null || response.Result.Code == null)
					return NotFound(); // cần UI

				var uri = new UriBuilder(_configuration["EmailSettings:MailBodyHtml"]);
				var query = HttpUtility.ParseQueryString(uri.Query);
				query["id"] = response.Result.Id;
				query["code"] = response.Result.Code;
				uri.Query = query.ToString();
				var url = uri.ToString();
				var subject = "(XÁC THỰC TÀI KHOẢN NGƯỜI DÙNG)";
				var htmlMessage =
					$"Xác thực tài khoản của bạn." +
					$"<a href='{url}' style='color:red;'>" +
						$"<strong>" +
							$"<u>" +
								$"<i>link tại đây</i>" +
							$"</u>" +
						$"</strong>" +
					$"</a>";
				var ok = true;
				try
				{
					await _emailSender.SendEmailAsync(request.Email, subject, htmlMessage);
				}
				catch { ok = false; }

				if(!ok)
					return View(); // cần UI (UI với hình thức gửi mail không thành công)
			}
			return View(); // cần UI (UI với hình thức đã gửi mail thành công)
		}

		/// <summary>
		/// Action ConfirmEmail
		/// Writer: DuyLH4
		/// </summary>
		/// <param name="id"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> ConfirmEmail(string id, string code)
		{
			if(id == null || code == null)
				return NotFound();

			var request = new ConfirmEmailRequest { Id = id, Code = code };

			var response = await _accountAPIClient.ConfirmEmail(request);

			var user = response.Result;

			return View(user); // cần UI
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest request)
		{
			var user = HttpContext.Session.GetString("USER");
			if(!string.IsNullOrEmpty(user))
				return RedirectToAction("Index", "Home");

			if(!ModelState.IsValid)
				return View(request);

			var result = await _accountAPIClient.Login(request);

			if(result.Result == null)
			{
				ModelState.AddModelError("", "Đăng nhập không thành công");
				return View();
			}
			var userPrincipal = this.ValidateToken(result.Result);

			var authProperties = new AuthenticationProperties
			{
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				IsPersistent = false
			};
			HttpContext.Session.SetString("USER", result.Result);
			await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						userPrincipal,
						authProperties);
			return RedirectToAction("Index", "Home");
		}

		private ClaimsPrincipal ValidateToken(string token)
		{
			IdentityModelEventSource.ShowPII = true;

			SecurityToken validatedToken;
			TokenValidationParameters validationParameters = new TokenValidationParameters();

			validationParameters.ValidateLifetime = true;

			validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
			validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
			validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

			ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);

			return principal;
		}
	}
}
