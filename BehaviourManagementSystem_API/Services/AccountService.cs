using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;


        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task<ResponseResult<string>> LoginAdmin(LoginAdminRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
