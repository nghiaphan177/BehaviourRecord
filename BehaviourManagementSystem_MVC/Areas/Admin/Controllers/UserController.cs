using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Area.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserAPIClient _userAPIClient;
        private readonly IIndividualAPIClient _IIndividualAPIClient;
        private readonly IConfiguration _config;
        public UserController(IUserAPIClient userAPIClient, IConfiguration configuration, IIndividualAPIClient IIndividualAPIClient)
        {
            _userAPIClient = userAPIClient;
            _config = configuration;
            _IIndividualAPIClient = IIndividualAPIClient;
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
            try
            {
                dynamic mymodel = new ExpandoObject();
                var response = await _userAPIClient.GetUserById(id);
                var responseStudent =  await _IIndividualAPIClient.GetAllStudentByTeacherId(id);
                if (response.Success == true)
                {
                    mymodel.Teacher = response.Result;
                    mymodel.Students = responseStudent.Result;
                    return View(mymodel);
                }

            }
            catch (System.Exception)
            {

                throw;
            }
            return NotFound();
        }

        // GET: UserController/Create
        public async Task<ActionResult> Create()
        {
            var listrole = await _userAPIClient.GetRole();
            if (listrole.Result != null)
            {
                ViewBag.Roles = listrole.Result.ConvertAll(r=> new SelectListItem
                {
                    Text = r.Name == "student"?"Học sinh":"Giáo viên",
                    Value = r.Id
                });
            }
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserProfileRequest request)
        {
            try
            {
                var response = await _userAPIClient.Create(request);
                if (response == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (response.Success == true)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
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
        public async Task<ActionResult> Edit(UserProfileRequest user)
        {
            try
            {
                var response = await _userAPIClient.UpdateUser( user);
                if(response.Success == false)
                {
                    return RedirectToAction(nameof(Edit), user.Id);
                }
                if (response.Success == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                throw;
            }
            return View();
        }

        // POST: UserController/Delete/5
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var response = await _userAPIClient.DeleteUser(id);
                /*var response = await _userAPIClient.DeleteUser(id);
                if (response.Success == true)
                {
                    return RedirectToAction(nameof(Index));
                }*/
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
