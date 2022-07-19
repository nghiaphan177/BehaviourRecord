using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IAnalyzeAntecedentEnvironmentalService
    {
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmental>>> Create(string content);
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmental>>> Update(string id, string content);
        Task<ResponseResult<List<AnalyzeAntecedentEnvironmental>>> Delete(string id);
        Task<ResponseResult<List<OptionsRequest>>> GetAll();
        Task<ResponseResult<OptionsRequest>> GetById(string id);
    }
}
