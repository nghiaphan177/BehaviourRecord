using BehaviourManagementSystem_MVC.APIIntegration;
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
using X.PagedList;

namespace BehaviourManagementSystem_MVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Teacher")]
    public class StudentController : Controller
    {
        private readonly IIndividualAPIClient _IIndividualAPIClient;
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IToastNotification toastNotification;
        private readonly IUserAPIClient _userAPIClient;

        public StudentController(IIndividualAPIClient IIndividualAPIClient, IToastNotification toastNotification, IAssessmentAPIClient assessmentAPIClient, IWebHostEnvironment webHostEnvironment, IUserAPIClient userAPIClient)
        {
            _IIndividualAPIClient = IIndividualAPIClient;
            _assessmentAPIClient = assessmentAPIClient;
            this.webHostEnvironment = webHostEnvironment;
            this.toastNotification = toastNotification;
            _userAPIClient = userAPIClient;
        }
        public async Task<IActionResult> StudentAssessment(int? page)
        {
            var id = User.FindFirst("Id").Value;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            try
            {
                var response = await _IIndividualAPIClient.GetAll(id);
                if (response.Success == true)
                {
                    return View(response.Result.ToPagedList(pageNumber, pageSize));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public async Task<IActionResult> StudentList(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            try
            {
                var id = User.FindFirst("Id").Value;
                //var response = await _IIndividualAPIClient.GetAll();
                var response = await _IIndividualAPIClient.GetAllStudentByTeacherId(id);
                if (response.Success == true)
                {
                    return View(response.Result.ToPagedList(pageNumber, pageSize));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public async Task<IActionResult> StudentDetail(string id, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            try
            {
                dynamic mymodel = new ExpandoObject();
                var responseIndi = await _IIndividualAPIClient.Detail(id);
                var responseAssess = await _assessmentAPIClient.GetAll(id);
                if (responseIndi.Success == true && (responseAssess.Success == true || responseAssess.Message == "Hiện tại không có dữ liệu"))
                {
                    ViewBag.IdIndi = id;
                    mymodel.Individual = responseIndi.Result;
                    mymodel.Assessment = responseAssess.Result;
                    if (mymodel.Assessment != null)
                    {
                        mymodel.Assessment = responseAssess.Result.ToPagedList(pageNumber, pageSize);
                    }
                    return View(mymodel);
                }
                else
                {
                    mymodel.Assessment = responseAssess.Result.ToPagedList(pageNumber, pageSize);
                    return View(mymodel);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IActionResult StudentAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentAdd(IFormFile imageModel, IndAssessRequest request)
        {
            if (!ModelState.IsValid)
                return NotFound();
            string webrootpath = webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            string fileName = null;
            if (files.Count != 0)
            {
                fileName = Guid.NewGuid().ToString().Replace("-", "") + request.UserName + Path.GetExtension(files[0].FileName);
                request.AvtName = fileName;
            }
            var response = await _IIndividualAPIClient.Create(request);
            if (response == null)
            {
                toastNotification.AddErrorToastMessage("Tạo học sinh không thành công");
                return View();
            }
            if (response.Success == false)
            {
                toastNotification.AddErrorToastMessage("Tạo tài khoản không thành công");
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
                toastNotification.AddSuccessToastMessage("Tạo học sinh thành công");
                return RedirectToAction(nameof(StudentList));
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> StudentEdit(string id)
        {
            try
            {
                var response = await _IIndividualAPIClient.GetThongTinSUa(id);

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

        [HttpPost]
        public async Task<IActionResult> StudentEdit(IndAssessRequest request)
        {
            try
            {
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
                var response = await _IIndividualAPIClient.Update(request);
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
                    toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công!");
                    return RedirectToAction("StudentList", response.Result);
                }
                else
                {
                    toastNotification.AddErrorToastMessage(response.Message);
                    return RedirectToAction("StudentList", response.Result);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(string idIndi, string idTeacher)
        {

            var response = await _IIndividualAPIClient.Delete(idTeacher, idIndi);
            if (response.Success == true)
            {
                toastNotification.AddSuccessToastMessage("Đã Xóa Thành Công!");
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                toastNotification.AddErrorToastMessage(response.Message);
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudentAssement(string id)
        {

            var response = await _assessmentAPIClient.DeleteAssementIndi(id);
            if (response.Success == true)
            {
                toastNotification.AddSuccessToastMessage("Đã Xóa Thành Công!");
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                toastNotification.AddErrorToastMessage(response.Message);
            }
            return NoContent();
        }
    }
}
