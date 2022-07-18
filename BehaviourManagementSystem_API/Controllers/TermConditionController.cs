using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// TermCondition
    /// writter: HoangDDN
    /// Description: List,Add,Edit,Delete,GetbyId
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TermConditionController : ControllerBase
    {
        private readonly ITermConditionService _termConditionService;

        public TermConditionController(ITermConditionService termConditionService)
        {
            _termConditionService = termConditionService;
        }

        [HttpGet("get-all")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy danh sách điều khoản dịch vụ TermCondition
        public async Task<IActionResult> GetAll()
        {
            var response = await _termConditionService.GetAll();

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpGet("get-by-id")]
        //[Authorize]
        //Lấy 1 điều khoản dịch vụ TermCondition
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid || id.CheckRequest())
                return BadRequest(ModelState);
            var response = await _termConditionService.GetById(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("create")]
        //[Authorize(Roles="admin")]
        //Tạo mới điều khoản dịch vụ TermCondition
        public async Task<IActionResult> Create(string content)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _termConditionService.Create(content);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update")]
        //[Authorize(Roles="admin")]
        //Chỉnh sửa điều khoản dịch vụ TermCondition
        public async Task<IActionResult> Update([FromBody] OptionsRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _termConditionService.Update(request.Id, request.Content);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete")]
        //[Authorize(Roles="admin")]
        //Xóa điều khoản dịch vụ TermCondition
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _termConditionService.Delete(id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
