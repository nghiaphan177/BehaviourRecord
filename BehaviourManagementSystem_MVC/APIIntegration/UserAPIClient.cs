using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponseModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration
{
    public class UserAPIClient : IUserAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UserAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<ResponseResult<List<UserResponse>>> GetAllUser()
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/api/Account/get-all-user");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<UserResponse>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<UserResponse>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseResult<UserResponse>> GetUserById(string id)
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            //truyen id user vao url api
            var response = await client.GetAsync($"/api/Account/get-user?id="+id);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<UserResponse>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<UserResponse>>(await response.Content.ReadAsStringAsync());
        }
    }
}
