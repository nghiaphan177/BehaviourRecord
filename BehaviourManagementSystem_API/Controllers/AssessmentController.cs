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

        [HttpPost("create-record")]
        //[Authorize(Roles="teacher")]
        //Tạo record assessment của individual
        public async Task<IActionResult> CreateRecord(string ind_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who)
        {
            var response = await _assessmentService.CreateRecord(ind_id,r_date,r_start,r_end,r_where,r_who);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-record")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa record assessment của individual
        public async Task<IActionResult> UpdateRecord(string id, DateTime r_date, string r_start, string r_end, string r_where, string r_who)
        {
            var response = await _assessmentService.UpdateRecord(id, r_date, r_start, r_end, r_where, r_who);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete-record")]
        //[Authorize(Roles="teacher")]
        //Xóa record assessment của individual
        public async Task<IActionResult> DeleteRecord(string id)
        {
            var response = await _assessmentService.DeleteRecord(id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
