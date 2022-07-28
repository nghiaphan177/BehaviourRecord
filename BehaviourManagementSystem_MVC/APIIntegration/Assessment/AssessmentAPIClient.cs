
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Assesstment
{
    public class AssessmentAPIClient : IAssessmentAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AssessmentAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseResult<AssessmentRequest>> CreateRecord(AssessmentRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            $"api/Assessment/create-record?ind_id={request.IndividualId}&r_date={request.RecordDate?.ToString("MM/dd/yyyy")}&r_start={request.RecordStart}&r_end={request.RecordEnd}&r_where={request.RecordWhere}&r_who={request.RecordWho}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<AssessmentRequest>> UpdateAnalyzeAntecedent(string AssId, AssessmentRequest content)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Put,
            $"/api/Assessment/update-antecedent?ass_id={AssId}&ana_ant_per={content.AnalyzeAntecedentPerceivedDescription}&ana_ant_envi={content.AnalyzeAntecedentEnvironmentalDescription}&ana_ant_act={content.AnalyzeAntecedentActivityDescription}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<AssessmentRequest>> CreateRecordBehaviour(string AssId, string content)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Put,
            $"api/Assessment/update-behaviour?ass_id={AssId}&ana_behaviour={content}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<AssessmentRequest>> UpdateAnalyzeConsequence(string AssId, AssessmentRequest content)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Put,
            $"/api/Assessment/update-consequence?ass_id={AssId}&ana_con_per={content.AnalyzeConsequencesPerceive}&ana_con_envi={content.AnalyzeConsequenceEnvironmental}&ana_con_act={content.AnalyzeConsequencesActivity}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<AssessmentRequest>>> Delete(string AssId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Delete,
            $"api/Assessment/delete?ass_id={AssId}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<AssessmentRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<AssessmentRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<AssessmentRequest>> Get(string assid)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"/api/Assessment/detail?ass_id={assid}");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<AssessmentRequest>>> GetAll(string indiID)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"/api/Assessment/get-all/{indiID}");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<AssessmentRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<AssessmentRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public Task<ResponseResult<List<AssessmentRequest>>> Update(AssessmentRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseResult<AssessmentRequest>> UpdateRecord(string AssId, AssessmentRequest content)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Put,
            $"/api/Assessment/update-record?ass_id={AssId}&r_date={content.RecordDate}&r_start={content.RecordStart}&r_end={content.RecordEnd}&r_where={content.RecordWhere}&r_who={content.RecordWho}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<AssessmentRequest>> UpdateFuntionAntecedent(string AssId, string content)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Put,
            $"/api/Assessment/update-funtion-antecedent?ass_id={AssId}&fun_ant={content}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<AssessmentRequest>> UpdateFuntionConsequence(string AssId, string content)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Put,
            $"/api/Assessment/update-funtion-consequece?ass_id={AssId}&fun_con={content}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<AssessmentRequest>>(await response.Content.ReadAsStringAsync());
        }
    }
}
