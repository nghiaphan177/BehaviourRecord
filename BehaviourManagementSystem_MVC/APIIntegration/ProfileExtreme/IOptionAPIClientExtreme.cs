using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.ProfileExtreme
{
    public interface IOptionAPIClientExtreme
    {
        Task<ResponseResult<List<OptionsRequest>>> GetAll();
        Task<ResponseResult<OptionsRequest>> Get(string id);
        Task<ResponseResult<List<OptionsRequest>>> Create(string content);
        Task<ResponseResult<List<OptionsRequest>>> Update(OptionsRequest request);
        Task<ResponseResult<List<OptionsRequest>>> Delete(string id);
    }
}
