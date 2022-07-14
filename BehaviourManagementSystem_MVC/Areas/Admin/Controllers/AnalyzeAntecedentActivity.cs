using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BehaviourManagementSystem_MVC.Areas.Admin.Controllers
{
    public class AnalyzeAntecedentActivity : Controller
    {
        // GET: AnalyzeAntecedentActivity
        public ActionResult Index()
        {
            return View();
        }

        // GET: AnalyzeAntecedentActivity/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnalyzeAntecedentActivity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnalyzeAntecedentActivity/Create
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

        // GET: AnalyzeAntecedentActivity/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnalyzeAntecedentActivity/Edit/5
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

        // GET: AnalyzeAntecedentActivity/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnalyzeAntecedentActivity/Delete/5
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
