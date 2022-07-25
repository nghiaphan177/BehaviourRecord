using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Area.StudentApp.Controllers
{
    [Area("StudentApp")]
    public class AssessmentController : Controller
    {
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        private readonly IAntecedentActivityAPIClient _iAntecedentActivityAPIClient;
        private readonly IAntecedentEnvironmentalAPIClient _iAntecedentEnvironmentalAPIClient;
        private readonly IAntecedentPerceivedAPIClient _iAntecedentPerceivedAPIClient;
        public AssessmentController(IAssessmentAPIClient assessmentAPIClient, 
            IAntecedentActivityAPIClient IAntecedentActivityAPIClient, 
            IAntecedentEnvironmentalAPIClient IAntecedentEnvironmentalAPIClient,
            IAntecedentPerceivedAPIClient IAntecedentPerceivedAPIClient)
        {
            _assessmentAPIClient = assessmentAPIClient;
            _iAntecedentActivityAPIClient= IAntecedentActivityAPIClient;
            _iAntecedentEnvironmentalAPIClient = IAntecedentEnvironmentalAPIClient;
            _iAntecedentPerceivedAPIClient = IAntecedentPerceivedAPIClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(string id)
        {
            return View(new AssessmentRequest() { IndividualId = id });
        }
        [HttpPost]
        public async Task<IActionResult> Create(string IndiId, AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.CreateRecord(IndiId, request);
                if (response == null)
                {
                    return Json(new { success = false });
                }
                if (response.Success == true)
                {
                    ViewBag.AssId = response.Result.Id;
                    return Json(new { success = true, assid = response.Result.Id });
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecordBehaviour(string assId, AssessmentRequest request)
        {
            try
            {
                var response = await _assessmentAPIClient.CreateRecordBehaviour(ViewBag.AssId, request.AnalyzeBehaviour);
                if (response == null)
                {
                    return Json(new { success = false });
                }
                if (response.Success == true)
                {
                    return Json(new { success = true });
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string AssessId)
        {
            try
            {
                var response = await _assessmentAPIClient.Delete(AssessId);
                if (response == null)
                {
                    return Json(new { success = false });
                }
                if (response.Success)
                {
                    return Json(new { success = true });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = false });
        }
        public async Task<PartialViewResult> DropdownAntecedent()
        {
            dynamic mymodel = new ExpandoObject();
            var responsePer = await _iAntecedentPerceivedAPIClient.GetAll();
            var responseEn = await _iAntecedentEnvironmentalAPIClient.GetAll();
            var responseAc = await _iAntecedentActivityAPIClient.GetAll();
            if (responsePer.Success == true && responseEn.Success == true && responseAc.Success == true)
            {
                mymodel.Perceived = responsePer.Result;
                mymodel.Environmental = responseEn.Result;
                mymodel.Activity = responseAc.Result;
                return PartialView("Antecedent", mymodel);
            }
            return PartialView();
        }
    }
}
