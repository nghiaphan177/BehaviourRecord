using BehaviourManagementSystem_MVC.APIIntegration.ProfileMild;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MildInterventionController : Controller
    {
        private readonly IOptionAPIClientMild _IOptionAPIClientMild;
        public MildInterventionController(IOptionAPIClientMild IOptionAPIClientMild)
        {
            _IOptionAPIClientMild = IOptionAPIClientMild;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _IOptionAPIClientMild.GetAll();
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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string content)
        {
            try
            {
                var response = await _IOptionAPIClientMild.Create(content);
                if (response.Success == true)
                {
                    return RedirectToAction("Index",response.Result);
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
                var response = await _IOptionAPIClientMild.Get(id);
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
                var response = await _IOptionAPIClientMild.Update(request);
                if (response.Success == true)
                {
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
                var response = await _IOptionAPIClientMild.Delete(id);
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
