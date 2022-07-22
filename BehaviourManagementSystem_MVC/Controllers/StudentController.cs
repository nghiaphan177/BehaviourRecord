﻿using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Teacher")]
    public class StudentController : Controller
    {
        private readonly IIndividualAPIClient _IIndividualAPIClient;
        public StudentController(IIndividualAPIClient IIndividualAPIClient)
        {
            _IIndividualAPIClient = IIndividualAPIClient;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var id = User.FindFirst("Id").Value;
                //var response = await _IIndividualAPIClient.GetAll();
                var response = await _IIndividualAPIClient.GetAllStudentByTeacherId(id);
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

        public IActionResult StudentDetail()
        {
            return View();
        }

        public IActionResult StudentAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentAdd(IndAssessRequest request)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var s = request;

            var response = await _IIndividualAPIClient.Create(request);
            if (response == null)
            {
                return View();
            }

            if (response.Success == true) return RedirectToAction(nameof(Index));
            else
            {
                ViewBag.MSError = response.Message;
                return View();
            }

        }

        public IActionResult StudentEdit()
        {
            return View();
        }

        public IActionResult TeacherProfile()
        {
            return View();
        }

        public IActionResult TeacherProfileEdit()
        {
            return View();
        }
    }
}
