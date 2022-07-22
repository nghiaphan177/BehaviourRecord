using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// ProfileModerate
    /// writter: HoangDDN
    /// Description: List,Add,Edit,Delete,GetbyId
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileModerateController : ControllerBase
    {
        private readonly IProfileModerateService _profileModerateService;

        public ProfileModerateController(IProfileModerateService profileModerateService)
        {
            _profileModerateService = profileModerateService;
        }
        [HttpGet("get-all")]
        //Lấy danh sách can thiệp Moderate
        public async Task<IActionResult> GetAll()
        {
            var response = await _profileModerateService.GetAll();
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("get-by-id")]
        //Lấy 1 can thiệp Moderate
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid || id.CheckRequest())
                return BadRequest(ModelState);
            var response = await _profileModerateService.GetById(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("create")]
        //Tạo mới can thiệp Moderate
        public async Task<IActionResult> Create(string content)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _profileModerateService.Create(content);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("update")]
        //Chỉnh sửa can thiệp Moderate
        public async Task<IActionResult> Update([FromBody] OptionsRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _profileModerateService.Update(request.Id, request.Content);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("delete")]
        //Xóa can thiệp Moderate
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _profileModerateService.Delete(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
