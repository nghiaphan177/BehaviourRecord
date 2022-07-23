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
    public class InterventionService : IInterventionService
    {
        private readonly ApplicationDbContext _context;

        public InterventionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<Intervention>> CreateProfile(string ass_id, DateTime p_date, string p_mild, string p_moder, string p_extre, string p_reco)
        {
            var a = new Intervention();
            if (!await _context.Assessments.AnyAsync(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Intervention>("Id assessment không tồn tại");
            if (p_date.ToString() == null || p_reco == null)
            {
                return new ResponseResultError<Intervention>("Chưa có dữ liệu");
            }
            else
            {
                await _context.Interventions.AddAsync(a = new Intervention()
                {
                    Id = Guid.NewGuid(),
                    ProfileDate = p_date,
                    ProfileMildDesciption = p_mild,
                    ProfileModerateDesciption = p_moder,
                    ProfileExtremeDesciption = p_extre,
                    ProfileRecoveryDesciption = p_reco,
                    AssesetmentId = new Guid(ass_id),
                    CreateDate = DateTime.Now,

                    ProfileIsCompeleted = true
                });
                await _context.SaveChangesAsync();
            }

            return new ResponseResultSuccess<Intervention>(a);
        }

        public async Task<ResponseResult<List<Intervention>>> Delete(string int_id)
        {
            if (!await _context.Interventions.AnyAsync(prop => prop.Id.ToString() == int_id))
                return new ResponseResultError<List<Intervention>>("Id intervention không tồn tại");
            var obj = await _context.Interventions.FindAsync(new Guid(int_id));
            _context.Interventions.Remove(obj);
            await _context.SaveChangesAsync();
            return new ResponseResultSuccess<List<Intervention>>(await _context.Interventions.ToListAsync());
        }

        public async Task<ResponseResult<InterventionRequest>> Detail(string int_id)
        {
            if (!await _context.Interventions.AnyAsync(prop => prop.Id.ToString() == int_id))
                return new ResponseResultError<InterventionRequest>("Id intervention không tồn tại");
            var obj = await _context.Interventions.FindAsync(new Guid(int_id));
            return new ResponseResultSuccess<InterventionRequest>(new InterventionRequest()
            {
                Id = obj.Id.ToString(),
                ProfileDate = obj.ProfileDate.GetValueOrDefault(),
                ProfileMildDesciption = obj.ProfileMildDesciption,
                ProfileModerateDesciption = obj.ProfileModerateDesciption,
                ProfileExtremeDesciption = obj.ProfileExtremeDesciption,
                ProfileRecoveryDesciption = obj.ProfileRecoveryDesciption,
                ManageMild = obj.ManageMild,
                ManageModerate = obj.ManageModerate,
                ManageExtreme = obj.ManageExtreme,
                ManageRecovery = obj.ManageRecovery,
                PreventStatus = obj.PreventStatus,
                PreventActivity = obj.PreventActivity,
                PreventInteraction = obj.PreventInteraction,
                PreventInvironmental = obj.PreventInvironmental

            });
        }

        public async Task<ResponseResult<List<InterventionRequest>>> GetAll(string ass_id)
        {
            var find = _context.Interventions.Where(p => p.AssesetmentId.ToString() == ass_id);
            var intervention = _context.Interventions.Take(find.Count());
            if (await intervention.AnyAsync() == false)
            {
                return new ResponseResultError<List<InterventionRequest>>("Hiện tại không có dữ liệu");
            }
            var result = new List<InterventionRequest>();
            foreach (var item in intervention)
            {
                result.Add(new InterventionRequest()
                {
                    Id = item.Id.ToString(),
                    ProfileDate = item.ProfileDate.GetValueOrDefault(),
                    ProfileIsCompeleted = item.ProfileIsCompeleted,
                    ManageIsCompleted = item.ManageIsCompleted,
                    PreventIsCompleted = item.PreventIsCompleted,
                    Summary = item.Summary,
                    AssesetmentId = item.AssesetmentId.ToString(),
                });
            }
            return new ResponseResultSuccess<List<InterventionRequest>>(result);
        }

        public async Task<ResponseResult<Intervention>> UpdateManage(string int_id, string m_mild, string m_moder, string m_extre, string m_reco)
        {
            if (!await _context.Interventions.AnyAsync(prop => prop.Id.ToString() == int_id))
                return new ResponseResultError<Intervention>("Id intrvention không tồn tại");
            var obj = await _context.Interventions.FindAsync(new Guid(int_id));
            if (m_reco == null)
            {
                return new ResponseResultError<Intervention>("Chưa có dữ liệu");
            }
            else
            {
                obj.ManageMild = m_mild;
                obj.ManageModerate = m_moder;
                obj.ManageExtreme = m_extre;
                obj.ManageRecovery = m_reco;
                obj.ManageIsCompleted = true;

                obj.UpdateDate = DateTime.Now;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return new ResponseResultSuccess<Intervention>(obj);
        }

        public async Task<ResponseResult<Intervention>> UpdatePrevent(string int_id, string pre_status, string pre_act, string pre_envi, string pre_inter)
        {
            if (!await _context.Interventions.AnyAsync(prop => prop.Id.ToString() == int_id))
                return new ResponseResultError<Intervention>("Id intrvention không tồn tại");
            var obj = await _context.Interventions.FindAsync(new Guid(int_id));
            if (pre_status == null || pre_act == null || pre_envi == null || pre_inter == null)
            {
                return new ResponseResultError<Intervention>("Chưa có dữ liệu");
            }
            else
            {
                obj.PreventStatus = pre_status;
                obj.PreventActivity = pre_act;
                obj.PreventInvironmental = pre_envi;
                obj.PreventInteraction = pre_inter;

                obj.PreventIsCompleted = true;
                obj.UpdateDate = DateTime.Now;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return new ResponseResultSuccess<Intervention>(obj);
        }

        public async Task<ResponseResult<Intervention>> UpdateProfile(string int_id, DateTime p_date, string p_mild, string p_moder, string p_extre, string p_reco)
        {
            if (!await _context.Interventions.AnyAsync(prop => prop.Id.ToString() == int_id))
                return new ResponseResultError<Intervention>("Id intrvention không tồn tại");
            var obj = await _context.Interventions.FindAsync(new Guid(int_id));
            if (p_date.ToString() == null || p_reco == null)
            {
                return new ResponseResultError<Intervention>("Chưa có dữ liệu");
            }
            else
            {
                obj.ProfileDate = p_date;
                obj.ProfileMildDesciption = p_mild;
                obj.ProfileModerateDesciption = p_moder;
                obj.ProfileExtremeDesciption = p_extre;
                obj.ProfileRecoveryDesciption = p_reco;

                obj.UpdateDate = DateTime.Now;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return new ResponseResultSuccess<Intervention>(obj);
        }
    }
}
