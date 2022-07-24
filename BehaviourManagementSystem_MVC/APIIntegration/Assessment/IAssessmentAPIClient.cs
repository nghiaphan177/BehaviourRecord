
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
        Task<ResponseResult<AssessmentRequest>> CreateRecord(string IndiId, AssessmentRequest content);
        Task<ResponseResult<AssessmentRequest>> CreateRecordBehaviour(string AssId, string content);
        Task<ResponseResult<AssessmentRequest>> CreateRecordAntecedent(string AssId, AssessmentRequest content);
        Task<ResponseResult<AssessmentRequest>> CreateRecordConsequence(string AssId, AssessmentRequest content);
        Task<ResponseResult<List<AssessmentRequest>>> Update(AssessmentRequest request);
        Task<ResponseResult<List<AssessmentRequest>>> Delete(string id);
    }
}
