using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BehaviourManagementSystem_MVC.Areas.StudentApp.Controllers
{
    [Area("StudentApp")]
    public class HomeController : Controller
    {
        [Route("student-dashboard")]
        //[Authorize(Roles="student")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
