using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public class AbBd : IAbBd
    {
        private readonly ApplicationDbContext _context;

        public AbBd(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<Tuple<int, int>>> GetAllAccountNotVerifyMail()
        {
            if(_context.Users == null)
                return new ResponseResultError<Tuple<int, int>>("Hệ thống chưa có tài khoản người dùng.");

            var count = await _context.Users.CountAsync(prop => prop.EmailConfirmed == false);
            var total = await _context.Users.CountAsync();

            return new ResponseResultSuccess<Tuple<int, int>>(new Tuple<int, int>(count, total));
        }

        public async Task<ResponseResult<Tuple<int, int, int>>> GetAllStudentAndTeacherAndAllAccount()
        {
            var role_sd = await _context.Roles.FirstOrDefaultAsync(prop => prop.Name == "student");
            var role_tc = await _context.Roles.FirstOrDefaultAsync(prop => prop.Name == "teacher");

            var count_sd = await _context.UserRoles.CountAsync(prop => prop.RoleId == role_sd.Id);
            var count_tc = await _context.UserRoles.CountAsync(prop => prop.RoleId == role_tc.Id);

            var total = count_sd + count_tc;

            return new ResponseResultSuccess<Tuple<int, int, int>>(new Tuple<int, int, int>(
                count_sd,
                count_tc,
                total));
        }

        public async Task<ResponseResult<List<Tuple<int, int>>>> GetCountAllAccountRegisterOfMonth(int m, int y)
        {

            try
            {
                int days = DateTime.DaysInMonth(y, m);
                var index = 0;
                var result = new List<Tuple<int, int>>();

                while(index < days)
                {
                    var count_user = await _context.Users
                        .CountAsync(prop => prop.CreateDate.Value.Year == y &&
                        prop.CreateDate.Value.Month == m &&
                        prop.CreateDate.Value.Day == (index + 1));
                    index++;
                    result.Add(new Tuple<int, int>(index, count_user));
                }

                return new ResponseResultSuccess<List<Tuple<int, int>>>(result);
            }
            catch(Exception ex)
            {
                return new ResponseResultError<List<Tuple<int, int>>>(ex.Message);
            }
        }

        public async Task<ResponseResult<List<Tuple<int, int>>>> GetCountAllAccountRegisterOfYear(string year)
        {
            try
            {
                var index = 0;
                var result = new List<Tuple<int, int>>();

                while(index < 12)
                {
                    var count_user = await _context.Users
                        .CountAsync(prop => prop.CreateDate.Value.Month == (index + 1) &&
                        prop.CreateDate.Value.Year == int.Parse(year));
                    index++;
                    result.Add(new Tuple<int, int>(index, count_user));
                }

                return new ResponseResultSuccess<List<Tuple<int, int>>>(result);
            }
            catch(Exception ex)
            {
                return new ResponseResultError<List<Tuple<int, int>>>(ex.Message);
            }
        }

        public async Task<ResponseResult<List<Tuple<string, int>>>> GetCountAllStudentOfAllClasses(Guid guid)
        {
            var classes = await GetAllClassesOfTeacher(guid);

            var result = new List<Tuple<string, int>>();

            foreach(var className in classes)
            {
                var count = await _context.Individuals.CountAsync(prop => prop.Organization == className);
                result.Add(new Tuple<string, int>(className, count));
            }

            return new ResponseResultSuccess<List<Tuple<string, int>>>(result);
        }

        private async Task<List<string>> GetAllClassesOfTeacher(Guid guid)
        {
            var inds = await _context.Individuals.Where(prop => prop.TeacherId == guid).ToListAsync();

            var classes = new List<string>();

            foreach(var ind in inds)
            {
                if(classes.Count > 0)
                {
                    var count = 0;
                    foreach(var className in classes)
                    {
                        if(ind.Organization == className)
                            break;
                        count++;

                        if(count == classes.Count)
                            classes.Add(ind.Organization);
                    }
                }
                else
                    classes.Add(ind.Organization);
            }
            return classes;
        }
    }
}
