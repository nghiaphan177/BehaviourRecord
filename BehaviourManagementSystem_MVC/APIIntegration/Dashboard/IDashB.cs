using BehaviourManagementSystem_ViewModels.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Dashboard
{
    public interface IDashB
    {
        Task<ResponseResult<List<Tuple<int, int>>>> GetCountAllAccountRegisterOfYear(string year);

        Task<ResponseResult<List<Tuple<int, int>>>> GetCountAllAccountRegisterOfMonth(string m, string y);

        Task<ResponseResult<List<Tuple<string, int>>>> GetCountAllStudentOfAllClasses(string teacherid);

        Task<ResponseResult<Tuple<int, int>>> GetAllAccountNotVerifyMail();

        Task<ResponseResult<Tuple<int, int, int>>> GetAllStudentAndTeacherAndAllAccount();

        Task<ResponseResult<List<Tuple<int, int, int>>>> GetAllAssessAndInterByMonthWithTeacher(string teacherid);
    }
}
