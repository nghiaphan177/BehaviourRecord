using Microsoft.AspNetCore.Mvc;

namespace BehaviourManagementSystem_MVC.Areas.StudentApp.Controllers
{
    public class AssessmentController : Controller
    {
        [Area("StudentApp")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
