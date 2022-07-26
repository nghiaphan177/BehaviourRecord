
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

        public Task<ResponseResult<AssessmentRequest>> CreateRecordAntecedent(string AssId, AssessmentRequest content)
        {
            throw new NotImplementedException();
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

        public Task<ResponseResult<AssessmentRequest>> CreateRecordConsequence(string AssId, AssessmentRequest content)
        {
            throw new NotImplementedException();
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
    }
}
