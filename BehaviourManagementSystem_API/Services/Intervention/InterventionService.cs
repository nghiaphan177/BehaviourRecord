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

        public async Task<ResponseResult<InterventionRequest>> Detail(string int_id)
        {
            if (!await _context.Interventions.AnyAsync(prop => prop.Id.ToString() == int_id))
                return new ResponseResultError<InterventionRequest>("Id không tồn tại");
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

    }
}
