using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileExtreme;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileMild;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileModerate;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileRecovery;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.ViewComponents
{
    public class DropDownViewComponent : ViewComponent
    {
        private readonly IOptionAPIClientRecovery _IOptionAPIClientRecovery;
        private readonly IOptionAPIClientExtreme _IOptionAPIClientExtreme;
        private readonly IOptionAPIClientModerate _IOptionAPIClientModerate;
        private readonly IOptionAPIClientMild _IOptionAPIClientMild;
        private readonly IAntecedentActivityAPIClient _IAntecedentActivityAPIClient;
        private readonly IAntecedentEnvironmentalAPIClient _IAntecedentEnvironmentalAPIClient;
        private readonly IAntecedentPerceivedAPIClient _IAntecedentPerceivedAPIClient;
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        public DropDownViewComponent(IAssessmentAPIClient assessmentAPIClient, IOptionAPIClientRecovery IOptionAPIClientRecovery, 
            IOptionAPIClientExtreme IOptionAPIClientExtreme, 
            IOptionAPIClientModerate IOptionAPIClientModerate, 
            IOptionAPIClientMild IOptionAPIClientMild,
            IAntecedentActivityAPIClient IAntecedentActivityAPIClient,
            IAntecedentEnvironmentalAPIClient IAntecedentEnvironmentalAPIClient,
            IAntecedentPerceivedAPIClient IAntecedentPerceivedAPIClient
            )
        {
            _assessmentAPIClient = assessmentAPIClient;
            _IOptionAPIClientRecovery = IOptionAPIClientRecovery;
            _IOptionAPIClientExtreme = IOptionAPIClientExtreme;
            _IOptionAPIClientModerate = IOptionAPIClientModerate;
            _IOptionAPIClientMild = IOptionAPIClientMild;
            _IAntecedentActivityAPIClient = IAntecedentActivityAPIClient;
            _IAntecedentEnvironmentalAPIClient = IAntecedentEnvironmentalAPIClient;
            _IAntecedentPerceivedAPIClient = IAntecedentPerceivedAPIClient;
        }
       
        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            if (name == "recovery")
            {
                var response = await _IOptionAPIClientRecovery.GetAll();
                if (response.Success == true)
                {
                    return View("Recovery",response.Result);
                }
                return View();
            }
            if (name == "extreme")
            {
                var response = await _IOptionAPIClientExtreme.GetAll();
                if (response.Success == true)
                {
                    return View("Extreme",response.Result);
                }
                
                return View();
            }
            if (name == "moderate")
            {
                var response = await _IOptionAPIClientModerate.GetAll();
                if (response.Success == true)
                {
                    return View("Moderate", response.Result);
                }
                return View();
            }
            if (name == "mild")
            {
                var response = await _IOptionAPIClientMild.GetAll();
                if (response.Success == true)
                {
                    return View("Mild", response.Result);
                }
                return View();
            }
            if (name == "Antecedent")
            {
                dynamic mymodel = new ExpandoObject();
                var responsePer = await _IAntecedentPerceivedAPIClient.GetAll();
                var responseEn = await _IAntecedentEnvironmentalAPIClient.GetAll();
                var responseAc = await _IAntecedentActivityAPIClient.GetAll();
                if (responsePer.Success == true && responseEn.Success == true && responseAc.Success == true)
                {
                    mymodel.Perceived = responsePer.Result;
                    mymodel.Environmental = responseEn.Result;
                    mymodel.Activity = responseAc.Result;
                    return View("Antecedent", mymodel);
                }
                return View();
            }
            return View();
        }
    }
}
