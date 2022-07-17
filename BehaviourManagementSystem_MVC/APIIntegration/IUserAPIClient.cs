using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
    public interface IUserAPIClient
    {
        Task<ResponseResult<List<UserProfileRequest>>> GetAllUser();
        Task<ResponseResult<UserProfileRequest>> GetUserById(string id);
    }
}
