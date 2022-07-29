using BehaviourManagementSystem_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("GetCountAllAccountRegisterOfMonth")]
        public async Task<IActionResult> GetCountAllAccountRegisterOfMonth([FromQuery] string month, string year)
        {
            int m, y;
            if(!int.TryParse(month, out m) ||
                !int.TryParse(year, out y))
                return NotFound("Truy cập không hợp lệ.");

            var res = await _abBd.GetCountAllAccountRegisterOfMonth(m, y);

            if(!res.Success)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpGet("GetCountAllStudentOfAllClasses")]
        public async Task<IActionResult> GetCountAllStudentOfAllClasses([FromQuery] string teacherId)
        {
            Guid guid;
            if(!Guid.TryParse(teacherId, out guid))
                return NotFound("Truy cập dữ liệu không hợp lệ.");

            var res = await _abBd.GetCountAllStudentOfAllClasses(guid);

            if(!res.Success)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpGet("GetAllAccountNotVerifyMail")]
        public async Task<IActionResult> GetAllAccountNotVerifyMail()
        {
            var res = await _abBd.GetAllAccountNotVerifyMail();

            if(!res.Success)
                return BadRequest(res);

            return Ok(res);
        }
    }
}
