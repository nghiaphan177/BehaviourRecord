using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Assesstment
{
    public interface IAssessmentAPIClient
    {
        Task<ResponseResult<List<AssessmentRequest>>> GetAll(string IndiID);
        Task<ResponseResult<AssessmentRequest>> Get(string id);
        Task<ResponseResult<Assessment>> CreateRecord(string IndiId, AssessmentRequest content);
        Task<ResponseResult<Assessment>> CreateRecordBehaviour(string AssId, AssessmentRequest content);
        Task<ResponseResult<Assessment>> CreateRecordAntecedent(string AssId, AssessmentRequest content);
        Task<ResponseResult<Assessment>> CreateRecordConsequence(string AssId, AssessmentRequest content);
        Task<ResponseResult<List<AssessmentRequest>>> Update(AssessmentRequest request);
        Task<ResponseResult<List<AssessmentRequest>>> Delete(string id);
    }
}
