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
    public class AnalyzeAntecedentActivityService : IAnalyzeAntecedentActivityService
    {
        private readonly ApplicationDbContext _context;

        public AnalyzeAntecedentActivityService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult<List<AnalyzeAntecedentActivity>>> Create(string content)
        {
            if (await _context.AnalyzeAntecedentActivities.AnyAsync(prop => prop.Content == content))
                return new ResponseResultError<List<AnalyzeAntecedentActivity>>("Dữ liệu đã tồn tại");

            await _context.AnalyzeAntecedentActivities.AddAsync(new AnalyzeAntecedentActivity()
            {
                Id = Guid.NewGuid(),
                Content = content,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return new ResponseResultSuccess<List<AnalyzeAntecedentActivity>>(
                await _context.AnalyzeAntecedentActivities.ToListAsync());
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentActivity>>> Delete(string id)
        {
            if (!await _context.AnalyzeAntecedentActivities.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<AnalyzeAntecedentActivity>>("Id không tồn tại");

            var obj = await _context.AnalyzeAntecedentActivities.FindAsync(new Guid(id));

            _context.AnalyzeAntecedentActivities.Remove(obj);

            await _context.SaveChangesAsync();

            return new ResponseResultSuccess<List<AnalyzeAntecedentActivity>>(
              await _context.AnalyzeAntecedentActivities.ToListAsync());
        }

        public async Task<ResponseResult<List<OptionsRequest>>> GetAll()
        {
            if (!await _context.AnalyzeAntecedentActivities.AnyAsync())
                return new ResponseResultError<List<OptionsRequest>>("Hiện tại không có dữ liệu");
            var activity = await _context.AnalyzeAntecedentActivities.ToListAsync();
            var result = new List<OptionsRequest>();
            int stt = 0;
            foreach (var item in activity)
            {
                result.Add(new OptionsRequest()
                {
                    STT = stt += 1,
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
            if (!await _context.AnalyzeAntecedentActivities.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<OptionsRequest>("Id không tồn tại");
            var obj = await _context.AnalyzeAntecedentActivities.FindAsync(new Guid(id));
            return new ResponseResultSuccess<OptionsRequest>(new OptionsRequest()
            {
                Id = obj.Id.ToString(),
                Content = obj.Content,
                CreateDate = obj.CreateDate.Value,
                UpdateDate = obj.UpdateDate.GetValueOrDefault()
            });
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentActivity>>> Update(string id, string content)
        {
            if (!await _context.AnalyzeAntecedentActivities.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<AnalyzeAntecedentActivity>>("Id không tồn tại");
            if (await _context.AnalyzeAntecedentActivities.AnyAsync(prop => prop.Content == content))
                return new ResponseResultError<List<AnalyzeAntecedentActivity>>("Dữ liệu đã tồn tại");
            var obj = await _context.AnalyzeAntecedentActivities.FindAsync(new Guid(id));
            obj.Content = content;
            obj.UpdateDate = DateTime.Now;

            _context.Entry(obj).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new ResponseResultSuccess<List<AnalyzeAntecedentActivity>>(
              await _context.AnalyzeAntecedentActivities.ToListAsync());
        }
    }
}
