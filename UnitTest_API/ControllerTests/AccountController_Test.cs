using BehaviourManagementSystem_API.Controllers;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        [Fact]
        public async Task Login_ReturnOK()
        {
            var req = new LoginRequest()
            {
                UserNameOrEmail = "admin",
                Password = "Password2@",
                Remember = true
            };

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.Login(req))
                  .ReturnsAsync(new ResponseResultSuccess<string>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.Login(req);
            var resultObj = (ResponseResult<string>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task Register_ReturnOk()
        {
            var req = new RegisterRequest()
            {
                UserName = "user1",
                Email = "email1@gamil.com",
                Password = "Password2@",
                RePassword = "Passowrd2@"
            };

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.Register(req))
                .ReturnsAsync(new ResponseResultSuccess<ConfirmEmailRequest>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.Register(req);
            var resultObj = (ResponseResult<ConfirmEmailRequest>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task VerifyEmail_ReturnOk()
        {
            var req = new ConfirmEmailRequest()
            {
                Id = Guid.NewGuid().ToString(),
                Code = "code"
            };

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.VerifyEmail(req))
                .ReturnsAsync(new ResponseResultSuccess<UserProfileRequest>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.VerifyEmail(req);
            var resultObj = (ResponseResult<UserProfileRequest>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task ResendConfirmEmail_ReturnOk()
        {
            var req = "email1@gmail.com";

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.ResendConfirmEmail(req))
                .ReturnsAsync(new ResponseResultSuccess<ConfirmEmailRequest>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.ResendConfirmEmail(req);
            var resultObj = (ResponseResult<ConfirmEmailRequest>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task ForgotPassword_ReturnOk()
        {
            var req = "userNameOrEmail";

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.ForgotPassword(req))
                .ReturnsAsync(new ResponseResultSuccess<ResetPasswordRequest>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.ForgotPassword(req);
            var resultObj = (ResponseResult<ResetPasswordRequest>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task ResetPassword_ReturnOk()
        {
            var req = new ResetPasswordRequest()
            {
                Id = Guid.NewGuid().ToString(),
                Code = "code"
            };

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.ResetPassword(req))
                .ReturnsAsync(new ResponseResultSuccess<bool>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.ResetPassword(req);
            var resultObj = (ResponseResult<bool>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task CheckEmailConfirmed_ReturnOk()
        {
            var req = "email1@gmail.com";

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.CheckEmailConfirmed(req))
                .ReturnsAsync(new ResponseResultSuccess<bool>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.CheckEmailConfirmed(req);
            var resultObj = (ResponseResult<bool>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task ChangPassword_ReturnOk()
        {
            var req = new ResetPasswordRequest()
            {
                Id = Guid.NewGuid().ToString(),
                PasswordOld = "Passold2@",
                PasswordNew = "Passnew2@",
                PasswordConfirm = "Passnew2@"
            };

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.ChangePassword(req))
                .ReturnsAsync(new ResponseResultSuccess<UserProfileRequest>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.ChangePassword(req);
            var resultObj = (ResponseResult<UserProfileRequest>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task GetUser_ReturnOk()
        {
            var req = new UserProfileRequest();

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.GetAccount(req))
                .ReturnsAsync(new ResponseResultSuccess<List<UserProfileRequest>>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.GetUser(req);
            var resultObj = (ResponseResult<List<UserProfileRequest>>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task GetAllUser_With_Role_ReturnOk()
        {
            var req = "role";

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.GetAll(req))
                .ReturnsAsync(new ResponseResultSuccess<List<UserProfileRequest>>(
                    new List<UserProfileRequest>
                    {
                        new UserProfileRequest()
                    }));

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.GetAllUser(req);
            var resultObj = (ResponseResult<List<UserProfileRequest>>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task GetAllUser_ReturnOk()
        {
            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.GetAll(null))
                .ReturnsAsync(new ResponseResultSuccess<List<UserProfileRequest>>(
                    new List<UserProfileRequest>
                    {
                        new UserProfileRequest()
                    }));

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.GetAllUser();
            var resultObj = (ResponseResult<List<UserProfileRequest>>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task GetUser_By_Id_ReturnOk()
        {
            var req = Guid.NewGuid().ToString();

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.GetUser(req))
                .ReturnsAsync(new ResponseResultSuccess<UserProfileRequest>(
                    new UserProfileRequest()));

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.GetUser(req);
            var resultObj = (ResponseResult<UserProfileRequest>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task CreateUserProfile_ReturnOk()
        {
            var req = new UserProfileRequest()
            {
                Id = Guid.NewGuid().ToString(),
                RoleId = Guid.NewGuid().ToString(),
            };

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.CreateUserProfile(req))
                .ReturnsAsync(new ResponseResultSuccess<List<UserProfileRequest>>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.CreateUserProfile(req);
            var resultObj = (ResponseResult<List<UserProfileRequest>>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

        [Fact]
        public async Task UpdateUserProfile_ReturnOk()
        {
            var req = new UserProfileRequest();

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.UpdateUserProfile(req))
                .ReturnsAsync(new ResponseResultSuccess<UserProfileRequest>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.UpdateUserProfile(req);
            var resultObj = (ResponseResult<UserProfileRequest>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }
        
        [Fact]
        public async Task DeleteUserProfile_ReturnOk()
        {
            var req = Guid.NewGuid().ToString();

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.DeleteUserProfile(req))
                .ReturnsAsync(new ResponseResultSuccess<List<UserProfileRequest>>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.DeleteUserProfile(req);
            var resultObj = (ResponseResult<List<UserProfileRequest>>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }
        
        [Fact]
        public async Task GoogleSigin_ReturnOk()
        {
            var req = "token";

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.GoogleSigin(req))
                .ReturnsAsync(new ResponseResultSuccess<string>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.GoogleSigin(req);
            var resultObj = (ResponseResult<string>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }
        
        [Fact]
        public async Task CheckPasswordNull_ReturnOk()
        {
            var req = Guid.NewGuid().ToString();

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.CheckPasswordNull(req))
                .ReturnsAsync(new ResponseResultSuccess<bool>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.CheckPasswordNull(req);
            var resultObj = (ResponseResult<bool>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }
        
        [Fact]
        public async Task NewPassOfAccountGoogle_ReturnOk()
        {
            var req = new ResetPasswordRequest();

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.NewPassOfAccountGoogle(req))
                .ReturnsAsync(new ResponseResultSuccess<string>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.NewPassOfAccountGoogle(req);
            var resultObj = (ResponseResult<string>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        } 
        
        [Fact]
        public async Task GetImg_ReturnOk()
        {
            var req = Guid.NewGuid().ToString();

            var accountService = new Mock<IAccountService>();
            accountService.Setup(method => method.GetImg(req))
                .ReturnsAsync(new ResponseResultSuccess<string>());

            var controller = new AccountController(accountService.Object, _configuration);
            var result = (ObjectResult)await controller.GetImg(req);
            var resultObj = (ResponseResult<string>)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(resultObj);
            Assert.True(resultObj.Success);
        }

    }
}
