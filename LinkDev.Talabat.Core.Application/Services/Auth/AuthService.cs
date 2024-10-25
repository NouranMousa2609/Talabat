using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                Token = await GenerateTokenAsync(user),

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
                Token =await GenerateTokenAsync(user),

            };
            return response;
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

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secret"));
            var signInCredintials=new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var tokenObj = new JwtSecurityToken
                (
                issuer: "TalabatIdentity",
                audience: "TalabatUsers",
                expires: DateTime.UtcNow.AddMinutes(10),
                claims: authClaims,
                signingCredentials: signInCredintials
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
        }
    }
}
