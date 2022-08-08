using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Dashboard
{
    public class DashB : IDashB
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashB(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseResult<Tuple<int, int>>> GetAllAccountNotVerifyMail()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"/api/AdBd/GetAllAccountNotVerifyMail");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ResponseResultSuccess<Tuple<int, int>>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ResponseResultError<Tuple<int, int>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<Tuple<int, int, int>>>> GetAllAssessAndInterByMonthWithTeacher(string teacherid)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"/api/AdBd/GetAllAssessAndInterByMonthWithTeacher?id={teacherid}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<Tuple<int, int, int>>>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ResponseResultError<List<Tuple<int, int, int>>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<Tuple<int, int, int>>> GetAllStudentAndTeacherAndAllAccount()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"/api/AdBd/GetAllStudentAndTeacherAndAllAccount");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ResponseResultSuccess<Tuple<int, int, int>>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ResponseResultError<Tuple<int, int, int>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<Tuple<int, int>>>> GetCountAllAccountRegisterOfMonth(string m, string y)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"/api/AdBd//GetCountAllAccountRegisterOfMonth?month={m}&year={y}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<Tuple<int, int>>>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ResponseResultError<List<Tuple<int, int>>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<Tuple<int, int>>>> GetCountAllAccountRegisterOfYear(string year)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"/api/AdBd/GetCountAllAccountRegisterOfYear?year={year}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<Tuple<int, int>>>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ResponseResultError<List<Tuple<int, int>>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<Tuple<string, int>>>> GetCountAllStudentOfAllClasses(string teacherid)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"/api/AdBd/GetCountAllStudentOfAllClasses?teacherId={teacherid}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<Tuple<string, int>>>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ResponseResultError<List<Tuple<string, int>>>>(await response.Content.ReadAsStringAsync());
        }
    }
}
