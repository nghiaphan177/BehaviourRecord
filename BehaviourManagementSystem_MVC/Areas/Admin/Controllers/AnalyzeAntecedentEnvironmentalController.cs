using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace BehaviourManagementSystem_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnalyzeAntecedentEnvironmentalController : Controller
    {
        private readonly IAntecedentEnvironmentalAPIClient _IAntecedentEnvironmentalAPIClient;
        public AnalyzeAntecedentEnvironmentalController(IAntecedentEnvironmentalAPIClient IAntecedentEnvironmentalAPIClient)
        {
            _IAntecedentEnvironmentalAPIClient = IAntecedentEnvironmentalAPIClient;
        }
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            try
            {
                var response = await _IAntecedentEnvironmentalAPIClient.GetAll();
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
                var response = await _IAntecedentEnvironmentalAPIClient.Create(content);
                if (response.Success == true)
                {
                    TempData["MessageCreate"] = "Thêm thành công!";
                    return RedirectToAction("Index", response.Result);
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
                var response = await _IAntecedentEnvironmentalAPIClient.Get(id);
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
                var response = await _IAntecedentEnvironmentalAPIClient.Update(request);
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
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var response = await _IAntecedentEnvironmentalAPIClient.Delete(id);
                if (response.Success == true)
                {
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
