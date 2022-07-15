using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// AnalyzeAntecedentActivity
    /// writter: HoangDDN
    /// Description: List,Add,Edit,Delete
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzeAntecedentActivityController : ControllerBase
    {
        private readonly IAnalyzeAntecedentActivityService _analyzeAntecedentActivityService;

        public AnalyzeAntecedentActivityController(IAnalyzeAntecedentActivityService analyzeAntecedentActivityService)
        {
            _analyzeAntecedentActivityService = analyzeAntecedentActivityService;
        }
        [HttpGet("get-all")]
        //Lấy danh sách tiền đề Activity
        public async Task<IActionResult> GetAll()
        {
            var response = await _analyzeAntecedentActivityService.GetAll();

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpGet("get-by-id")]
        //Lấy 1 tiền đề Activity
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid || id.CheckRequest())
                return BadRequest(ModelState);
            var response = await _analyzeAntecedentActivityService.GetById(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("create")]
        //Tạo mới tiền đề Activity
        public async Task<IActionResult> Create(string content)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _analyzeAntecedentActivityService.Create(content);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("update")]
        //Chỉnh sửa tiền đề Activity
        public async Task<IActionResult> Update([FromBody] AnalyzeAntecedentActivityRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _analyzeAntecedentActivityService.Update(request.Id, request.Content);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("delete")]
        //Xóa tiền đề Activity
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _analyzeAntecedentActivityService.Delete(id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
