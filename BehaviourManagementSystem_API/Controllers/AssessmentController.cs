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
    /// Description: List,Detail,CRUD Record, Analyze, Funtion
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

        [HttpGet("detail")]
        //[Authorize(Roles="admin,teacher")]
        //Lấy 1 assessment của individual
        public async Task<IActionResult> Detail(string ass_id)
        {
            var response = await _assessmentService.Detail(ass_id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete")]
        //[Authorize(Roles="teacher")]
        //Xóa toàn bộ intervention có trong assessment của individual
        public async Task<IActionResult> Delete(string ass_id)
        {
            var response = await _assessmentService.Delete(ass_id);

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
        public async Task<IActionResult> UpdateRecord(string ass_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who)
        {
            var response = await _assessmentService.UpdateRecord(ass_id, r_date, r_start, r_end, r_where, r_who);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpPut("update-behaviour")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa analyze behaviour assessment của individual
        public async Task<IActionResult> UpdateAnalyzeBehaviour(string ass_id, string ana_behaviour)
        {
            var response = await _assessmentService.UpdateAnalyzeBehaviour(ass_id, ana_behaviour);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-antecedent")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa analyze antecedent assessment của individual
        public async Task<IActionResult> UpdateAnalyzeAntecedent(string ass_id, string ana_ant_per, string ana_ant_envi, string ana_ant_act)
        {
            var response = await _assessmentService.UpdateAnalyzeAntecedent(ass_id, ana_ant_per,ana_ant_envi,ana_ant_act);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-consequence")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa analyze consequences assessment của individual
        public async Task<IActionResult> UpdateAnalyzeConsequence(string ass_id, string ana_con_per, string ana_con_envi, string ana_con_act)
        {
            var response = await _assessmentService.UpdateAnalyzeConsequence(ass_id, ana_con_per, ana_con_envi, ana_con_act);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-funtion-antecedent")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa funtion antecedent assessment của individual
        public async Task<IActionResult> UpdateFuntionAntecedent(string ass_id, string fun_ant)
        {
            var response = await _assessmentService.UpdateFuntionAntecedent(ass_id, fun_ant);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-funtion-consequece")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa funtion consequece assessment của individual
        public async Task<IActionResult> UpdateFuntionConsequece(string ass_id, string fun_con)
        {
            var response = await _assessmentService.UpdateFuntionConsequece(ass_id, fun_con);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }


    }
}
