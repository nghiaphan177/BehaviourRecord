using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IInterventionService
    {
        Task<ResponseResult<List<InterventionRequest>>> GetAll(string ass_id);
        Task<ResponseResult<InterventionRequest>> Detail(string id);
    }
}
