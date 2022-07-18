using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Profile
{
    public interface IRecoveryOptionAPIClient
    {
        Task<ResponseResult<List<OptionsRequest>>> GetAll();
        Task<ResponseResult<OptionsRequest>> Get(string id);
        Task<ResponseResult<List<OptionsRequest>>> Create(string content);
        Task<ResponseResult<List<OptionsRequest>>> Update(OptionsRequest request);
        Task<ResponseResult<List<OptionsRequest>>> Delete(string id);
    }
}
