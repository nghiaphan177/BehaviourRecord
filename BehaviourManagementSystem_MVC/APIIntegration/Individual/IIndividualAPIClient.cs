using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Individual
{
    public interface IIndividualAPIClient
    {
        Task<ResponseResult<List<IndAssessRequest>>> GetAll(string id);
        Task<ResponseResult<List<IndividualRequest>>> GetAllList();
        Task<ResponseResult<IndividualRequest>> Detail(string id);
        Task<ResponseResult<List<IndAssessRequest>>> Create(IndAssessRequest request);
    }
}
