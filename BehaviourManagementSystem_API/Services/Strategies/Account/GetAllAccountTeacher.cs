using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services.Strategies.Account
{
    public class GetAllAccountTeacher : IAccountStratery
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IRoleService _roleService;

        public GetAllAccountTeacher(ApplicationDbContext context, UserManager<User> userManager, IRoleService roleService)
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

                var role = await _context.Roles
                    .FirstAsync(prop => prop.NormalizedName == "TEACHER" &&
                    prop.NormalizedName == request.RoleName);

                var userRoles = await _context.UserRoles
                    .Where(prop => prop.RoleId == role.Id)
                    .ToListAsync();

                var users = new List<UserProfileRequest>();

                foreach(var userRole in userRoles)
                {
                    var user = await _userManager.FindByIdAsync(userRole.UserId.ToString());
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
                        Img = user.Img,
                        Active = user.Activity,
                        RoleName = role.NormalizedName
                    });
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
