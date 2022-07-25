using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IRoleService
    {
        Task<ResponseResult<string>> GetRoleNameByUserId(string id);
        Task<ResponseResult<List<RoleRequest>>> GetAll();
    }
}