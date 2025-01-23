using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
