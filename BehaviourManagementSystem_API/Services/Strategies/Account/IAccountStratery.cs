using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services.Strategies.Account
{
    public interface IAccountStratery
    {
        Task<ResponseResult<List<UserProfileRequest>>> GetAccountArgsAsync(UserProfileRequest request);
    }
}