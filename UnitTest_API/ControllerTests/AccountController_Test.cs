using BehaviourManagementSystem_API.Controllers;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IO;
using Xunit;

namespace UnitTest_API.ControllerTests
{
    public class AccountController_Test
    {
        private readonly IConfigurationRoot _configuration;

        public AccountController_Test()
        {
            _configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(@"appsettings.json")
               .AddEnvironmentVariables()
               .Build();
        }

        [Fact]
        public void GetGoogleClientId_ReturnOK()
        {
            var pass = _configuration["Google:ClientId"];

            var mock = new Mock<IAccountService>();

            var controller = new AccountController(mock.Object, _configuration);

            var result = (ObjectResult)controller.GetGoogleClientId();
            var resultObj = (ResponseResult<string>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
            Assert.IsType<string>(resultObj.Result);
            Assert.Equal(pass, resultObj.Result);
        }
    }
}
