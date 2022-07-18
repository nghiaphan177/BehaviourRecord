using BehaviourManagementSystem_MVC.APIIntegration.ProfileMild;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileModerate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ModerateInterventionController : Controller
    {
        private readonly IOptionAPIClientModerate _IOptionAPIClientModerate;
        public ModerateInterventionController(IOptionAPIClientModerate IOptionAPIClientModerate)
        {
            _IOptionAPIClientModerate = IOptionAPIClientModerate;
        }
        public async Task<IActionResult>  Index()
        {
            try
            {
                var response = await _IOptionAPIClientModerate.GetAll();
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
        public async Task<IActionResult> Create(string content)
        {
            try
            {
                var response = await _IOptionAPIClientModerate.Create(content);
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
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var response = await _IOptionAPIClientModerate.Delete(id);
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
