
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
        Task<ResponseResult<AssessmentRequest>> CreateRecord( AssessmentRequest content);
        Task<ResponseResult<AssessmentRequest>> CreateRecordBehaviour(string AssId, string content);
        Task<ResponseResult<AssessmentRequest>> UpdateFuntionAntecedent(string AssId, string content);
        Task<ResponseResult<AssessmentRequest>> UpdateFuntionConsequence(string AssId, string content);
        Task<ResponseResult<AssessmentRequest>> UpdateAnalyzeAntecedent(string AssId, AssessmentRequest content);
        Task<ResponseResult<AssessmentRequest>> UpdateRecord(string AssId, AssessmentRequest content);
        Task<ResponseResult<AssessmentRequest>> UpdateAnalyzeConsequence(string AssId, AssessmentRequest content);
        Task<ResponseResult<List<AssessmentRequest>>> Update(AssessmentRequest request);
        Task<ResponseResult<List<AssessmentRequest>>> Delete(string id);
        Task<ResponseResult<string>> DeleteAssementIndi(string id);
    }
}
