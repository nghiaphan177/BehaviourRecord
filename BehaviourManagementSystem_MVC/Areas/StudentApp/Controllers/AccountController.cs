using BehaviourManagementSystem_MVC.APIIntegration.Account;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BehaviourManagementSystem_MVC.Area.StudentApp.Controllers
{
	[Area("StudentApp")]
	public class AccountController : Controller
	{
		private readonly IAccountAPIClient _accountAPIClient;
		private readonly IConfiguration _configuration;
		private readonly IEmailSender _emailSender;

		public AccountController(IAccountAPIClient accountAPIClient, IConfiguration configuration, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _accountAPIClient = accountAPIClient;
            _configuration = configuration;
            _emailSender = emailSender;
        }
		[HttpGet]
		public async Task<IActionResult> Login(string ReturnUrl = "/StudentApp/Home")
		{
			await HttpContext.SignOutAsync(
			   "Student");
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest request, string ReturnUrl = "/StudentApp/Home")
		{
			if (ReturnUrl == null)
			{
				ReturnUrl = "/StudentApp/Home";
			}
			if (!ModelState.IsValid)
				return View(request);

			var result = await _accountAPIClient.Login(request);

			if (result == null)
			{
				ModelState.AddModelError("", "Đăng nhập không thành công");
				return View();
			}
			if(result.Success == false)
            {
				ModelState.AddModelError("", result.Message);
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
						"Student",
						userPrincipal,
						authProperties);
			return LocalRedirect(ReturnUrl);
		}
		public async Task<IActionResult> Logout(string returnUrl = null)
		{
			// Clear the existing external cookie
			await HttpContext.SignOutAsync(
				"Student");

			return RedirectToAction("Login");
		}

		[HttpGet]
		public IActionResult ConfirmEmail()
		{
			// màn hình chờ email confirm
			return View(); // Cần UI
		}

		[HttpPost]
		public async Task<IActionResult> ConfirmEmailed()
		{
			var email = HttpContext.Session.GetString("EMAILCONFIRMED");

			var response = await _accountAPIClient.GetEmailConfirmed(email);

			if(response == null)
				return NotFound(); // trang not found

			if(response.Success)
				if(response.Result)
					return View();// trang home
			return View(); // trang chờ confirm email
		}

		[HttpPost]
		public async Task<IActionResult> ConfirmEmail(string id, string code)
		{
			if(id == null || code == null)
				return NotFound(); // Cần UI

			var request = new ConfirmEmailRequest { Id = id, Code = code };

			var response = await _accountAPIClient.ConfirmEmail(request);

			// response sucess true thì view view home / fasle thì resend mail

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResendConfirmEmail(string email)
		{
			if(string.IsNullOrEmpty(email))
				return View(); // lỗi không có email

			var response = await _accountAPIClient.ResendConfirmEmail(email);

			if(response.Success)
			{
				// https://localhost:port/Account/ConfirmEmail?id=****&code=****/
				var uri = new UriBuilder(_configuration["EmailSettings:MailBodyHtml"] + "/Account/ConfirmEmail");
				var query = HttpUtility.ParseQueryString(uri.Query);
				query["id"] = response.Result.Id;
				query["code"] = response.Result.Code;
				uri.Query = query.ToString();
				var url = uri.ToString();
				var subject = "XÁC THỰC TÀI KHOẢN NGƯỜI DÙNG";
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
					await _emailSender.SendEmailAsync(email, subject, htmlMessage);
				}
				catch { ok = false; }

				if(!ok)
					return View(); // cần UI (UI với hình thức gửi mail không thành công) 
			}
			return View(); // cần UI (UI với hình thức đã gửi mail thành công) action confirm eamil with method get
		}

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(string userNameOrEmail)
		{
			if(string.IsNullOrEmpty(userNameOrEmail))
				return View(); // màn hình lỗi text rỗng

			var response = await _accountAPIClient.ForgotPassword(userNameOrEmail);

			if(response.Success)
			{
				// https://localhost:port/Account/ResetPassword?id=****&code=****/
				var uri = new UriBuilder(_configuration["EmailSettings:MailBodyHtml"] + "/Account/ResetPassword");
				var query = HttpUtility.ParseQueryString(uri.Query);
				query["id"] = response.Result.Id;
				query["code"] = response.Result.Code;
				uri.Query = query.ToString();
				var url = uri.ToString();
				var subject = "Đặt lại mật khẩu của bạn";
				var htmlMessage =
					$"Đặt lại mật khẩu của bạn." +
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
					await _emailSender.SendEmailAsync(response.Result.UserOrEmail, subject, htmlMessage);
				}
				catch { ok = false; }

				if(!ok)
					return View(); // cần UI (UI với hình thức gửi mail không thành công) 
			}
			return View(); // cần UI (UI với hình thức đã gửi mail thành công) action confirm emaiil with method get
		}

		[HttpGet]
		public IActionResult ResetPassword()
		{
			ViewBag.IdAccount = User.FindFirst("Id").Value;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
		{
			if(!ModelState.IsValid)
				return View(ModelState);

			var response = await _accountAPIClient.ResetPassword(request);

			if(!response.Success)
				return View(); // reset pass không thành không
			return RedirectToAction("ChangePassSuccess"); // reset thành công
		}
		[HttpGet]
		public IActionResult ChangePassSuccess()
		{
			return View();
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
