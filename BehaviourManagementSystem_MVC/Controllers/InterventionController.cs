using BehaviourManagementSystem_MVC.APIIntegration.Intervention;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileExtreme;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileRecovery;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    public class InterventionController : Controller
    {
        private readonly IOptionAPIClientRecovery _IOptionAPIClientRecovery;
        private readonly IOptionAPIClientExtreme _IOptionAPIClientExtreme;
        private readonly IInterventionAPIClient _IInterventionAPIClient;
        public InterventionController(IOptionAPIClientRecovery IOptionAPIClientRecovery, IOptionAPIClientExtreme IOptionAPIClientExtreme, IInterventionAPIClient IInterventionAPIClient)
        {
            _IOptionAPIClientRecovery = IOptionAPIClientRecovery;
            _IOptionAPIClientExtreme = IOptionAPIClientExtreme;
            _IInterventionAPIClient = IInterventionAPIClient;
        }
        public IActionResult Index()
        {

            return View();
        }


        //public async Task<PartialViewResult> DropdownExtreme()
        //{
        //    var response = await _IOptionAPIClientExtreme.GetAll();
        //    if (response.Success == true)
        //    {
        //        return PartialView(response.Result);
        //    }
        //    return PartialView();
        //}

        public IActionResult CreateProfile()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProfile(InterventionRequest request)
        {
            try
            {
                var response = await _IInterventionAPIClient.CreateProfile(request);
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
    }
}
