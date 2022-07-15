using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponsesModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IAnalyzeAntecedentEnvironmentalService
    {
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmental>>> Create(string content);
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmental>>> Update(string id, string content);
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmental>>> Delete(string id);
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmentalResponse>>> GetAll();
    }
}
