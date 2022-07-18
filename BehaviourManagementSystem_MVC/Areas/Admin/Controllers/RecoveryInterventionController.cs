using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RecoveryInterventionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
