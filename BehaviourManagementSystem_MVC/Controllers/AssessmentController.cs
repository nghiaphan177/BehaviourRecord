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
        private readonly IAssesstmentAPIClient _assesstmentAPIClient;
        public AssessmentController(IAssesstmentAPIClient assesstmentAPIClient)
        {
            _assesstmentAPIClient = assesstmentAPIClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(string id)
        {
            return View(new AssessmentRequest() { IndividualId = "af0ff046-a823-4f38-a723-624848392312" });
        }
        [HttpPost]
        public async Task<IActionResult> Create(string IndiId, AssessmentRequest request)
        {
            try
            {
                var response = await _assesstmentAPIClient.Create(IndiId, request);
                if (response == null) { return View(); }
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(request);
        }
    }
}
