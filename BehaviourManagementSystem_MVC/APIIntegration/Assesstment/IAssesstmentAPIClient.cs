using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Assesstment
{
    public interface IAssesstmentAPIClient
    {
        Task<ResponseResult<List<AssessmentRequest>>> GetAll(string IndiID);
        Task<ResponseResult<AssessmentRequest>> Get(string id);
        Task<ResponseResult<List<AssessmentRequest>>> Create(string IndiId, AssessmentRequest content);
        Task<ResponseResult<List<AssessmentRequest>>> Update(AssessmentRequest request);
        Task<ResponseResult<List<AssessmentRequest>>> Delete(string id);
    }
}
