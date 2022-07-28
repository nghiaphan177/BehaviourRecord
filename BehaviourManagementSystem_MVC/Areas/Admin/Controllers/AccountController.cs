using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Account;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

namespace BehaviourManagementSystem_MVC.Area.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountAPIClient _accountAPIClient;
        private readonly IUserAPIClient _userAPIClient;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;
        public AccountController(IAccountAPIClient accountAPIClient, IUserAPIClient userAPIClient, IConfiguration configuration, IEmailSender emailSender)
        {
            _accountAPIClient = accountAPIClient;
            _userAPIClient = userAPIClient;
            _config = configuration;
            _emailSender = emailSender;
        }
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string ReturnUrl = "/Admin/Home")
        {
            await HttpContext.SignOutAsync(
               "Admin");
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request, string ReturnUrl = "/Admin/Home")
        {

            if(ReturnUrl == null)
            {
                ReturnUrl = "/Admin/Home";
            }
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
                        "Admin",
                        userPrincipal,
                        authProperties);
            //return RedirectToAction("Index", "Home",new {area = "Admin" });
            return LocalRedirect(ReturnUrl);
        }
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            HttpContext.Session.Remove("USER");
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                "Admin");

            return RedirectToAction("Login");
        }
        public async Task<ActionResult> Detail(string id)
        {
            try
            {
                var user = await _userAPIClient.GetUserById(id);
                return View(user.Result);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }

        }
        public IActionResult RecoverPass()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RecoverPass(string useremail)
        {
            if (string.IsNullOrEmpty(useremail))
                return View(); // màn hình lỗi text rỗng

            var response = await _accountAPIClient.ForgotPassword(useremail);

            if (response.Success)
            {
                // https://localhost:port/Account/ResetPassword?id=****&code=****/
                var uri = new UriBuilder(_config["EmailSettings:MailBodyHtml"] + "/Admin/Account/ResetPassword");
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

                if (ok == false)
                {
                    ViewBag.Message = "Gửi mail không thành công";
                    return View(); // cần UI (UI với hình thức gửi mail không thành công) 
                }
            }
            ViewBag.Message = "Gửi mail thành công";
            return View(); // cần UI (UI với hình thức đã gửi mail thành công) action confirm eamil with method get
        }

        [HttpGet]
        public IActionResult ResetPassword(string id, string code)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(code))
                return NotFound();// trang not found 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var response = await _accountAPIClient.ResetPassword(request);

            if (!response.Success)
                return View(); // reset pass không thành không
            return RedirectToAction("ChangePassSuccess"); // reset thành công
        }
        [HttpGet]
        public IActionResult ChangePassSuccess()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
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
