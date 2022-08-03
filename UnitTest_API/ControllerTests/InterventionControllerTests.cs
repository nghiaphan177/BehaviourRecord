using BehaviourManagementSystem_API.Controllers;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest_API.Services;
using Xunit;

namespace UnitTest_API.ControllerTests
{
    /// <summary>
    /// Intervention_UnitTest
    /// writter: HoangDDN
    /// Description: GetAll,AddProfile,UpdateProfile,UpdateManage,UpdatePrevent,Remove,Detail
    /// </summary>
    public class InterventionControllerTests
    {
        private readonly InterventionController _controller;
        private readonly IInterventionService _service;

        public InterventionControllerTests()
        {
            _service = new InterventionServiceFakes();
            _controller = new InterventionController(_service);
        }
        // Test GetAll Intervention với Id tồn tại và trả về Ok
        [Fact]
        public async void GetAll_Intervention_ExistingId_ThenOk_Test()
        {
            // Arrange
            var ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            // Act
            var okResult = await _controller.GetAll(ass_id) as OkObjectResult;
            var item = (ResponseResultSuccess<List<InterventionRequest>>)okResult.Value;

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<InterventionRequest>>(item.Result);
        }
        // Test GetAll Intervention với Id không tồn tại và trả về Badrequest
        [Fact]
        public async Task GetAll_Intervention_WithNotExistingId_ThenBadRequest_Test()
        {
            // Arrange
            var ass_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            // Act
            var badResponse = await _controller.GetAll(ass_id_NotExisting) as BadRequestObjectResult;
            var item = (ResponseResultError<List<InterventionRequest>>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Hiện tại không có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Detail Intervention với Id tồn tại và trả về Ok
        [Fact]
        public async void Detail_Intervention_ExistingId_ThenOk_Test()
        {
            // Arrange
            var int_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c300";
            // Act
            var okResult = await _controller.Detail(int_id) as OkObjectResult;
            var item = (ResponseResultSuccess<InterventionRequest>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<InterventionRequest>(item.Result);
        }
        // Test Detail Intervention với Id không tồn tại và trả về Badrequest
        [Fact]
        public async Task Detail_Intervention_WithNotExistingId_ThenBadRequest_Test()
        {
            var int_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c301";
            // Act
            var badResponse = await _controller.Detail(int_id_NotExisting) as BadRequestObjectResult;
            var item = (ResponseResultError<InterventionRequest>)badResponse.Value;

            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id intervention không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Add Intervention Profile và trả về Ok
        [Fact]
        public async void Add_InterventionProfile_ThenOk_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            DateTime p_date = DateTime.Parse("3/3/2022");
            string p_mild = "Chép phạt";
            string p_moder ="";
            string p_extre = "";
            string p_reco = "Xin lỗi nạn nhân";
            // Act
            var createdResponse = await _controller.CreateProfile(ass_id, p_date, p_mild, p_moder, p_extre, p_reco) as OkObjectResult;
            var item = (ResponseResultSuccess<Intervention>)createdResponse.Value;
            // Assert
            Assert.Equal(200, createdResponse.StatusCode);
            Assert.IsType<Intervention>(item.Result);
        }
        // Test Add Intervention Profile với Id không tồn tại và trả về Badrequest
        [Fact]
        public async void Add_InterventionProfile_WithNotExistingIndId_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            DateTime p_date = DateTime.Parse("3/3/2022");
            string p_mild = "Chép phạt";
            string p_moder = "";
            string p_extre = "";
            string p_reco = "Xin lỗi nạn nhân";
            // Act
            var badResponse = await _controller.CreateProfile(ass_id_NotExisting, p_date, p_mild, p_moder, p_extre, p_reco) as BadRequestObjectResult;
            var item = (ResponseResultError<Intervention>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id assessment không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Add Intervention Profile với dữ liệu Null và trả về Badrequest
        [Fact]
        public async void Add_InterventionProfile_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            DateTime p_date = DateTime.Parse("3/3/2022");
            string p_mild = null;
            string p_moder = null;
            string p_extre = null;
            string p_reco = null;
            // Act
            var badResponse = await _controller.CreateProfile(ass_id, p_date, p_mild, p_moder, p_extre, p_reco) as BadRequestObjectResult;
            var item = (ResponseResultError<Intervention>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Intervention Profile và trả về Ok
        [Fact]
        public async void Update_InterventionProfile_ThenOk_Test()
        {
            // Arrange
            string int_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c300";
            DateTime p_date = DateTime.Parse("3/3/2022");
            string p_mild = "Nhắc nhở";
            string p_moder = "";
            string p_extre = "";
            string p_reco = "Xin lỗi nạn nhân";
            // Act
            var okResult = await _controller.UpdateProfile(int_id, p_date, p_mild, p_moder, p_extre, p_reco) as OkObjectResult;
            var item = (ResponseResultSuccess<Intervention>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<Intervention>(item.Result);
        }
        // Test Update Intervention Profile với Id không tồn tại và trả về Badrequest
        [Fact]
        public async void Update_InterventionProfile_WithNotExistingAssId_ThenBadRequest_Test()
        {
            // Arrange
            string int_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c301";
            DateTime p_date = DateTime.Parse("3/3/2022");
            string p_mild = "Nhắc nhở";
            string p_moder = "";
            string p_extre = "";
            string p_reco = "Xin lỗi nạn nhân";
            // Act
            var badResponse = await _controller.UpdateProfile(int_id_NotExisting, p_date, p_mild, p_moder, p_extre, p_reco) as BadRequestObjectResult;
            var item = (ResponseResultError<Intervention>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id intervention không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Intervention Profile với dữ liệu Null và trả về Badrequest
        [Fact]
        public async void Update_InterventionProfile_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string int_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c300";
            DateTime p_date = DateTime.Parse("3/3/2022");
            string p_mild = null;
            string p_moder = null;
            string p_extre = null;
            string p_reco = null;
            // Act
            var badResponse = await _controller.UpdateProfile(int_id, p_date, p_mild, p_moder, p_extre, p_reco) as BadRequestObjectResult;
            var item = (ResponseResultError<Intervention>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Intervention Manage và trả về Ok
        [Fact]
        public async void Update_InterventionManage_ThenOk_Test()
        {
            // Arrange
            string int_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c300";
            string m_mild = "Nhắc nhở học sinh";
            string m_moder = "";
            string m_extre = "";
            string m_reco = "Xin lỗi nạn nhân";
            // Act
            var okResult = await _controller.UpdateManage(int_id, m_mild, m_moder, m_extre, m_reco) as OkObjectResult;
            var item = (ResponseResultSuccess<Intervention>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<Intervention>(item.Result);
        }
        // Test Update Intervention Manage với Id không tồn tại và trả về Badrequest
        [Fact]
        public async void Update_InterventionManage_WithNotExistingAssId_ThenBadRequest_Test()
        {
            // Arrange
            string int_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c301";
            string m_mild = "Nhắc nhở học sinh";
            string m_moder = "";
            string m_extre = "";
            string m_reco = "Xin lỗi nạn nhân";
            // Act
            var badResponse = await _controller.UpdateManage(int_id_NotExisting, m_mild, m_moder, m_extre, m_reco) as BadRequestObjectResult;
            var item = (ResponseResultError<Intervention>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id intervention không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Intervention Manage với dữ liệu Null và trả về Badrequest
        [Fact]
        public async void Update_InterventionManage_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string int_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c300";
            string m_mild = null;
            string m_moder = null;
            string m_extre = null;
            string m_reco = null;
            // Act
            var badResponse = await _controller.UpdateManage(int_id, m_mild, m_moder, m_extre, m_reco) as BadRequestObjectResult;
            var item = (ResponseResultError<Intervention>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Intervention Prevent và trả về Ok
        [Fact]
        public async void Update_InterventionPrevent_ThenOk_Test()
        {
            // Arrange
            string int_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c300";
            string pre_status = "Tức giận";
            string pre_act = "Hạn chế tiếp xúc với nạn nhân";
            string pre_envi = "Không cho đến nơi đông người";
            string pre_inter = "Công tác tư tưởng cho học sinh";
            // Act
            var okResult = await _controller.UpdatePrevent(int_id, pre_status, pre_act, pre_envi, pre_inter) as OkObjectResult;
            var item = (ResponseResultSuccess<Intervention>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<Intervention>(item.Result);
        }
        // Test Update Intervention Prevent với Id không tồn tại và trả về Badrequest
        [Fact]
        public async void Update_InterventionPrevent_WithNotExistingAssId_ThenBadRequest_Test()
        {
            // Arrange
            string int_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c301";
            string pre_status = "Tức giận";
            string pre_act = "Hạn chế tiếp xúc với nạn nhân";
            string pre_envi = "Không cho đến nơi đông người";
            string pre_inter = "Công tác tư tưởng cho học sinh";
            // Act
            var badResponse = await _controller.UpdatePrevent(int_id_NotExisting, pre_status, pre_act, pre_envi, pre_inter) as BadRequestObjectResult;
            var item = (ResponseResultError<Intervention>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id intervention không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Intervention Prevent với dữ liệu Null và trả về Badrequest
        [Fact]
        public async void Update_InterventionPrevent_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string int_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c300";
            string pre_status = null;
            string pre_act = null;
            string pre_envi = null;
            string pre_inter = null;
            // Act
            var badResponse = await _controller.UpdatePrevent(int_id, pre_status, pre_act, pre_envi, pre_inter) as BadRequestObjectResult;
            var item = (ResponseResultError<Intervention>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Remove Intervention và trả về Ok
        [Fact]
        public async void Remove_Intervention_ThenOk_Test()
        {
            // Arrange
            string int_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c300";

            // Act
            var okResult = await _controller.Delete(int_id) as OkObjectResult;
            var item = (ResponseResultSuccess<List<Intervention>>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<Intervention>>(item.Result);
        }
        // Test Remove Intervention với Id không tồn tại và trả về Ok
        [Fact]
        public async void Remove_Intervention_WithNotExistingId_ThenBadRequest_Test()
        {
            // Arrange
            var int_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c301";
            // Act
            var badResponse = await _controller.Delete(int_id_NotExisting) as BadRequestObjectResult;
            var item = (ResponseResultError<List<Intervention>>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id intervention không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}
