using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services.Strategies.Account;
using BehaviourManagementSystem_API.Utilities;
using BehaviourManagementSystem_API.Utilities.JwtGenarator;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IRoleService _roleService;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context,
            IJwtGenerator jwtGenerator,
            IRoleService roleService,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _jwtGenerator = jwtGenerator;
            _roleService = roleService;
            _roleManager = roleManager;
        }

        public async Task<ResponseResult<bool>> CheckEmailConfirmed(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
                return new ResponseResultError<bool>("Email không tồn tại");

            if(user.EmailConfirmed)
                return new ResponseResultSuccess<bool>(true);
            return new ResponseResultSuccess<bool>(false);
        }

        public async Task<ResponseResult<ResetPasswordRequest>> ForgotPassword(string userNameOfEmail)
        {
            var user = await _userManager.FindByNameAsync(userNameOfEmail);
            if(user == null)
                user = await _userManager.FindByEmailAsync(userNameOfEmail);
            if(user == null)
                return new ResponseResultError<ResetPasswordRequest>("Tài khoản không tồn tại");

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            return new ResponseResultSuccess<ResetPasswordRequest>(
                new ResetPasswordRequest
                {
                    Id = user.Id.ToString(),
                    UserOrEmail = user.Email,
                    Code = code,
                });
            ;
        }

        public async Task<ResponseResult<List<UserProfileRequest>>> GetAll(string roleName)
        {
            if(!await _context.Users.AnyAsync())
                return new ResponseResultError<List<UserProfileRequest>>("Dữ liệu hiện tại rỗng");

            var users = new List<UserProfileRequest>();

            if(roleName == null)
            {
                var list = await _context.Users.ToListAsync();
                foreach(var user in list)
                {
                    var roleOfUser = await _roleService.GetRoleNameByUserId(user.Id.ToString());
                    users.Add(new UserProfileRequest()
                    {
                        Id = user.Id.ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        DOB = user.DOB,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Address = user.Address,
                        AvtName = user.AvtName,
                        RoleName = roleOfUser.Result
                    });
                }
                return new ResponseResultSuccess<List<UserProfileRequest>>(users);
            }

            var role = await _roleManager.FindByNameAsync(roleName);

            if(role != null)
            {
                var userRole = await _context.UserRoles
                    .Where(prop => prop.RoleId == role.Id)
                    .ToListAsync();

                foreach(var item in userRole)
                {
                    if(!item.UserId.Equals(null))
                    {
                        var user = await _context.Users.FindAsync(item.UserId);
                        users.Add(new UserProfileRequest()
                        {
                            Id = user.Id.ToString(),
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Gender = user.Gender,
                            DOB = user.DOB,
                            PhoneNumber = user.PhoneNumber,
                            Email = user.Email,
                            Address = user.Address,
                            AvtName = user.AvtName,
                            RoleId = role.Id.ToString(),
                            RoleName = roleName
                        });
                    }
                }
                return new ResponseResultSuccess<List<UserProfileRequest>>(users);
            }

            return new ResponseResultError<List<UserProfileRequest>>($"Lỗi truy xuất. {roleName}");
        }

        public async Task<ResponseResult<UserProfileRequest>> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(new Guid(id));
            if(user == null)
                return new ResponseResultError<UserProfileRequest>("Tài khoản không tồn tại");

            var role = await _roleService.GetRoleNameByUserId(id);

            return new ResponseResultSuccess<UserProfileRequest>(new UserProfileRequest()
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DOB = user.DOB,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Address = user.Address,
                AvtName = user.AvtName,
                RoleName = role.Result
            });
        }

        public async Task<ResponseResult<string>> Login(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
                if(user == null)
                    user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
                if(user == null)
                    return new ResponseResultError<string>("Tài khoản không tồn tại");
                if(!await _userManager.CheckPasswordAsync(user, request.Password))
                    return new ResponseResultError<string>("Mật khẩu không chính xác");

                var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.Remember, false);

                if(!result.Succeeded)
                    return new ResponseResultError<string>("Mật khẩu không đúng");


                var token = await _jwtGenerator.GenerateTokenLoginSuccessAsync(user);

                return new ResponseResultSuccess<string>(token);
            }
            catch(Exception ex)
            {
                return new ResponseResultError<string>(ex.Message);
            }
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

            var id = Guid.NewGuid();
            var stamp = Guid.NewGuid().ToString();

            var user = new User
            {
                Id = id,
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpper(),
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                SecurityStamp = stamp,
                ConcurrencyStamp = stamp.ToLower(),
                AvtName = "default_avt.png",
                Activity = false,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("teacher");

                await _context.UserRoles.AddAsync(new IdentityUserRole<Guid>
                {
                    UserId = id,
                    RoleId = role.Id
                });

                await _context.SaveChangesAsync();

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

        public async Task<ResponseResult<ConfirmEmailRequest>> ResenConfirmEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
                return new ResponseResultError<ConfirmEmailRequest>("Email chưa được đăng ký");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            return new ResponseResultSuccess<ConfirmEmailRequest>(new ConfirmEmailRequest
            {
                Id = user.Id.ToString(),
                Code = code
            });
        }

        public async Task<ResponseResult<bool>> ResetPassword(ResetPasswordRequest repuest)
        {
            var user = await _userManager.FindByIdAsync(repuest.Id);
            if(user == null)
                return new ResponseResultError<bool>("Tài khoản không tồn tại");

            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(repuest.Code));
            var result = await _userManager.ResetPasswordAsync(user, code, repuest.PasswordNew);

            if(result.Succeeded)
                return new ResponseResultSuccess<bool>();
            return new ResponseResultError<bool>();
        }

        public async Task<ResponseResult<UserProfileRequest>> VerifyEmail(ConfirmEmailRequest request)
        {
            var user = await _context.Users.FindAsync(new Guid(request.Id));
            if(user == null)
                return new ResponseResultError<UserProfileRequest>("Tài khoản không tồn tại");

            request.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            var result = await _userManager.ConfirmEmailAsync(user, request.Code);

            if(result.Succeeded)
                return new ResponseResultSuccess<UserProfileRequest>(new UserProfileRequest
                {
                    Id = user.Id.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    DOB = user.DOB,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Address = user.Address,
                    AvtName = user.AvtName,
                });

            return new ResponseResultError<UserProfileRequest>("Xác thực Email không thành công");
        }

        public async Task<ResponseResult<List<UserProfileRequest>>> CreateUserProfile(UserProfileRequest request)
        {
            try
            {
                var users = new ResponseResult<List<UserProfileRequest>>();
                if(await CheckEmailIsExist(request.Email))
                {
                    users = await GetAll(null);
                    return new ResponseResult<List<UserProfileRequest>>
                    {
                        Success = false,
                        Message = "Email đã tồn tại.",
                        Result = users.Result
                    };
                }
                if(await CheckUserNameIsExist(request.UserName))
                {
                    users = await GetAll(null);
                    return new ResponseResult<List<UserProfileRequest>>
                    {
                        Success = false,
                        Message = "Tên tài khoản đã tồn tại.",
                        Result = users.Result
                    };
                }
                if(await CheckPhoneNumberIsExist(request.PhoneNumber))
                {
                    users = await GetAll(null);
                    return new ResponseResult<List<UserProfileRequest>>
                    {
                        Success = false,
                        Message = "Số điện thoại đã tồn tại.",
                        Result = users.Result
                    };
                }
                if(!CheckPhoneGenderIsvalid(request.Gender))
                {
                    users = await GetAll(null);
                    return new ResponseResult<List<UserProfileRequest>>
                    {
                        Success = false,
                        Message = "Giới tính không hợp lệ.",
                        Result = users.Result
                    };
                }

                if(!await CheckRoleUserLoginCurrent(request.Id))
                {
                    users = await GetAll(null);
                    return new ResponseResult<List<UserProfileRequest>>
                    {
                        Success = false,
                        Message = "Tài khoản đăng nhập không có quyền truy xuất này.",
                        Result = users.Result
                    };
                }

                if(!await CheckRoleOfRoleIdRequest(request.RoleId))
                {
                    users = await GetAll(null);
                    return new ResponseResult<List<UserProfileRequest>>
                    {
                        Success = false,
                        Message = "Cấp quyền truy cập không hợp lệ.",
                        Result = users.Result
                    };
                }

                var role = await _roleManager.FindByIdAsync(request.RoleId);
                var id = Guid.NewGuid();
                var stamp = Guid.NewGuid().ToString();
                if(role.Name.ToLower() == "teacher")
                {
                    var user = new User
                    {
                        Id = id,
                        UserName = request.UserName,
                        NormalizedUserName = request.UserName.ToUpper(),
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Gender = request.Gender,
                        DOB = request.DOB,
                        PhoneNumber = request.PhoneNumber,
                        Email = request.Email,
                        EmailConfirmed = true,
                        Address = request.Address,
                        AvtName = request.AvtName == null ? "default_avt.png" : request.FirstName,
                        SecurityStamp = stamp,
                        ConcurrencyStamp = stamp.ToUpper()
                    };

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    var userRole = new IdentityUserRole<Guid>
                    {
                        UserId = id,
                        RoleId = role.Id,
                    };

                    await _context.UserRoles.AddAsync(userRole);
                    await _context.SaveChangesAsync();
                }

                if(role.Name.ToLower() == "student")
                {
                    if(!await CheckTeacherIdRequest(request.TeacherId))
                    {
                        users = await GetAll(null);
                        return new ResponseResult<List<UserProfileRequest>>
                        {
                            Success = false,
                            Message = "Tài khoản giáo viên của thêm không tồn tại.",
                            Result = users.Result
                        };
                    }

                    var user = new User
                    {
                        Id = id,
                        UserName = request.UserName,
                        NormalizedUserName = request.UserName.ToUpper(),
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Gender = request.Gender,
                        DOB = request.DOB,
                        PhoneNumber = request.PhoneNumber,
                        Email = request.Email,
                        EmailConfirmed = true,
                        Address = request.Address,
                        AvtName = request.AvtName,
                        SecurityStamp = stamp,
                        ConcurrencyStamp = stamp.ToUpper(),
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    };

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    var userRole = new IdentityUserRole<Guid>
                    {
                        UserId = id,
                        RoleId = role.Id,
                    };

                    await _context.UserRoles.AddAsync(userRole);
                    await _context.SaveChangesAsync();

                    var ind = new Individual
                    {
                        Id = id,
                        TeacherId = new Guid(request.TeacherId),
                        StudentId = id,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    };

                    await _context.UserRoles.AddAsync(userRole);
                    await _context.SaveChangesAsync();
                }
                users = await GetAll(null);
                return new ResponseResultSuccess<List<UserProfileRequest>>(users.Result);
            }
            catch(Exception ex)
            {
                return new ResponseResultError<List<UserProfileRequest>>(ex.Message);
            }
        }

        private bool CheckPhoneGenderIsvalid(string gender)
        {
            if(gender.ToLower() == "nam")
                return true;
            if(gender.ToLower() == "nữ")
                return true;
            return false;
        }

        private async Task<bool> CheckPhoneNumberIsExist(string phoneNumber)
        {
            var user = await _context.Users.Where(prop => prop.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
            if(user == null)
                return false;
            return true;
        }

        private async Task<bool> CheckUserNameIsExist(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
                return false;
            return true;
        }

        private async Task<bool> CheckEmailIsExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
                return false;
            return true;
        }

        private async Task<bool> CheckTeacherIdRequest(string teacherId)
        {
            Guid id;
            if(!Guid.TryParse(teacherId, out id))
                return false;

            var user = await _userManager.FindByIdAsync(teacherId);
            if(user == null)
                return false;

            return true;
        }

        private async Task<bool> CheckRoleOfRoleIdRequest(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role == null)
                return false;

            if(role.Name.ToLower() == "admin")
                return false;

            return true;
        }

        private async Task<bool> CheckRoleUserLoginCurrent(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
                return false;

            var userRole = await _context.UserRoles
                .Where(prop => prop.UserId == user.Id)
                .FirstAsync();
            if(userRole == null)
                return false;

            var role = await _roleManager.FindByIdAsync(userRole.RoleId.ToString());
            if(role == null)
                return false;

            if(role.Name.ToLower() != "admin")
                return false;

            return true;
        }

        public async Task<ResponseResult<UserProfileRequest>> UpdateUserProfile(UserProfileRequest request)
        {
            Guid id;
            if(!Guid.TryParse(request.Id, out id))
                return new ResponseResultError<UserProfileRequest>("Thông tin truy xuất không hợp lệ.");

            var user = await _context.Users.FindAsync(new Guid(request.Id));
            if(user == null)
                return new ResponseResultError<UserProfileRequest>("Tài khoản không tồn tại.");

            if(!request.FirstName.CheckRequest())
                user.FirstName = request.FirstName;
            if(!request.LastName.CheckRequest())
                user.LastName = request.LastName;
            if(!request.Gender.CheckRequest())
                user.Gender = request.Gender;
            if(string.IsNullOrEmpty(request.DOB.ToString()))
                user.DOB = request.DOB;
            if(!request.PhoneNumber.CheckRequest())
                user.PhoneNumber = request.PhoneNumber;
            if(!request.Email.CheckRequest())
                user.Email = request.Email;
            if(!request.Address.CheckRequest())
                user.Address = request.Address;
            if(!request.AvtName.CheckRequest())
                user.AvtName = request.AvtName;

            try
            {
                await _userManager.UpdateAsync(user);
                return new ResponseResultSuccess<UserProfileRequest>(new UserProfileRequest
                {
                    Id = user.Id.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    DOB = user.DOB,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Address = user.Address,
                    AvtName = user.AvtName,
                });
            }
            catch
            {
                user = await _context.Users.FindAsync(new Guid(request.Id));
                return new ResponseResult<UserProfileRequest>()
                {
                    Success = false,
                    Message = "Cập nhật không thành công",
                    Result = new UserProfileRequest
                    {
                        Id = user.Id.ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        DOB = user.DOB,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Address = user.Address,
                        AvtName = user.AvtName,
                    }
                };
            }
        }

        public async Task<ResponseResult<List<UserProfileRequest>>> DeleteUserProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                var upfs = await GetAll(null);
                return new ResponseResult<List<UserProfileRequest>>()
                {
                    Success = false,
                    Message = "Không tồn tại chỉ định xóa.",
                    Result = upfs.Result
                };
            }

            try
            {
                await _userManager.DeleteAsync(user);
                var upfs = await GetAll(null);
                return new ResponseResultSuccess<List<UserProfileRequest>>(upfs.Result);
            }
            catch(Exception ex)
            {
                var upfs = await GetAll(null);
                return new ResponseResult<List<UserProfileRequest>>()
                {
                    Success = false,
                    Message = ex.Message,
                    Result = upfs.Result
                };
            }
        }

        public async Task<ResponseResult<UserProfileRequest>> ChangePassword(ResetPasswordRequest repuest)
        {
            var user = await _userManager.FindByIdAsync(repuest.Id);
            if(user == null)
                return new ResponseResultError<UserProfileRequest>("Tài khoản không tồn tại");

            try
            {
                var checkPassOld = await _userManager.CheckPasswordAsync(user, repuest.PasswordNew);

                if(!checkPassOld)
                    return new ResponseResultError<UserProfileRequest>("Mật khẩu củ không chính xác.");

                var result = await _userManager.ChangePasswordAsync(user, repuest.PasswordOld, repuest.PasswordNew);

                if(result.Succeeded)
                {
                    var role = await _roleService.GetRoleNameByUserId(user.Id.ToString());

                    return new ResponseResultSuccess<UserProfileRequest>(new UserProfileRequest
                    {
                        Id = user.Id.ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        DOB = user.DOB,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Address = user.Address,
                        AvtName = user.AvtName,
                        RoleName = role.Result
                    });
                }
                return new ResponseResultError<UserProfileRequest>("Đổi mật khẩu không thành công");
            }
            catch(Exception)
            {
                return new ResponseResultError<UserProfileRequest>("Đổi mật khẩu không thành công");
            }
        }

        public async Task<ResponseResult<List<UserProfileRequest>>> GetAccount(UserProfileRequest request)
        {
            try
            {
                var acc = new AccountStratery();

                if(request.Id != null)
                {
                    acc.SetAccountStratery(new GetAccount(_userManager, _roleService));
                    return await acc.GetAccountStratery(request);
                }

                else if(request.RoleName != null)
                {
                    acc.SetAccountStratery(new GetAllAccountTeacher(_context, _userManager, _roleService));
                    return await acc.GetAccountStratery(request);
                }

                else if(request.Active == true)
                {
                    acc.SetAccountStratery(new GetAllAccountActivity(_context, _userManager, _roleService));
                    return await acc.GetAccountStratery(request);
                }

                else if(request.Active == false)
                {
                    acc.SetAccountStratery(new GetAllAcountNotActivity(_context, _userManager, _roleService));
                    return await acc.GetAccountStratery(request);
                }

                else if(request.TeacherId != null)
                {
                    acc.SetAccountStratery(new GetAllAccountStudentOfTeacher(_context, _userManager, _roleService));
                    return await acc.GetAccountStratery(request);
                }

                else
                {
                    acc.SetAccountStratery(new GetAllAccount(_context, _userManager, _roleService));
                    return await acc.GetAccountStratery(request);
                }
            }
            catch(Exception ex)
            {
                return new ResponseResultError<List<UserProfileRequest>>(ex.Message);
            }
        }

        public async Task<ResponseResult<string>> GoogleSigin(string token)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(token);

                var user = await _userManager.FindByEmailAsync(payload.Email);

                var stamp = Guid.NewGuid();

                if(user is null)
                {
                    var user_id = Guid.NewGuid();
                    user = new User
                    {
                        Id = user_id,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName,
                        UserName = new MailAddress(payload.Email).User,
                        NormalizedUserName = new MailAddress(payload.Email).User.ToUpper(),
                        DisplayName = payload.Name,
                        Email = payload.Email,
                        EmailConfirmed = true,
                        AvtName = payload.Picture,
                        SecurityStamp = stamp.ToString(),
                        ConcurrencyStamp = stamp.ToString().ToUpper(),
                        Activity = false,
                    };
                    //var pass = GenegatorPass();
                    var irs = await _userManager.CreateAsync(user);
                    if(irs.Succeeded)
                    {
                        var role = await _roleManager.FindByNameAsync("teacher");
                        var user_role = new IdentityUserRole<Guid>
                        {
                            UserId = user_id,
                            RoleId = role.Id
                        };
                        await _context.UserRoles.AddAsync(user_role);
                        if(await _context.SaveChangesAsync() > 0)
                        {
                            token = await _jwtGenerator.GenerateTokenLoginSuccessAsync(user);
                            return new ResponseResultSuccess<string>(token);
                        }
                    }
                    return new ResponseResultError<string>("Lỗi thông tin đăng nhập.");
                }
                else
                {
                    token = await _jwtGenerator.GenerateTokenLoginSuccessAsync(user);
                    return new ResponseResultSuccess<string>(token);
                }
            }
            catch(Exception ex)
            {
                return new ResponseResultError<string>(ex.Message);
            }
        }

        public string GenegatorPass()
        {
        Start:
            var str = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789[!@#$%^&*()_+=\[{\]};:<>|./?,-]";
            var random = new Random();
            var count = 9;
            var pass = "";
            while(count > 0)
            {
                pass = pass + str[random.Next(str.Length)];
                count--;
            }
            if(pass.CheckPaswordRepuest())
                return pass;
            goto Start;
        }

        public async Task<ResponseResult<string>> GetImg(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
                return new ResponseResultError<string>("Thông tin truy cập không tồn tại");

            return new ResponseResultSuccess<string>(user.AvtName);
        }

        public async Task<ResponseResult<bool>> CheckPassworkNull(string id)
        {
            if(!await _context.Users.AnyAsync())
                return new ResponseResultError<bool>("Dữ liêu tồn tại không.");

            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
                return new ResponseResultError<bool>("Dữ liêu tồn tại không.");

            if(user.PasswordHash.CheckRequest())
                return new ResponseResultError<bool>("Không tồn tại mật khẩu.");

            return new ResponseResult<bool>
            {
                Success = true,
                Message = "Mật khẩu đã tồn tại."
            };
        }

        public async Task<ResponseResult<string>> NewPassOfAccountGoogle(ResetPasswordRequest req)
        {
            var user = await _userManager.FindByIdAsync(req.Id);

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, code, req.PasswordNew);

            if(!result.Succeeded)
                return new ResponseResultError<string>();

            var token = await _jwtGenerator.GenerateTokenLoginSuccessAsync(user);

            if(token == null)
                return new ResponseResultError<string>("Không có token");
            return new ResponseResultSuccess<string>(token);
        }
    }
}