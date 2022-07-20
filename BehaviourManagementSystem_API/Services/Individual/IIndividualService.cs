using BehaviourManagementSystem_API.Models;
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
        Task<ResponseResult<List<Individual>>> Create(Individual individual);
        //Task<ResponseResult<List<Individual>>> Update();
        //Task<ResponseResult<List<Individual>>> Delete();

    }
}
