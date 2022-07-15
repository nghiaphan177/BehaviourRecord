using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public class ProfileMildService : IProfileMildService
    {
        private readonly ApplicationDbContext _context;
        public ProfileMildService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<List<ProfileMild>>> Create(string content)
        {
            if (await _context.ProfileMilds.AnyAsync(prop => prop.Content == content))
                return new ResponseResultError<List<ProfileMild>>("Dữ liệu đã tồn tại");
            await _context.ProfileMilds.AddAsync(new ProfileMild()
            {
                Id = Guid.NewGuid(),
                Content = content,
                CreateDate = DateTime.Now,
            });
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<List<ProfileMild>>(await _context.ProfileMilds.ToListAsync());
        }

        public async Task<ResponseResult<List<ProfileMild>>> Delete(string id)
        {
            if (!await _context.ProfileMilds.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<ProfileMild>>("Id không tồn tại");
            var obj = await _context.ProfileMilds.FindAsync(new Guid(id));
            _context.ProfileMilds.Remove(obj);
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<List<ProfileMild>>(await _context.ProfileMilds.ToListAsync());
        }

        public async Task<ResponseResult<List<ProfileMildResponse>>> GetAll()
        {
            if (!await _context.ProfileMilds.AnyAsync())
                return new ResponseResultError<List<ProfileMildResponse>>("Hiện tại không có dữ liệu");
            var mild = await _context.ProfileMilds.ToListAsync();
            var result = new List<ProfileMildResponse>();
            foreach (var item in mild)
            {
                result.Add(new ProfileMildResponse(){
                    Id = item.Id.ToString(),
                    Content = item.Content,
                    CreateDate = item.CreateDate.Value,
                    UpdateDate = item.UpdateDate.GetValueOrDefault()
                });
            }
            return new ResponseResultSuccess<List<ProfileMildResponse>>(result);

        }

        public async Task<ResponseResult<List<ProfileMild>>> Update(string id, string content)
        {
            if (!await _context.ProfileMilds.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<ProfileMild>>("Id không tồn tại");
            if (await _context.ProfileMilds.AnyAsync(prop => prop.Content == content))
                return new ResponseResultError<List<ProfileMild>>("Dữ liệu đã tồn tại");
            var obj = await _context.ProfileMilds.FindAsync(new Guid(id));
            obj.Content = content;
            obj.UpdateDate = DateTime.Now;
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<List<ProfileMild>>(await _context.ProfileMilds.ToListAsync());
        }
    }
}
