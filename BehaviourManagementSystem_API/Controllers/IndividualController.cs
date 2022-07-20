using BehaviourManagementSystem_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualController : ControllerBase
    {
        private readonly IIndividualService _individualService;

        public IndividualController(IIndividualService individualService)
        {
            _individualService = individualService;
        }

        [HttpGet("get-all")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy danh sách individual
        public async Task<IActionResult> GetAll()
        {
            var response = await _individualService.GetAll();

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("detail")]
        //[Authorize]
        //Lấy 1 intervention của assessment
        public async Task<IActionResult> Detail(string id)
        {
            var response = await _individualService.Detail(id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }


    }
}
