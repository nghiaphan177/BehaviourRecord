using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponseModels;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
	public interface IAccountAPIClient
	{
		Task<ResponseResult<string>> Login(LoginRequest request);
		Task<ResponseResult<UserResponse>> ConfirmEmail(ConfirmEmailRequest request);
		Task<ResponseResult<ConfirmEmailRequest>> Register(RegisterRequest request);
	}
}