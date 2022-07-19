using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public class ProfileRecoveryService : IProfileRecoveryService
    {
        private readonly ApplicationDbContext _context;

        public ProfileRecoveryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<List<ProfileRecovery>>> Create(string content)
        {
            if (await _context.ProfileRecoveries.AnyAsync(prop => prop.Content == content))
                return new ResponseResultError<List<ProfileRecovery>>("Dữ liệu đã tồn tại");
            await _context.ProfileRecoveries.AddAsync(new ProfileRecovery()
            {
                Id = Guid.NewGuid(),
                Content = content,
                CreateDate = DateTime.Now,
            });
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<List<ProfileRecovery>>(await _context.ProfileRecoveries.ToListAsync());
        }

        public async Task<ResponseResult<List<ProfileRecovery>>> Delete(string id)
        {
            if (!await _context.ProfileRecoveries.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<ProfileRecovery>>("Id không tồn tại");
            var obj = await _context.ProfileRecoveries.FindAsync(new Guid(id));
            _context.ProfileRecoveries.Remove(obj);
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<List<ProfileRecovery>>(await _context.ProfileRecoveries.ToListAsync());
        }

        public async Task<ResponseResult<List<OptionsRequest>>> GetAll()
        {
            if (!await _context.ProfileRecoveries.AnyAsync())
                return new ResponseResultError<List<OptionsRequest>>("Hiện tại không có dữ liệu");
            var recovery = await _context.ProfileRecoveries.ToListAsync();
            var result = new List<OptionsRequest>();
            foreach (var item in recovery)
            {
                result.Add(new OptionsRequest()
                {
                    Id = item.Id.ToString(),
                    Content = item.Content,
                    CreateDate = item.CreateDate.Value,
                    UpdateDate = item.UpdateDate.GetValueOrDefault()
                });
            }
            return new ResponseResultSuccess<List<OptionsRequest>>(result);
        }

        public async Task<ResponseResult<OptionsRequest>> GetById(string id)
        {
            if (!await _context.ProfileRecoveries.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<OptionsRequest>("Id không tồn tại");
            var obj = await _context.ProfileRecoveries.FindAsync(new Guid(id));
            return new ResponseResultSuccess<OptionsRequest>(new OptionsRequest()
            {
                Id = obj.Id.ToString(),
                Content = obj.Content,
                CreateDate = obj.CreateDate.Value,
                UpdateDate = obj.UpdateDate.GetValueOrDefault()
            });
        }

        public async Task<ResponseResult<List<ProfileRecovery>>> Update(string id, string content)
        {
            if (!await _context.ProfileRecoveries.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<ProfileRecovery>>("Id không tồn tại");
            if (await _context.ProfileRecoveries.AnyAsync(prop => prop.Content == content))
                return new ResponseResultError<List<ProfileRecovery>>("Dữ liệu đã tồn tại");
            var obj = await _context.ProfileRecoveries.FindAsync(new Guid(id));
            obj.Content = content;
            obj.UpdateDate = DateTime.Now;
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<List<ProfileRecovery>>(await _context.ProfileRecoveries.ToListAsync());
        }
    }
}
