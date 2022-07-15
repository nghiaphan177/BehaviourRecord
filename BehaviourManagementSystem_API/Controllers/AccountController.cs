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
        public async Task<IActionResult> Login([FromBody] LoginAdminRequest request)
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
            var result = await _accountService.GetUser(id);

            if(result.Result == null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
