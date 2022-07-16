using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponsesModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
    public class AntecedentEvironmentalAPIClient : IAntecedentEvironmentalAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public AntecedentEvironmentalAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<ResponseResult<List<AnalyzeAntecedentEnvironmentalResponse>>> Create(string content)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            "/api/AnalyzeAntecedentEnvironmental/create?content=" + content);
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<AnalyzeAntecedentEnvironmentalResponse>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<AnalyzeAntecedentEnvironmentalResponse>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentEnvironmentalResponse>>> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            "/api/AnalyzeAntecedentEnvironmental/delete?id=" + id);
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<AnalyzeAntecedentEnvironmentalResponse>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<AnalyzeAntecedentEnvironmentalResponse>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentEnvironmentalResponse>>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/api/AnalyzeAntecedentEnvironmental/get-all");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<AnalyzeAntecedentEnvironmentalResponse>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<AnalyzeAntecedentEnvironmentalResponse>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<AnalyzeAntecedentEnvironmentalResponse>>> Update(AnalyzeAntecedentEnvironmentalRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/api/AnalyzeAntecedentEnvironmental/update",httpContent);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<AnalyzeAntecedentEnvironmentalResponse>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<AnalyzeAntecedentEnvironmentalResponse>>>(await response.Content.ReadAsStringAsync());
        }
    }
}
