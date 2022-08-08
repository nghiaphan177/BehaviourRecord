using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Dashboard;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Area.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "Admin",Policy ="AdminOnly")]
    public class HomeController : Controller
    {
        private readonly IUserAPIClient _IUserAPIClient;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IDashB _dashB;

        public HomeController(IUserAPIClient IUserAPIClient, IWebHostEnvironment webHostEnvironment, IToastNotification toastNotification,IDashB dashB)
        {
            _IUserAPIClient = IUserAPIClient;
            this.webHostEnvironment = webHostEnvironment;
            _toastNotification = toastNotification;
            _dashB = dashB;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAccOfYear(string year)
        {
            var response = await _dashB.GetCountAllAccountRegisterOfYear(year);
            return Json(response.Result);
        }
        public async Task<JsonResult> GetAccOfMonth(string month,string year)
        {
            try
            {
                var response = await _dashB.GetCountAllAccountRegisterOfMonth(month, year);
                if (response != null)
                {
                    return Json(response.Result);
                }
                return Json(null);
            }
            catch (Exception)
            {
                return Json(null);
                throw;
            }
            
        }
        public async Task<JsonResult> GetAllAcc()
        {
            try
            {
                var response = await _dashB.GetAllStudentAndTeacherAndAllAccount();
                if (response != null)
                {
                    return Json(response.Result);
                }
                return Json(null);
            }
            catch (Exception)
            {
                return Json(null);
                throw;
            }
            
        }
        public async Task<JsonResult> GetAllVerify()
        {
            try
            {
                var response = await _dashB.GetAllAccountNotVerifyMail();
                if(response != null)
                {
                    return Json(response.Result);
                }
                return Json(null);
            }
            catch (Exception)
            {
                return Json(null);
                throw;
            }
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
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

        // GET: HomeController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var response = await _IUserAPIClient.GetUserById(id);
                if (response == null)
                {
                    return NoContent();
                }
                if (response.Success == false)
                {
                    _toastNotification.AddInfoToastMessage("Không tìm thấy người dùng này");
                    return RedirectToAction("Index","User");
                }
                if (response.Success == true)
                {
                    return View(response.Result);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        // POST: HomeController/Edit/5
        public async Task<ActionResult> Edit(IFormCollection collection, UserProfileRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);
            string webrootpath = webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            string fileName = null;
            string oldfile = null;
            if (files.Count != 0)
            {
                fileName = Guid.NewGuid().ToString().Replace("-", "") + request.UserName + Path.GetExtension(files[0].FileName);
                oldfile = request.AvtName;
                request.AvtName = fileName;
            }
            var response = await _IUserAPIClient.UpdateUser(request);
            if (response == null)
            {
                _toastNotification.AddErrorToastMessage("Không thể cập nhật");
                return View();
            }
            if (response.Success == false)
            {
                _toastNotification.AddErrorToastMessage("Cập nhật thông tin không thành công");
                return View();
            }
            if (response.Success == true)
            {
                if (fileName != null)
                {
                    var uploads = Path.Combine(webrootpath, @"images");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    if (oldfile != null && oldfile != "default_avt.png")
                    {
                        string _imageToBeDeleted = Path.Combine(webrootpath, @"images", oldfile);
                        if (System.IO.File.Exists(_imageToBeDeleted))
                        {
                            System.IO.File.Delete(_imageToBeDeleted);
                        }
                    }
                }
                _toastNotification.AddSuccessToastMessage("Cập nhật thành công");
                return RedirectToAction("Edit", new { id = User.FindFirst("Id").Value });
            }
            return View();
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
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
