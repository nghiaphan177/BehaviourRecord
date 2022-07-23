using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Teacher")]
    public class StudentController : Controller
    {
        private readonly IIndividualAPIClient _IIndividualAPIClient;
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        private readonly IWebHostEnvironment webHostEnvironment;
        public StudentController(IIndividualAPIClient IIndividualAPIClient, IAssessmentAPIClient assessmentAPIClient,IWebHostEnvironment webHostEnvironment)
        {
            _IIndividualAPIClient = IIndividualAPIClient;
            _assessmentAPIClient = assessmentAPIClient;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> StudentAssessment()
        {
            var id = User.FindFirst("Id").Value;
            try
            {
                var response = await _IIndividualAPIClient.GetAll(id);
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

        public async Task<IActionResult> StudentList()
        {
            try
            {
                var id = User.FindFirst("Id").Value;
                //var response = await _IIndividualAPIClient.GetAll();
                var response = await _IIndividualAPIClient.GetAllStudentByTeacherId(id);
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

        public async Task<IActionResult> StudentDetail(string id)
        {

            try
            {
                dynamic mymodel = new ExpandoObject();
                var responseIndi = await _IIndividualAPIClient.Detail(id);
                var responseAssess = await _assessmentAPIClient.GetAll("dba7641b-80de-490f-8257-0c1897b50543");             
                if (responseIndi.Success == true && responseAssess.Success == true)
                {
                    mymodel.Individual = responseIndi.Result;
                    mymodel.Assessment = responseAssess.Result;
                    return View(mymodel);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return NotFound();
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
            string fileName = String.Empty;
            if (files.Count != 0)
            {
                fileName = Guid.NewGuid().ToString().Replace("-", "") + request.UserName + Path.GetExtension(files[0].FileName);
                request.AvtName = fileName;
                var response = await _IIndividualAPIClient.Create(request);
                if (response == null)
                {
                    ViewBag.MSError = response.Message;
                    return View();
                }
                if (response.Success == true)
                {
                    var uploads = Path.Combine(webrootpath, @"images");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    return RedirectToAction(nameof(StudentList));
                }
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
                var response = await _IIndividualAPIClient.Update(request);
                if (response.Success == true)
                {
                    TempData["MessageCreate"] = "Sửa thành công!";
                    return RedirectToAction("StudentList", response.Result);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
        }
        public IActionResult TeacherProfile()
        {
            return View();
        }

        public IActionResult TeacherProfileEdit()
        {
            return View();
        }
    }
}
