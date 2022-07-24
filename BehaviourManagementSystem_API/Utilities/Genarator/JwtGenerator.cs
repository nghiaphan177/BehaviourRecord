using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Utilities.JwtGenarator
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;

        public JwtGenerator(IConfiguration configuration, IRoleService roleService)
        {
            _configuration = configuration;
            _roleService = roleService;
        }

        public async Task<string> GenerateTokenLoginSuccessAsync(User user)
        {
            var role = await _roleService.GetRoleNameByUserId(user.Id.ToString());
            var roleNameNormal = role.Success ? role.Result : null;

            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName",user.UserName),
                new Claim(ClaimTypes.Surname, user.FirstName),
                new Claim(ClaimTypes.Name, user.LastName),
                new Claim(ClaimTypes.Role, roleNameNormal),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Dep:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddHours(12);

            var token = new JwtSecurityToken(
                _configuration["Tokens:Dep:Issuer"],
                _configuration["Tokens:Dep:Issuer"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
