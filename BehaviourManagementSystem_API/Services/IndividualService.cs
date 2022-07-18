using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public class IndividualService : IIndividualService
    {
        private readonly ApplicationDbContext _context;

        public IndividualService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<ResponseResult<IndividualRequest>> Detail(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseResult<List<IndividualRequest>>> GetAll()
        {

            //var find = _context.Individuals.Where(p => p.UserId.ToString() == a.ToString());
            //var a = await _context.Users.FindAsync(new Guid("fb9039e1-9343-4275-85f4-061bfd2dd342"));
            var a = await _context.Users.FindAsync(new Guid());
            var fullname = a.FirstName +" "+ a.LastName;
            var email = a.Email;          
            if(!await _context.Individuals.AnyAsync())
                return new ResponseResultError<List<IndividualRequest>>("Hiện tại không có dữ liệu");
            var individual = await _context.Individuals.ToListAsync();
            var result = new List<IndividualRequest>();          
            foreach (var item in individual)
            {
                result.Add(new IndividualRequest()
                {
                    Id = item.Id.ToString(),
                    FullName = fullname,
                    Email = email,
                    Organization = item.Organization,
                    
                    

                });
            }
            return new ResponseResultSuccess<List<IndividualRequest>>(result);
        }
    }
}
