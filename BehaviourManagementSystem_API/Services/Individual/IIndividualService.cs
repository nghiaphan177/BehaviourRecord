using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IIndividualService
    {
        Task<ResponseResult<List<IndividualRequest>>> GetAll();
        Task<ResponseResult<IndividualRequest>> Detail(string id);

    }
}
