using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    public class InterventionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
