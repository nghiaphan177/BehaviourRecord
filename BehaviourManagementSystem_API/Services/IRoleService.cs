using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IRoleService
    {
        Task<ResponseResult<string>> GetRoleNameByUserId(string id);
    }
}