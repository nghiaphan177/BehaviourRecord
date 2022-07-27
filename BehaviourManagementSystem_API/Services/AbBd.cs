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

        public async Task<ResponseResult<List<Tuple<int, int>>>> GetCountAllAccountRegisterOfYear(string year)
        {
            try
            {
                var index = 0;
                var lenght = int.Parse(year);
                var result = new List<Tuple<int, int>>();
                while(index < lenght)
                {
                    var user = await _context.Users
                        .CountAsync(prop => prop.CreateDate.Value.Month == (index + 1));

                    result.Add(new Tuple<int, int>(index + 1, user));

                    index++;
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
