using BehaviourManagementSystem_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// RoleController
    /// Writer:DuyLH4
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("get-role-name-by-user-id")]
        public async Task<IActionResult> GetRoleNameByUserId(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _roleService.GetRoleNameByUserId(id);

            if (string.IsNullOrEmpty(response.Result))
                return BadRequest(response);

            return Ok(response);
        }
    }
}
