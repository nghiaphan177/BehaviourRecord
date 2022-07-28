using BehaviourManagementSystem_MVC.APIIntegration;
using BehaviourManagementSystem_MVC.APIIntegration.Account;
using BehaviourManagementSystem_MVC.APIIntegration.Assesstment;
using BehaviourManagementSystem_MVC.APIIntegration.Individual;
using BehaviourManagementSystem_MVC.APIIntegration.Intervention;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileExtreme;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileMild;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileModerate;
using BehaviourManagementSystem_MVC.APIIntegration.ProfileRecovery;
using BehaviourManagementSystem_MVC.Utilities.EmailSender;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BehaviourManagementSystem_MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("Teacher", options =>
                 {
                     options.Cookie.Name = "Teacher";
                     options.SlidingExpiration = true;
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                     options.LoginPath = "/Account/Login";
                     options.LogoutPath = "/Account/Logout";
                 })
                .AddCookie("Admin", options =>
                {
                    options.Cookie.Name = "Admin";
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    options.LoginPath = "/Admin/Account/Login";
                    options.LogoutPath = "/Admin/Account/Logout";
                    options.AccessDeniedPath = new PathString("/Admin/Account/AccessDenined");
                }).AddCookie("Student", options =>
                {
                    options.Cookie.Name = "Student";
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    options.LoginPath = "/StudentApp/Account/Login";
                    options.LogoutPath = "/StudentApp/Account/Logout";
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "ADMIN"));
                options.AddPolicy("StudentOnly", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "STUDENT"));
            });
            services.AddSession(options =>
            {
                options.Cookie.Name = "BMS";
                options.IdleTimeout = new TimeSpan(1, 0, 0);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAccountAPIClient, AccountAPIClient>();
            services.AddTransient<IUserAPIClient, UserAPIClient>();
            services.AddTransient<IAntecedentEnvironmentalAPIClient, AntecedentEnvironmentalAPIClient>();
            services.AddTransient<IAntecedentActivityAPIClient, AntecedentActivityAPIClient>();
            services.AddTransient<IAntecedentPerceivedAPIClient, AntecedentPerceivedAPIClient>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IOptionAPIClientMild, ProfileMildAPIClient>();
            services.AddTransient<IOptionAPIClientModerate, ProfileModerateAPIClient>();
            services.AddTransient<IOptionAPIClientExtreme, ProfileExtremeAPIClient>();
            services.AddTransient<IOptionAPIClientRecovery, ProfileRecoveryAPIClient>();
            services.AddTransient<IIndividualAPIClient, IndividualAPIClient>();
            services.AddTransient<IAssessmentAPIClient, AssessmentAPIClient>();
            services.AddTransient<IInterventionAPIClient, InterventionAPIClient>();
            services.AddOptions();// Kích hoạt Options
            var mailsettings = Configuration.GetSection("EmailSettings");  // đọc config
            services.Configure<EmailSettings>(mailsettings);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddControllersWithViews().AddNToastNotifyNoty(new NToastNotify.NotyOptions()
            {
                ProgressBar = true,
                Timeout = 3000,
                Theme = "mint"
            }) ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseNToastNotify();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthentication();


            app.UseRouting();


            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                         name: "Admin",
                         areaName: "Admin",
                         pattern: "Admin/{controller=Home}/{action=Index}");
                endpoints.MapAreaControllerRoute(
                         name: "StudentApp",
                         areaName: "StudentApp",
                         pattern: "StudentApp/{controller=Home}/{action=Index}");
                endpoints.MapDefaultControllerRoute();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

            });

        }
    }
}
