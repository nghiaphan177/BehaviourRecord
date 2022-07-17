using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// ProfileMild
    /// writter: HoangDDN
    /// Description: List,Add,Edit,Delete,GetbyId
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileMildController : ControllerBase
    {
        private readonly IProfileMildService _profileMildService;

        public ProfileMildController(IProfileMildService profileMildService)
        {
            _profileMildService = profileMildService;
        }
        [HttpGet("get-all")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy danh sách can thiệp Mild
        public async Task<IActionResult> GetAll()
        {
            var response = await _profileMildService.GetAll();
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("get-by-id{id}")]
        //[Authorize]
        //Lấy 1 can thiệp Mild
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid || id.CheckRequest())
                return BadRequest(ModelState);
            var response = await _profileMildService.GetById(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("create")]
        //[Authorize(Roles="admin")]
        //Tạo mới can thiệp Mild
        public async Task<IActionResult> Create(string content)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _profileMildService.Create(content);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("update{id}")]
        //[Authorize(Roles="admin")]
        //Chỉnh sửa can thiệp Mild
        public async Task<IActionResult> Update([FromBody] OptionsRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _profileMildService.Update(request.Id,request.Content);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("delete{id}")]
        //[Authorize(Roles="admin")]
        //Xóa can thiệp Mild
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _profileMildService.Delete(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
