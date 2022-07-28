﻿using BehaviourManagementSystem_MVC.APIIntegration;
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
        public IActionResult Create(string id)
        {
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
                TempData["MessageCreateAss"] = "Sửa thành công!";
                toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                return RedirectToAction("StudentDetail", "Student", new { id = request.IndividualId });
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
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
                    return Json(new { success = false });
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["UpdateRecord"] = "Sửa thành công!";
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.CreateRecord(request);
                if(response == null)
                {
                    return Json(new { success = false });
                }
                if(response.Success == true)
                {
                    ViewData["assid"] = response.Result.Id;
                    return Json(new { success = true, assid = response.Result.Id });
                }

            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
                throw;
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecordBehaviour(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.CreateRecordBehaviour(request.Id, request.AnalyzeBehaviour);
                if(response == null)
                {
                    return Json(new { success = false });
                }
                if(response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["MessageEditBehavior"] = "Sửa thành công!";
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
            }
            catch(Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFuntionAntecedent(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.UpdateFuntionAntecedent(request.Id, request.FunctionAntecedent);
                if (response == null)
                {
                    return Json(new { success = false });
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["UpdateFuntionAntecedent"] = "Sửa thành công!";
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = false });
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
                    return Json(new { success = false });
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    return RedirectToAction("StudentDetail", "Student", new { id = responseIndividual.Result.IndividualId });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnaEntecedent(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.UpdateAnalyzeAntecedent(request.Id, request);
                if (response == null)
                {
                    return Json(new { success = false });
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["UpdateAnalyzeAntecedent"] = "Sửa thành công!";
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public async Task<IActionResult> CreateAnaConsequence(AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.UpdateAnalyzeConsequence(request.Id, request);
                if (response == null)
                {
                    return Json(new { success = false });
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["UpdateAnalyzeConsequence"] = "Sửa thành công!";
                    return RedirectToAction("Edit", new { assid = response.Result.Id });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string AssessId)
        {
            try
            {
                var response = await _assessmentAPIClient.Delete(AssessId);
                if(response == null)
                {
                    return Json(new { success = false });
                }
                if(response.Success)
                {
                    return Json(new { success = true });
                }
            }
            catch(Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }
    }
}
