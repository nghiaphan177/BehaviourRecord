using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Area.StudentApp.Controllers
{
    [Area("StudentApp")]
    [Authorize(AuthenticationSchemes = "Student", Policy = "StudentOnly")]
    public class HomeController : Controller
    {
        private readonly IIndividualAPIClient _IIndividualAPIClient;
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUserAPIClient _userAPIClient;

        public HomeController(IIndividualAPIClient IIndividualAPIClient, IAssessmentAPIClient assessmentAPIClient, IWebHostEnvironment webHostEnvironment,IUserAPIClient userAPIClient)
        {
            _IIndividualAPIClient = IIndividualAPIClient;
            _assessmentAPIClient = assessmentAPIClient;
            this.webHostEnvironment = webHostEnvironment;
            _userAPIClient = userAPIClient;
        }
        public async Task<IActionResult> Index()
        {
            var id = User.FindFirst("Id").Value;
            try
            {
                var response = await _userAPIClient.GetUserById(id);
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
    }
}
