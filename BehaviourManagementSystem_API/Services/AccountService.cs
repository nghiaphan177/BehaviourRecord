using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Extensions;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Utilities.JwtGenarator;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    /// <summary>
    /// Class AccountService Implement IAccountService. Design parttern repository.
    /// WriterL DuyLH4
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IJwtGenerator _jwtGenerator;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<ResponseResult<List<UserResponse>>> GetAll()
        {
            if(!await _context.Users.AnyAsync())
                return new ResponseResultError<List<UserResponse>>("Dữ liệu hiện tại rỗng");

            var users = await _context.Users.ToListAsync();
            var reuslt = new List<UserResponse>();
            foreach(var user in users)
            {
                reuslt.Add(new UserResponse()
                {
                    Id = user.Id.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    DOB = user.DOB,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Address = user.Address,
                    Img = user.Img,
                });
            }

            return new ResponseResultSuccess<List<UserResponse>>(reuslt);
        }

        public async Task<ResponseResult<UserResponse>> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(new Guid(id));
            if(user == null)
                return new ResponseResultError<UserResponse>("Tài khoản không tồn tại");

            return new ResponseResultSuccess<UserResponse>(new UserResponse()
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DOB = user.DOB,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Address = user.Address,
                Img = user.Img,
            });
        }

        public async Task<ResponseResult<string>> Login(LoginAdminRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if(user == null)
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if(user == null)
                return new ResponseResultError<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if(!result.Succeeded)
                return new ResponseResultError<string>("Mật khẩu không đúng");


            var token = await _jwtGenerator.GenerateTokenLoginSuccessAsync(user);

            return new ResponseResultSuccess<string>(token);
        }
    }
}
