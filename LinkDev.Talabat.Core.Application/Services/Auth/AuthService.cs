﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs._Commons;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Application.Extensions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
    public class AuthService(
        IOptions<JwtSettings> jwtSettings,
        UserManager<ApplicationUser> _userManager,
        SignInManager <ApplicationUser> _signInManager,
        IMapper mapper) : IAuthService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task<bool> EmailExists(string Email)
        {
           
            return await _userManager.FindByEmailAsync(Email!) is not null;
        }

        public async Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email!);

            return new UserDto()
            {
                Id = user!.Id,
                Email = user!.Email!,
               DisplayName  = user.DisplayName,
               Token=await GenerateTokenAsync(user),
            };
        }

        public async Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindUserWithAddress(claimsPrincipal!);

            var address= mapper.Map<AddressDto>(user!.Address);
            return address;

        }

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
                Token = await GenerateTokenAsync(user),

            };
            return response;

        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)   
        {
            //if (EmailExists(model.Email).Result)
            //    throw new BadRequestException("this Email is already in Use");

            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                PasswordHash=model.Password
                
            };
            var result = await _userManager.CreateAsync(user,user.PasswordHash);
            if (!result.Succeeded) throw new ValidationException() { Errors=result.Errors.Select(E=>E.Description).ToArray() };

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = model.Email,
                Token =await GenerateTokenAsync(user),

            };
            return response;
        }

        public async Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal, AddressDto addressDto)
        {
            var Updatedaddress = mapper.Map<Address>(addressDto);

            var user = await _userManager.FindUserWithAddress(claimsPrincipal!);

            if (user?.Address is not null) Updatedaddress.Id= user.Address.Id;

            user!.Address= Updatedaddress; 
            
            var result =await _userManager.UpdateAsync(user);

            if (!result.Succeeded) throw new BadRequestException(result.Errors.Select(error => error.Description).Aggregate((X, Y) => $"{X} , {Y}"));

            return addressDto;
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roleAsClaims = new List<Claim>(); 
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                roleAsClaims.Add(new Claim(ClaimTypes.Role,role.ToString()));
            }

            var authClaims = new List<Claim>()
            {
                new Claim (ClaimTypes.PrimarySid,user.Id),
                new Claim (ClaimTypes.Email,user.Email),
                new Claim (ClaimTypes.GivenName,user.DisplayName),

            }.Union(userClaims).Union(roleAsClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signInCredintials=new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var tokenObj = new JwtSecurityToken
                (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                claims: authClaims,
                signingCredentials: signInCredintials
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
        }
    }
}
