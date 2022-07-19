using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// ProfileRecovery
    /// writter: HoangDDN
    /// Description: List,Add,Edit,Delete,GetbyId
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileRecoveryController : ControllerBase
    {
        private readonly IProfileRecoveryService _profileRecoveryService;

        public ProfileRecoveryController(IProfileRecoveryService profileRecoveryService)
        {
            _profileRecoveryService = profileRecoveryService;
        }

        [HttpGet("get-all")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy danh sách can thiệp Moderate
        public async Task<IActionResult> GetAll()
        {
            var response = await _profileRecoveryService.GetAll();
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("get-by-id")]
        //[Authorize]
        //Lấy 1 can thiệp Moderate
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid || id.CheckRequest())
                return BadRequest(ModelState);
            var response = await _profileRecoveryService.GetById(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("create")]
        //[Authorize(Roles="admin")]
        //Tạo mới can thiệp Moderate
        public async Task<IActionResult> Create(string content)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _profileRecoveryService.Create(content);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("update")]
        //[Authorize(Roles="admin")]
        //Chỉnh sửa can thiệp Moderate
        public async Task<IActionResult> Update([FromBody] OptionsRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _profileRecoveryService.Update(request.Id, request.Content);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("delete")]
        //[Authorize(Roles="admin")]
        //Xóa can thiệp Moderate
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _profileRecoveryService.Delete(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
