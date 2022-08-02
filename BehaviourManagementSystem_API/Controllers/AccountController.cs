using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpGet("GetGoogleClientId"), AllowAnonymous]
        public IActionResult GetGoogleClientId()
        {
            var res = _configuration["Google:ClientId"];
            if(string.IsNullOrEmpty(res))
                return BadRequest(new ResponseResultError<string>("Kết quá không tìm thấy"));
            return Ok(new ResponseResultSuccess<string>(res));
        }

        [HttpPost("Login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(request.UserNameOrEmail.CheckRequest() || request.Password.CheckRequest())
                return BadRequest(request);

            var response = await _accountService.Login(request);

            if(response == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Register"), AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.Register(request);

            if(!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("VerifyEmail"), AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromBody] ConfirmEmailRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _accountService.VerifyEmail(request);

            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("ResendConfirmEmail"), AllowAnonymous]
        public async Task<IActionResult> ResendConfirmEmail(string email)
        {
            if(email.CheckRequest())
                return BadRequest(email);

            var response = await _accountService.ResenConfirmEmail(email);

            if(!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("ForgotPassword"), AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string userNameOfEmail)
        {
            if(userNameOfEmail.CheckRequest())
                return BadRequest(userNameOfEmail);

            var response = await _accountService.ForgotPassword(userNameOfEmail);

            if(!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("ResetPassword"), AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest repuest)
        {
            if(!ModelState.IsValid || repuest.Id.CheckRequest() || repuest.Code.CheckRequest())
                return BadRequest();

            var response = await _accountService.ResetPassword(repuest);

            if(!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("CheckEmailConfirmed"), AllowAnonymous]
        public async Task<IActionResult> CheckEmailConfirmed(string email)
        {
            if(email.CheckRequest())
                return BadRequest(email);

            var response = await _accountService.CheckEmailConfirmed(email);

            if(!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangPassword([FromBody] ResetPasswordRequest repuest)
        {
            if(!ModelState.IsValid)
                return BadRequest(repuest);

            if(repuest.Id.CheckRequest() ||
                repuest.PasswordNew.CheckRequest() ||
                !repuest.PasswordNew.CheckPaswordRepuest() ||
                repuest.PasswordNew != repuest.PasswordConfirm)
                return BadRequest(repuest);

            var response = await _accountService.ChangePassword(repuest);

            if(!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser([FromQuery] UserProfileRequest request)
        {
            var response = await _accountService.GetAccount(request);

            if(!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("AllUser/{roleName}")]
        public async Task<IActionResult> GetAllUser(string roleName = null)
        {
            var result = await _accountService.GetAll(roleName);

            if(result.Result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("AllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _accountService.GetAll(null);

            if(result.Result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("User"), AllowAnonymous]
        public async Task<IActionResult> GetUser(string id)
        {
            if(!ModelState.IsValid || id.CheckRequest())
                return BadRequest(ModelState);

            var result = await _accountService.GetUser(id);

            if(result.Result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("CreateUserProfile")]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileRequest request)
        {
            Guid idadmin;
            Guid roleid;
            if(!Guid.TryParse(request.Id, out idadmin) ||
                !Guid.TryParse(request.RoleId, result: out roleid))
                return BadRequest("Thông tin truy cập không hợp lệ.");

            var response = await _accountService.CreateUserProfile(request);

            if(!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(request);

            var response = await _accountService.UpdateUserProfile(request);

            if(!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("DeleteUserProfile")]
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            if(id.CheckRequest())
                return BadRequest(id);

            var response = await _accountService.DeleteUserProfile(id);

            if(!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("GoogleSigin")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleSigin(string token)
        {
            if(token.CheckRequest())
                return BadRequest("Mã đăng nhập rỗng");

            var result = await _accountService.GoogleSigin(token);

            if(!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("CheckPasswordNull/{id}"), AllowAnonymous]
        public async Task<IActionResult> CheckPasswordNull(string id)
        {
            Guid guid;
            if(string.IsNullOrEmpty(id))
                return BadRequest("Dữ liệu yêu cầu không hợp lệ");
            if(!Guid.TryParse(id, out guid))
                return BadRequest("Truy cập không hợp lệ.");

            var res = await _accountService.CheckPassworkNull(id);

            if(!res.Success)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpPost("NewPassOfAccountGoogle")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> NewPassOfAccountGoogle([FromBody] ResetPasswordRequest req)
        {
            var res = await _accountService.NewPassOfAccountGoogle(req);

            if(!res.Success)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpGet("GetImg")]
        public async Task<IActionResult> GetImg([FromQuery] string id)
        {
            Guid guid;
            if(!Guid.TryParse(id, out guid))
                return BadRequest("Không thể truy xuất thông tin");

            var res = await _accountService.GetImg(id);

            if(!res.Success)
                return BadRequest(res);

            return Ok(res);
        }
    }
}
