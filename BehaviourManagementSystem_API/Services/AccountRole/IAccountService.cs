using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
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
        Task<ResponseResult<List<UserProfileRequest>>> GetAll(string role);
        Task<ResponseResult<UserProfileRequest>> GetUser(string id);
        Task<ResponseResult<ConfirmEmailRequest>> Register(RegisterRequest request);
        Task<ResponseResult<UserProfileRequest>> VerifyEmail(ConfirmEmailRequest request);
        Task<ResponseResult<ConfirmEmailRequest>> ResenConfirmEmail(string email);
        Task<ResponseResult<ResetPasswordRequest>> ForgotPassword(string userNameOfEmail);
        Task<ResponseResult<bool>> CheckEmailConfirmed(string email);
        Task<ResponseResult<bool>> ResetPassword(ResetPasswordRequest repuest);
        Task<ResponseResult<UserProfileRequest>> UpdateUserProfile(UserProfileRequest request);
        Task<ResponseResult<List<UserProfileRequest>>> CreateUserProfile(UserProfileRequest request);
        Task<ResponseResult<List<UserProfileRequest>>> DeleteUserProfile(string id);
        Task<ResponseResult<UserProfileRequest>> ChangePassword(ResetPasswordRequest repuest);
    }
}