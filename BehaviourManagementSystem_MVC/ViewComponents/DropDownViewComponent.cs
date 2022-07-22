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
    public class DropDownViewComponent : ViewComponent
    {
        private readonly IOptionAPIClientRecovery _IOptionAPIClientRecovery;
        private readonly IOptionAPIClientExtreme _IOptionAPIClientExtreme;
        private readonly IOptionAPIClientModerate _IOptionAPIClientModerate;
        private readonly IOptionAPIClientMild _IOptionAPIClientMild;
        public DropDownViewComponent(IOptionAPIClientRecovery IOptionAPIClientRecovery, IOptionAPIClientExtreme IOptionAPIClientExtreme, IOptionAPIClientModerate IOptionAPIClientModerate, IOptionAPIClientMild IOptionAPIClientMild)
        {
            _IOptionAPIClientRecovery = IOptionAPIClientRecovery;
            _IOptionAPIClientExtreme = IOptionAPIClientExtreme;
            _IOptionAPIClientModerate = IOptionAPIClientModerate;
            _IOptionAPIClientMild = IOptionAPIClientMild;
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
            return View();
        }
    }
}
