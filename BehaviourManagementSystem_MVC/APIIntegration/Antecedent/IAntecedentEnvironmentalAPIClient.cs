using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
    public interface IAntecedentEnvironmentalAPIClient
    {
        Task<ResponseResult<List<OptionsRequest>>> GetAll();
        Task<ResponseResult<List<OptionsRequest>>> Create(string content);
        Task<ResponseResult<List<OptionsRequest>>> Update(OptionsRequest request);
        Task<ResponseResult<List<OptionsRequest>>> Delete(string id);
    }
}