using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserAPIClient _userAPIClient;
        private readonly IConfiguration _config;
        public UserController(IUserAPIClient userAPIClient, IConfiguration configuration)
        {
            _userAPIClient = userAPIClient;
            _config = configuration;
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var response = await _userAPIClient.GetAllUser();
            if (response.Success == true)
            {

                return View(response.Result);
            }


            return View();
        }

        // GET: UserController/Detail/5
        public async Task<ActionResult> Detail(string id)
        {

            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var response = await _userAPIClient.GetUserById(id);
                if (response.Success == true)
                {
                    return View(response.Result);
                }

            }
            catch (System.Exception)
            {

                throw;
            }
            return NotFound();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, UserProfileRequest user)
        {
            try
            {
                var response = await _userAPIClient.UpdateUser(id, user);
                if (response.Success == true)
                {
                    return View(response.Result);
                }
            }
            catch
            {
                throw;
            }
            return View();
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
