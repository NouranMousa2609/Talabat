﻿using LinkDev.Talabat.Core.Application.Abstraction.DTOs._Commons;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync (LoginDto model);

        Task<UserDto> RegisterAsync (RegisterDto model);

        Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);

        Task<AddressDto> GetUserAddress(ClaimsPrincipal claimsPrincipal);



    }
}
