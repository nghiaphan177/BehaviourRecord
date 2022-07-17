using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// AccountController
    /// Writer: DuyLH4
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
	//[Authorize]
	public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
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

        [HttpPost("Register")]
        [AllowAnonymous]
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

		[HttpPost("VerifyEmail"),AllowAnonymous]
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

        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _accountService.GetAll();

            if(result.Result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser(string id)
        {
            if(!ModelState.IsValid || id.CheckRequest())
                return BadRequest(ModelState);

            var result = await _accountService.GetUser(id);

            if(result.Result == null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
