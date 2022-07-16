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
    /// <summary>
    /// JwtGenerator Implement IJwtGenerator.
    /// Writer: DuyLH4
    /// </summary>
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
                new Claim("Role",roleNameNormal),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
