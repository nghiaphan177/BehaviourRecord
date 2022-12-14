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

        [HttpGet("RoleOfUser{id}")]
        public async Task<IActionResult> GetRoleNameByUserId(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _roleService.GetRoleNameByUserId(id);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _roleService.GetAll();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
