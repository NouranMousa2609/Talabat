using LinkDev.Talabat.Core.Domain.Contracts.Presistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
    public class StoreIdentityDbInitializer(StoreIdentityDbContext _DbContext, UserManager<ApplicationUser> _userManager) : DbInitializer(_DbContext), IStoreIdentityDbInitializer
    {


        public override async Task SeedAsync()
        {
            if (!_userManager.Users.Any())
            {

                var user = new ApplicationUser()
                {
                    DisplayName = "Nouran Mousa",
                    UserName = "Nouran_Mousa",
                    Email = "NouranMousa@gmail.com",
                    PhoneNumber = "011111111111",
                    
                };
                
                await _userManager.CreateAsync(user, "P@ssw0rd");
                await _userManager.AddToRolesAsync(user, new[] {"Admin"});
                
            }
        }
    }
}
