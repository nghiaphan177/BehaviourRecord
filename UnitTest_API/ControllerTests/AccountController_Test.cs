using BehaviourManagementSystem_API.Controllers;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
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
        public void GetGoogleClientId_Return_Google_ClientId()
        {
            var pass = _configuration["Google:ClientId"];

            var action = _accountController.GetGoogleClientId();

            var result = action as OkObjectResult;
            var resultObj = result.Value as ResponseResultSuccess<string>;

            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
            Assert.Equal(pass, resultObj.Result);
        }
    }
}
