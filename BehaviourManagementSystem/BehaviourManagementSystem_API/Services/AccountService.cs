using BehaviourManagementSystem_API.Entities;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<ResponceResult<string>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if(user == null)
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if(user == null)
                return new ResponceResultError<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if(!result.Succeeded)
                return new ResponceResultError<string>("Mật khẩu không chính xác");

            var claims = new[]
            {
                new Claim("id",user.Id.ToString())
            };

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Tokens:Key"])),
                SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                claims,
                expires: expiry,
                signingCredentials: creds);
            return new ResponceResultSuccess<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
