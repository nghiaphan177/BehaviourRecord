using BehaviourManagementSystem_MVC.APIIntegration.Intervention;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileExtreme;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileMild;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileModerate;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileRecovery;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.ViewComponents
{
    public class InterventionAllViewComponent : ViewComponent
    {
        private readonly IInterventionAPIClient _IInterventionAPIClient;
     
        public InterventionAllViewComponent(IInterventionAPIClient IInterventionAPIClient)
        {
            _IInterventionAPIClient = IInterventionAPIClient;
            
        }
       
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            try
            {
                var response = await _IInterventionAPIClient.GetAll(id);
                if (response.Success == true)
                {
                    return View("GetAllIntervention",response.Result);
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
