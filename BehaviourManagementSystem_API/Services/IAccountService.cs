using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IAccountService
    {
        Task<ResponseResult<string>> LoginAdmin(LoginAdminRequest request);
    }
}