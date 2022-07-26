﻿using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Account;
using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System.Web;

namespace BehaviourManagementSystem_MVC.Area.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "Admin", Policy = "AdminOnly")]
    public class UserController : Controller
    {
        private readonly IAccountAPIClient _accountAPIClient;
        private readonly IUserAPIClient _userAPIClient;
        private readonly IIndividualAPIClient _IIndividualAPIClient;
        private readonly IConfiguration _config;
        public UserController(IAccountAPIClient accountAPIClient, IUserAPIClient userAPIClient, IConfiguration configuration, IIndividualAPIClient IIndividualAPIClient)
        {
            this._accountAPIClient = accountAPIClient;
            _userAPIClient = userAPIClient;
            _config = configuration;
            _IIndividualAPIClient = IIndividualAPIClient;
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var response = await _userAPIClient.GetAllUser();
            if(response.Success == true)
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
                var responseStudent = await _IIndividualAPIClient.GetAllStudentByTeacherId(id);
                if(response.Success == true)
                {
                    mymodel.Teacher = response.Result;
                    mymodel.Students = responseStudent.Result;
                    return View(mymodel);
                }

            }
            catch(System.Exception)
            {

                throw;
            }
            return NotFound();
        }

        // GET: UserController/Create
        public async Task<ActionResult> Create()
        {
            var listrole = await _userAPIClient.GetRole();
            if(listrole.Result != null)
            {
                ViewBag.Roles = listrole.Result.ConvertAll(r => new SelectListItem
                {
                    Text = r.Name == "student" ? "Học sinh" : "Giáo viên",
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
                request.Id = User.FindFirst("Id").Value;

                var response = await _userAPIClient.Create(request);

                if(response == null)
                    return RedirectToAction(nameof(Index));

                if(!response.Success == true)
                    return RedirectToAction(nameof(Create));

                // Chổ này cần gửi mail để cho account vừa tạo nhập mật khẩu
                var res = await _accountAPIClient.ForgotPassword(request.Email);

                // https://localhost:port/Account/ResetPassword?id=****&code=****/
                var uri = new UriBuilder(_config["EmailSettings:MailBodyHtml"] + "/Admin/User/NewPassword");
                var query = HttpUtility.ParseQueryString(uri.Query);
                query["id"] = res.Result.Id;
                query["code"] = res.Result.Code;
                uri.Query = query.ToString();
                var url = uri.ToString();
                var subject = "Bạn đã được tạo mới một tại khoản tại GR4BMS";
                var htmlMessage =
                    $"Hãy nhấp vào link sao để đặt mật khẩu mới cho tài khoản của bạn." +
                    $"<a href='{url}' style='color:red;'>" +
                        $"<strong>" +
                            $"<u>" +
                                $"<i>link tại đây</i>" +
                            $"</u>" +
                        $"</strong>" +
                    $"</a>";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Create));
            }
        }

        [HttpGet]
        public IActionResult NewPassword(string id, string code)
        {
            if(string.IsNullOrEmpty(id) ||
                string.IsNullOrEmpty(code))
                return NotFound();
            return View(); // cần form new pass
        }

        [HttpPost]
        public async Task<IActionResult> NewPassword(ResetPasswordRequest req)
        {
            if(!ModelState.IsValid)
                return NotFound(); // cần view lỗi

            if(string.IsNullOrEmpty(req.Id) ||
            string.IsNullOrEmpty(req.Code) ||
            string.IsNullOrEmpty(req.PasswordNew) ||
            string.IsNullOrEmpty(req.PasswordConfirm))
                return View(ModelState); // cần view lỗi

            if(req.PasswordNew != req.PasswordConfirm)
                return View(ModelState);

            var res = await _accountAPIClient.ResetPassword(req);

            if(res == null)
            {
                ViewBag.MessageError = "Đặt mật khẩu không thành công";
                return View();
            }

            if(!res.Success)
            {
                ViewBag.MessageError = "Đặt mật khẩu không thành công";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var response = await _userAPIClient.GetUserById(id);
                if(response.Success == true)
                {
                    return View(response.Result);
                }

            }
            catch(System.Exception)
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
                var response = await _userAPIClient.UpdateUser(user);
                if(response.Success == false)
                {
                    return RedirectToAction(nameof(Edit), user.Id);
                }
                if(response.Success == true)
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
