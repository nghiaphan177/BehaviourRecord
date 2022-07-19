using BehaviourManagementSystem_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// Assessment
    /// writter: HoangDDN
    /// Description: List,Detail,
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;

        public AssessmentController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpGet("get-all/{individualId}")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy danh sách assessment của individual
        public async Task<IActionResult> GetAll(string individualId)
        {
            var response = await _assessmentService.GetAll(individualId);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("detail")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy 1 assessment của individual
        public async Task<IActionResult> Detail(string id)
        {
            var response = await _assessmentService.Detail(id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
