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
        //Task<ResponseResult<Intervention>> CreateProfile(string ass_id, DateTime p_date, string pro_mild, string pro_moder, string pro_extre, string pro_reco);
        //Task<ResponseResult<Intervention>> UpdateProfile(string int_id, DateTime p_date, string pro_mild, string pro_moder, string pro_extre, string pro_reco);
        //Task<ResponseResult<Intervention>> DeleteProfile(string int_id);
    }
}
