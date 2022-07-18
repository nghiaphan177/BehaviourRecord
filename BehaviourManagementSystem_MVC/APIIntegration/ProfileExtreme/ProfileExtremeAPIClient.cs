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
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.ProfileExtreme
{
    public class ProfileExtremeAPIClient : IOptionAPIClientExtreme
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProfileExtremeAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
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
            $"/api/ProfileExtreme/create?content=" + content);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<List<OptionsRequest>>> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.DeleteAsync($"/api/ProfileExtreme/delete?id=" + id);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public Task<ResponseResult<OptionsRequest>> Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseResult<List<OptionsRequest>>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/api/ProfileExtreme/get-all");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<OptionsRequest>>>(await response.Content.ReadAsStringAsync());
        }

        public Task<ResponseResult<List<OptionsRequest>>> Update(OptionsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
