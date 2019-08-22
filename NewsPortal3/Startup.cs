using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NewsPortal3.Config;
using NewsPortal3.Data;
using NewsPortal3.Domain.ErrorHandler;
using NewsPortal3.Domain.Services;
using NewsPortal3.Domain.Services.Interfaces;
using NewsPortal3.Models;
using NewsPortal3.Models.Auxiliary;
using NewsPortal3.Models.Identity;
using NewsPortal3.Models.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using ProblemDetails = NewsPortal3.Domain.ErrorHandler.ProblemDetails;

namespace NewsPortal3
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
            services.AddControllersWithViews();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));

            services.AddDefaultIdentity<User>()
                            .AddRoles<Role>()
                            .AddEntityFrameworkStores<DataContext>()
                            .AddDefaultTokenProviders();

            #region
            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Default User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "YourAppCookieName";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/account/login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.Configure<PasswordHasherOptions>(option =>
            {
                option.IterationCount = 12000;
            });
            #endregion

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INewsService, NewsService>();

            services.AddSwaggerGen(SwaggerConfig.SwaggerGenConfig);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                        var exception = errorFeature.Error;

                        var errorDetail = exception.ToString();
                        var problemDetails = new ProblemDetails
                        {
                            Instance = $"urn:myorganization:error:{Guid.NewGuid()}"
                        };

                        if (exception is BadHttpRequestException badHttpRequestException)
                        {
                            problemDetails.Title = "Invalid request";
                            problemDetails.Status = (int)typeof(BadHttpRequestException).GetProperty("StatusCode",
                                BindingFlags.NonPublic | BindingFlags.Instance).GetValue(badHttpRequestException);
                            problemDetails.Detail = badHttpRequestException.Message;
                        }
                        else
                        {
                            problemDetails.Title = "An unexpected error occurred!";
                            problemDetails.Status = 500;
                            problemDetails.Detail = exception.ToString();
                        }

                        // log the exception etc..

                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.WriteJson(problemDetails, "application/problem+json");
                    });
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(SwaggerConfig.SwaggerUIConfig);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
