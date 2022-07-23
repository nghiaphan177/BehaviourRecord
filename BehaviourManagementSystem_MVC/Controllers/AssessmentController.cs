using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        public AssessmentController(IAssessmentAPIClient assessmentAPIClient)
        {
            _assessmentAPIClient = assessmentAPIClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(string id)
        {
            return View(new AssessmentRequest() { IndividualId = "dba7641b-80de-490f-8257-0c1897b50543" });
        }
        [HttpPost]
        public async Task<IActionResult> Create(string IndiId, AssessmentRequest request)
        {
            try
            {
                //var response = await _assessmentAPIClient.CreateRecord(IndiId, request);
                //if (response == null)
                //{
                //    return Json(new { success = false });
                //}
                //if (response.Success)
                //{
                //    return Json(new { success = true });
                //}

            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }
    }
}
