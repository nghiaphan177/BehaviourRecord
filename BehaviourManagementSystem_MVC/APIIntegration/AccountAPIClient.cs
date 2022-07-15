using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponseModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
    public class AccountAPIClient : IAccountAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public AccountAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
      
        public async Task<ResponseResult<string>> Login(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync($"/api/account/login", httpContent);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<string>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<string>>(await response.Content.ReadAsStringAsync());
        }
    }
}
