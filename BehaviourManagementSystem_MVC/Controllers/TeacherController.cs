using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Account;
using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly IIndividualAPIClient _IIndividualAPIClient;
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly IUserAPIClient _userAPIClient;
        private readonly IAccountAPIClient _accountAPIClient;

        public TeacherController(IAccountAPIClient accountAPIClient, IIndividualAPIClient IIndividualAPIClient, IToastNotification toastNotification, IAssessmentAPIClient assessmentAPIClient, IWebHostEnvironment webHostEnvironment, IUserAPIClient userAPIClient)
        {
            _IIndividualAPIClient = IIndividualAPIClient;
            _assessmentAPIClient = assessmentAPIClient;
            this.webHostEnvironment = webHostEnvironment;
            this._toastNotification = toastNotification;
            _userAPIClient = userAPIClient;
            _accountAPIClient = accountAPIClient;
        }
        public async Task<IActionResult> TeacherProfile()
        {
            try
            {
                var id = User.FindFirst("Id").Value;
                var response = await _userAPIClient.GetUserById(id);
                if (response == null)
                {
                    return NoContent();
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

        [HttpGet]
        public async Task<IActionResult> TeacherProfileEdit(string id)
        {
            try
            {


                var response = await _userAPIClient.GetUserById(id);
                if (response == null)
                {
                    return NoContent();
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

        
        public async Task<IActionResult> TeacherProfileEdit(IFormFile file, UserProfileRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);
            string webrootpath = webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            string fileName = null;
            if (files.Count != 0)
            {
                fileName = Guid.NewGuid().ToString().Replace("-", "") + request.UserName + Path.GetExtension(files[0].FileName);
                request.AvtName = fileName;
            }
            var response = await _userAPIClient.UpdateUser(request);
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
                }
                _toastNotification.AddSuccessToastMessage("Cập nhật thành công");
                return RedirectToAction("TeacherProfileEdit", new { Id = User.FindFirst("Id").Value });
            }
            return View();
        }
        public async Task<IActionResult> ChangePassword()
        {
            try
            {
                var id = User.FindFirst("Id").Value;
                var response = await _userAPIClient.GetUserById(id);
                if (response == null)
                {
                    return NoContent();
                }
                if (response.Success == true)
                {
                    ViewBag.userProfileRequest = response.Result;
                    return View();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("Đổi mật khẩu thất bại");
                return View(ModelState);
            }

            var response = await _accountAPIClient.ChangePassword(request);
            if (response != null)
            {
                if (response.Success)
                {
                    _toastNotification.AddSuccessToastMessage("Đổi mật khẩu thành công");
                    return RedirectToAction("ChangePassSuccess");
                }
                else if (response.Success == false)
                {
                    _toastNotification.AddErrorToastMessage("Đổi mật khẩu thất bại");
                    return RedirectToAction("ChangePassword");
                }
            }

            _toastNotification.AddAlertToastMessage("Không thể đổi mật khẩu");
            return RedirectToAction("ChangePassword");
        }
        [HttpGet]
        public IActionResult ChangePassSuccess()
        {
            return View();
        }


    }
}
