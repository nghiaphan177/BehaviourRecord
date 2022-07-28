using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AdBdController : ControllerBase
    {
        private readonly IAbBd _abBd;

        public AdBdController(IAbBd abBd)
        {
            _abBd = abBd;
        }

        [HttpGet("GetCountAllAccountRegisterOfYear")]
        public async Task<IActionResult> GetCountAllAccountRegisterOfYear([FromQuery] string year)
        {
            int _int;
            if(!int.TryParse(year, out _int))
                return NotFound();

                var res = await _abBd.GetCountAllAccountRegisterOfYear(year);

            if(!res.Success)
                return BadRequest(res);

            return Ok(res);
        }
    }
}
