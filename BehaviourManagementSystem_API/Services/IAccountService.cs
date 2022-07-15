using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    /// <summary>
    /// Interface IAccountServiceB. Design parttern repository.
    /// Writer: DuyLH4
    /// </summary>
    public interface IAccountService
    {
        Task<ResponseResult<string>> Login(LoginRequest request);
        Task<ResponseResult<List<UserResponse>>> GetAll();
        Task<ResponseResult<UserResponse>> GetUser(string id);
	}
}