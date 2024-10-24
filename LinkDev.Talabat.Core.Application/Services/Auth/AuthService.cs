using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
    public class AuthService(UserManager<ApplicationUser> _userManager, SignInManager <ApplicationUser> _signInManager) : IAuthService
    {
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) throw new UnAuthorizedException("Invalid Login");
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
            if (result.IsNotAllowed) throw new UnAuthorizedException("Account not Confirmed yet.");
            if(result.IsLockedOut) throw new UnAuthorizedException("Account is locked");
            //if (result.RequiresTwoFactor) throw new UnAuthorizedException("Require two-factor Authentication");

            if (!result.Succeeded) throw new UnAuthorizedException("Invalid Login");

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = model.Email,
                Token = "this will be token"

            };
            return response;

        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)   
        {
            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded) throw new ValidationException() { Errors=result.Errors.Select(E=>E.Description) };

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = model.Email,
                Token = "this will be token"

            };
            return response;
        }
    }
}
