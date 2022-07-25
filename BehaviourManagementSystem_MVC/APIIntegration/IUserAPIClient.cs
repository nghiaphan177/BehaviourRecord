using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
    public interface IUserAPIClient
    {
        Task<ResponseResult<List<UserProfileRequest>>> GetAllUser();
        Task<ResponseResult<List<UserProfileRequest>>> GetAllUserExAdmin(UserProfileRequest request);
        Task<ResponseResult<List<UserProfileRequest>>> Create(UserProfileRequest request);
        Task<ResponseResult<UserProfileRequest>> GetUserById(string id);
        Task<ResponseResult<UserProfileRequest>> UpdateUser(string id, UserProfileRequest user);
        Task<ResponseResult<List<UserProfileRequest>>> DeleteUser(string id);
        Task<ResponseResult<List<RoleRequest>>> GetRole();
    }
}
