using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
    /// <summary>
    /// AnalyzeAntecedentPerceive
    /// writter: HoangDDN
    /// Description: List,Add,Edit,Delete
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzeAntecedentPerciceController : ControllerBase
    {
        private readonly IAnalyzeAntecedentPerceiveService _analyzeAntecedentPerceiveService;

        public AnalyzeAntecedentPerciceController(IAnalyzeAntecedentPerceiveService analyzeAntecedentPerceiveService)
        {
            _analyzeAntecedentPerceiveService = analyzeAntecedentPerceiveService;
        }
        [HttpGet("get-all")]
        //Lấy danh sách tiền đề Percive
        public async Task<IActionResult> GetAll()
        {
            var response = await _analyzeAntecedentPerceiveService.GetAll();

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("create")]
        //Tạo mới tiền đề Percive
        public async Task<IActionResult> Create(string content)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _analyzeAntecedentPerceiveService.Create(content);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("update")]
        //Chỉnh sửa tiền đề Percive
        public async Task<IActionResult> Update([FromBody] AnalyzeAntecedentPerciceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _analyzeAntecedentPerceiveService.Update(request.Id, request.Content);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("delete")]
        //Xóa tiền đề Percive
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _analyzeAntecedentPerceiveService.Delete(id);

            if (response.Result == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
