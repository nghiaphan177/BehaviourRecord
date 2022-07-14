using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BehaviourManagementSystem_MVC.Areas.Admin.Controllers
{
    public class AnalyzeAntecedentPerceivedController : Controller
    {
        // GET: AnalyzeAntecedentPerceivedController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AnalyzeAntecedentPerceivedController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnalyzeAntecedentPerceivedController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnalyzeAntecedentPerceivedController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnalyzeAntecedentPerceivedController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnalyzeAntecedentPerceivedController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnalyzeAntecedentPerceivedController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnalyzeAntecedentPerceivedController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
