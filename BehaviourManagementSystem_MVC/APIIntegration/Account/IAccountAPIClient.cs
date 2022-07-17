﻿using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Account
{
	public interface IAccountAPIClient
	{
		Task<ResponseResult<string>> Login(LoginRequest request);

		Task<ResponseResult<ConfirmEmailRequest>> Register(RegisterRequest request);

		Task<ResponseResult<UserProfileRequest>> ConfirmEmail(ConfirmEmailRequest request);

		Task<ResponseResult<ConfirmEmailRequest>> ResendConfirmEmail(string email);

		Task<ResponseResult<ResetPasswordRepuest>> ForgotPassword(string userNameOrEmail);

		Task<ResponseResult<string>> ResetPassword(ResetPasswordRepuest repuest);

		Task<ResponseResult<bool>> GetEmailConfirmed(string email);
	}
}