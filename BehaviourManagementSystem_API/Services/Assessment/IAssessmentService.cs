using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IAssessmentService
    {
        Task<ResponseResult<List<AssessmentRequest>>> GetAll(string ind_id);
        Task<ResponseResult<AssessmentRequest>> Detail(string id);
        Task<ResponseResult<List<Assesetment>>> CreateRecord(string ind_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who);
    }
}
