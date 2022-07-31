using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_MVC.APIIntegration.Intervention;
using BehaviourManagementSystem_ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Area.StudentApp.Controllers
{
    [Area("StudentApp")]
    [Authorize(AuthenticationSchemes = "Student", Policy = "StudentOnly")]
    public class HomeController : Controller
    {
        private readonly IIndividualAPIClient _IIndividualAPIClient;
        private readonly IAssessmentAPIClient _assessmentAPIClient;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IInterventionAPIClient _interventionAPIClient;
        private readonly IToastNotification _toastNotification;
        private readonly IUserAPIClient _userAPIClient;

        public HomeController(IToastNotification toastNotification, IUserAPIClient userAPIClient, IIndividualAPIClient IIndividualAPIClient, IAssessmentAPIClient assessmentAPIClient, IWebHostEnvironment webHostEnvironment, IInterventionAPIClient interventionAPIClient )
        {
            _IIndividualAPIClient = IIndividualAPIClient;
            _assessmentAPIClient = assessmentAPIClient;
            this.webHostEnvironment = webHostEnvironment;
            _interventionAPIClient = interventionAPIClient;
            _userAPIClient = userAPIClient;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var user_id = User.FindFirst("Id").Value;
            try
            {
                var response = await _IIndividualAPIClient.GetThongTinSUa(user_id);
                
                if (response.Success == true)
                {
                    try
                    {
                        var individualId = response.Result.Ind_Id;
                        var response_assessment = await _assessmentAPIClient.GetAll(individualId);
                        List<InterventionRequest> intervention_list = new List<InterventionRequest>();
                        if (response_assessment.Success == true)
                        {
                            ViewBag.ListAssessment = response_assessment.Result;
                            foreach (var item in response_assessment.Result)
                            {
                                var response_intervention = await _interventionAPIClient.GetAll(item.Id);
                                if(response_intervention.Success)
                                {
                                    response_intervention.Result.ForEach(inter => intervention_list.Add(inter));
                                }
                            }
                            ViewBag.ListIntervention = intervention_list;
                        }
                        else
                        {
                            ViewBag.ListIntervention = null;
                        }
                        return View(response.Result);
                    }
                    catch(Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    ViewBag.ListAssessment = null;
                    ViewBag.ListIntervention = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        public async Task<IActionResult> Assessment(string id)
        {
            try
            {
                var response = await _assessmentAPIClient.Get(id);
                if(response.Success==true)
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
        public async Task<IActionResult> Intervention (string id)
        {
            try
            {
                var response = await _interventionAPIClient.Get(id);
                if(response.Success==true)
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
        public async Task<IActionResult> StudentProfile()
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
        public async Task<IActionResult> StudentProfileEdit(string id)
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

        public async Task<IActionResult> StudentProfileEdit(IFormFile file, UserProfileRequest request)
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
                return RedirectToAction("StudentProfileEdit", new { Id = User.FindFirst("Id").Value });
            }
            return View();
        }
    }
}
