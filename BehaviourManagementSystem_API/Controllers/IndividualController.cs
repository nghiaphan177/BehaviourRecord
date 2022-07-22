﻿using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
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

            if(response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("detail")]
        //[Authorize]
        //Lấy 1 Individual
        public async Task<IActionResult> Detail(string id)
        {
            var response = await _individualService.Detail(id);

            if(response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("GetAllIndWithAssessment")]
        public async Task<IActionResult> GetAllIndWithAssessment(string id)
        {
            if(id.CheckRequest())
                return BadRequest("Lỗi truy xuất thông tin với tài khoản của bạn.");

            var res = await _individualService.GetAllIndWithAssessment(id);

            if(!res.Success)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpGet("GetAllIndWithTeacher")]
        public async Task<IActionResult> GetAllIndWithTeacher(string id)
        {
            if(id.CheckRequest())
                return BadRequest("Lỗi truy xuất thông tin với tài khoản của bạn.");

            var res = await _individualService.GetAllIndWithTeacher(id);

            if(!res.Success)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] IndAssessRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(request.Gender.ToLower() == "nam" || request.Gender.ToLower() == "nữ")
            {
                var res = await _individualService.Create(request);

                if(!res.Success)
                    return BadRequest(res);

                return Ok(res);
            }

            return BadRequest("Thông tin giới tính không hợp lệ");
        }

        [HttpGet("GetIndById")]
        public async Task<IActionResult> GetIndById(string id)
        {
            if(id.CheckRequest())
                return BadRequest("Không tìm thấy thông tin cập nhật");

            var res = await _individualService.GetIndById(id);

            if(!res.Success)
                return BadRequest(res);

            return Ok(res);
        }


        //[HttpPut("Update")]
        //public async Task<IActionResult> Udpate([FromBody] IndAssessRequest request)
        //{

        //}
    }
}
