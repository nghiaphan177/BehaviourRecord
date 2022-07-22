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
    public class AssessmentService : IAssessmentService
    {
        private readonly ApplicationDbContext _context;

        public AssessmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<Assessment>> CreateRecord(string ind_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who)
        {
            var a = new Assessment();
            if (!await _context.Individuals.AnyAsync(prop => prop.Id.ToString() == ind_id))
                return new ResponseResultError<Assessment>("Id individual không tồn tại");
            if (r_date.ToString() == null || r_start == null || r_end == null)
            {
                return new ResponseResultError<Assessment>("Chưa có dữ liệu");
            }
            else
            {
                await _context.Assesetments.AddAsync(a = new Assessment()
                {
                    Id = Guid.NewGuid(),
                    RecordDate = r_date,
                    RecordStart = TimeSpan.Parse(r_start),
                    RecordEnd = TimeSpan.Parse(r_end),
                    RecordWhere = r_where,
                    RecordWho = r_who,
                    IndividualId = new Guid(ind_id),
                    CreateDate = DateTime.Now,

                    RecordIsCompeleted = true

                });
                await _context.SaveChangesAsync();
            }
                          
            return new ResponseResultSuccess<Assessment>(a);
        }

        public async Task<ResponseResult<Assessment>> DeleteAnalyzeAntecedent(string ass_id)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            obj.AnalyzeAntecedentPerceivedDescription = null;
            obj.AnalyzeAntecedentEnvironmentalDescription = null;
            obj.AnalyzeAntecedentActivityDescription = null;
            obj.UpdateDate = DateTime.Now;
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<Assessment>> DeleteAnalyzeBehaviour(string ass_id)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            obj.AnalyzeBehaviour = null;
            obj.UpdateDate = DateTime.Now;
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<Assessment>> DeleteAnalyzeConsequence(string ass_id)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            obj.AnalyzeConsequencesPerceive = null;
            obj.AnalyzeConsequenceEnvironmental = null;
            obj.AnalyzeConsequencesActivity = null;
            obj.AnalyzeIsCompeleted = false;
            obj.UpdateDate = DateTime.Now;
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<Assessment>> DeleteFuntionAntecedent(string ass_id)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            obj.FunctionAntecedent = null;
            obj.UpdateDate = DateTime.Now;
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<Assessment>> DeleteFuntionConsequece(string ass_id)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            obj.FunctionConsequece = null;
            obj.FunctionIsCompeleted = false;
            obj.UpdateDate = DateTime.Now;
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<Assessment>> DeleteRecord(string ass_id)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            obj.RecordDate = null;
            obj.RecordStart = null;
            obj.RecordEnd = null;
            obj.RecordWhere = null;
            obj.RecordWho = null;
            obj.RecordIsCompeleted = false;
            obj.UpdateDate = DateTime.Now;
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<AssessmentRequest>> Detail(string ass_id)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<AssessmentRequest>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            return new ResponseResultSuccess<AssessmentRequest>(new AssessmentRequest()
            {
                Id = obj.Id.ToString(),
                RecordDate = obj.RecordDate.GetValueOrDefault(),
                RecordStart = obj.RecordStart,
                RecordEnd = obj.RecordEnd,
                RecordDuring = obj.RecordDuring,
                RecordWhere = obj.RecordWhere,
                RecordWho = obj.RecordWho,
                AnalyzeAntecedentActivityDescription = obj.AnalyzeAntecedentActivityDescription,
                AnalyzeAntecedentEnvironmentalDescription = obj.AnalyzeAntecedentEnvironmentalDescription,
                AnalyzeAntecedentPerceivedDescription = obj.AnalyzeAntecedentPerceivedDescription,
                AnalyzeBehaviour = obj.AnalyzeBehaviour,
                FunctionAntecedent = obj.FunctionAntecedent,
                FunctionConsequece = obj.FunctionConsequece,
            });
        }

        public async Task<ResponseResult<List<AssessmentRequest>>> GetAll(string ind_id)
        {
            var find = _context.Assesetments.Where(p => p.IndividualId.ToString() == ind_id);
            var assessment = _context.Assesetments.Take(find.Count());
            if (await assessment.AnyAsync() == false)
            {
                return new ResponseResultError<List<AssessmentRequest>>("Hiện tại không có dữ liệu");
            }
            var result = new List<AssessmentRequest>();
            foreach (var item in assessment)
            {
                result.Add(new AssessmentRequest()
                {
                    Id = item.Id.ToString(),
                    RecordDate = item.RecordDate.GetValueOrDefault(),
                    RecordStart = item.RecordStart,
                    RecordIsCompeleted = item.RecordIsCompeleted,
                    AnalyzeIsCompeleted = item.AnalyzeIsCompeleted,
                    FunctionIsCompeleted = item.FunctionIsCompeleted,
                    AnalyzeBehaviour = item.AnalyzeBehaviour,
                    IndividualId = item.IndividualId.ToString(),

                });
            }
            return new ResponseResultSuccess<List<AssessmentRequest>>(result);
        }

        public async Task<ResponseResult<Assessment>> UpdateAnalyzeAntecedent(string ass_id, string ana_ant_per, string ana_ant_envi, string ana_ant_act)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            if (ana_ant_per == null || ana_ant_envi == null || ana_ant_act == null)
            {
                return new ResponseResultError<Assessment>("Chưa có dữ liệu");
            }
            else
            {
                obj.AnalyzeAntecedentPerceivedDescription = ana_ant_per;
                obj.AnalyzeAntecedentEnvironmentalDescription = ana_ant_envi;
                obj.AnalyzeAntecedentActivityDescription = ana_ant_act;
                obj.UpdateDate = DateTime.Now;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<Assessment>> UpdateAnalyzeBehaviour(string ass_id, string ana_behaviour)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            if (ana_behaviour == null)
            {
                return new ResponseResultError<Assessment>("Chưa có dữ liệu");
            }
            else
            {
                obj.AnalyzeBehaviour = ana_behaviour;
                obj.UpdateDate = DateTime.Now;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<Assessment>> UpdateAnalyzeConsequence(string ass_id, string ana_con_per, string ana_con_envi, string ana_con_act)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            if (ana_con_per == null || ana_con_envi == null || ana_con_act == null)
            {
                return new ResponseResultError<Assessment>("Chưa có dữ liệu");
            }
            else
            {
                obj.AnalyzeConsequencesPerceive = ana_con_per;
                obj.AnalyzeConsequenceEnvironmental = ana_con_envi;
                obj.AnalyzeConsequencesActivity = ana_con_act;
                obj.AnalyzeIsCompeleted = true;
                obj.UpdateDate = DateTime.Now;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<Assessment>> UpdateFuntionAntecedent(string ass_id, string fun_ant)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            if (fun_ant == null)
            {
                return new ResponseResultError<Assessment>("Chưa có dữ liệu");
            }
            else
            {
                obj.FunctionAntecedent = fun_ant;
                obj.UpdateDate = DateTime.Now;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<Assessment>> UpdateFuntionConsequece(string ass_id, string fun_con)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            if (fun_con == null)
            {
                return new ResponseResultError<Assessment>("Chưa có dữ liệu");
            }
            else
            {
                obj.FunctionConsequece = fun_con;
                obj.FunctionIsCompeleted = true;
                obj.UpdateDate = DateTime.Now;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return new ResponseResultSuccess<Assessment>(obj);
        }

        public async Task<ResponseResult<Assessment>> UpdateRecord(string ass_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(ass_id));
            if (r_date.ToString() == null || r_start == null || r_end == null)
            {
                return new ResponseResultError<Assessment>("Chưa có dữ liệu");
            }
            else
            {
                obj.RecordDate = r_date;
                obj.RecordStart = TimeSpan.Parse(r_start);
                obj.RecordEnd = TimeSpan.Parse(r_end);
                obj.RecordWhere = r_where;
                obj.RecordWho = r_who;

                obj.UpdateDate = DateTime.Now;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }          
            return new ResponseResultSuccess<Assessment>(obj);
        }
    }
}
