using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services.Strategies.Account
{
    public class AccountStratery
    {
        private IAccountStratery _accountStratery;

        public AccountStratery()
        {
        }

        public AccountStratery(IAccountStratery accountStratery)
        {
            _accountStratery = accountStratery;
        }

        public IAccountStratery GetAccountStratery()
        {
            return _accountStratery;
        }

        public void SetAccountStratery(IAccountStratery value)
        {
            _accountStratery = value;
        }

        public async Task<ResponseResult<List<UserProfileRequest>>> GetAccountStratery(UserProfileRequest request)
        {
            return await _accountStratery.GetAccountArgsAsync(request);
        }
    }
}
