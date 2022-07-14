using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
    public interface IAccountAPIClient
    {
        Task<ResponseResult<string>> Login(LoginAdminRequest request);
    }
}