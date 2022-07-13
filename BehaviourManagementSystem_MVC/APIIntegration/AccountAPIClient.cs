using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
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

        public async Task<ResponceResult<string>> Login(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/api/account/login", httpContent);

            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponceResultSuccess<string>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponceResultError<string>>(await response.Content.ReadAsStringAsync());
        }
    }
}
