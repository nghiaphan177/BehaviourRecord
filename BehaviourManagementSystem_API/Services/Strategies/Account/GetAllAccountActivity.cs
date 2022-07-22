using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services.Strategies.Account
{
    public class GetAllAccountActivity : IAccountStratery
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IRoleService _roleService;

        public GetAllAccountActivity(ApplicationDbContext context, UserManager<User> userManager, IRoleService roleService)
        {
            _context = context;
            _userManager = userManager;
            _roleService = roleService;
        }

        public async Task<ResponseResult<List<UserProfileRequest>>> GetAccountArgsAsync(UserProfileRequest request)
        {
            try
            {
                if(!await _context.Users.AnyAsync())
                    return new ResponseResultError<List<UserProfileRequest>>("Dữ liệu hiện tại không đang rỗng");

                if(request.Active.CheckRequest())
                    return new ResponseResultError<List<UserProfileRequest>>("Không tìm thấy dữ liệu theo yêu cầu.");

                var users = new List<UserProfileRequest>();

                foreach(var user in await _userManager.Users.ToListAsync())
                {
                    var role = await _roleService.GetRoleNameByUserId(user.Id.ToString());
                    if(role.Result.ToLower() == request.RoleName.ToLower() &&
                        role.Result.ToLower() == "teacher" &&
                        user.Activity == true)
                    {
                        users.Add(new UserProfileRequest
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
                        });
                    }
                }
                return new ResponseResultSuccess<List<UserProfileRequest>>(users);
            }
            catch(Exception ex)
            {
                return new ResponseResultError<List<UserProfileRequest>>(ex.Message);
            }
        }
    }
}
