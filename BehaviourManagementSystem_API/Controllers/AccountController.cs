using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost("login-admin")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                return BadRequest(request);

            var response = await _accountService.LoginAdmin(request);

            if (string.IsNullOrEmpty(response.Result))
                return BadRequest(response);

            return Ok(response);
        }
    }
}
