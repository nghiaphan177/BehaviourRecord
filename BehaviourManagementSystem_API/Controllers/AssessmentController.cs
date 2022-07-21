using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("get-all/{ind_id}")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy danh sách assessment của individual
        public async Task<IActionResult> GetAll(string ind_id)
        {
            var response = await _assessmentService.GetAll(ind_id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("detail/{ass_id}")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy 1 assessment của individual
        public async Task<IActionResult> Detail(string ass_id)
        {
            var response = await _assessmentService.Detail(ass_id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("create-record/{ind_id}")]
        //[Authorize(Roles="teacher")]
        //Tạo record assessment của individual
        public async Task<IActionResult> CreateRecord(string ind_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who)
        {
            var response = await _assessmentService.CreateRecord(ind_id,r_date,r_start,r_end,r_where,r_who);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-record/{ass_id}")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa record assessment của individual
        public async Task<IActionResult> UpdateRecord(string ass_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who)
        {
            var response = await _assessmentService.UpdateRecord(ass_id, r_date, r_start, r_end, r_where, r_who);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete-record")]
        //[Authorize(Roles="teacher")]
        //Xóa record assessment của individual
        public async Task<IActionResult> DeleteRecord(string ass_id)
        {
            var response = await _assessmentService.DeleteRecord(ass_id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        //[HttpPost("create-behaviour")]
        ////[Authorize(Roles="teacher")]
        ////Tạo analyze behaviour assessment của individual
        //public async Task<IActionResult> CreateAnalyzeBehaviour(string ass_id, string ana_behaviour)
        //{
        //    var response = await _assessmentService.CreateAnalyzeBehaviour(ass_id, ana_behaviour);

        //    if (response.Result == null)
        //        return BadRequest(response);

        //    return Ok(response);
        //}

        [HttpPut("update-behaviour/{ass_id}")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa analyze behaviour assessment của individual
        public async Task<IActionResult> UpdateAnalyzeBehaviour(string ass_id, string ana_behaviour)
        {
            var response = await _assessmentService.UpdateAnalyzeBehaviour(ass_id, ana_behaviour);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete-behaviour")]
        //[Authorize(Roles="teacher")]
        //Xóa analyze behaviour assessment của individual
        public async Task<IActionResult> DeleteAnalyzeBehaviour(string ass_id)
        {
            var response = await _assessmentService.DeleteAnalyzeBehaviour(ass_id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
