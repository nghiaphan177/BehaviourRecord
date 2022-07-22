using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    //[Authorize(AuthenticationSchemes = "Teacher")]
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
                var response = await _IIndividualAPIClient.GetAllList();
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
