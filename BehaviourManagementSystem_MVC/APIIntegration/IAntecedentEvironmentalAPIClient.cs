using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponsesModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
    public interface IAntecedentEvironmentalAPIClient
    {
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmentalResponse>>> GetAll();
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmentalResponse>>> Create(string content);
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmentalResponse>>> Update(AnalyzeAntecedentEnvironmentalRequest request);
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmentalResponse>>> Delete(string id);
    }
}