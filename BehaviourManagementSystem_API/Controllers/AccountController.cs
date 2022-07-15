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

        [HttpPost("login")]
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

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{

            return Ok();
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
