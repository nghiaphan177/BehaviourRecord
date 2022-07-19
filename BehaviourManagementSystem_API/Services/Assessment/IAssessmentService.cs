using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IAssessmentService
    {
        Task<ResponseResult<List<AssessmentRequest>>> GetAll(string ind_id);
        Task<ResponseResult<AssessmentRequest>> Detail(string id);
    }
}
