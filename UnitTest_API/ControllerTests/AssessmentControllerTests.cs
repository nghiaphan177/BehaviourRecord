using BehaviourManagementSystem_API.Controllers;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest_API.Services;
using Xunit;

namespace UnitTest_API.ControllerTests
{
    public class AssessmentControllerTests
    {
        private readonly AssessmentController _controller;
        private readonly IAssessmentService _service;
        public AssessmentControllerTests()
        {
            _service = new AssessmentServiceFakes();
            _controller = new AssessmentController(_service);
        }

        [Fact]
        public async void GetAll_Assessment_ThenOk_ExistingId_Test()
        {
            // Arrange
            var ind_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c100";
            // Act
            var okResult = await _controller.GetAll(ind_id) as OkObjectResult;
            var item = (ResponseResultSuccess<List<AssessmentRequest>>)okResult.Value;

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<AssessmentRequest>>(item.Result);
        }

        [Fact]
        public async Task GetAll_Assessment_WithNotExistingId_ThenBadRequest_Test()
        {
            // Arrange
            var ind_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c101";
            // Act
            var badResponse = await _controller.GetAll(ind_id_NotExisting) as BadRequestObjectResult;
            var item = (ResponseResultError<List<AssessmentRequest>>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Hiện tại không có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Detail_Assessment_ExistingId_ThenOk_Test()
        {
            // Arrange
            var ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            // Act
            var okResult = await _controller.Detail(ass_id) as OkObjectResult;
            var item = (ResponseResultSuccess<AssessmentRequest>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<AssessmentRequest>(item.Result);
        }

        [Fact]
        public async Task Detail_Assessment_WithNotExistingId_ThenBadRequest_Test()
        {
            var ass_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            // Act
            var badResponse = await _controller.Detail(ass_id_NotExisting) as BadRequestObjectResult;
            var item = (ResponseResultError<AssessmentRequest>)badResponse.Value;

            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id assessment không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Add_AssessmentRecord_ThenOk_Test()
        {
            // Arrange
            string ind_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c100";
            DateTime r_date = DateTime.Parse("3/3/2022");
            string r_start = "7:00";
            string r_end = "10:00";
            string r_where = "Nhà";
            string r_who = "Hiếu";
            // Act
            var createdResponse = await _controller.CreateRecord(ind_id, r_date, r_start, r_end, r_where, r_who) as OkObjectResult;
            var item = (ResponseResultSuccess<Assessment>)createdResponse.Value;
            // Assert
            Assert.Equal(200, createdResponse.StatusCode);
            Assert.IsType<Assessment>(item.Result);
        }

        [Fact]
        public async void Add_AssessmentRecord_WithNotExistingIndId_ThenBadRequest_Test()
        {
            // Arrange
            string ind_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c101";
            DateTime r_date = DateTime.Parse("3/3/2022");
            string r_start = "7:00";
            string r_end = "10:00";
            string r_where = "Nhà";
            string r_who = "Hiếu";
            // Act
            var badResponse = await _controller.CreateRecord(ind_id_NotExisting, r_date, r_start, r_end, r_where, r_who) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id individual không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Add_AssessmentRecord_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string ind_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c100";
            DateTime r_date = DateTime.Parse("3/3/2022");
            string r_start = null;
            string r_end = null;
            string r_where = null;
            string r_who = null;
            // Act
            var badResponse = await _controller.CreateRecord(ind_id, r_date, r_start, r_end, r_where, r_who) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Update_AssessmentRecord_ThenOk_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            DateTime r_date = DateTime.Parse("3/3/2022");
            string r_start = "7:00";
            string r_end = "10:00";
            string r_where = "Nhà";
            string r_who = "Hiếu";
            // Act
            var okResult = await _controller.UpdateRecord(ass_id, r_date, r_start, r_end, r_where, r_who) as OkObjectResult;
            var item = (ResponseResultSuccess<Assessment>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<Assessment>(item.Result);
        }

        [Fact]
        public async void Update_AssessmentRecord_WithNotExistingAssId_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            DateTime r_date = DateTime.Parse("3/3/2022");
            string r_start = "7:00";
            string r_end = "10:00";
            string r_where = "Nhà";
            string r_who = "Hiếu";
            // Act
            var badResponse = await _controller.UpdateRecord(ass_id, r_date, r_start, r_end, r_where, r_who) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id assessment không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Update_AssessmentRecord_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            DateTime r_date = DateTime.Parse("3/3/2022");
            string r_start = null;
            string r_end = null;
            string r_where = null;
            string r_who = null;
            // Act
            var badResponse = await _controller.UpdateRecord(ass_id, r_date, r_start, r_end, r_where, r_who) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Remove_Assessment_ThenOk_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            // Act
            var okResult = await _controller.Delete(ass_id) as OkObjectResult;
            var item = (ResponseResultSuccess<List<Assessment>>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<Assessment>>(item.Result);
        }

        [Fact]
        public async void Remove_Assessment_WithNotExistingId_ThenBadRequest_Test()
        {
            // Arrange
            var ass_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            // Act
            var badResponse = await _controller.Delete(ass_id_NotExisting) as BadRequestObjectResult;
            var item = (ResponseResultError<List<Assessment>>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id không tồn tại",item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Remove_AllAssessment_ThenOk_Test()
        {
            // Arrange
            var ind_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c100";
            // Act
            var okResult = await _controller.DeleteAllAssessWithInd(ind_id) as OkObjectResult;
            var item = (ResponseResultSuccess<string>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Xóa thành công", item.Result);
            Assert.IsType<OkObjectResult>(okResult);
        }

    }
}
