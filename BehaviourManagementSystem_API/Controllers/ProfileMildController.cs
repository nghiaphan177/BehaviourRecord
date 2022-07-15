using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
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
        public async Task<IActionResult> GetAll()
        {
            var response = await _profileMildService.GetAll();
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("create")]
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
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] ProfileMildRequest request)
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
        [HttpPost("delete")]
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
