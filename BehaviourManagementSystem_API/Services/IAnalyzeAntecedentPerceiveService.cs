using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponsesModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IAnalyzeAntecedentPerceiveService
    {
        Task<ResponseResult<List<AnalyzeAntecedentPerceive>>> Create(string content);
        Task<ResponseResult<List<AnalyzeAntecedentPerceive>>> Update(string id, string content);
        Task<ResponseResult<List<AnalyzeAntecedentPerceive>>> Delete(string id);
        Task<ResponseResult<List<AnalyzeAntecedentPerciveResponse>>> GetAll();
    }
}
