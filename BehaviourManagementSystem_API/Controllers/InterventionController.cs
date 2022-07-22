using BehaviourManagementSystem_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// Intervention
    /// writter: HoangDDN
    /// Description: List,Detail
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionController : ControllerBase
    {
        private readonly IInterventionService _interventionService;

        public InterventionController(IInterventionService interventionService)
        {
            _interventionService = interventionService;
        }

        [HttpGet("get-all/{assessmentId}")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy danh sách intervention của assessment
        public async Task<IActionResult> GetAll(string assessmentId)
        {
            var response = await _interventionService.GetAll(assessmentId);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("detail")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy 1 intervention của assessment
        public async Task<IActionResult> Detail(string int_id)
        {
            var response = await _interventionService.Detail(int_id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
