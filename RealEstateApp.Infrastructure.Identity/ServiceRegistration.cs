﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Domain.Settings;
using RealEstateApp.Infraestructure.Identity.Contexts;
using RealEstateApp.Infraestructure.Identity.Entities;
using RealEstateApp.Infraestructure.Identity.Seeds;
using RealEstateApp.Infraestructure.Identity.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Services;

namespace RealEstateApp.Infraestructure.Persistence
{
    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            #endregion
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Index";
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;
                option.SaveToken = false;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
                option.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not authorized" });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {   
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not authorized to acces to this resorce" });
                        return c.Response.WriteAsync(result);
                    }
                };
            });
            #region Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAgenteService, AgenteService>();
            #endregion
        }
        public async static Task AddIdentitySedds(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;

                try
                {
                    var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaultAdminUser.SeedAsync(userManager);
                    await DefaultAgenteUser.SeedAsync(userManager);
                    await DefaultDesarrolladorUser.SeedAsync(userManager);
                    await DefaultClienteUser.SeedAsync(userManager);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
