﻿using BehaviourManagementSystem_MVC.APIIntegration.ProfileRecovery;
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
        private readonly IOptionAPIClientRecovery _IOptionAPIClientExtreme;
        public ExtremeInterventionController(IOptionAPIClientRecovery IOptionAPIClientExtreme)
        {
            _IOptionAPIClientExtreme = IOptionAPIClientExtreme;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _IOptionAPIClientExtreme.GetAll();
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

        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var response = await _IOptionAPIClientExtreme.Delete(id);
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

        [HttpPost]
        public async Task<IActionResult> Create(string content)
        {
            try
            {
                var response = await _IOptionAPIClientExtreme.Create(content);
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
    }
}
