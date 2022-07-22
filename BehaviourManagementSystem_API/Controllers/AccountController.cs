using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(request.UserNameOrEmail.CheckRequest() || request.Password.CheckRequest())
                return BadRequest(request);

            var response = await _accountService.Login(request);

            if(string.IsNullOrEmpty(response.Result))
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

        [HttpGet("ResenConfirmEmail"), AllowAnonymous]
        public async Task<IActionResult> ResenConfirmEmail(string email)
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
        public async Task<IActionResult> GetUser([FromBody] UserProfileRequest request)
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

        [HttpGet("User")]
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
    }
}
