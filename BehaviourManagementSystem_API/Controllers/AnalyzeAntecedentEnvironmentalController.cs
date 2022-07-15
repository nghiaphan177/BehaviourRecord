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
    /// AnalyzeAntecedentEnvironmental
    /// writter: HoangDDN
    /// Description: List,Add,Edit,Delete
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzeAntecedentEnvironmentalController : ControllerBase
    {
        private readonly IAnalyzeAntecedentEnvironmentalService _analyzeAntecedentEnvironmentalService;

        public AnalyzeAntecedentEnvironmentalController(IAnalyzeAntecedentEnvironmentalService analyzeAntecedentEnvironmentalService)
        {
            _analyzeAntecedentEnvironmentalService = analyzeAntecedentEnvironmentalService;
        }

        [HttpGet("get-all")]
        //Lấy danh sách tiền đề Environmental
        public async Task<IActionResult> GetAll()
        {
            var response = await _analyzeAntecedentEnvironmentalService.GetAll();

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpGet("get-by-id")]
        //Lấy 1 tiền đề Environmental
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid || id.CheckRequest())
                return BadRequest(ModelState);
            var response = await _analyzeAntecedentEnvironmentalService.GetById(id);
            if (response.Result == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("create")]
        //Tạo mới tiền đề Environmental
        public async Task<IActionResult> Create(string content)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _analyzeAntecedentEnvironmentalService.Create(content);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("update")]
        //Chỉnh sửa tiền đề Environmental
        public async Task<IActionResult> Update([FromBody] AnalyzeAntecedentEnvironmentalRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _analyzeAntecedentEnvironmentalService.Update(request.Id, request.Content);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("delete")]
        //Xóa tiền đề Environmental
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _analyzeAntecedentEnvironmentalService.Delete(id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
