using BehaviourManagementSystem_MVC.APIIntegration.Account;
using BehaviourManagementSystem_MVC.APIIntegration.Dashboard;
using BehaviourManagementSystem_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Teacher")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountAPIClient _accountAPIClient;
        private readonly IDashB _dashB;

        public HomeController(ILogger<HomeController> logger, IAccountAPIClient accountAPIClient, IDashB dashB)
        {
            _logger = logger;
            _accountAPIClient = accountAPIClient;
            _dashB = dashB;
        }

        public  IActionResult Index()
        {
            ViewBag.Token =  HttpContext.Session.GetString("Token");
            return View();
        }
        public async Task<JsonResult> GetAllStudentTeacher()
        {
            try
            {
                var response = await _dashB.GetCountAllStudentOfAllClasses(User.FindFirst("Id").Value);
                if(response != null)
                {
                    return Json(response.Result);
                }
                
            }
            catch (Exception)
            {
                return Json(null);
                throw;
            }
            return Json(null);
        }
        public async Task<JsonResult> GetAllAssesStudent()
        {
            try
            {
                var response = await _dashB.GetAllAssessAndInterByMonthWithTeacher(User.FindFirst("Id").Value);
                if (response != null)
                {
                    return Json(response.Result);
                }

            }
            catch (Exception)
            {
                return Json(null);
                throw;
            }
            return Json(null);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}
