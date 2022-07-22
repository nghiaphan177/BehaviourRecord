using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
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
        public async Task<IActionResult> StudentAssessment()
        {
            var id = User.FindFirst("Id").Value;
            try
            {
                var response = await _IIndividualAPIClient.GetAll(id);
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

        public async Task<IActionResult> StudentList()
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

        public async Task<IActionResult> StudentDetail(string id)
        {

            try
            {
                var response = await _IIndividualAPIClient.Detail(id);
                if (response.Success == true)
                {
                    return View(response.Result);
                }

            }
            catch (System.Exception)
            {

                throw;
            }
            return NotFound();
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

            if (response.Success == true) 
                return RedirectToAction(nameof(StudentList));
            else
            {
                ViewBag.MSError = response.Message;
                return View();
            }

        }

        public async Task<IActionResult> StudentEdit(string id)
        {
            try
            {
                var response = await _IIndividualAPIClient.GetThongTinSUa(id);
                if (response.Success == true)
                {
                    return View(response.Result);
                }

            }
            catch (System.Exception)
            {

                throw;
            }
            return NotFound();
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
