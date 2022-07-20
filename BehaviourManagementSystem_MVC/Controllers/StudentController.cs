using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
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
                var response = await _IIndividualAPIClient.GetAll();
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
