using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_MVC.APIIntegration.Intervention;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileExtreme;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileRecovery;
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
    public class InterventionController : Controller
    {
        private readonly IOptionAPIClientRecovery _IOptionAPIClientRecovery;
        private readonly IOptionAPIClientExtreme _IOptionAPIClientExtreme;
        private readonly IInterventionAPIClient _IInterventionAPIClient;
        private readonly IToastNotification toastNotification;
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        public InterventionController(IAssessmentAPIClient assessmentAPIClient,IOptionAPIClientRecovery IOptionAPIClientRecovery, IOptionAPIClientExtreme IOptionAPIClientExtreme, IInterventionAPIClient IInterventionAPIClient, IToastNotification toastNotification)
        {
            this.toastNotification = toastNotification;
            _IOptionAPIClientRecovery = IOptionAPIClientRecovery;
            _IOptionAPIClientExtreme = IOptionAPIClientExtreme;
            _IInterventionAPIClient = IInterventionAPIClient;
            _assessmentAPIClient = assessmentAPIClient;
        }
        public IActionResult Index()
        {

            return View();
        }

        
        public async Task<IActionResult> GetInterventionById(string id)
        {
            try
            {
                var response = await _IInterventionAPIClient.GetAll(id);
                if (response.Success == true)
                {
                    return View(response.Result);
                }
                ViewBag.IdAssiment = id;

            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public async Task<IActionResult> Detail(string id)
        {
            try
            {
                var response = await _IInterventionAPIClient.Get(id);
                if (response.Success == true)
                {
                    return View(response.Result);
                }

            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        //Create Start
        public IActionResult Create(string id)
        {
            ViewBag.IdIntervention = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(InterventionRequest request)
        {
            try
            {
                var response = await _IInterventionAPIClient.CreateProfile(request);
                if (response == null)
                {
                    return Json(new { success = false });
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Thêm Thành Công!");
                    return RedirectToAction("GetInterventionById", "Intervention", new { id = response.Result.AssesetmentId });
                }
                //if (response.Success)
                //{

                //    return RedirectToAction("Create", new { id = request.AssesetmentId });
                //}

            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }
        //Create End

        //Edit Start
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var response = await _IInterventionAPIClient.Get(id);
                if (response.Success == true)
                {
                    return View(response.Result);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(InterventionRequest request)
        {

            try
            {
                var response = await _IInterventionAPIClient.Update(request);
                if (response == null)
                {
                    return Json(new { success = false });
                }
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["MessageEdit"] = "Sửa thành công!";
                    return RedirectToAction("Edit", new { id = request.Id });
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
                throw;
            }
            return Json(new { success = false });

        }

        [HttpPost]
        public async Task<IActionResult> EditManage(InterventionRequest request)
        {
            try
            {
                var response = await _IInterventionAPIClient.UpdateManage(request);
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    TempData["MessageEditManage"] = "Sửa thành công!";
                    return RedirectToAction("Edit", new { id = request.Id });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditPrevent(InterventionRequest request)
        {
            var response = await _IInterventionAPIClient.UpdatePrevent(request);

            try
            {
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Đã Cập Nhật!");
                    return RedirectToAction("GetInterventionById", "Intervention", new { id = response.Result.AssesetmentId });
                }
            }
            catch (Exception)
            {

                toastNotification.AddErrorToastMessage("Vui lòng thử lại!");
            }
            return RedirectToAction("GetInterventionById", "Intervention", new { id = request.AssesetmentId });

        }

        
      //Edit End
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {

            var response = await _IInterventionAPIClient.Delete(id);
            if (response.Success == true)
            {
                toastNotification.AddSuccessToastMessage("Đã Xóa Thành Công!");
                return Json(new
                {
                    status = true
                });
            }
            return NoContent();
        }
    }
}
