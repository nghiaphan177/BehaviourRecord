﻿using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
    public class AntecedentPerceivedAPIClient : IAntecedentPerceivedAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AntecedentPerceivedAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseResult<List<OptionsRequest>>> Create(string content)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            "/api/AnalyzeAntecedentPercice/create?content=" + content);
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<OptionsRequest>>> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Delete,
            "/api/AnalyzeAntecedentPercice/delete?id=" + id);
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<OptionsRequest>> Get(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"/api/AnalyzeAntecedentPercice/get-by-id?id=" + id);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<OptionsRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<OptionsRequest>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<OptionsRequest>>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/api/AnalyzeAntecedentPercice/get-all");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<OptionsRequest>>> Update(OptionsRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/AnalyzeAntecedentPercice/update",httpContent);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
        }
    }
}
