﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BehaviourManagementSystem_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AnalyzeAntecedentEnviromentalController : Controller
    {
        // GET: AnalyzeAntecedentEnviromentalController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AnalyzeAntecedentEnviromentalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnalyzeAntecedentEnviromentalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnalyzeAntecedentEnviromentalController/Create
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

        // GET: AnalyzeAntecedentEnviromentalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnalyzeAntecedentEnviromentalController/Edit/5
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

        // GET: AnalyzeAntecedentEnviromentalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnalyzeAntecedentEnviromentalController/Delete/5
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
