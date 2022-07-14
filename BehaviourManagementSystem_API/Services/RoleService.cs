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
            var user = await _context.Users.FindAsync(new Guid(id));
            if (user == null)
                return new ResponseResultError<string>("Tài khoản không tồn tại");

            var userRole = _context.UserRoles.FirstOrDefault(prop => prop.UserId == user.Id);

            if (userRole == null)
                return new ResponseResultError<string>("Tài khoản hiện tại chưa được cấp quyền");

            var role = await _context.Roles.FindAsync(userRole.RoleId);
            if (role == null)
                return new ResponseResultError<string>("Không có quyền truy cập hợp lệ");

            return new ResponseResultSuccess<string>(role.NormalizedName);
        }
    }
}
