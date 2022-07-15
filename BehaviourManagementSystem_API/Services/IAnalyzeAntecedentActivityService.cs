using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponsesModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IAnalyzeAntecedentActivityService
    {
        Task<ResponseResult<List<AnalyzeAntecedentActivity>>> Create(string content);
        Task<ResponseResult<List<AnalyzeAntecedentActivity>>> Update(string id, string content);
        Task<ResponseResult<List<AnalyzeAntecedentActivity>>> Delete(string id);
        Task<ResponseResult<List<AnalyzeAntecedentActivityResponse>>> GetAll();
        Task<ResponseResult<AnalyzeAntecedentActivityResponse>> GetById(string id);
    }
}
