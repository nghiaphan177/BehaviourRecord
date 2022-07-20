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
        Task<ResponseResult<List<IndividualRequest>>> GetAll();
        Task<ResponseResult<IndividualRequest>> Detail(string id);
    }
}
