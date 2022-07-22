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
        public InterventionController(IOptionAPIClientRecovery IOptionAPIClientRecovery, IOptionAPIClientExtreme IOptionAPIClientExtreme)
        {
            _IOptionAPIClientRecovery = IOptionAPIClientRecovery;
            _IOptionAPIClientExtreme = IOptionAPIClientExtreme;
        }
        public IActionResult Index()
        {
            var baseViewModel = new List<OptionsRequest>();
            return View(baseViewModel);
        }

        
        public async Task<PartialViewResult> DropdownExtreme()
        {
            var response = await _IOptionAPIClientExtreme.GetAll();
            if (response.Success == true)
            {
                return PartialView(response.Result);
            }
            return PartialView();
        }
    }
}
