using BehaviourManagementSystem_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// Intervention
    /// writter: HoangDDN
    /// Description: List,Detail, CRUD Profile, Manage, Prevent
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

        [HttpDelete("delete")]
        //[Authorize(Roles="teacher")]
        //Xóa intervention của assessment
        public async Task<IActionResult> Delete(string int_id)
        {
            var response = await _interventionService.Delete(int_id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("create-profile")]
        //[Authorize(Roles="teacher")]
        //Tạo profile intervention của assessment
        public async Task<IActionResult> CreateProfile(string ass_id, DateTime p_date, string p_mild, string p_moder, string p_extre, string p_reco)
        {
            var response = await _interventionService.CreateProfile(ass_id, p_date, p_mild, p_moder, p_extre, p_reco);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-profile")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa profile intervention của assessment
        public async Task<IActionResult> UpdateProfile(string int_id, DateTime p_date, string p_mild, string p_moder, string p_extre, string p_reco)
        {
            var response = await _interventionService.UpdateProfile(int_id, p_date, p_mild, p_moder, p_extre, p_reco);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-manage")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa manage intervention của assessment
        public async Task<IActionResult> UpdateManage(string int_id, string m_mild, string m_moder, string m_extre, string m_reco)
        {
            var response = await _interventionService.UpdateManage(int_id, m_mild, m_moder, m_extre, m_reco);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-prevent")]
        //[Authorize(Roles="teacher")]
        //Chỉnh sửa prevent intervention của assessment
        public async Task<IActionResult> UpdatePrevent(string int_id, string pre_status, string pre_act, string pre_envi, string pre_inter)
        {
            var response = await _interventionService.UpdatePrevent(int_id, pre_status, pre_act, pre_envi, pre_inter);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
