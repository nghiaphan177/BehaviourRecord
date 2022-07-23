using BehaviourManagementSystem_MVC.APIIntegration.Intervention;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileExtreme;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileRecovery;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    public class InterventionController : Controller
    {
        private readonly IOptionAPIClientRecovery _IOptionAPIClientRecovery;
        private readonly IOptionAPIClientExtreme _IOptionAPIClientExtreme;
        private readonly IInterventionAPIClient _IInterventionAPIClient;
        public InterventionController(IOptionAPIClientRecovery IOptionAPIClientRecovery, IOptionAPIClientExtreme IOptionAPIClientExtreme, IInterventionAPIClient IInterventionAPIClient)
        {
            _IOptionAPIClientRecovery = IOptionAPIClientRecovery;
            _IOptionAPIClientExtreme = IOptionAPIClientExtreme;
            _IInterventionAPIClient = IInterventionAPIClient;
        }
        public IActionResult Index()
        {

            return View();
        }


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
                if (response.Success == true)
                {
                    TempData["MessageCreate"] = "Sửa thành công!";
                    return RedirectToAction("Index", response.Result);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
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
                if (response.Success)
                {
                    TempData["MessageCreate"] = "Thêm thành công!";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {

            var response = await _IInterventionAPIClient.Delete(id);
            if (response.Success == true)
            {
                return Json(new
                {
                    status = true
                });
            }
            return NoContent();
        }
    }
}
