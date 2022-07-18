﻿using BehaviourManagementSystem_MVC.APIIntegration;
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
    public class AnalyzeAntecedentEnvironmentalController : Controller
    {
        private readonly IAntecedentEnvironmentalAPIClient _IAntecedentEnvironmentalAPIClient;
        public AnalyzeAntecedentEnvironmentalController(IAntecedentEnvironmentalAPIClient IAntecedentEnvironmentalAPIClient)
        {
            _IAntecedentEnvironmentalAPIClient = IAntecedentEnvironmentalAPIClient;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _IAntecedentEnvironmentalAPIClient.GetAll();
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
                var response = await _IAntecedentEnvironmentalAPIClient.Create(content);
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