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

namespace BehaviourManagementSystem_MVC.APIIntegration.Intervention
{
    public class InterventionAPIClient : IInterventionAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InterventionAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseResult<InterventionRequest>> CreateProfile(InterventionRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            $"api/Intervention/create-profile?ass_id={request.AssesetmentId}&p_date={request.ProfileDate?.ToString("MM/dd/yyyy")}&p_mild={request.ProfileMildDesciption}&p_moder={request.ProfileModerateDesciption}&p_extre={request.ProfileExtremeDesciption}&p_reco={request.ProfileRecoveryDesciption}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<InterventionRequest>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<InterventionRequest>>(await response.Content.ReadAsStringAsync());
        }

        public Task<ResponseResult<InterventionRequest>> Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseResult<List<InterventionRequest>>> GetAll(string ass_id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await client.GetAsync($"/api/Intervention/get-all/{ass_id}");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseResultSuccess<List<InterventionRequest>>>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<ResponseResultError<List<InterventionRequest>>>(await response.Content.ReadAsStringAsync());
        }
    }
}
