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
    public class AnalyzeAntecedentPerceiveService : IAnalyzeAntecedentPerceiveService
    {
        private readonly ApplicationDbContext _context;
        public AnalyzeAntecedentPerceiveService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentPerceive>>> Create(string content)
        {
            if (await _context.AnalyzeAntecedentPerceives.AnyAsync(prop => prop.Content == content))
                return new ResponseResultError<List<AnalyzeAntecedentPerceive>>("Dữ liệu đã tồn tại");

            await _context.AnalyzeAntecedentPerceives.AddAsync(new AnalyzeAntecedentPerceive()
            {
                Id = Guid.NewGuid(),
                Content = content,
                CreateDate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return new ResponseResultSuccess<List<AnalyzeAntecedentPerceive>>(
                await _context.AnalyzeAntecedentPerceives.ToListAsync());
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentPerceive>>> Delete(string id)
        {
            if (!await _context.AnalyzeAntecedentPerceives.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<AnalyzeAntecedentPerceive>>("Id không tồn tại");

            var obj = await _context.AnalyzeAntecedentPerceives.FindAsync(new Guid(id));

            _context.AnalyzeAntecedentPerceives.Remove(obj);

            await _context.SaveChangesAsync();

            return new ResponseResultSuccess<List<AnalyzeAntecedentPerceive>>(
              await _context.AnalyzeAntecedentPerceives.ToListAsync());
        }

        public async Task<ResponseResult<List<OptionsRequest>>> GetAll()
        {
            if (!await _context.AnalyzeAntecedentPerceives.AnyAsync())
                return new ResponseResultError<List<OptionsRequest>>("Hiện tại không có dữ liệu");
            var pervive = await _context.AnalyzeAntecedentPerceives.ToListAsync();
            var result = new List<OptionsRequest>();
            foreach (var item in pervive)
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
            if (!await _context.AnalyzeAntecedentPerceives.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<OptionsRequest>("Id không tồn tại");
            var obj = await _context.AnalyzeAntecedentPerceives.FindAsync(new Guid(id));
            return new ResponseResultSuccess<OptionsRequest>(new OptionsRequest()
            {
                Id = obj.Id.ToString(),
                Content = obj.Content,
                CreateDate = obj.CreateDate.Value,
                UpdateDate = obj.UpdateDate.GetValueOrDefault()
            });
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentPerceive>>> Update(string id, string content)
        {
            if (!await _context.AnalyzeAntecedentPerceives.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<AnalyzeAntecedentPerceive>>("Id không tồn tại");
            if (await _context.AnalyzeAntecedentPerceives.AnyAsync(prop => prop.Content == content))
                return new ResponseResultError<List<AnalyzeAntecedentPerceive>>("Dữ liệu đã tồn tại");

            var obj = await _context.AnalyzeAntecedentPerceives.FindAsync(new Guid(id));
            obj.Content = content;
            obj.UpdateDate = DateTime.Now;

            _context.Entry(obj).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new ResponseResultSuccess<List<AnalyzeAntecedentPerceive>>(
              await _context.AnalyzeAntecedentPerceives.ToListAsync());
        }
    }
}
