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

namespace BehaviourManagementSystem_API.Services
{
    public class IndividualService : IIndividualService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndividualService(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ResponseResult<List<IndAssessRequest>>> Create(IndAssessRequest request)
        {
            try
            {
            start:
                var id = Guid.NewGuid();
                var stamp = Guid.NewGuid();
                var user = await _userManager.FindByIdAsync(id.ToString());
                var ind = await _context.Individuals.FindAsync(id);
                if(user != null || ind != null)
                    goto start;

                user = new User()
                {
                    Id = id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    Gender = request.Gender,
                    DOB = request.DOB,
                    AvtName = request.AvtName ?? "default_avt.png",
                    Email = request.Email,
                    UserName = request.UserName,
                    NormalizedUserName = request.UserName.ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = stamp.ToString(),
                    ConcurrencyStamp = stamp.ToString().ToUpper(),
                    CreateDate = DateTime.Now.Date,
                    UpdateDate = DateTime.Now.Date
                };
                var result_save_user = await _userManager.CreateAsync(user, request.Password);
                if(result_save_user.Succeeded)
                {
                    ind = new Individual()
                    {
                        Id = id,
                        StudentId = id,
                        TeacherId = new Guid(request.TeacherId),
                        CreateDate = DateTime.Now.Date,
                        UpdateDate = DateTime.Now.Date,
                        Organization = request.Classes // thay đổi sao
                    };

                    await _context.Individuals.AddAsync(ind);
                    var result_save_ind = await _context.SaveChangesAsync();
                    if(result_save_ind > 0)
                    {
                        var role = await _context.Roles.FirstAsync(prop => prop.NormalizedName == "STUDENT");
                        await _context.UserRoles.AddAsync(new IdentityUserRole<Guid>
                        {
                            RoleId = role.Id,
                            UserId = user.Id,
                        });
                        await _context.SaveChangesAsync();

                        var result = await GetAllIndByTeacherId(request);
                        return new ResponseResultSuccess<List<IndAssessRequest>>(result);
                    }
                    else
                    {
                        var result = await GetAllIndByTeacherId(request);
                        return new ResponseResult<List<IndAssessRequest>>()
                        {
                            Success = true,
                            Result = result
                        };
                    }
                }
                else
                {
                    user = await _userManager.FindByIdAsync(id.ToString());
                    await _userManager.DeleteAsync(user);
                    await _context.SaveChangesAsync();

                    var result = await GetAllIndByTeacherId(request);
                    return new ResponseResult<List<IndAssessRequest>>()
                    {
                        Success = true,
                        Result = result
                    };
                }
            }
            catch(Exception ex)
            {
                return new ResponseResultError<List<IndAssessRequest>>(ex.Message);
            }
        }

        private async Task<List<IndAssessRequest>> GetAllIndByTeacherId(IndAssessRequest request)
        {
            var result = new List<IndAssessRequest>();

            var inds = await _context.Individuals
                .Where(prop => prop.TeacherId == new Guid(request.TeacherId))
                .ToListAsync();

            foreach(var r_ind in inds)
            {
                var r_user = await _userManager.FindByIdAsync(r_ind.StudentId.ToString());
                result.Add(new IndAssessRequest
                {
                    Ind_Id = r_ind.Id.ToString(),
                    FirstName = r_user.FirstName,
                    LastName = r_user.LastName,
                    Gender = r_user.Gender,
                    DOB = r_user.DOB,
                    Address = r_user.Address,
                    Classes = r_ind.Organization,
                    Email = r_user.Email
                });
            }

            return result;
        }

        public async Task<ResponseResult<IndividualRequest>> Detail(string id)
        {
            if(!await _context.Individuals.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<IndividualRequest>("Id không tồn tại");

            var obj = await _context.Individuals.FindAsync(new Guid(id));

            foreach(var user in await _context.Users.ToListAsync())
                if(obj.StudentId == user.Id)
                    return new ResponseResultSuccess<IndividualRequest>(new IndividualRequest()
                    {
                        Id = obj.Id.ToString(),
                        FullName = user.FirstName + " " + user.LastName,
                        Email = user.Email,
                        Organization = obj.Organization,
                        Address = user.Address,
                        PhoneIndividual = user.PhoneNumber,
                        Gender = user.Gender,
                        DOB = user.DOB,
                        UserId = obj.StudentId.ToString()
                    });
            return new ResponseResultError<IndividualRequest>("Lỗi hệ thống.");
        }

        public async Task<ResponseResult<List<IndividualRequest>>> GetAll()
        {
            if(!await _context.Individuals.AnyAsync())
                return new ResponseResultError<List<IndividualRequest>>("Hiện tại không có dữ liệu");
            var result = new List<IndividualRequest>();
            foreach(var ind in await _context.Individuals.ToListAsync())
            {
                foreach(var user in await _context.Users.ToListAsync())
                {
                    if(ind.StudentId == user.Id)
                    {
                        result.Add(new IndividualRequest()
                        {
                            Id = ind.Id.ToString(),
                            FullName = user.FirstName + " " + user.LastName,
                            Email = user.Email,
                            Organization = ind.Organization,
                            DOB = user.DOB,
                            Address = user.Address
                        });
                    }
                }
            }
            return new ResponseResultSuccess<List<IndividualRequest>>(result);
        }

        public async Task<ResponseResult<List<IndAssessRequest>>> GetAllIndWithAssessment(string id)
        {
            try
            {
                if(!await _context.Individuals.AnyAsync(prop => prop.TeacherId == new Guid(id)))
                    return new ResponseResultError<List<IndAssessRequest>>("Thông tin truy xuất không tồn tại");

                var result = new List<IndAssessRequest>();

                var inds = await _context.Individuals
                    .Where(prop => prop.TeacherId == new Guid(id))
                    .ToListAsync();

                foreach(var ind in inds)
                    if(await _context.Assessments
                        .CountAsync(prop => prop.IndividualId == ind.Id) > 0)
                    {
                        var user = await _userManager.FindByIdAsync(ind.StudentId.ToString());
                        result.Add(new IndAssessRequest
                        {
                            Ind_Id = ind.Id.ToString(),
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Gender = user.Gender,
                            DOB = user.DOB,
                            Address = user.Address,
                            Classes = ind.Organization,
                            Email = user.Email
                        });
                    }
                return new ResponseResultSuccess<List<IndAssessRequest>>(result);
            }
            catch(Exception ex)
            {
                return new ResponseResultError<List<IndAssessRequest>>(ex.Message);
            }
        }

        public async Task<ResponseResult<List<IndAssessRequest>>> GetAllIndWithTeacher(string id)
        {
            try
            {
                Guid tc_id;
                if(!Guid.TryParse(id, out tc_id))
                    return new ResponseResultError<List<IndAssessRequest>>("Thông tin truy xuất không hợp lệ");

                if(!await _context.Individuals.AnyAsync(prop => prop.TeacherId == new Guid(id)))
                    return new ResponseResultError<List<IndAssessRequest>>("Thông tin truy xuất không tồn tại");

                var result = new List<IndAssessRequest>();

                var inds = await _context.Individuals
                    .Where(prop => prop.TeacherId == new Guid(id))
                    .ToListAsync();

                foreach(var ind in inds)
                {
                    var user = await _userManager.FindByIdAsync(ind.StudentId.ToString());
                    result.Add(new IndAssessRequest
                    {
                        Ind_Id = ind.Id.ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        DOB = user.DOB,
                        Address = user.Address,
                        Classes = ind.Organization,
                        Email = user.Email,
                        TeacherId = ind.TeacherId.ToString()
                    });
                }
                return new ResponseResultSuccess<List<IndAssessRequest>>(result);
            }
            catch(Exception ex)
            {
                return new ResponseResultError<List<IndAssessRequest>>(ex.Message);
            }
        }

        public async Task<ResponseResult<IndAssessRequest>> GetIndById(string id)
        {
            try
            {
                Guid ind_id;
                if(!Guid.TryParse(id, out ind_id))
                    return new ResponseResultError<IndAssessRequest>("Thông tin truy xuất không hợp lệ");

                var ind = await _context.Individuals.FindAsync(ind_id);
                if(ind is null)
                    return new ResponseResultError<IndAssessRequest>("Thông tin cái nhân truy xuất không tồn tại");

                var user = await _userManager.FindByIdAsync(ind.StudentId.ToString());
                if(user is null)
                    return new ResponseResultSuccess<IndAssessRequest>(new IndAssessRequest
                    {
                        Ind_Id = ind.Id.ToString(),
                        Classes = ind.Organization
                    });
                else
                    return new ResponseResultSuccess<IndAssessRequest>(new IndAssessRequest
                    {
                        Ind_Id = ind.Id.ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        DOB = user.DOB,
                        Address = user.Address,
                        Classes = ind.Organization,
                        Email = user.Email,
                        TeacherId = ind.TeacherId.ToString(),
                    });
            }
            catch(Exception ex)
            {
                return new ResponseResultError<IndAssessRequest>(ex.Message);
            }
        }

        public async Task<ResponseResult<List<IndAssessRequest>>> Update(IndAssessRequest request)
        {
            try
            {
                var result = new ResponseResult<List<IndAssessRequest>>();
                var ind = await _context.Individuals.FindAsync(new Guid(request.Ind_Id));
                if(ind is null)
                {
                    result = await GetAllIndWithTeacher(request.TeacherId);
                    return new ResponseResult<List<IndAssessRequest>>
                    {
                        Success = false,
                        Message = "Thông tin cái nhân không tồn tại",
                        Result = result.Result
                    };
                }

                var user = await _userManager.FindByIdAsync(ind.StudentId.ToString());

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Gender = request.Gender;
                user.DOB = request.DOB;
                user.Address = request.Address;
                ind.Organization = request.Classes;
                ind.UpdateDate = DateTime.Now;
                user.Email = request.Email;

                _context.Attach(user);
                _context.Entry(user).State = EntityState.Modified;
                _context.Attach(ind);
                _context.Entry(ind).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                result = await GetAllIndWithTeacher(request.TeacherId);
                return new ResponseResultSuccess<List<IndAssessRequest>>(result.Result);
            }
            catch(Exception ex)
            {
                var result = await GetAllIndWithTeacher(request.TeacherId);
                return new ResponseResult<List<IndAssessRequest>>
                {
                    Success = false,
                    Message = ex.Message,
                    Result = result.Result
                };
            }
        }
    }
}
