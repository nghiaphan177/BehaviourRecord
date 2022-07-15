using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IProfileMildService
    {
        Task<ResponseResult<List<ProfileMild>>> Create(string content);
        Task<ResponseResult<List<ProfileMild>>> Update(string id, string content);
        Task<ResponseResult<List<ProfileMild>>> Delete(string id);
        Task<ResponseResult<List<ProfileMildResponse>>> GetAll();

    }
}
