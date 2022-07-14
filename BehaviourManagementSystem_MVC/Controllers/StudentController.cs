using Microsoft.AspNetCore.Mvc;

namespace BehaviourManagementSystem_MVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StudentDetail()
        {
            return View();
        }

        public IActionResult StudentAdd()
        {
            return View();
        }
    }
}
