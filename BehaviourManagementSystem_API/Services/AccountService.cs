using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_API.Utilities.JwtGenarator;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using BehaviourManagementSystem_ViewModels.Responses.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
		private readonly IRoleService _roleService;

		public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context, IJwtGenerator jwtGenerator, IRoleService roleService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_jwtGenerator = jwtGenerator;
			_roleService = roleService;
		}

		public async Task<ResponseResult<List<UserResponse>>> GetAll()
		{
			if(!await _context.Users.AnyAsync())
				return new ResponseResultError<List<UserResponse>>("Dữ liệu hiện tại rỗng");

			var users = await _context.Users.ToListAsync();
			var reuslt = new List<UserResponse>();
			foreach(var user in users)
			{
				var role = await _roleService.GetRoleNameByUserId(user.Id.ToString());

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
					RoleName = role.Result
				});
			}

			return new ResponseResultSuccess<List<UserResponse>>(reuslt);
		}

		public async Task<ResponseResult<UserResponse>> GetUser(string id)
		{
			var user = await _context.Users.FindAsync(new Guid(id));
			if(user == null)
				return new ResponseResultError<UserResponse>("Tài khoản không tồn tại");

			var role = await _roleService.GetRoleNameByUserId(id);

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
				RoleName = role.Result
			});
		}

		public async Task<ResponseResult<string>> Login(LoginRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
			if(user == null)
				user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
			if(user == null)
				return new ResponseResultError<string>("Tài khoản không tồn tại");

			var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.Remember, false);

			if(!result.Succeeded)
				return new ResponseResultError<string>("Mật khẩu không đúng");


			var token = await _jwtGenerator.GenerateTokenLoginSuccessAsync(user);

			return new ResponseResultSuccess<string>(token);
		}

		public async Task<ResponseResult<ConfirmEmailRequest>> Register(RegisterRequest request)
		{
			if(await _context.Users.CountAsync(prop => prop.UserName == request.UserName) > 0)
				return new ResponseResultError<ConfirmEmailRequest>("Tên tài khoản của bạn đã tồn tại.");

			if(await _context.Users.CountAsync(prop => prop.Email == request.Email) > 0)
				return new ResponseResultError<ConfirmEmailRequest>("Email của bạn đã tồn tại.");

			if(!request.UserName.CheckUserNameRepuest())
				return new ResponseResultError<ConfirmEmailRequest>("Tên tài khoản không hợp lệ.");

			if(!request.Password.CheckPaswordRepuest())
				return new ResponseResultError<ConfirmEmailRequest>("Mật khẩu không hợp lệ.");

			var user = new User
			{
				Id = Guid.NewGuid(),
				UserName = request.UserName,
				NormalizedUserName = request.UserName.ToUpper(),
				Email = request.Email,
				NormalizedEmail = request.Email.ToUpper(),
				SecurityStamp = Guid.NewGuid().ToString(),
				ConcurrencyStamp = Guid.NewGuid().ToString().ToUpper(),
				Activity = false,
				CreateDate = DateTime.Now,
			};

			var result = await _userManager.CreateAsync(user, request.Password);
			if(result.Succeeded)
			{
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

				return new ResponseResultSuccess<ConfirmEmailRequest>(new ConfirmEmailRequest
				{
					Id = user.Id.ToString(),
					Code = code
				});
			}

			return new ResponseResultError<ConfirmEmailRequest>("Đăng ký không thành công.");
		}

		public async Task<ResponseResult<UserResponse>> VerifyEmail(ConfirmEmailRequest request)
		{
			var user = await _context.Users.FindAsync(new Guid(request.Id));
			if(user == null)
				return new ResponseResultError<UserResponse>("Tài khoản không tồn tại");

			request.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
			var result = await _userManager.ConfirmEmailAsync(user, request.Code);

			if(result.Succeeded)
				return new ResponseResultSuccess<UserResponse>(new UserResponse
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

			return new ResponseResultError<UserResponse>("Xác thực Email không thành công");
		}
	}
}

