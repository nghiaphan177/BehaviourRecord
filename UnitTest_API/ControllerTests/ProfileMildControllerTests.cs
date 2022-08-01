using BehaviourManagementSystem_API.Controllers;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnitTest_API.Services;
using Xunit;

namespace UnitTest_API.ControllerTests
{
    public class ProfileMildControllerTests
    {
        private readonly ProfileMildController _controller;
        private readonly IProfileMildService _service;
        public ProfileMildControllerTests()
        {
            _service = new ProfileMildServiceFakes();
            _controller = new ProfileMildController(_service);
        }

        [Fact]
        public async void Get_WithoutParam_ThenOk_Test()
        {
            // Act
            var okResult = await _controller.GetAll() as OkObjectResult;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<OkObjectResult>(okResult);
        }

        //[Fact]
        //public async void Get_WithNoData_ThenBadRequest_Test()
        //{
        //    // Act
        //    var badResponse = await _controller.GetAll() as BadRequestObjectResult;
        //    // Assert
        //    Assert.Equal(400, badResponse.StatusCode);
        //    Assert.IsType<BadRequestObjectResult>(badResponse);
        //}

        [Fact]
        public async Task GetById_WithNotExistingId_ThenBadRequest_TestAsync()
        {
            var notExistingId = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            // Act
            var badResponse = await _controller.GetById(notExistingId) as BadRequestObjectResult;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void GetById_ExistingId_ThenOk_Test()
        {
            // Arrange
            var ExistingId = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            // Act
            var okResult = await _controller.GetById(ExistingId) as OkObjectResult;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async void Add_Content_ThenOk_Test()
        {
            // Arrange
            string content = "UnitTest4";

            // Act
            var createdResponse = await _controller.Create(content) as OkObjectResult;
            var item = createdResponse.Value;
            // Assert
            Assert.Equal(200, createdResponse.StatusCode);
            Assert.Equal(JsonConvert.SerializeObject(item), JsonConvert.SerializeObject(createdResponse.Value));
        }

        [Fact]
        public async void Add_ExistingContent_ThenBadRequest_Test()
        {
            // Arrange
            string content = "UnitTest3";

            // Act
            var badResponse = await _controller.Create(content) as BadRequestObjectResult;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.IsType<BadRequestObjectResult>(badResponse);

        }

        [Fact]
        public async void Update_WithNotExistingId_ThenBadRequest_Test()
        {
            // Arrange
            var ExistingId = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            string content = "UnitTest5";
            OptionsRequest request = new OptionsRequest() { Id = ExistingId, Content = content };
            // Act
            var badResponse = await _controller.Update(request) as BadRequestObjectResult;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Update_WithExistingContent_ThenBadRequest_Test()
        {
            // Arrange
            var ExistingId = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string content = "UnitTest2";
            OptionsRequest request = new OptionsRequest() { Id = ExistingId, Content = content };
            // Act
            var badResponse = await _controller.Update(request) as BadRequestObjectResult;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Update_Content_ThenOk_Test()
        {
            // Arrange
            var ExistingId = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string content = "UnitTest5";
            OptionsRequest request = new OptionsRequest() { Id = ExistingId, Content = content };
            // Act
            var okResult = await _controller.Update(request) as OkObjectResult;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async void Remove_WithNotExistingId_ThenBadRequest_Test()
        {
            // Arrange
            var notExistingId = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";

            // Act
            var badResponse = await _controller.Delete(notExistingId) as BadRequestObjectResult;

            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.IsType<BadRequestObjectResult>(badResponse);

        }

        [Fact]
        public async void Remove_ExistingId_ThenOk_Test()
        {
            // Arrange
            var ExistingId = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            // Act
            var okResult = await _controller.Delete(ExistingId) as OkObjectResult;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<OkObjectResult>(okResult);
        }

    }
}
