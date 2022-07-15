using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponsesModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public class AnalyzeAntecedentEnvironmentalService : IAnalyzeAntecedentEnvironmentalService
    {
        private readonly ApplicationDbContext _context;

        public AnalyzeAntecedentEnvironmentalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentEnvironmental>>> Create(string content)
        {
            if (await _context.AnalyzeAntecedentEnvironmentals.CountAsync(prop => prop.Content == content) > 0)
                return new ResponseResultError<List<AnalyzeAntecedentEnvironmental>>("Dữ liệu đã tồn tại");

            await _context.AnalyzeAntecedentEnvironmentals.AddAsync(new AnalyzeAntecedentEnvironmental()
            {
                Id = Guid.NewGuid(),
                Content = content,
                CreateDate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return new ResponseResultSuccess<List<AnalyzeAntecedentEnvironmental>>(
                await _context.AnalyzeAntecedentEnvironmentals.ToListAsync());
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentEnvironmental>>> Delete(string id)
        {
            if (!await _context.AnalyzeAntecedentEnvironmentals.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<AnalyzeAntecedentEnvironmental>>("Id không tồn tại");

            var obj = await _context.AnalyzeAntecedentEnvironmentals.FindAsync(new Guid(id));

            _context.AnalyzeAntecedentEnvironmentals.Remove(obj);

            await _context.SaveChangesAsync();

            return new ResponseResultSuccess<List<AnalyzeAntecedentEnvironmental>>(
              await _context.AnalyzeAntecedentEnvironmentals.ToListAsync());
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentEnvironmentalResponse>>> GetAll()
        {
            if (!await _context.AnalyzeAntecedentEnvironmentals.AnyAsync())
                return new ResponseResultError<List<AnalyzeAntecedentEnvironmentalResponse>>("Hiện tại không có dữ liệu");
            var enviroment = await _context.AnalyzeAntecedentEnvironmentals.ToListAsync();
            var result = new List<AnalyzeAntecedentEnvironmentalResponse>();
            foreach (var item in enviroment)
            {
                result.Add(new AnalyzeAntecedentEnvironmentalResponse()
                {
                    Id = item.Id.ToString(),
                    Content = item.Content,
                    CreateDate = item.CreateDate.Value,
                    UpdateDate = item.UpdateDate.GetValueOrDefault()
                });
            }
            return new ResponseResultSuccess<List<AnalyzeAntecedentEnvironmentalResponse>>(result);
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentEnvironmental>>> Update(string id, string content)
        {
            if (!await _context.AnalyzeAntecedentEnvironmentals.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<AnalyzeAntecedentEnvironmental>>("Id không tồn tại");
            if (await _context.AnalyzeAntecedentEnvironmentals.CountAsync(prop => prop.Content == content) > 0)
                return new ResponseResultError<List<AnalyzeAntecedentEnvironmental>>("Dữ liệu đã tồn tại");

            var obj = await _context.AnalyzeAntecedentEnvironmentals.FindAsync(new Guid(id));
            obj.Content = content;
            obj.UpdateDate = DateTime.Now;

            _context.Entry(obj).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new ResponseResultSuccess<List<AnalyzeAntecedentEnvironmental>>(
              await _context.AnalyzeAntecedentEnvironmentals.ToListAsync());
        }
    }
}
