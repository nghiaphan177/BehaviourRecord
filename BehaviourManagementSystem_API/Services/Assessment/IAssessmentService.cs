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
        Task<ResponseResult<AssessmentRequest>> Detail(string ass_id);
        Task<ResponseResult<List<Assessment>>> Delete(string ass_id);
        Task<ResponseResult<Assessment>> CreateRecord(string ind_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who);
        Task<ResponseResult<Assessment>> UpdateRecord(string ass_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who);
        Task<ResponseResult<Assessment>> UpdateAnalyzeBehaviour(string ass_id, string ana_behaviour);
        Task<ResponseResult<Assessment>> UpdateAnalyzeAntecedent(string ass_id, string ana_ant_per, string ana_ant_envi, string ana_ant_act);
        Task<ResponseResult<Assessment>> UpdateAnalyzeConsequence(string ass_id, string ana_con_per, string ana_con_envi, string ana_con_act);
        Task<ResponseResult<Assessment>> UpdateFuntionAntecedent(string ass_id, string fun_ant);
        Task<ResponseResult<Assessment>> UpdateFuntionConsequece(string ass_id, string fun_con);
        Task<ResponseResult<string>> DeleteAllAssessWithInd(string id);
    }
}
