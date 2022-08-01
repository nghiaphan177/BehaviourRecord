using BehaviourManagementSystem_MVC.APIIntegration;
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
    public class AnalyzeAntecedentPerceivedController : Controller
    {
        private readonly IAntecedentPerceivedAPIClient _IAntecedentPerceivedAPIClient;
        private readonly IToastNotification toastNotification;
        public AnalyzeAntecedentPerceivedController(IAntecedentPerceivedAPIClient IAntecedentOptionAPIClient, IToastNotification toastNotification)
        {
            this.toastNotification = toastNotification;
            _IAntecedentPerceivedAPIClient = IAntecedentOptionAPIClient;
        }
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            try
            {
                var response = await _IAntecedentPerceivedAPIClient.GetAll();
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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string content)
        {
            try
            {
                var response = await _IAntecedentPerceivedAPIClient.Create(content);
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
                var response = await _IAntecedentPerceivedAPIClient.Get(id);
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
                var response = await _IAntecedentPerceivedAPIClient.Update(request);
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
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var response = await _IAntecedentPerceivedAPIClient.Delete(id);
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
    }
}
