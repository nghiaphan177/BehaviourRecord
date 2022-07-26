using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_MVC.APIIntegration.Intervention;
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
        private readonly IInterventionAPIClient _interventionAPIClient;

        public HomeController(IIndividualAPIClient IIndividualAPIClient, IAssessmentAPIClient assessmentAPIClient, IWebHostEnvironment webHostEnvironment, IInterventionAPIClient interventionAPIClient )
        {
            _IIndividualAPIClient = IIndividualAPIClient;
            _assessmentAPIClient = assessmentAPIClient;
            this.webHostEnvironment = webHostEnvironment;
            _interventionAPIClient = interventionAPIClient;
        }
        public async Task<IActionResult> Index()
        {
            var id = User.FindFirst("Id").Value;
            try
            {
                var response = await _IIndividualAPIClient.GetThongTinSUa(id);
                
                if (response.Success == true)
                {
                    try
                    {
                        var individualId = response.Result.Ind_Id;
                        var response_assessment = await _assessmentAPIClient.GetAll(individualId);
                        if (response_assessment.Success == true)
                        {
                            ViewBag.ListAssessment = response_assessment.Result;
                            foreach (var item in response_assessment.Result)
                            {
                                var response_intervention = await _interventionAPIClient.GetAll(item.Id);
                                response_intervention.Result.ForEach(inter => ViewBag.ListIntervention.Add(inter));
                            }
                        }
                        
                    }
                    catch(Exception)
                    {
                        throw;
                    }
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
