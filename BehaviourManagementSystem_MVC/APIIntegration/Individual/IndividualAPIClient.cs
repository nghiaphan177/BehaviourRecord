using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Individual
{
    public class IndividualAPIClient : IIndividualAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndividualAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseResult<List<IndAssessRequest>>> Create(IndAssessRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");



            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            //truyen id user vao url api
            var response = await client.PostAsync($"/api/Individual/Create", httpContent);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<IndAssessRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<IndAssessRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<IndividualRequest>> Detail(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            //truyen id user vao url api
            var response = await client.GetAsync($"/api/Individual/detail?id=" + id);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<IndividualRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<IndividualRequest>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<IndAssessRequest>>> GetAll(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/api/Individual/GetAllIndWithAssessment?id="+id);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<IndAssessRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<IndAssessRequest>>>(await response.Content.ReadAsStringAsync());
        }

        //public async Task<ResponseResult<List<IndividualRequest>>> GetAll()
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri(_configuration["BaseAddress"]);
        //    var response = await client.GetAsync($"/api/Individual/get-all");

        //    if (response.IsSuccessStatusCode)
        //        return JsonConvert.DeserializeObject<ResponseResultSuccess<List<IndividualRequest>>>(await response.Content.ReadAsStringAsync());
        //    return JsonConvert.DeserializeObject<ResponseResultError<List<IndividualRequest>>>(await response.Content.ReadAsStringAsync());
        //}


        public async Task<ResponseResult<List<IndAssessRequest>>> GetAllStudentByTeacherId(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/api/Individual/GetAllIndWithTeacher?id={id}");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResult<List<IndAssessRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResult<List<IndAssessRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<IndividualRequest>>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/api/Individual/get-all");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<IndividualRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<IndividualRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<IndAssessRequest>> GetThongTinSUa(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/api/Individual/GetIndById?id={id}");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<IndAssessRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<IndAssessRequest>>(await response.Content.ReadAsStringAsync());
        }
    }
}
