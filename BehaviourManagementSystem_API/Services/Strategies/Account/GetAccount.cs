using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services.Strategies.Account
{
    public class GetAccount : IAccountStratery
    {
        private readonly UserManager<User> _userManager;
        private readonly IRoleService _roleService;

        public GetAccount(UserManager<User> userManager, IRoleService roleService)
        {
            _userManager = userManager;
            _roleService = roleService;
        }

        public async Task<ResponseResult<List<UserProfileRequest>>> GetAccountArgsAsync(UserProfileRequest request)
        {
            try
            {
                if(request.Id.CheckRequest())
                    return new ResponseResultError<List<UserProfileRequest>>("Không tìm thấy profile");

                var user = await _userManager.FindByIdAsync(request.Id);

                if(user == null)
                    return new ResponseResultError<List<UserProfileRequest>>("Thông tin không tồn tại");

                var role = await _roleService.GetRoleNameByUserId(user.Id.ToString());

                return new ResponseResultSuccess<List<UserProfileRequest>>(new List<UserProfileRequest>()
                {
                    new UserProfileRequest()
                    {
                        Id = user.Id.ToString(),
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        DOB = user.DOB,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Address = user.Address,
                        AvtName = user.AvtName,
                        Active = user.Activity,
                        RoleName = role.Result
                    }
                });
            }
            catch(Exception ex)
            {
                return new ResponseResultError<List<UserProfileRequest>>(ex.Message);
            }
        }
    }
}
