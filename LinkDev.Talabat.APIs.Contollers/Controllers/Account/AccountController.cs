using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs._Commons;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager _serviceManager):BaseApiController
    {
        [HttpPost("login")]
        public async Task <ActionResult<UserDto>> Login (LoginDto model)
        {
            var result = await _serviceManager.AuthService.LoginAsync(model);
            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {

            var result = await _serviceManager.AuthService.RegisterAsync(model);
            return Ok(result);
        }


        [Authorize]
        [HttpGet] //GET : /api/Account
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var result = await _serviceManager.AuthService.GetCurrentUser(User);
            return Ok(result);
        }


        [Authorize]
        [HttpGet("address")] //GET : /api/Account/address
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var result = await _serviceManager.AuthService.GetUserAddress(User);
            return Ok(result);

        }


        [Authorize]
        [HttpPut("address")] //PUT : /api/Account/address
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var result = await _serviceManager.AuthService.UpdateUserAddress(User,addressDto);
            return Ok(result);

        }


        [HttpGet("emailexists")] //GET : /api/Account/emailexists?email=NouranMousa@gmail.com
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
          
            return Ok(await _serviceManager.AuthService.EmailExists(email!));
                
        }
    }
}
