using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// ProfileExtreme
    /// writter: HoangDDN
    /// Description: List,Add,Edit,Delete,GetbyId
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileExtremeController : ControllerBase
    {
        private readonly IProfileExtremeService _profileExtremeService;

        public ProfileExtremeController(IProfileExtremeService profileExtremeService)
        {
            _profileExtremeService = profileExtremeService;
        }
        [HttpGet("get-all")]
        //Lấy danh sách can thiệp Extreme
        public async Task<IActionResult> GetAll()
        {
            var response = await _profileExtremeService.GetAll();
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("get-by-id")]
        //Lấy 1 can thiệp Extreme
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid || id.CheckRequest())
                return BadRequest(ModelState);
            var response = await _profileExtremeService.GetById(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("create")]
        //Tạo mới can thiệp Extreme
        public async Task<IActionResult> Create(string content)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _profileExtremeService.Create(content);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("update")]
        //Chỉnh sửa can thiệp Extreme
        public async Task<IActionResult> Update([FromBody] OptionsRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _profileExtremeService.Update(request.Id, request.Content);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("delete")]
        //Xóa can thiệp Extreme
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _profileExtremeService.Delete(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
