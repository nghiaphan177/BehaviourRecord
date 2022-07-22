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
    public class GetAllAccountStudentOfTeacher : IAccountStratery
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IRoleService _roleService;

        public GetAllAccountStudentOfTeacher(ApplicationDbContext context, UserManager<User> userManager, IRoleService roleService)
        {
            _context = context;
            _userManager = userManager;
            _roleService = roleService;
        }

        public async Task<ResponseResult<List<UserProfileRequest>>> GetAccountArgsAsync(UserProfileRequest request)
        {
            try
            {
                if(!await _context.Individuals.AnyAsync())
                    return new ResponseResultError<List<UserProfileRequest>>("Dữ liệu hiện tại không đang rỗng");

                var individuals = await _context.Individuals
                    .Where(prop => prop.TeacherId == new Guid(request.TeacherId))
                    .ToListAsync();

                var users = new List<UserProfileRequest>();

                foreach(var ind in individuals)
                {
                    var user = await _userManager.FindByIdAsync(ind.StudentId.ToString());

                    var role = await _roleService.GetRoleNameByUserId(user.Id.ToString());

                    if(role.Result.ToLower() == "student")
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
                return new ResponseResultSuccess<List<UserProfileRequest>>(users);
            }
            catch(Exception ex)
            {
                return new ResponseResultError<List<UserProfileRequest>>(ex.Message);
            }
        }
    }
}
