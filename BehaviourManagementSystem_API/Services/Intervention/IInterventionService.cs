using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IInterventionService
    {
        Task<ResponseResult<List<InterventionRequest>>> GetAll(string ass_id);
        Task<ResponseResult<InterventionRequest>> Detail(string int_id);
        Task<ResponseResult<List<Intervention>>> Delete(string int_id);
        Task<ResponseResult<Intervention>> CreateProfile(string ass_id, DateTime p_date, string p_mild, string p_moder, string p_extre, string p_reco);
        Task<ResponseResult<Intervention>> UpdateProfile(string int_id, DateTime p_date, string p_mild, string p_moder, string p_extre, string p_reco);
        Task<ResponseResult<Intervention>> UpdateManage(string int_id, string m_mild, string m_moder, string m_extre, string m_reco);
        Task<ResponseResult<Intervention>> UpdatePrevent(string int_id, string pre_status, string pre_act, string pre_envi, string pre_inter);
        

    }
}
