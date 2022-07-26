using BehaviourManagementSystem_MVC.APIIntegration.Account;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        public async Task<IActionResult> Login()
        {
            ViewBag.api = _configuration["BaseAddress"];

            var res = await _accountAPIClient.GetGoogleClientId();

            ViewBag.GoogleClientId = res.Result;

            await HttpContext.SignOutAsync(
               "Teacher");
            return View();
        }

        [HttpGet]
        public IActionResult NewPass(string id)
        {
            ViewData["id"] = id;
            return View(); // view đẻ người dùng nhập pass
        }
        
        [HttpPost]
        public async Task<IActionResult> NewPass(ResetPasswordRequest req)
        {
            if(req == null)
                return View(ModelState); // view hiện tại khi model null

            if(string.IsNullOrEmpty(req.PasswordNew) ||
            string.IsNullOrEmpty(req.PasswordConfirm) ||
            req.PasswordNew != req.PasswordConfirm)
                return View(ModelState); // view hiện tại không validate

            req.Id = User.FindFirst("Id").Value; 

            var res = await _accountAPIClient.NewPassOfAccountGoogle(req);

            if(!res.Success)
                return RedirectToAction("Index", "Home");

            return View(ModelState);
        }
        
        [HttpPost]
        public async Task<IActionResult> GoogleSigin(string token)
        {
            if(string.IsNullOrEmpty(token))
                return RedirectToAction("Login");

            var userPrincipal = ValidateToken(token);

            var id = userPrincipal.FindFirst("Id").Value;

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);

            HttpContext.Session.SetString("google", token);

            await HttpContext.SignInAsync(
                        "Teacher",
                        userPrincipal,
                        authProperties);

            if(!await CheckPasswordIsExist(id))
            {
                return RedirectToAction("NewPass", "Account", new { id });
            }

            //return RedirectToAction("Index", "Home",new {area = "Admin" });
            return RedirectToAction("Index", "Home");
        }

        private async Task<bool> CheckPasswordIsExist(string id)
        {
            if(string.IsNullOrEmpty(id))
                return false;

            var res = await _accountAPIClient.CheckPasswordNull(id);

            if(!res.Success)
                return false;

            return true;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request, string ReturnUrl = "/Home")
        {
            if(ReturnUrl == null)
            {
                ReturnUrl = "/Home";
            }
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Đăng nhập không thành công");
                return View(request);
            }

            var result = await _accountAPIClient.Login(request);

            if(result == null)
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

            if(request.Remember)
            {
                authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);
            }

            HttpContext.Session.SetString("Token", result.Result);

            await HttpContext.SignInAsync(
                        "Teacher",
                        userPrincipal,
                        authProperties);
            //return RedirectToAction("Index", "Home",new {area = "Admin" });
            return LocalRedirect(ReturnUrl);
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                "Teacher");

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if(!ModelState.IsValid)
                return View(request);

            var response = await _accountAPIClient.Register(request);

            // nếu respose success true thì tiến hành gửi mail với giá trị trả về là id và code
            if(response.Success)
            {
                if(response.Result.Id == null || response.Result.Code == null)
                    return NotFound(); // cần UI

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
                    await _emailSender.SendEmailAsync(request.Email, subject, htmlMessage);
                }
                catch { ok = false; }

                if(!ok)
                    return View(); // cần UI (UI với hình thức gửi mail không thành công) 
            }
            HttpContext.Session.SetString("EMAILCONFIRMED", request.Email);
            return View(); // cần UI (UI với hình thức đã gửi mail thành công) action confirm eamil with method get
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
            return View(); // cần UI (UI với hình thức đã gửi mail thành công) action confirm eamil with method get
        }

        [HttpGet]
        public IActionResult ResetPassword(string id, string code)
        {
            if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(code))
                return NotFound();// trang not found 
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if(!ModelState.IsValid)
                return View(ModelState);

            if(request.PasswordNew != request.PasswordConfirm)
            {
                ViewBag.Error = "Mật khẩu không giống nhau";
                return View(ModelState);
            }

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
