using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
    public interface IUserAPIClient
    {
        Task<ResponseResult<List<UserResponse>>> GetAllUser();
        Task<ResponseResult<UserResponse>> GetUserById(string id);
    }
}
