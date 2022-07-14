using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<string>> GetRoleNameByUserId(string id)
        {
            var userRole = await _context.UserRoles.FindAsync(new Guid(id));
            if (userRole == null)
                return new ResponseResultError<string>(new string[] {
                "Error",
                "UserNotRole",
                "Tải khoản chưa xét quyền truy cập"
                });

            var role = await _context.Roles.FindAsync(userRole.RoleId);

            return new ResponseResultSuccess<string>(role.NormalizedName);
        }
    }
}
