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
    /// <summary>
    /// Assessment_UnitTest
    /// writter: HoangDDN
    /// Description: GetAll,AddRecord,UpdateRecord,UpdateAnalyzeAntecedent,UpdateAnalyzeConsequence,UpdateAnalyzeBehaviour,
    /// Description: UpdateFuntionAntecedent,UpdateFuntionConsequence,Remove,Detail
    /// </summary>
    public class AssessmentControllerTests
    {
        private readonly AssessmentController _controller;
        private readonly IAssessmentService _service;
        public AssessmentControllerTests()
        {
            _service = new AssessmentServiceFakes();
            _controller = new AssessmentController(_service);
        }
        // Test GetAll Assessment với Id tồn tại và trả về Ok
        [Fact]
        public async void GetAll_Assessment_ExistingId_ThenOk_Test()
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
        // Test GetAll Assessment với Id không tồn tại và trả về Badrequest
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
        // Test Detail Assessment với Id tồn tại và trả về Ok
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
        // Test Detail Assessment với Id không tồn tại và trả về Badrequest
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
        // Test Add Assessment Record và trả về Ok
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
        // Test Add Assessment Record với Id không tồn tại và trả về Badrequest
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
        // Test Add Assessment Record với dữ liệu Null và trả về Badrequest
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
        // Test Update Assessment Record và trả về Ok
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
        // Test Update Assessment Record với Id không tồn tại và trả về Badrequest
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
        // Test Update Assessment Record với dữ liệu Null và trả về Badrequest
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
        // Test Update Assessment Analyze Antecedent và trả về Ok
        [Fact]
        public async void Update_AssessmentAnalyzeAntecedent_ThenOk_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string ana_ant_per = "Tức giận";
            string ana_ant_envi = "Nhiều người xung quanh";
            string ana_ant_act = "Chạy trốn";
            // Act
            var okResult = await _controller.UpdateAnalyzeAntecedent(ass_id, ana_ant_per, ana_ant_envi, ana_ant_act) as OkObjectResult;
            var item = (ResponseResultSuccess<Assessment>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<Assessment>(item.Result);
        }
        // Test Update Assessment Analyze Antecedent với Id không tồn tại và trả về Badrequest
        [Fact]
        public async void Update_AssessmentAnalyzeAntecedent_WithNotExistingAssId_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            string ana_ant_per = "Tức giận";
            string ana_ant_envi = "Nhiều người xung quanh";
            string ana_ant_act = "Chạy trốn";
            // Act
            var badResponse = await _controller.UpdateAnalyzeAntecedent(ass_id_NotExisting, ana_ant_per, ana_ant_envi, ana_ant_act) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id assessment không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Assessment Analyze Antecedent với dữ liệu Null và trả về Badrequest
        [Fact]
        public async void Update_AssessmentAnalyzeAntecedent_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string ana_ant_per = null;
            string ana_ant_envi = null;
            string ana_ant_act = null;
            // Act
            var badResponse = await _controller.UpdateAnalyzeAntecedent(ass_id, ana_ant_per, ana_ant_envi, ana_ant_act) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Assessment Analyze Consequence và trả về Ok
        [Fact]
        public async void Update_AssessmentAnalyzeConsequence_ThenOk_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string ana_con_per = "Tức giận";
            string ana_con_envi = "Nhiều người xung quanh";
            string ana_con_act = "Chạy trốn";
            // Act
            var okResult = await _controller.UpdateAnalyzeConsequence(ass_id, ana_con_per, ana_con_envi, ana_con_act) as OkObjectResult;
            var item = (ResponseResultSuccess<Assessment>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<Assessment>(item.Result);
        }
        // Test Update Assessment Analyze Consequence với Id không tồn tại và trả về Badrequest
        [Fact]
        public async void Update_AssessmentAnalyzeConsequence_WithNotExistingAssId_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            string ana_con_per = "Tức giận";
            string ana_con_envi = "Nhiều người xung quanh";
            string ana_con_act = "Chạy trốn";
            // Act
            var badResponse = await _controller.UpdateAnalyzeConsequence(ass_id_NotExisting, ana_con_per, ana_con_envi, ana_con_act) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id assessment không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Assessment Analyze Consequence với dữ liệu Null và trả về Badrequest
        [Fact]
        public async void Update_AssessmentAnalyzeConsequence_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string ana_con_per = null;
            string ana_con_envi = null;
            string ana_con_act = null;
            // Act
            var badResponse = await _controller.UpdateAnalyzeConsequence(ass_id, ana_con_per, ana_con_envi, ana_con_act) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Assessment Analyze Behaviour và trả về Ok
        [Fact]
        public async void Update_AssessmentAnalyzeBehaviour_ThenOk_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string ana_behaviour = "Đánh nhau";
            // Act
            var okResult = await _controller.UpdateAnalyzeBehaviour(ass_id, ana_behaviour) as OkObjectResult;
            var item = (ResponseResultSuccess<Assessment>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<Assessment>(item.Result);
        }
        // Test Update Assessment Analyze Behaviour với Id không tồn tại và trả về Badrequest
        [Fact]
        public async void Update_AssessmentAnalyzeBehaviour_WithNotExistingAssId_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            string ana_behaviour = "Đánh nhau";
            // Act
            var badResponse = await _controller.UpdateAnalyzeBehaviour(ass_id_NotExisting, ana_behaviour) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id assessment không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Assessment Analyze Behaviour với dữ liệu Null và trả về Badrequest
        [Fact]
        public async void Update_AssessmentAnalyzeBehaviour_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string ana_behaviour = null;
            // Act
            var badResponse = await _controller.UpdateAnalyzeBehaviour(ass_id, ana_behaviour) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Assessment Funtion Antecendent và trả về Ok
        [Fact]
        public async void Update_AssessmentFuntionAntecedent_ThenOk_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string fun_ant = "Đánh bạn bị thương nhập viện";
            // Act
            var okResult = await _controller.UpdateFuntionAntecedent(ass_id, fun_ant) as OkObjectResult;
            var item = (ResponseResultSuccess<Assessment>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<Assessment>(item.Result);
        }
        // Test Update Assessment Funtion Antecendent với Id không tồn tại và trả về Badrequest
        [Fact]
        public async void Update_AssessmentFuntionAntecedent_WithNotExistingAssId_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            string fun_ant = "Đánh bạn bị thương nhập viện";
            // Act
            var badResponse = await _controller.UpdateFuntionAntecedent(ass_id_NotExisting, fun_ant) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id assessment không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Assessment Funtion Antecendent với dữ liệu Null và trả về Badrequest
        [Fact]
        public async void Update_AssessmentFuntionAntecedent_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string fun_ant = null;
            // Act
            var badResponse = await _controller.UpdateFuntionAntecedent(ass_id, fun_ant) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Assessment Funtion Consequence và trả về Ok
        [Fact]
        public async void Update_AssessmentFuntionConsequence_ThenOk_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string fun_con = "Sau đó chạy trốn khỏi hiện trường";
            // Act
            var okResult = await _controller.UpdateFuntionConsequece(ass_id, fun_con) as OkObjectResult;
            var item = (ResponseResultSuccess<Assessment>)okResult.Value;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<Assessment>(item.Result);
        }
        // Test Update Assessment Funtion Consequence với Id không tồn tại và trả về Badrequest
        [Fact]
        public async void Update_AssessmentFuntionConsequence_WithNotExistingAssId_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id_NotExisting = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201";
            string fun_con = "Sau đó chạy trốn khỏi hiện trường";
            // Act
            var badResponse = await _controller.UpdateFuntionConsequece(ass_id_NotExisting, fun_con) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Id assessment không tồn tại", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Update Assessment Funtion Consequence với dữ liệu Null và trả về Badrequest
        [Fact]
        public async void Update_AssessmentFuntionConsequence_WithNullData_ThenBadRequest_Test()
        {
            // Arrange
            string ass_id = "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200";
            string fun_con = null;
            // Act
            var badResponse = await _controller.UpdateFuntionConsequece(ass_id, fun_con) as BadRequestObjectResult;
            var item = (ResponseResultError<Assessment>)badResponse.Value;
            // Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("Chưa có dữ liệu", item.Message);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        // Test Remove Assessment và trả về Ok
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
        // Test Remove Assessment với Id không tồn tại và trả về Badrequest
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
        // Test Remove All Assessment và trả về Ok
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
