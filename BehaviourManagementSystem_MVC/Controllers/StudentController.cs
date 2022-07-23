using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Teacher")]
    public class StudentController : Controller
    {
        private readonly IIndividualAPIClient _IIndividualAPIClient;
        private readonly IWebHostEnvironment webHostEnvironment;
        public StudentController(IIndividualAPIClient IIndividualAPIClient, IWebHostEnvironment webHostEnvironment)
        {
            _IIndividualAPIClient = IIndividualAPIClient;
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

        public IActionResult StudentDetail(string id)
        {
            return View();
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

        public IActionResult StudentEdit()
        {
            return View();
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
