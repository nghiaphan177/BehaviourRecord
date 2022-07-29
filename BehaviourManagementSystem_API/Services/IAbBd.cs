using BehaviourManagementSystem_ViewModels.Responses.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public interface IAbBd
    {
        Task<ResponseResult<List<Tuple<int, int>>>> GetCountAllAccountRegisterOfYear(string year);
      
        Task<ResponseResult<List<Tuple<int, int>>>> GetCountAllAccountRegisterOfMonth(int m, int y);
        
        Task<ResponseResult<List<Tuple<string, int>>>> GetCountAllStudentOfAllClasses(Guid guid);
        
        Task<ResponseResult<Tuple<int, int>>> GetAllAccountNotVerifyMail();
    }
}