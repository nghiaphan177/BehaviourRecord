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
        Task<ResponseResult<List<IndAssessRequest>>> Create(IndAssessRequest request);
        Task<ResponseResult<List<IndAssessRequest>>> GetAllIndWithAssessment(string id);
        Task<ResponseResult<List<IndAssessRequest>>> GetAllIndWithTeacher(string id);
        Task<ResponseResult<IndAssessRequest>> GetIndById(string id);
    }
}
