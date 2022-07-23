using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Intervention
{
    public interface IInterventionAPIClient
    {
        Task<ResponseResult<List<InterventionRequest>>> GetAll(string ass_id);
        Task<ResponseResult<InterventionRequest>> Get(string id);
        Task<ResponseResult<InterventionRequest>> CreateProfile(InterventionRequest request);
        Task<ResponseResult<List<InterventionRequest>>> Delete(string id);
        Task<ResponseResult<InterventionRequest>> Update(InterventionRequest request);
    }
}
