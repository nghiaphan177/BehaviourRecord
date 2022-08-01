using BehaviourManagementSystem_API.Controllers;
using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities.JwtGenarator;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest_API.ControllerTests
{
    public class AccountController_Test
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly AccountController _accountController;

        public AccountController_Test()
        {
            _configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(@"appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            _accountService = A.Fake<IAccountService>();

            _accountController = new AccountController(_accountService, _configuration);
        }

        [Fact]
        public void GetGoogleClientId_ReturnOK()
        {
            var pass = _configuration["Google:ClientId"];
            var controller = new AccountController(_accountService, _configuration);

            var result = (ObjectResult)_accountController.GetGoogleClientId();
            var resultObj = (ResponseResult<string>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
            Assert.IsType<string>(resultObj.Result);
            Assert.Equal(pass, resultObj.Result);
        }
    }
}
