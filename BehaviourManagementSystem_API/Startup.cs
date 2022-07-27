using System;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BehaviourManagementSystem_API.Utilities.JwtGenarator;

namespace BehaviourManagementSystem_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AzureConnection"));
            });

            services.AddIdentity<User, Role>((options) =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // Config Login
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            #region Declare DI parttern repo
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAnalyzeAntecedentPerceiveService, AnalyzeAntecedentPerceiveService>();
            services.AddScoped<IAnalyzeAntecedentEnvironmentalService, AnalyzeAntecedentEnvironmentalService>();
            services.AddScoped<IAnalyzeAntecedentActivityService, AnalyzeAntecedentActivityService>();
            services.AddScoped<IProfileMildService, ProfileMildService>();
            services.AddScoped<IProfileModerateService, ProfileModerateService>();
            services.AddScoped<IProfileExtremeService, ProfileExtremeService>();
            services.AddScoped<IProfileRecoveryService, ProfileRecoveryService>();
            services.AddScoped<IAssessmentService, Assessmentservice>();
            services.AddScoped<IInterventionService, InterventionService>();
            services.AddScoped<IIndividualService, IndividualService>();
            #endregion

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Tokens:Dep:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Tokens:Dep:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            Configuration["Tokens:Dep:Key"]))
                };
            })
            .AddJwtBearer("jwt_ad_3011", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Tokens:Dep:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Tokens:Dep:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            Configuration["Tokens:Student:Key"]))
                };
            })
            .AddJwtBearer("jwt_tc_3011", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Tokens:Dep:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Tokens:Dep:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            Configuration["Tokens:Student:Key"]))
                };
            })
            .AddJwtBearer("jwt_sd_3011", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Tokens:Dep:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Tokens:Dep:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            Configuration["Tokens:Student:Key"]))
                };
            })
            .AddGoogle(options =>
            {
                options.ClientId = Configuration["Google:ClientId"];
                options.ClientSecret = Configuration["Google:ClientSecret"];
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "BehaviourManagementSystem API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityDefinition("jwt_ad_3011", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "jwt-ad"
                });
                option.AddSecurityDefinition("jwt_tc_3011", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "jwt-tc"
                });
                option.AddSecurityDefinition("jwt_sd_3011", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "jwt-sd"
                });
                // bearer
                option.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });
                // ad
                option.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "jwt_ad_3011"
                            },
                            Scheme = "oauth2",
                            Name = "jwt_ad_3011",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });
                // tc
                option.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "jwt_tc_3011"
                            },
                            Scheme = "oauth2",
                            Name = "jwt_tc_3011",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });
                //sd
                option.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "jwt_sd_3011"
                            },
                            Scheme = "oauth2",
                            Name = "jwt_sd_3011",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BehaviourManagementSystem API v1"));

            }
            else if(env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BehaviourManagementSystem API v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("AllowAllOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
