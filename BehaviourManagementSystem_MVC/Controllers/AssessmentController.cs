using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        private readonly IAntecedentActivityAPIClient _iAntecedentActivityAPIClient;
        private readonly IAntecedentEnvironmentalAPIClient _iAntecedentEnvironmentalAPIClient;
        private readonly IAntecedentPerceivedAPIClient _iAntecedentPerceivedAPIClient;
        private readonly IToastNotification toastNotification;
        public AssessmentController(IAssessmentAPIClient assessmentAPIClient,
            IAntecedentActivityAPIClient IAntecedentActivityAPIClient,
            IAntecedentEnvironmentalAPIClient IAntecedentEnvironmentalAPIClient,
            IAntecedentPerceivedAPIClient IAntecedentPerceivedAPIClient,
            IToastNotification toastNotification)
        {
            _assessmentAPIClient = assessmentAPIClient;
            _iAntecedentActivityAPIClient = IAntecedentActivityAPIClient;
            _iAntecedentEnvironmentalAPIClient = IAntecedentEnvironmentalAPIClient;
            _iAntecedentPerceivedAPIClient = IAntecedentPerceivedAPIClient;
            this.toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(string assid)
        {
            try
            {
                var response = await _assessmentAPIClient.Get(assid);
                if(response == null)
                {
                    return NotFound();
                }
                return View(response.Result);
            }
            catch(Exception)
            {

                throw;
            }
        }

        //Create Start

        public async Task<IActionResult> CreateAssement(string id)
        {
            var responseAnaPer = await _iAntecedentPerceivedAPIClient.GetAll();
            var responseAnaEnvi = await _iAntecedentEnvironmentalAPIClient.GetAll();
            var responseAnaActi = await _iAntecedentActivityAPIClient.GetAll();
            ViewBag.AntecedentPerceived = responseAnaPer.Result;
            ViewBag.AntecedentEnvironment = responseAnaEnvi.Result;
            ViewBag.AntecedentActivity = responseAnaActi.Result;
            var response = await _assessmentAPIClient.Get(id);
            return View(response.Result);
        }

        public async Task<IActionResult> Create(string id)
        {
            var responseAnaPer = await _iAntecedentPerceivedAPIClient.GetAll();
            var responseAnaEnvi = await _iAntecedentEnvironmentalAPIClient.GetAll();
            var responseAnaActi = await _iAntecedentActivityAPIClient.GetAll();
            ViewBag.AntecedentPerceived = responseAnaPer.Result;
            ViewBag.AntecedentEnvironment = responseAnaEnvi.Result;
            ViewBag.AntecedentActivity = responseAnaActi.Result;
            return View(new AssessmentRequest() { Id = "", IndividualId = id });
        }
        [HttpPost]
        public async Task<IActionResult> Create(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.CreateRecord(request);
                if(response == null || !response.Success)
                {
                    toastNotification.AddErrorToastMessage(response.Message);
                    return View();
                }
                TempData["MessageCreateAss"] = "Thêm Thành Công!";
                TempData["IdAsssetment"] = response.Result.Id;
                toastNotification.AddSuccessToastMessage("Thêm Thành Công!");
                return RedirectToAction("CreateAssement", new { id = response.Result.Id });
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecordBehaviour(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.CreateRecordBehaviour(request.Id, request.AnalyzeBehaviour);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Thêm Thất Bại!");
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Thêm Thành Công!");
                    TempData["MessageEditBehavior"] = "Thêm Thành Công!!";
                    return RedirectToAction("CreateAssement", new { id = response.Result.Id });
                }
                if(response.Success==false)
                {
                    toastNotification.AddErrorToastMessage("Thêm Thất Bại!");
                }
            }
            catch (Exception)
            {

                throw;
            }
            TempData["MessageEditBehavior"] = "Thêm Thất Bại!!";
            return RedirectToAction("CreateAssement", new { id = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnaEntecedent(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.UpdateAnalyzeAntecedent(request.Id, request);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Thêm Thất Bại!");
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Thêm Thành Công!");
                    TempData["UpdateAnalyzeAntecedent"] = "Thêm Thành Công!";
                    return RedirectToAction("CreateAssement", new { id = response.Result.Id });
                }
                if(response.Success==false)
                {
                    toastNotification.AddErrorToastMessage("Thêm Thất Bại!");
                }

            }
            catch (Exception)
            {

                throw;
            }
            TempData["UpdateAnalyzeAntecedent"] = "Thêm Thất Bại!";
            return RedirectToAction("CreateAssement", new { id = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnaConsequence(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.UpdateAnalyzeConsequence(request.Id, request);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Thêm Thất Bại!");
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Thêm Thành Công!");
                    TempData["UpdateAnalyzeConsequence"] = "Thêm Thành Công!";
                    return RedirectToAction("CreateAssement", new { id = response.Result.Id });
                }
                if (response.Success == false)
                {
                    toastNotification.AddErrorToastMessage("Thêm Thất Bại!");
                }
            }
            catch (Exception)
            {

                throw;
            }
            TempData["UpdateAnalyzeConsequence"] = "Thêm Thất Bại!";
            return RedirectToAction("CreateAssement", new { id = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateFuntionAntecedent(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.UpdateFuntionAntecedent(request.Id, request.FunctionAntecedent);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Thêm Thất Bại!");
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Thêm thành công!");
                    TempData["UpdateFuntionAntecedent"] = "Thêm thành công!";
                    return RedirectToAction("CreateAssement", new { id = response.Result.Id });
                }
                if(response.Success==false)
                {
                    toastNotification.AddErrorToastMessage("Không thể thêm!");
                }

            }
            catch (Exception)
            {
                throw;
            }
            TempData["UpdateFuntionAntecedent"] = "Thêm thất bại!";
            return RedirectToAction("CreateAssement", new { id = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateFuntionConsequence(AssessmentRequest request)
        {
            try
            {
                var responseIndividual = await _assessmentAPIClient.Get(request.Id);
                var response = await _assessmentAPIClient.UpdateFuntionConsequence(request.Id, request.FunctionConsequece);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Thêm Thất Bại!");
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Thêm thành công!");
                    return RedirectToAction("StudentDetail", "Student", new { id = responseIndividual.Result.IndividualId });
                }
                if (response.Success == false)
                {
                    toastNotification.AddErrorToastMessage("Không thể thêm!");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("CreateAssement", new { id = request.Id });
        }
        //Create End

        //Edit Start

        public async Task<IActionResult> Edit(string assid)
        {
            try
            {
                var response = await _assessmentAPIClient.Get(assid);
                var responseAnaPer = await _iAntecedentPerceivedAPIClient.GetAll();
                var responseAnaEnvi = await _iAntecedentEnvironmentalAPIClient.GetAll();
                var responseAnaActi = await _iAntecedentActivityAPIClient.GetAll();
                if (response == null)
                {
                    return NotFound();
                }
                if(response.Success == true)
                {
                    ViewBag.AntecedentPerceived = responseAnaPer.Result;
                    ViewBag.AntecedentEnvironment = responseAnaEnvi.Result;
                    ViewBag.AntecedentActivity = responseAnaActi.Result;
                    return View(response.Result);
                }
            }
            catch(Exception)
            {

                throw;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditRecord(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.UpdateRecord(request.Id, request);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["UpdateRecord"] = "Cập nhật thành công!";
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
                if (response.Success == false)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");
                }
            }
            catch (Exception)
            {

                throw;
            }
            TempData["UpdateRecord"] = "Cập nhật thất bại!";
            return RedirectToAction("Edit", new { assid = request.Id });

        }

        [HttpPost]
        public async Task<IActionResult> Edit(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.CreateRecord(request);
                if(response == null)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");

                }
                if (response.Success == true)
                {
                    ViewData["assid"] = response.Result.Id;
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
                if (response.Success == false)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");
                }
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
                throw;
            }
            return RedirectToAction("Edit", new { assid = request.Id });
        }


        [HttpPost]
        public async Task<IActionResult> UpdateRecordBehaviour(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.CreateRecordBehaviour(request.Id, request.AnalyzeBehaviour);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["MessageEditBehavior"] = "Cập nhật thất bại!";
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
                if (response.Success == false)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");
                }
            }
            catch (Exception)
            {

                throw;
            }
            TempData["MessageEditBehavior"] = "Cập nhật thất bại!";
            return RedirectToAction("Edit", new { assid = request.Id });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateFuntionAntecedent(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.UpdateFuntionAntecedent(request.Id, request.FunctionAntecedent);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["UpdateFuntionAntecedent"] = "Cập nhật thành công!";
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
                if (response.Success == false)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");
                }
            }
            catch (Exception)
            {

                throw;
            }
            TempData["UpdateFuntionAntecedent"] = "Cập nhật thất bại!";
            return RedirectToAction("Edit", new { assid = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFuntionConsequence(AssessmentRequest request)
        {
            try
            {
                var responseIndividual = await _assessmentAPIClient.Get(request.Id);
                var response = await _assessmentAPIClient.UpdateFuntionConsequence(request.Id, request.FunctionConsequece);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");

                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    return RedirectToAction("StudentDetail", "Student", new { id = responseIndividual.Result.IndividualId });
                }
                if (response.Success == false)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");

                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Edit", new { assid = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAnaEntecedent(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.UpdateAnalyzeAntecedent(request.Id, request);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");

                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["UpdateAnalyzeAntecedent"] = "Cập nhật thành công!";
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
                if (response.Success == false)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");
                }
            }
            catch (Exception)
            {

                throw;
            }
            TempData["UpdateAnalyzeAntecedent"] = "Cập nhật thất bại!";
            return RedirectToAction("Edit", new { assid = request.Id });

        }


        [HttpPost]
        public async Task<IActionResult> UpdateAnaConsequence(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.UpdateAnalyzeConsequence(request.Id, request);
                if (response == null)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");

                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["UpdateAnalyzeConsequence"] = "Cập nhật thành công!";
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
                if (response.Success == false)
                {
                    toastNotification.AddErrorToastMessage("Cập Nhật Thất Bại!");

                }
            }
            catch (Exception)
            {

                throw;
            }
            TempData["UpdateAnalyzeConsequence"] = "Cập nhật thất bại!";
            return RedirectToAction("Edit", new { assid = request.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string AssessId)
        {
            try
            {
                var response = await _assessmentAPIClient.Delete(AssessId);
                if(response == null)
                {
                    toastNotification.AddSuccessToastMessage("Xóa Không Thành Công!");

                }
                if (response.Success)
                {
                    toastNotification.AddErrorToastMessage("Xóa Thành Công!");
                    return Json(new
                    {
                        status = true
                    });
                }
                if (response.Success == false)
                {
                    toastNotification.AddSuccessToastMessage("Xóa Không Thành Công!");

                }
            }
            catch(Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
        }
    }
}
