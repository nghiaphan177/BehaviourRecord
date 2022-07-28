using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}
