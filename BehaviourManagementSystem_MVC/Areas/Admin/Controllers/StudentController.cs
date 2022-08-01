using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_MVC.APIIntegration.Intervention;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Area.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly IIndividualAPIClient _IIndividualAPIClient;
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        private readonly IInterventionAPIClient _IInterventionAPIClient;
        public StudentController(IIndividualAPIClient IIndividualAPIClient, IAssessmentAPIClient assessmentAPIClient, IInterventionAPIClient IInterventionAPIClient)
        {
            _IIndividualAPIClient = IIndividualAPIClient;
            _assessmentAPIClient = assessmentAPIClient;
            _IInterventionAPIClient = IInterventionAPIClient;
        }
        // GET: StudentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StudentController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            try
            {
                dynamic mymodel = new ExpandoObject();
                var responseIndi = await _IIndividualAPIClient.Detail(id);
                var responseAssess = await _assessmentAPIClient.GetAll(id);               
                if (responseIndi.Success == true && (responseAssess.Success == true || responseAssess.Message == "Hiện tại không có dữ liệu"))
                {
                    List<InterventionRequest> intervention_list = new List<InterventionRequest>();
                    if(responseAssess.Result != null)
                    {
                        foreach (var item in responseAssess.Result)
                        {
                            var response_intervention = await _IInterventionAPIClient.GetAll(item.Id);
                            if (response_intervention != null && response_intervention.Success == true)
                            {
                                response_intervention.Result.ForEach(inter => intervention_list.Add(inter));
                            }
                        }
                    }
                    
                    mymodel.Individual = responseIndi.Result;
                    mymodel.Assessment = responseAssess.Result;
                    mymodel.Intervention = intervention_list;
                    return View(mymodel);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return NotFound();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult AssesetmentStudent(string id)
        {
            return View();
        }
        public IActionResult InterventionStudent(string id)
        {
            return View();
        }
    }
}
