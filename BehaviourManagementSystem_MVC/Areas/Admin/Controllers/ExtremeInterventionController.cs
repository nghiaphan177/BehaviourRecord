using BehaviourManagementSystem_MVC.APIIntegration.ProfileExtreme;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileRecovery;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace BehaviourManagementSystem_MVC.Area.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "Admin", Policy = "AdminOnly")]
    public class ExtremeInterventionController : Controller
    {
        private readonly IOptionAPIClientExtreme _IOptionAPIClientExtreme;
        private readonly IToastNotification toastNotification;
        public ExtremeInterventionController(IOptionAPIClientExtreme IOptionAPIClientExtreme, IToastNotification toastNotification)
        {
            this.toastNotification = toastNotification;
            _IOptionAPIClientExtreme = IOptionAPIClientExtreme;
        }
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            try
            {
                var response = await _IOptionAPIClientExtreme.GetAll();
                if (response.Success == true)
                {
                    return View(response.Result.ToPagedList(pageNumber, pageSize));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var response = await _IOptionAPIClientExtreme.Delete(id);
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Xóa Thành Công!");
                    return Json(new
                    {
                        status = true
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string content)
        {
            try
            {
                var response = await _IOptionAPIClientExtreme.Create(content);
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Thêm Thành Công!");
                    return RedirectToAction("Index", response.Result);
                }
                else
                {
                    string Message = response.Message;
                    toastNotification.AddErrorToastMessage(Message);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var response = await _IOptionAPIClientExtreme.Get(id);
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
        public async Task<IActionResult> Edit(OptionsRequest request)
        {
            try
            {
                var response = await _IOptionAPIClientExtreme.Update(request);
                if (response.Success == true)
                {
                    toastNotification.AddSuccessToastMessage("Sửa Thành Công!");
                    return RedirectToAction("Index", response.Result);
                }
                else
                {
                    string Message = response.Message;
                    toastNotification.AddErrorToastMessage(Message);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
        }
    }
}
