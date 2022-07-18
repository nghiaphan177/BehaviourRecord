using BehaviourManagementSystem_MVC.APIIntegration.Profile;
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
    public class ExtremeInterventionController : Controller
    {
        private readonly IExtremeOptionAPIClient _IExtremeOptionAPIClient;
        public ExtremeInterventionController(IExtremeOptionAPIClient IExtremeOptionAPIClient)
        {
            _IExtremeOptionAPIClient = IExtremeOptionAPIClient;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _IExtremeOptionAPIClient.GetAll();
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
        public async Task<IActionResult> CreateAsync(string content)
        {
            try
            {
                var response = await _IExtremeOptionAPIClient.Create(content);
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

        public IActionResult Edit()
        {
            return View();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var response = await _IExtremeOptionAPIClient.Delete(id);
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
