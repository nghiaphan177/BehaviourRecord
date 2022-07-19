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

        public async Task<ResponseResult<IndividualRequest>> Detail(string id)
        {
            var a = await _context.Users.Include("Individuals").FirstAsync(p => p.Individuals.Any(i => i.UserId == p.Id));
            var fullname = a.FirstName + " " + a.LastName;
            var email = a.Email;
            var address = a.Address;
            var phone = a.PhoneNumber;
            var gender = a.Gender;
            DateTime dayofbirth = a.DOB;
            if (!await _context.Individuals.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<IndividualRequest>("Id không tồn tại");
            var obj = await _context.Individuals.FindAsync(new Guid(id));
            return new ResponseResultSuccess<IndividualRequest>(new IndividualRequest()
            {
                Id = obj.Id.ToString(),
                FullName = fullname,
                Email = email,
                Organization = obj.Organization,
                Address = address,
                PhoneIndividual = phone,
                Gender = gender,
                DateofBirth = dayofbirth
            });
        }

        public async Task<ResponseResult<List<IndividualRequest>>> GetAll()
        {
            var a = await _context.Users.Include("Individuals").FirstAsync(p => p.Individuals.Any(i => i.UserId == p.Id));
            var fullname = a.FirstName + " " + a.LastName;
            var email = a.Email;
            if (!await _context.Individuals.AnyAsync())
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
