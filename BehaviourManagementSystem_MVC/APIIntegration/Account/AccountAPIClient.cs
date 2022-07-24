using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.APIIntegration.Account
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

			var response = await client.PostAsync($"/api/Account/Login", httpContent);
			//var response = await client.PostAsync($"/api/account/user-img/{id}", httpContent);

			if(response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ResponseResultSuccess<string>>(await response.Content.ReadAsStringAsync());
			return JsonConvert.DeserializeObject<ResponseResultError<string>>(await response.Content.ReadAsStringAsync());
		}

		public async Task<ResponseResult<UserProfileRequest>> ConfirmEmail(ConfirmEmailRequest request)
		{
			var json = JsonConvert.SerializeObject(request);

			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var client = _httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(_configuration["BaseAddress"]);

			var response = await client.PostAsync($"/api/Account/VerifyEmail", httpContent);

			if(response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ResponseResultSuccess<UserProfileRequest>>(await response.Content.ReadAsStringAsync());
			return JsonConvert.DeserializeObject<ResponseResultError<UserProfileRequest>>(await response.Content.ReadAsStringAsync());
		}

		public async Task<ResponseResult<ConfirmEmailRequest>> Register(RegisterRequest request)
		{
			var json = JsonConvert.SerializeObject(request);

			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var client = _httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(_configuration["BaseAddress"]);

			var response = await client.PostAsync($"/api/Account/Register", httpContent);

			if(response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ResponseResultSuccess<ConfirmEmailRequest>>(await response.Content.ReadAsStringAsync());
			return JsonConvert.DeserializeObject<ResponseResultError<ConfirmEmailRequest>>(await response.Content.ReadAsStringAsync());
		}

		public async Task<ResponseResult<ConfirmEmailRequest>> ResendConfirmEmail(string email)
		{
			var client = _httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(_configuration["BaseAddress"]);

			var response = await client.GetAsync($"/api/Account/ResenConfirmEmail?email={email}");

			if(response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ResponseResultSuccess<ConfirmEmailRequest>>(await response.Content.ReadAsStringAsync());
			return JsonConvert.DeserializeObject<ResponseResultError<ConfirmEmailRequest>>(await response.Content.ReadAsStringAsync());
		}

		public async Task<ResponseResult<ResetPasswordRequest>> ForgotPassword(string userNameOrEmail)
		{
			var client = _httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(_configuration["BaseAddress"]);

			var response = await client.GetAsync($"/api/Account/ForgotPassword?userNameOfEmail={userNameOrEmail}");

			if(response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ResponseResultSuccess<ResetPasswordRequest>>(await response.Content.ReadAsStringAsync());
			return JsonConvert.DeserializeObject<ResponseResultError<ResetPasswordRequest>>(await response.Content.ReadAsStringAsync());
		}

		public async Task<ResponseResult<bool>> GetEmailConfirmed(string email)
		{
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);

			var response = await client.GetAsync($"/api/Account/CheckEmailConfirmed?email={email}");

			if(response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ResponseResultSuccess<bool>>(await response.Content.ReadAsStringAsync());
			return JsonConvert.DeserializeObject<ResponseResultError<bool>>(await response.Content.ReadAsStringAsync());
		}

		public async Task<ResponseResult<string>> ResetPassword(ResetPasswordRequest request)
		{
			var json = JsonConvert.SerializeObject(request);

			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);

			var response = await client.PutAsync($"/api/Account/ResetPassword", httpContent);

			if(response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ResponseResultSuccess<string>>(await response.Content.ReadAsStringAsync());
			return JsonConvert.DeserializeObject<ResponseResultError<string>>(await response.Content.ReadAsStringAsync());
		}

        public async Task<ResponseResult<string>> GetImg(string userId)
        {
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);

			var res = await client.GetAsync($"/api/Account/GetImg?id={userId}");

			if(res.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ResponseResultSuccess<string>>(await res.Content.ReadAsStringAsync());
			return JsonConvert.DeserializeObject<ResponseResultError<string>>(await res.Content.ReadAsStringAsync());
		}

        public async Task<ResponseResult<string>> CheckImgUrl(string result)
        {
			var client = _httpClientFactory.CreateClient();
			var res = await client.GetAsync(result);

			if(res.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<ResponseResultSuccess<string>>(await res.Content.ReadAsStringAsync());
			return JsonConvert.DeserializeObject<ResponseResultError<string>>(await res.Content.ReadAsStringAsync());
		}
	}
}
