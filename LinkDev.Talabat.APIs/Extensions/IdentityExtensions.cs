using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration configuration) 
        {
            services.Configure<JwtSettings>(configuration.GetSection("jwtSettings"));
            services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;
                //identityOptions.User.AllowedUserNameCharacters = "";

                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;
                identityOptions.SignIn.RequireConfirmedAccount = true;

                identityOptions.Password.RequiredUniqueChars = 2;
                identityOptions.Password.RequireNonAlphanumeric = true;
                identityOptions.Password.RequireUppercase = true;
                identityOptions.Password.RequireLowercase = true;

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            })
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            //services.AddScoped(typeof(IAuthService), typeof(AuthService));
            //services.AddScoped(typeof(Func<IAuthService>), (serviceprovider) =>
            //{
            //    return()=> serviceprovider.GetServices<IAuthService>();
            //});

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<Func<IAuthService>>(serviceProvider =>
                () => serviceProvider.GetRequiredService<IAuthService>());

            services.AddAuthentication((AuthOptions) =>
            {
                AuthOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                AuthOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer((ConfigOptions)=>
            {
                ConfigOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ClockSkew=TimeSpan.FromMinutes(0),
                    ValidIssuer = configuration["jwtSettings:issuer"],
                    ValidAudience= configuration["jwtSettings:audience"],
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtSettings:Key"]!))

                };



            });
            return services;
        }



    }
}
